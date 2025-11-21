---Stored Procedure siêu mạnh – lấy danh sách đánh giá
--drop PROCEDURE sp_GetDanhGiaNhanVien
--drop PROCEDURE sp_TuDongPhat_NghiKhongPhep_Qua3Ngay
--drop PROCEDURE sp_TuDongThuongPhatKyLuat
CREATE PROCEDURE [dbo].[sp_GetDanhGiaNhanVien]
    @IdDangNhap VARCHAR(20),
    @Thang INT,
    @Nam INT,
    @SearchTen NVARCHAR(255) = NULL,
    @SearchPhongBan INT = NULL,
    @SearchChucVu INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IsGiamDoc BIT = CASE WHEN @IdDangNhap LIKE 'GD%' THEN 1 ELSE 0 END
    DECLARE @IdPhongBanCuaTP INT = NULL

    -- Lấy phòng ban của trưởng phòng (nếu là TP)
    IF @IsGiamDoc = 0 AND @IdDangNhap LIKE 'TP%'
    BEGIN
        SELECT @IdPhongBanCuaTP = nv.idPhongBan
        FROM NhanVien nv
        JOIN ChucVu cv ON nv.idChucVu = cv.id
        WHERE nv.id = @IdDangNhap AND cv.TenChucVu LIKE N'Trưởng phòng%'
    END

    -- Tính số ngày tự ý nghỉ trong tháng @Thang/@Nam
    ;WITH MissesCTE AS (
        SELECT 
            cc.idNhanVien,
            COUNT(*) AS Misses
        FROM ChamCong cc
        CROSS APPLY (VALUES (CAST(cc.NgayChamCong AS DATE))) v(Ngay)
        WHERE MONTH(cc.NgayChamCong) = @Thang
          AND YEAR(cc.NgayChamCong) = @Nam
          AND DATENAME(WEEKDAY, cc.NgayChamCong) NOT IN ('Saturday', 'Sunday')
          AND (cc.GioVao IS NULL OR cc.GioRa IS NULL)
          AND NOT EXISTS (
                SELECT 1 FROM NghiPhep np
                WHERE np.idNhanVien = cc.idNhanVien
                  AND np.TrangThai = N'Đã duyệt'
                  AND CAST(cc.NgayChamCong AS DATE) BETWEEN np.NgayBatDau AND np.NgayKetThuc
          )
        GROUP BY cc.idNhanVien
    ),
    LatestDanhGia AS (
        SELECT 
            dg.*,
            ROW_NUMBER() OVER (PARTITION BY dg.idNhanVien ORDER BY dg.ngayTao DESC) AS RN
        FROM DanhGiaNhanVien dg
        WHERE MONTH(dg.ngayTao) = @Thang AND YEAR(dg.ngayTao) = @Nam
           OR dg.ngayTao IS NULL -- cho phép lấy nếu chưa có đánh giá
    )

    SELECT 
        ISNULL(dg.id, 0) AS ID,
        nv.id AS IDNhanVien,
        nv.TenNhanVien,
        pb.TenPhongBan,
        cv.TenChucVu,
        ISNULL(m.Misses, 0) AS Misses,
        ISNULL(dg.DiemChuyenCan, 5) AS DiemChuyenCanStored,
        ISNULL(dg.DiemNangLuc, 5) AS DiemNangLucStored,
        ISNULL(dg.DiemSo, 0) AS DiemSo,
        dg.NhanXet,
        ISNULL(dg.ngayTao, GETDATE()) AS NgayTao
    FROM NhanVien nv
    JOIN PhongBan pb ON nv.idPhongBan = pb.id
    JOIN ChucVu cv ON nv.idChucVu = cv.id
    LEFT JOIN LatestDanhGia dg ON nv.id = dg.idNhanVien AND dg.RN = 1
    LEFT JOIN MissesCTE m ON nv.id = m.idNhanVien
    WHERE nv.DaXoa = 0
      AND nv.LoaiNhanVien = N'Nhân viên chính thức'
      AND nv.id != @IdDangNhap
      AND (@IsGiamDoc = 1 OR nv.idPhongBan = @IdPhongBanCuaTP)
      AND (@SearchTen IS NULL OR nv.TenNhanVien LIKE N'%' + @SearchTen + N'%')
      AND (@SearchPhongBan IS NULL OR nv.idPhongBan = @SearchPhongBan)
      AND (@SearchChucVu IS NULL OR nv.idChucVu = @SearchChucVu)
ORDER BY nv.TenNhanVien
END

GO
---Thêm danh mục phạt vào bảng ThuongPhat (chạy 1 lần)
INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao) VALUES
(2000000, N'Thưởng', N'Thưởng nhân viên xuất sắc đạt đánh giá TỐT 2 tháng liên tiếp', 'TPGD0013'),
(0, N'Phạt', N'Cảnh cáo bằng văn bản do đánh giá TỆ 2 tháng liên tiếp', 'TPGD0013')
GO
---Stored Procedure siêu mạnh – Tự động phạt khi điểm tệ và thưởng khi => trung bình liên tiếp 2 tháng
CREATE PROCEDURE sp_TuDongThuongPhatKyLuat
    @Thang INT,
    @Nam INT,
    @idNguoiLap VARCHAR(10) = 'TPGD0013'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idThuong INT = (SELECT TOP 1 id FROM ThuongPhat WHERE loai = N'Thưởng' AND lyDo LIKE N'%TỐT%');
    DECLARE @idKyLuat INT = (SELECT TOP 1 id FROM ThuongPhat WHERE loai = N'Phạt' AND lyDo LIKE N'%TỆ%');

    -- Xóa thưởng/phạt/kỷ luật của tháng này trước khi tạo mới (tránh trùng
    DELETE FROM NhanVien_ThuongPhat
    WHERE MONTH(thangApDung) = @Thang 
      AND YEAR(thangApDung) = @Nam
      AND idThuongPhat IN (SELECT id FROM ThuongPhat WHERE loai IN (N'Thưởng', N'Phạt', N'Kỷ luật'));

    DECLARE @NgayApDung DATE = DATEFROMPARTS(@Nam, @Thang, 1);

    -- === 1. TỰ ĐỘNG THƯỞNG: Tốt (DiemSo >= 9) 2 tháng liên tiếp ===
    INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
    SELECT DISTINCT dg.idNhanVien, @idThuong, @NgayApDung
    FROM DanhGiaNhanVien dg
    WHERE dg.DiemSo >= 9
      AND (
            (MONTH(dg.ngayTao) = @Thang AND YEAR(dg.ngayTao) = @Nam)
         OR (MONTH(dg.ngayTao) = @Thang - 1 AND YEAR(dg.ngayTao) = @Nam)
         OR (@Thang = 1 AND MONTH(dg.ngayTao) = 12 AND YEAR(dg.ngayTao) = @Nam - 1)
          )
    GROUP BY dg.idNhanVien
    HAVING COUNT(*) >= 2;

    -- === 2. TỰ ĐỘNG KỶ LUẬT + PHẠT TIỀN: Tệ (DiemSo <= 6) 2 tháng liên tiếp ===
    INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
    SELECT DISTINCT dg.idNhanVien, @idKyLuat, @NgayApDung
    FROM DanhGiaNhanVien dg
    WHERE dg.DiemSo <= 6
      AND (
            (MONTH(dg.ngayTao) = @Thang AND YEAR(dg.ngayTao) = @Nam)
         OR (MONTH(dg.ngayTao) = @Thang - 1 AND YEAR(dg.ngayTao) = @Nam)
         OR (@Thang = 1 AND MONTH(dg.ngayTao) = 12 AND YEAR(dg.ngayTao) = @Nam - 1)
          )
    GROUP BY dg.idNhanVien
    HAVING COUNT(*) >= 2;

    -- Nếu bạn muốn phạt tiền thay vì chỉ kỷ luật, bỏ comment dòng dưới
    -- INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
    -- SELECT DISTINCT idNhanVien, @idPhatTien, @NgayApDung FROM (...) same query

    -- Trả về kết quả để hiển thị thông báo
    SELECT 
        (SELECT COUNT(*) FROM NhanVien_ThuongPhat WHERE idThuongPhat = @idThuong AND MONTH(thangApDung)=@Thang AND YEAR(thangApDung)=@Nam) AS SoNguoiThuong,
(SELECT COUNT(*) FROM NhanVien_ThuongPhat WHERE idThuongPhat = @idKyLuat AND MONTH(thangApDung)=@Thang AND YEAR(thangApDung)=@Nam) AS SoNguoiKyLuat;
END
GO

-- Thêm mục phạt "Nghỉ không phép quá 3 ngày"
INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao)
VALUES (0, N'Phạt', N'Cảnh cáo do nghỉ không phép quá 3 ngày trong tháng', 'TPGD0013');

go
---Stored Procedure siêu mạnh – Tự động phạt nghỉ không phép > 3 ngày
CREATE OR ALTER PROCEDURE sp_TuDongPhat_NghiKhongPhep_Qua3Ngay
    @Thang INT,
    @Nam INT,
    @idNguoiLap VARCHAR(10) = 'TPGD0013'
AS
BEGIN
    SET NOCOUNT ON;

    -- Lấy id mục phạt nghỉ không phép
    DECLARE @idPhat INT = (SELECT TOP 1 id FROM ThuongPhat WHERE lyDo LIKE N'%nghỉ không phép quá 3 ngày%');

    IF @idPhat IS NULL
    BEGIN
        RAISERROR(N'Chưa có danh mục phạt nghỉ không phép trong bảng ThuongPhat!', 16, 1);
        RETURN;
    END

    DECLARE @NgayDauThang DATE = DATEFROMPARTS(@Nam, @Thang, 1);
    DECLARE @NgayCuoiThang DATE = EOMONTH(@NgayDauThang);

    -- Xóa phạt cũ của tháng này trước khi tạo mới (tránh trùng khi chạy lại)
    DELETE FROM NhanVien_ThuongPhat
    WHERE idThuongPhat = @idPhat
      AND MONTH(thangApDung) = @Thang AND YEAR(thangApDung) = @Nam;

    -- CTE tính số ngày nghỉ không phép (không chấm công + không có phép duyệt)
    ;WITH NgayLamViec AS (
        SELECT DATEADD(DAY, number, @NgayDauThang) AS Ngay
        FROM master..spt_values
        WHERE type = 'P'
          AND DATEADD(DAY, number, @NgayDauThang) <= @NgayCuoiThang
          AND DATENAME(WEEKDAY, DATEADD(DAY, number, @NgayDauThang)) NOT IN ('Saturday', 'Sunday')
    ),
    NgayNghiKhongPhep AS (
        SELECT 
            nv.id AS idNhanVien,
            COUNT(*) AS SoNgayNghiKhongPhep
        FROM NhanVien nv
        CROSS JOIN NgayLamViec nlv
        LEFT JOIN ChamCong cc ON cc.idNhanVien = nv.id AND CAST(cc.NgayChamCong AS DATE) = nlv.Ngay
        LEFT JOIN NghiPhep np ON np.idNhanVien = nv.id 
                              AND nlv.Ngay BETWEEN np.NgayBatDau AND np.NgayKetThuc
                              AND np.TrangThai = N'Đã duyệt'
        WHERE nv.DaXoa = 0
          AND (cc.id IS NULL OR cc.GioVao IS NULL) -- Không chấm công
          AND np.id IS NULL -- Không có phép được duyệt
        GROUP BY nv.id
        HAVING COUNT(*) > 3  -- Nghỉ quá 3 ngày
    )

    -- Tạo phạt tự động
    INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
    SELECT 
        idNhanVien,
        @idPhat,
        @NgayDauThang
    FROM NgayNghiKhongPhep;

    -- Trả về số người bị phạt
    SELECT COUNT(*) AS SoNguoiBiPhat
    FROM NgayNghiKhongPhep;
END
GO

CREATE PROCEDURE [dbo].[sp_BaoCaoTuyenDungTheoQuy]
    @Quy NVARCHAR(10) = NULL,
    @Nam INT,
    @PhongBan NVARCHAR(50) = NULL,  -- Tên phòng ban để lọc
    @ViTri NVARCHAR(50) = NULL      -- Tên chức vụ để lọc
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        pb.TenPhongBan,
        cv.TenChucVu AS TenViTri,
        COUNT(CASE WHEN uv.trangThai = N'Đang xét duyệt' THEN 1 END) AS UngTuyen,
        COUNT(CASE WHEN uv.trangThai = N'Trúng tuyển' THEN 1 END) AS TrungTuyen,
        COUNT(CASE WHEN uv.trangThai = N'Bị loại' THEN 1 END) AS BiLoai,
        COUNT(CASE WHEN uv.trangThai = N'Đang phỏng vấn' THEN 1 END) AS DangPhongVan
    FROM UngVien uv
    INNER JOIN TuyenDung td ON uv.idTuyenDung = td.id
    INNER JOIN ChucVu cv ON td.idChucVu = cv.id
    INNER JOIN PhongBan pb ON td.idPhongBan = pb.id
    WHERE YEAR(uv.ngayUngTuyen) = @Nam
      AND uv.daXoa = 0  -- Chỉ lấy ứng viên chưa bị xóa
      AND (@Quy IS NULL OR
           (@Quy = 'Q1' AND MONTH(uv.ngayUngTuyen) BETWEEN 1 AND 3) OR
           (@Quy = 'Q2' AND MONTH(uv.ngayUngTuyen) BETWEEN 4 AND 6) OR
           (@Quy = 'Q3' AND MONTH(uv.ngayUngTuyen) BETWEEN 7 AND 9) OR
           (@Quy = 'Q4' AND MONTH(uv.ngayUngTuyen) BETWEEN 10 AND 12))
      AND (@PhongBan IS NULL OR pb.TenPhongBan LIKE '%' + @PhongBan + '%')
      AND (@ViTri IS NULL OR cv.TenChucVu LIKE '%' + @ViTri + '%')
    GROUP BY pb.TenPhongBan, cv.TenChucVu
    ORDER BY pb.TenPhongBan, cv.TenChucVu;
END

IF OBJECT_ID('sp_BaoCao_KhenThuong', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoCao_KhenThuong;
GO

CREATE PROCEDURE dbo.sp_BaoCao_KhenThuong @IdNhanVien NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        tp.id AS ThuongPhatID,
        nv.TenNhanVien,
        nv.GioiTinh,
        nv.NgaySinh,
        nv.DiaChi,
        tp.tienThuongPhat,
        tp.loai AS LoaiThuongPhat,
        tp.lyDo,
        tp.idNguoiTao
    FROM ThuongPhat tp
    INNER JOIN NhanVien_ThuongPhat tp_nv ON tp_nv.idThuongPhat = tp.id
    INNER JOIN NhanVien nv ON nv.id = tp_nv.idNhanVien

    WHERE 1=1 AND (@IdNhanVien IS NULL OR nv.id = @IdNhanVien)
     
END;
GO


IF OBJECT_ID('dbo.sp_BaoCao_Luong', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoCao_Luong;
GO

CREATE PROCEDURE dbo.sp_BaoCao_Luong
AS
BEGIN
    SET NOCOUNT ON;

    -- Tự động lấy ID kỳ lương mới nhất (dựa trên ngày bắt đầu muộn nhất)
    DECLARE @IdKyLuongMoiNhat INT;
    
    SELECT TOP 1 @IdKyLuongMoiNhat = id
    FROM KyLuong
    ORDER BY ngayBatDau DESC;   -- kỳ mới nhất

    -- Nếu chưa có kỳ lương nào thì báo lỗi nhẹ
    IF @IdKyLuongMoiNhat IS NULL
    BEGIN
        RAISERROR(N'Chưa có kỳ lương nào trong hệ thống!', 16, 1);
        RETURN;
    END

    -- Báo cáo lương kỳ mới nhất
    SELECT
        l.id AS LuongID,
        nv.id AS MaNhanVien,
        nv.TenNhanVien,
        nv.GioiTinh,
        pb.TenPhongBan,
        cv.TenChucVu,
        l.ngayNhanLuong,
        l.luongTruocKhauTru,
        l.luongSauKhauTru,
        l.tongKhauTru,
        l.tongPhuCap,
        l.tongKhenThuong,
        l.tongTienPhat,
        l.trangThai,
        l.ghiChu,
        kl.ngayBatDau AS TuNgay,
        kl.ngayKetThuc AS DenNgay,
        kl.ngayChiTra,
        ThucNhan = l.luongSauKhauTru + l.tongPhuCap + l.tongKhenThuong - l.tongTienPhat
    FROM ChiTietLuong l
    INNER JOIN NhanVien nv ON l.idNhanVien = nv.id
    INNER JOIN KyLuong kl  ON l.idKyLuong = kl.id
    LEFT  JOIN PhongBan pb ON nv.idPhongBan = pb.id
    LEFT  JOIN ChucVu cv   ON nv.idChucVu = cv.id
    WHERE l.idKyLuong = @IdKyLuongMoiNhat
      AND nv.DaXoa = 0
    ORDER BY nv.TenNhanVien;
END;


IF OBJECT_ID('sp_BaoCao_KhenThuong', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoCao_KhenThuong;
GO

CREATE PROCEDURE dbo.sp_BaoCao_KhenThuong @IdNhanVien NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        tp.id AS ThuongPhatID,
        nv.TenNhanVien,
        nv.GioiTinh,
        nv.NgaySinh,
        nv.DiaChi,
        tp.tienThuongPhat,
        tp.loai AS LoaiThuongPhat,
        tp.lyDo,
        tp.idNguoiTao
    FROM ThuongPhat tp
    INNER JOIN NhanVien_ThuongPhat tp_nv ON tp_nv.idThuongPhat = tp.id
    INNER JOIN NhanVien nv ON nv.id = tp_nv.idNhanVien

    WHERE 1=1 AND (@IdNhanVien IS NULL OR nv.id = @IdNhanVien)
     
END;
GO

EXEC sp_BaoCao_KhenThuong;
EXEC sp_BaoCao_KhenThuong @IdNhanVien='NVNS000029';

IF OBJECT_ID('dbo.sp_BaoCao_Luong', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoCao_Luong;
GO

update ChiTietLuong
set idKyLuong = 23
where idKyLuong = 1

CREATE PROCEDURE dbo.sp_BaoCao_Luong
AS
BEGIN
    SET NOCOUNT ON;

    -- Tự động lấy ID kỳ lương mới nhất (dựa trên ngày bắt đầu muộn nhất)
    DECLARE @IdKyLuongMoiNhat INT;
    
    SELECT TOP 1 @IdKyLuongMoiNhat = id
    FROM KyLuong
    ORDER BY ngayBatDau DESC;   -- kỳ mới nhất

    -- Nếu chưa có kỳ lương nào thì báo lỗi nhẹ
    IF @IdKyLuongMoiNhat IS NULL
    BEGIN
        RAISERROR(N'Chưa có kỳ lương nào trong hệ thống!', 16, 1);
        RETURN;
    END

    -- Báo cáo lương kỳ mới nhất
    SELECT
        l.id AS LuongID,
        nv.id AS MaNhanVien,
        nv.TenNhanVien,
        nv.GioiTinh,
        pb.TenPhongBan,
        cv.TenChucVu,
        l.ngayNhanLuong,
        l.luongTruocKhauTru,
        l.luongSauKhauTru,
        l.tongKhauTru,
        l.tongPhuCap,
        l.tongKhenThuong,
        l.tongTienPhat,
        l.trangThai,
        l.ghiChu,
        kl.ngayBatDau AS TuNgay,
        kl.ngayKetThuc AS DenNgay,
        kl.ngayChiTra,
        ThucNhan = l.luongSauKhauTru + l.tongPhuCap + l.tongKhenThuong - l.tongTienPhat
    FROM ChiTietLuong l
    INNER JOIN NhanVien nv ON l.idNhanVien = nv.id
    INNER JOIN KyLuong kl  ON l.idKyLuong = kl.id
    LEFT  JOIN PhongBan pb ON nv.idPhongBan = pb.id
    LEFT  JOIN ChucVu cv   ON nv.idChucVu = cv.id
    WHERE l.idKyLuong = @IdKyLuongMoiNhat
      AND nv.DaXoa = 0
    ORDER BY nv.TenNhanVien;
END;
GO
EXEC sp_BaoCao_Luong

IF OBJECT_ID('dbo.sp_BaoCao_HopDongNhanVien', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoCao_HopDongNhanVien;
GO

CREATE PROCEDURE dbo.sp_BaoCao_HopDongNhanVien
    @IdNhanVien NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        hd.id AS HopDongID,
        nv.TenNhanVien,
        nv.GioiTinh,
        nv.NgaySinh,
        nv.DiaChi,
        nv.Que,
        nv.Email,
        hd.LoaiHopDong,
        hd.NgayKy,
        hd.NgayBatDau,
        hd.NgayKetThuc,
        hd.Luong,
        ctl.tongPhuCap,
        hd.MoTa,
        hd.HinhAnh,
        cv.TenChucVu,
        nv.idPhongBan
FROM HopDongLaoDong hd
    INNER JOIN NhanVien nv ON hd.idNhanVien = nv.id
    INNER JOIN ChucVu cv ON cv.id = nv.idChucVu
    INNER JOIN ChiTietLuong ctl ON ctl.idNhanVien = nv.id
    WHERE 1=1 AND (@IdNhanVien IS NULL OR nv.id = @IdNhanVien)
END;
GO

EXEC sp_BaoCao_HopDongNhanVien @IdNhanVien = 'NVNS000029'

-- 1. Xem trước tình trạng hiện tại (có bao nhiêu nhân viên bị trùng)
SELECT 
    idNhanVien,
    COUNT(*) AS SoBanGhi
FROM ChiTietLuong
GROUP BY idNhanVien
HAVING COUNT(*) > 1
ORDER BY SoBanGhi DESC;
GO

-- 2. Tạo bảng tạm chứa ID của các bản ghi cần giữ lại 
-- (giữ lại bản ghi có id lớn nhất = mới nhất của mỗi nhân viên)
WITH DuLieuCanGiu AS (
    SELECT 
        id,
        ROW_NUMBER() OVER (PARTITION BY idNhanVien ORDER BY id DESC) AS STT
    FROM ChiTietLuong
    WHERE YEAR(ngayNhanLuong) = 2025 AND MONTH(ngayNhanLuong) = 11  -- chỉ xét tháng 11/2025
       OR ngayNhanLuong >= '2025-11-01'                              -- hoặc ngày >= 01/11/2025
)
SELECT id INTO #IdCanGiu
FROM DuLieuCanGiu
WHERE STT = 1;   -- chỉ giữ lại bản ghi mới nhất của mỗi nhân viên
GO

-- 3. Xem trước có bao nhiêu bản ghi sẽ bị xóa
SELECT COUNT(*) AS TongBanGhiSeBiXoa
FROM ChiTietLuong ctl
WHERE NOT EXISTS (SELECT 1 FROM #IdCanGiu k WHERE k.id = ctl.id)
  AND (YEAR(ctl.ngayNhanLuong) = 2025 AND MONTH(ctl.ngayNhanLuong) = 11 
       OR ctl.ngayNhanLuong >= '2025-11-01');
GO

-- 4. XÓA THẬT SỰ các bản ghi thừa (chỉ trong tháng 11/2025)
BEGIN TRANSACTION;
GO

DELETE ctl
FROM ChiTietLuong ctl
WHERE NOT EXISTS (SELECT 1 FROM #IdCanGiu k WHERE k.id = ctl.id)
  AND (YEAR(ctl.ngayNhanLuong) = 2025 AND MONTH(ctl.ngayNhanLuong) = 11 
       OR ctl.ngayNhanLuong >= '2025-11-01');

-- Kiểm tra số lượng đã xóa
PRINT N'Đã xóa ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + N' bản ghi thừa';

-- Dọn dẹp bảng tạm
DROP TABLE #IdCanGiu;

COMMIT TRANSACTION;
GO

-- 5. Kiểm tra lại kết quả cuối cùng → phải không còn nhân viên nào bị trùng trong tháng 11/2025
SELECT 
    idNhanVien,
    COUNT(*) AS SoBanGhiConLai
FROM ChiTietLuong
WHERE YEAR(ngayNhanLuong) = 2025 AND MONTH(ngayNhanLuong) = 11
   OR ngayNhanLuong >= '2025-11-01'
GROUP BY idNhanVien
HAVING COUNT(*) > 1;
GO



-- Bước 1: Thêm 2 cột tính toán (năm và tháng của ngayTao)
ALTER TABLE DanhGiaNhanVien
ADD NamDanhGia AS YEAR(ngayTao) PERSISTED;

ALTER TABLE DanhGiaNhanVien
ADD ThangDanhGia AS MONTH(ngayTao) PERSISTED;
GO

-- Bước 2: Tạo chỉ mục UNIQUE trên 3 cột: idNhanVien + Nam + Thang
CREATE UNIQUE INDEX UK_DanhGiaNhanVien_Thang 
ON DanhGiaNhanVien (idNhanVien, NamDanhGia, ThangDanhGia);
GO