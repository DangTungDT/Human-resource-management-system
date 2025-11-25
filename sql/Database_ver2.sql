----Database đồ án quản lý nhân sự
-- Create Database (if not exists)
IF DB_ID(N'PersonnelManagement') IS NULL
BEGIN
    CREATE DATABASE PersonnelManagement;
END
GO

USE PersonnelManagement;
GO

-- Set format date = dd/mm/yyyy
SET DATEFORMAT dmy;
GO

-- =========================
-- Schema
-- =========================

CREATE TABLE PhongBan
(
	id INT IDENTITY(1,1) NOT NULL,
	TenPhongBan NVARCHAR(255) NOT NULL,
	Mota NVARCHAR(255),
	PRIMARY KEY(id)
);
GO

CREATE TABLE ChucVu
(
	id INT IDENTITY(1,1) NOT NULL,
	TenChucVu NVARCHAR(255) NOT NULL,
	luongCoBan DECIMAL(18,2) NOT NULL,
	tyLeHoaHong DECIMAL(5,2) DEFAULT 0,
	moTa NVARCHAR(255),
	idPhongBan INT NOT NULL,
	PRIMARY KEY(id)
);
GO

CREATE TABLE NhanVien
(
	id VARCHAR(10) NOT NULL,
	TenNhanVien NVARCHAR(255) NOT NULL,
	NgaySinh DATE NOT NULL,
	DiaChi NVARCHAR(255) NOT NULL,
	Que NVARCHAR(100) NOT NULL,
	GioiTinh NVARCHAR(4) NOT NULL,
	Email VARCHAR(100) NOT NULL UNIQUE,
	AnhDaiDien VARCHAR(255),
	idChucVu INT NOT NULL,
	idPhongBan INT NOT NULL,
	LoaiNhanVien NVARCHAR(100) NOT NULL DEFAULT N'Nhân viên chính thức',
	DaXoa BIT DEFAULT 0 NOT NULL,
	constraint chk_NgaySinhNV CHECK (NgaySinh <= DATEADD(year, -16, GETDATE())),
	PRIMARY KEY(id)
);
GO

CREATE TABLE TaiKhoan
(
	id INT IDENTITY(1,1) NOT NULL,
	taiKhoan VARCHAR(50) UNIQUE NOT NULL,
	idNhanVien VARCHAR(10) NOT NULL, 
	matKhau VARCHAR(255) NOT NULL,
	PRIMARY KEY(id)
);
GO

CREATE TABLE QuenMatKhau
(
    id INT IDENTITY(1,1) NOT NULL,
    taiKhoan VARCHAR(50) NOT NULL,
    otp VARCHAR(6) NOT NULL,
    thoiGianHetHan DATETIME NOT NULL,
    daXacNhan BIT DEFAULT 0,
    PRIMARY KEY(id),
    FOREIGN KEY (taiKhoan) REFERENCES TaiKhoan(taiKhoan)
);
GO

CREATE TABLE ChamCong
(
	id INT IDENTITY(1,1) NOT NULL,
	NgayChamCong DATE NOT NULL,
	GioRa TIME,
	GioVao TIME,
	idNhanVien VARCHAR(10) NOT NULL,
	PRIMARY KEY(id)
);
GO

CREATE TABLE HopDongLaoDong
(
	id INT IDENTITY(1,1) NOT NULL,
	LoaiHopDong NVARCHAR(100) NOT NULL,
	NgayKy DATE NOT NULL,
	NgayBatDau DATE NOT NULL,
	NgayKetThuc DATE,
	Luong DECIMAL(18,2) NOT NULL,
	HinhAnh VARCHAR(255),
	idNhanVien VARCHAR(10),
	MoTa NVARCHAR(255),
	PRIMARY KEY(id),
	-- allow NULL NgayKetThuc for indefinite contracts
	constraint chk_NgayKetThuc check (NgayKetThuc IS NULL OR NgayBatDau <= NgayKetThuc)
);
GO

CREATE TABLE NghiPhep
(
	id INT IDENTITY(1,1) NOT NULL,
	NgayBatDau DATE NOT NULL,
	NgayKetThuc DATE NOT NULL,
	LyDoNghi NVARCHAR(500) NOT NULL,
	LoaiNghiPhep NVARCHAR(50) CHECK(LoaiNghiPhep IN (N'Có lương', N'Không lương')) NOT NULL,
	idNhanVien VARCHAR(10) NOT NULL,
	TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang yêu cầu',
	NgayDanhGia date null,
	LoaiTruongHop nvarchar(255) null,
	PRIMARY KEY(id),
	constraint chk_NghiPhep_Ngay CHECK (NgayBatDau <= NgayKetThuc)
);
GO

CREATE TABLE DanhGiaNhanVien
(
	id INT IDENTITY(1,1) NOT NULL,
	DiemSo INT NOT NULL,
	DiemChuyenCan int not null default 5,
	DiemNangLuc int not null default 5,
	NhanXet NTEXT,
	ngayTao DATE NOT NULL,
	idNhanVien VARCHAR(10) NOT NULL,
	idNguoiDanhGia VARCHAR(10) NOT NULL,
	PRIMARY KEY(id),
	constraint chk_NguoiDanhGia CHECK (idNhanVien != idNguoiDanhGia),
	constraint chk_DiemSo CHECK (DiemSo BETWEEN 1 AND 10)
);
GO

CREATE TABLE PhuCap
(
	id INT IDENTITY(1,1) NOT NULL,
	soTien DECIMAL(18,2) DEFAULT 0 NOT NULL,
	lyDoPhuCap NVARCHAR(100) NOT NULL,
	PRIMARY KEY(id)
);
GO

CREATE TABLE ChiTietLuong
(
	id INT IDENTITY(1,1) NOT NULL,
	ngayNhanLuong DATE NOT NULL DEFAULT GETDATE(),
	luongTruocKhauTru DECIMAL(18,2) NOT NULL,
	luongSauKhauTru DECIMAL(18,2) NOT NULL,
	tongKhauTru DECIMAL(18,2) NOT NULL,
	tongPhuCap DECIMAL(18,2) NOT NULL,
	tongKhenThuong DECIMAL(18,2) DEFAULT 0 NOT NULL,
	tongTienPhat DECIMAL(18,2) DEFAULT 0 NOT NULL,
	trangThai NVARCHAR(20) DEFAULT N'Chưa giải quyết' NOT NULL,
	ghiChu NVARCHAR(255),
	idNhanVien VARCHAR(10) NOT NULL,
	idKyLuong INT NOT NULL,
	capNhatLuong BIT DEFAULT 0 NOT NULL,
	PRIMARY KEY(id)
);
GO

CREATE TABLE TuyenDung (
    id INT PRIMARY KEY IDENTITY(1,1),
    tieuDe NVARCHAR(150) NOT NULL,
    idPhongBan INT NOT NULL,
    idChucVu INT NOT NULL,
    idNguoiTao VARCHAR(10) NOT NULL,
    trangThai NVARCHAR(50) DEFAULT N'Đang tuyển',
    ngayTao DATETIME DEFAULT GETDATE(),
	ghiChu NVARCHAR(50),
	xacThucYeuCau NVARCHAR(50),
	soLuong INT NOT NULL DEFAULT 1,
	soLuongDuyet INT,
	ghiChuDuyet NVARCHAR(255)
);
GO

CREATE TABLE UngVien (
    id INT PRIMARY KEY IDENTITY(1,1),
    tenNhanVien NVARCHAR(255) NOT NULL,
	ngaySinh DATE NOT NULL,
	diaChi NVARCHAR(255) NOT NULL,
	que NVARCHAR(100) NOT NULL,
	gioiTinh NVARCHAR(4) NOT NULL,
	email VARCHAR(100) NOT NULL UNIQUE,
    duongDanCV NVARCHAR(255),
    idChucVuUngTuyen INT NOT NULL,
	idTuyenDung INT NOT NULL,
    ngayUngTuyen DATE DEFAULT GETDATE(),
    trangThai NVARCHAR(50) DEFAULT N'Đang xét duyệt',
	daXoa BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE NhanVien_PhuCap
(
	idNhanVien VARCHAR(10) NOT NULL,
	idPhuCap INT NOT NULL,
	trangThai NVARCHAR(20) NOT NULL DEFAULT N'Hoạt động',
	constraint check_TrangThai CHECK(trangThai IN (N'Hoạt động', N'Ngưng hoạt động')),
	PRIMARY KEY(idNhanVien, idPhuCap)
);
GO

CREATE TABLE KhauTru
(
	id INT IDENTITY(1,1) NOT NULL,
	loaiKhauTru NVARCHAR(50) NOT NULL,
	soTien DECIMAL(18,2) DEFAULT 0 NOT NULL,
	moTa NVARCHAR(255),
	idNguoiTao VARCHAR(10) NOT NULL,
	PRIMARY KEY(id)
);
GO

CREATE TABLE NhanVien_KhauTru
(
	id INT IDENTITY(1,1) NOT NULL,
	idNhanVien VARCHAR(10),
	idKhauTru INT,
	thangApDung DATE,
	PRIMARY KEY(id)
);
GO

CREATE TABLE ThuongPhat
(
	id INT IDENTITY(1,1) NOT NULL,
	tienThuongPhat DECIMAL(18,2) DEFAULT 0 NOT NULL,
	loai NVARCHAR(20) DEFAULT N'Thưởng',
	lyDo NVARCHAR(255) NOT NULL,
	idNguoiTao VARCHAR(10) NOT NULL,
	constraint check_loai_ThuongPhat CHECK(loai IN (N'Thưởng', N'Phạt', N'Kỷ luật')),
	PRIMARY KEY(id)
);
GO

CREATE TABLE NhanVien_ThuongPhat
(
	id INT IDENTITY(1,1) NOT NULL,
	idNhanVien VARCHAR(10) NOT NULL,
	idThuongPhat INT NOT NULL,
	thangApDung DATE,
	PRIMARY KEY(id)
);
GO

CREATE TABLE KyLuong
(
	id INT IDENTITY(1,1) NOT NULL,
	ngayBatDau DATE,
	ngayKetThuc DATE,
	ngayChiTra DATE,
	trangThai NVARCHAR(20) DEFAULT N'Chưa giải quyết' NOT NULL,
	constraint check_TT_KyLuong CHECK(trangThai IN (N'Chưa giải quyết', N'Đang giải quyết', N'Đã trả')),
	PRIMARY KEY(id)
);
GO

-- =========================
-- Helper function: remove Vietnamese diacritics (used to build ascii emails)
-- If function exists, drop then create to ensure updated version
-- =========================
IF OBJECT_ID('dbo.ufn_RemoveDiacritics', 'FN') IS NOT NULL
    DROP FUNCTION dbo.ufn_RemoveDiacritics;
GO

CREATE FUNCTION dbo.ufn_RemoveDiacritics(@input NVARCHAR(400))
RETURNS NVARCHAR(400)
AS
BEGIN
    DECLARE @s NVARCHAR(400) = @input;

    SET @s = REPLACE(@s, N'á', N'a'); SET @s = REPLACE(@s, N'à', N'a'); SET @s = REPLACE(@s, N'ạ', N'a'); SET @s = REPLACE(@s, N'ả', N'a'); SET @s = REPLACE(@s, N'ã', N'a');
    SET @s = REPLACE(@s, N'â', N'a'); SET @s = REPLACE(@s, N'ấ', N'a'); SET @s = REPLACE(@s, N'ầ', N'a'); SET @s = REPLACE(@s, N'ậ', N'a'); SET @s = REPLACE(@s, N'ẩ', N'a'); SET @s = REPLACE(@s, N'ẫ', N'a');
    SET @s = REPLACE(@s, N'ă', N'a'); SET @s = REPLACE(@s, N'ắ', N'a'); SET @s = REPLACE(@s, N'ằ', N'a'); SET @s = REPLACE(@s, N'ặ', N'a'); SET @s = REPLACE(@s, N'ẳ', N'a'); SET @s = REPLACE(@s, N'ẵ', N'a');

    SET @s = REPLACE(@s, N'Á', N'A'); SET @s = REPLACE(@s, N'À', N'A'); SET @s = REPLACE(@s, N'Ạ', N'A'); SET @s = REPLACE(@s, N'Ả', N'A'); SET @s = REPLACE(@s, N'Ã', N'A');
    SET @s = REPLACE(@s, N'Â', N'A'); SET @s = REPLACE(@s, N'Ấ', N'A'); SET @s = REPLACE(@s, N'Ầ', N'A'); SET @s = REPLACE(@s, N'Ậ', N'A'); SET @s = REPLACE(@s, N'Ẩ', N'A'); SET @s = REPLACE(@s, N'Ẫ', N'A');
    SET @s = REPLACE(@s, N'Ă', N'A'); SET @s = REPLACE(@s, N'Ắ', N'A'); SET @s = REPLACE(@s, N'Ằ', N'A'); SET @s = REPLACE(@s, N'Ặ', N'A'); SET @s = REPLACE(@s, N'Ẳ', N'A'); SET @s = REPLACE(@s, N'Ẵ', N'A');

    SET @s = REPLACE(@s, N'é', N'e'); SET @s = REPLACE(@s, N'è', N'e'); SET @s = REPLACE(@s, N'ẹ', N'e'); SET @s = REPLACE(@s, N'ẻ', N'e'); SET @s = REPLACE(@s, N'ẽ', N'e');
    SET @s = REPLACE(@s, N'ê', N'e'); SET @s = REPLACE(@s, N'ế', N'e'); SET @s = REPLACE(@s, N'ề', N'e'); SET @s = REPLACE(@s, N'ệ', N'e'); SET @s = REPLACE(@s, N'ể', N'e'); SET @s = REPLACE(@s, N'ễ', N'e');
    SET @s = REPLACE(@s, N'É', N'E'); SET @s = REPLACE(@s, N'È', N'E'); SET @s = REPLACE(@s, N'Ẹ', N'E'); SET @s = REPLACE(@s, N'Ẻ', N'E'); SET @s = REPLACE(@s, N'Ẽ', N'E');
    SET @s = REPLACE(@s, N'Ê', N'E'); SET @s = REPLACE(@s, N'Ế', N'E'); SET @s = REPLACE(@s, N'Ề', N'E'); SET @s = REPLACE(@s, N'Ệ', N'E'); SET @s = REPLACE(@s, N'Ể', N'E'); SET @s = REPLACE(@s, N'Ễ', N'E');

    SET @s = REPLACE(@s, N'í', N'i'); SET @s = REPLACE(@s, N'ì', N'i'); SET @s = REPLACE(@s, N'ị', N'i'); SET @s = REPLACE(@s, N'ỉ', N'i'); SET @s = REPLACE(@s, N'ĩ', N'i');
    SET @s = REPLACE(@s, N'Í', N'I'); SET @s = REPLACE(@s, N'Ì', N'I'); SET @s = REPLACE(@s, N'Ị', N'I'); SET @s = REPLACE(@s, N'Ỉ', N'I'); SET @s = REPLACE(@s, N'Ĩ', N'I');

    SET @s = REPLACE(@s, N'ó', N'o'); SET @s = REPLACE(@s, N'ò', N'o'); SET @s = REPLACE(@s, N'ọ', N'o'); SET @s = REPLACE(@s, N'ỏ', N'o'); SET @s = REPLACE(@s, N'õ', N'o');
    SET @s = REPLACE(@s, N'ô', N'o'); SET @s = REPLACE(@s, N'ố', N'o'); SET @s = REPLACE(@s, N'ồ', N'o'); SET @s = REPLACE(@s, N'ộ', N'o'); SET @s = REPLACE(@s, N'ổ', N'o'); SET @s = REPLACE(@s, N'ỗ', N'o');
    SET @s = REPLACE(@s, N'ơ', N'o'); SET @s = REPLACE(@s, N'ớ', N'o'); SET @s = REPLACE(@s, N'ờ', N'o'); SET @s = REPLACE(@s, N'ợ', N'o'); SET @s = REPLACE(@s, N'ở', N'o'); SET @s = REPLACE(@s, N'ỡ', N'o');
    SET @s = REPLACE(@s, N'Ó', N'O'); SET @s = REPLACE(@s, N'Ò', N'O'); SET @s = REPLACE(@s, N'Ọ', N'O'); SET @s = REPLACE(@s, N'Ỏ', N'O'); SET @s = REPLACE(@s, N'Õ', N'O');
    SET @s = REPLACE(@s, N'Ô', N'O'); SET @s = REPLACE(@s, N'Ố', N'O'); SET @s = REPLACE(@s, N'Ồ', N'O'); SET @s = REPLACE(@s, N'Ộ', N'O'); SET @s = REPLACE(@s, N'Ổ', N'O'); SET @s = REPLACE(@s, N'Ỗ', N'O');
    SET @s = REPLACE(@s, N'Ơ', N'O'); SET @s = REPLACE(@s, N'Ớ', N'O'); SET @s = REPLACE(@s, N'Ờ', N'O'); SET @s = REPLACE(@s, N'Ợ', N'O'); SET @s = REPLACE(@s, N'Ở', N'O'); SET @s = REPLACE(@s, N'Ỡ', N'O');

    SET @s = REPLACE(@s, N'ú', N'u'); SET @s = REPLACE(@s, N'ù', N'u'); SET @s = REPLACE(@s, N'ụ', N'u'); SET @s = REPLACE(@s, N'ủ', N'u'); SET @s = REPLACE(@s, N'ũ', N'u');
    SET @s = REPLACE(@s, N'ư', N'u'); SET @s = REPLACE(@s, N'ứ', N'u'); SET @s = REPLACE(@s, N'ừ', N'u'); SET @s = REPLACE(@s, N'ự', N'u'); SET @s = REPLACE(@s, N'ử', N'u'); SET @s = REPLACE(@s, N'ữ', N'u');
    SET @s = REPLACE(@s, N'Ú', N'U'); SET @s = REPLACE(@s, N'Ù', N'U'); SET @s = REPLACE(@s, N'Ụ', N'U'); SET @s = REPLACE(@s, N'Ủ', N'U'); SET @s = REPLACE(@s, N'Ũ', N'U');
    SET @s = REPLACE(@s, N'Ư', N'U'); SET @s = REPLACE(@s, N'Ứ', N'U'); SET @s = REPLACE(@s, N'Ừ', N'U'); SET @s = REPLACE(@s, N'Ự', N'U'); SET @s = REPLACE(@s, N'Ử', N'U'); SET @s = REPLACE(@s, N'Ữ', N'U');

    SET @s = REPLACE(@s, N'ý', N'y'); SET @s = REPLACE(@s, N'ỳ', N'y'); SET @s = REPLACE(@s, N'ỵ', N'y'); SET @s = REPLACE(@s, N'ỷ', N'y'); SET @s = REPLACE(@s, N'ỹ', N'y');
    SET @s = REPLACE(@s, N'Ý', N'Y'); SET @s = REPLACE(@s, N'Ỳ', N'Y'); SET @s = REPLACE(@s, N'Ỵ', N'Y'); SET @s = REPLACE(@s, N'Ỷ', N'Y'); SET @s = REPLACE(@s, N'Ỹ', N'Y');

    SET @s = REPLACE(@s, N'đ', N'd'); SET @s = REPLACE(@s, N'Đ', N'D');

    -- Replace some punctuation characters that might appear in names
    SET @s = REPLACE(@s, N'.', N'');
    SET @s = REPLACE(@s, N',', N'');
    SET @s = REPLACE(@s, N'`', N'');
    SET @s = REPLACE(@s, N'´', N'');
    SET @s = REPLACE(@s, N'’', N'');
    SET @s = REPLACE(@s, N'\', N'');
    SET @s = REPLACE(@s, N'"', N'');
    SET @s = REPLACE(@s, N'–', N'');
    SET @s = REPLACE(@s, N'-', N' ');
    -- return resulting string (may still have spaces)
    RETURN @s;
END;
GO

-- =========================
-- Foreign keys (added after tables)
-- =========================

ALTER TABLE ChucVu
ADD CONSTRAINT fk_PB_ChucVu FOREIGN KEY(idPhongBan) REFERENCES PhongBan(id);
GO

ALTER TABLE NhanVien
ADD CONSTRAINT fk_ChucVu_NhanVien FOREIGN KEY(idChucVu) REFERENCES ChucVu(id),
	CONSTRAINT fk_PhongBan_NhanVien FOREIGN KEY(idPhongBan) REFERENCES PhongBan(id);
GO

ALTER TABLE ThuongPhat
ADD CONSTRAINT fk_NV_ThuongPhat FOREIGN KEY(idNguoiTao) REFERENCES NhanVien(id);
GO

ALTER TABLE KhauTru
ADD CONSTRAINT fk_NV_KhauTru FOREIGN KEY(idNguoiTao) REFERENCES NhanVien(id);
GO

ALTER TABLE NhanVien_ThuongPhat
ADD CONSTRAINT fk_NV_NVTP FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id),
    CONSTRAINT fk_TP_NVTP FOREIGN KEY(idThuongPhat) REFERENCES ThuongPhat(id);
GO

ALTER TABLE ChiTietLuong
ADD CONSTRAINT fk_NhanVien_ChiTietLuong FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id),
	CONSTRAINT fk_KyLuong_ChiTietLuong FOREIGN KEY(idKyLuong) REFERENCES KyLuong(id);
GO

ALTER TABLE NhanVien_PhuCap
ADD CONSTRAINT fk_NhanVien_NVPC FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id),
    CONSTRAINT fk_PhuCap_NVPC FOREIGN KEY(idPhuCap) REFERENCES PhuCap(id);
GO

ALTER TABLE ChamCong
ADD CONSTRAINT fk_NhanVien_ChamCong FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id);
GO

ALTER TABLE TaiKhoan
ADD CONSTRAINT fk_NhanVien_TaiKhoan FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id);
GO

ALTER TABLE NghiPhep
ADD CONSTRAINT fk_NhanVien_NghiPhep FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id);
GO

ALTER TABLE HopDongLaoDong
ADD CONSTRAINT fk_NhanVien_HopDongLaoDong FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id);
GO

ALTER TABLE DanhGiaNhanVien
ADD CONSTRAINT fk_NV1_DGNV FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id),
	CONSTRAINT fk_NV2_DGNV FOREIGN KEY(idNguoiDanhGia) REFERENCES NhanVien(id);
GO

ALTER TABLE UngVien
ADD CONSTRAINT FK_UngVien_ChucVu FOREIGN KEY (idChucVuUngTuyen) REFERENCES ChucVu(id);
GO

ALTER TABLE TuyenDung
ADD CONSTRAINT FK_TuyenDung_PhongBan FOREIGN KEY (idPhongBan) REFERENCES PhongBan(id),
    CONSTRAINT FK_TuyenDung_ChucVu FOREIGN KEY (idChucVu) REFERENCES ChucVu(id),
    CONSTRAINT FK_TuyenDung_NhanVien FOREIGN KEY (idNguoiTao) REFERENCES NhanVien(id);
GO

ALTER TABLE NhanVien_KhauTru
ADD CONSTRAINT fk_NhanVien_NVKT FOREIGN KEY(idNhanVien) REFERENCES NhanVien(id),
CONSTRAINT fk_KhauTru_NVKT FOREIGN KEY(idKhauTru) REFERENCES KhauTru(id);
GO

-- =========================
-- Seed data (idempotent and protected)
-- Changes made:
--  - NhanVien email is generated from name without diacritics + 4-digit seq + '@gmail.com'
--  - AnhDaiDien for NhanVien is set to '1.png'
--  - duongDanCV for UngVien is set to '1.png'
--  - IMPORTANT: NghiPhep seed now uses 'Có lương' / 'Không lương' to match CHECK constraint
-- =========================

USE PersonnelManagement;
GO
SET DATEFORMAT dmy;
GO

-- Wrap seed in transaction with TRY/CATCH
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRAN;

    -- 1) Ensure departments (required ones plus some extras)
    IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Nhân sự')
        INSERT INTO PhongBan (TenPhongBan, Mota) VALUES (N'Nhân sự', N'Phòng ban phụ trách nhân sự');

    IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Công nghệ thông tin')
        INSERT INTO PhongBan (TenPhongBan, Mota) VALUES (N'Công nghệ thông tin', N'Phòng ban chịu trách nhiệm CNTT');

    IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Giám đốc')
        INSERT INTO PhongBan (TenPhongBan, Mota) VALUES (N'Giám đốc', N'Phòng ban Giám đốc');

    IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Kế toán')
        INSERT INTO PhongBan (TenPhongBan, Mota) VALUES (N'Kế toán', N'Phòng ban kế toán');

    IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Marketing')
        INSERT INTO PhongBan (TenPhongBan, Mota) VALUES (N'Marketing', N'Phòng ban Marketing');

    -- 2) Ensure ChucVu entries for each PhongBan (if missing create defaults)
    DECLARE @pbid_seed INT, @pbname_seed NVARCHAR(255);
    DECLARE cur_pb_seed CURSOR LOCAL FAST_FORWARD FOR SELECT id, TenPhongBan FROM PhongBan;
    OPEN cur_pb_seed;
    FETCH NEXT FROM cur_pb_seed INTO @pbid_seed, @pbname_seed;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF @pbname_seed = N'Giám đốc'
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE idPhongBan = @pbid_seed AND TenChucVu = N'Giám đốc')
            BEGIN
                INSERT INTO ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan)
                VALUES (N'Giám đốc', 50000000, 10, N'Auto-created Giám đốc', @pbid_seed);
            END
        END
        ELSE
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE idPhongBan = @pbid_seed AND TenChucVu LIKE N'Nhân viên%')
            BEGIN
                INSERT INTO ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan)
                VALUES (N'Nhân viên ' + @pbname_seed, 10000000, 0, N'Auto-created Nhân viên', @pbid_seed);
            END
            IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE idPhongBan = @pbid_seed AND TenChucVu LIKE N'Trưởng phòng%')
            BEGIN
                INSERT INTO ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan)
                VALUES (N'Trưởng phòng ' + @pbname_seed, 20000000, 0, N'Auto-created Trưởng phòng', @pbid_seed);
            END
        END

        FETCH NEXT FROM cur_pb_seed INTO @pbid_seed, @pbname_seed;
    END
    CLOSE cur_pb_seed;
    DEALLOCATE cur_pb_seed;

    -- 3) Prepare list of 30 names (temp table)
    IF OBJECT_ID('tempdb..#Names') IS NOT NULL DROP TABLE #Names;
    CREATE TABLE #Names (rn INT IDENTITY(1,1) PRIMARY KEY, TenNhanVien NVARCHAR(255));
    INSERT INTO #Names (TenNhanVien)
    SELECT * FROM (VALUES
        (N'Nguyễn Văn An'), (N'Lê Thị Bích'), (N'Phạm Văn Cường'), (N'Hoàng Thị Dung'),
        (N'Vũ Minh Đức'), (N'Nguyễn Thị Giang'), (N'Đặng Thanh Tùng'), (N'Nguyễn Thành Tuấn'),
        (N'Lý Thị Thanh Ngân'), (N'Trần Văn Hùng'), (N'Phan Thị Hồng'), (N'Bùi Quang Huy'),
        (N'Nguyễn Thị Hoa'), (N'Đỗ Văn Khang'), (N'Phạm Thị Lan'), (N'Nguyễn Văn Long'),
        (N'Trần Thị Mai'), (N'Võ Anh Minh'), (N'Hoàng Ngọc Nam'), (N'Lê Văn Nam'),
        (N'Ngô Văn Nam'), (N'Trần Đức Phú'), (N'Nguyễn Tuấn Quang'), (N'Phan Thanh Sang'),
        (N'Nguyễn Minh Tâm'), (N'Đinh Thị Thảo'), (N'Lê Văn Tiến'), (N'Nguyễn Thị Tuyết'),
        (N'Phạm Văn Trung'), (N'Trần Thị Vân')
    ) AS t(name);

    -- 4) Build department short codes temp table
    IF OBJECT_ID('tempdb..#Dept') IS NOT NULL DROP TABLE #Dept;
    CREATE TABLE #Dept (id INT PRIMARY KEY, TenPhongBan NVARCHAR(255), Code NVARCHAR(20));
    INSERT INTO #Dept (id, TenPhongBan, Code)
    SELECT id, TenPhongBan,
        CASE
            WHEN TenPhongBan LIKE N'%Công nghệ%' OR TenPhongBan = N'Công nghệ thông tin' THEN 'CNTT'
            WHEN TenPhongBan LIKE N'%Nhân sự%' THEN 'NS'
            WHEN TenPhongBan LIKE N'%Giám đốc%' THEN 'GD'
            WHEN TenPhongBan LIKE N'%Kế toán%' THEN 'KT'
            WHEN TenPhongBan LIKE N'%Marketing%' THEN 'MK'
            ELSE UPPER(LEFT(TenPhongBan,3))
        END
    FROM PhongBan;

    -- 5) Map ChucVu per department
    IF OBJECT_ID('tempdb..#ChucVuMap') IS NOT NULL DROP TABLE #ChucVuMap;
    CREATE TABLE #ChucVuMap (DeptId INT, DeptCode NVARCHAR(20), TenPhongBan NVARCHAR(255), ChucVu_Nv_Id INT, ChucVu_Tp_Id INT, ChucVu_Gd_Id INT);

    INSERT INTO #ChucVuMap (DeptId, DeptCode, TenPhongBan, ChucVu_Nv_Id, ChucVu_Tp_Id, ChucVu_Gd_Id)
    SELECT d.id, d.Code, d.TenPhongBan,
        (SELECT TOP 1 id FROM ChucVu WHERE TenChucVu = N'Nhân viên ' + d.TenPhongBan AND idPhongBan = d.id),
        (SELECT TOP 1 id FROM ChucVu WHERE TenChucVu = N'Trưởng phòng ' + d.TenPhongBan AND idPhongBan = d.id),
        (SELECT TOP 1 id FROM ChucVu WHERE TenChucVu = N'Giám đốc' AND idPhongBan = d.id)
    FROM #Dept d;

    UPDATE #ChucVuMap
    SET ChucVu_Nv_Id = ISNULL(ChucVu_Nv_Id, (SELECT TOP 1 id FROM ChucVu cv WHERE cv.idPhongBan = DeptId AND cv.TenChucVu LIKE N'Nhân viên%')),
        ChucVu_Tp_Id = ISNULL(ChucVu_Tp_Id, (SELECT TOP 1 id FROM ChucVu cv WHERE cv.idPhongBan = DeptId AND cv.TenChucVu LIKE N'Trưởng phòng%')),
        ChucVu_Gd_Id = ISNULL(ChucVu_Gd_Id, (SELECT TOP 1 id FROM ChucVu cv WHERE cv.idPhongBan = DeptId AND cv.TenChucVu LIKE N'Giám đốc%'));

    -- 6) Prepare EmployeesToInsert temp table with 30 rows
    IF OBJECT_ID('tempdb..#EmployeesToInsert') IS NOT NULL DROP TABLE #EmployeesToInsert;
    CREATE TABLE #EmployeesToInsert
    (
        rn INT PRIMARY KEY,
        TenNhanVien NVARCHAR(255),
        DeptId INT,
        DeptCode NVARCHAR(20),
        ChucVuId INT,
        ChucVuAbbrev NVARCHAR(5),
        IdNhanVien VARCHAR(10),
        NgaySinh DATE,
        DiaChi NVARCHAR(255),
        Que NVARCHAR(100),
        GioiTinh NVARCHAR(4),
        Email VARCHAR(100),
        AnhDaiDien VARCHAR(255),
        LoaiNhanVien NVARCHAR(100),
        DaXoa BIT
    );

    ;WITH D AS (
        SELECT ROW_NUMBER() OVER(ORDER BY id) rn, id, TenPhongBan, Code FROM #Dept
    ),
    N AS (
        SELECT rn, TenNhanVien FROM #Names
    ),
    Assign AS (
        SELECT n.rn,
               n.TenNhanVien,
               d.id AS DeptId,
               d.Code AS DeptCode,
               d.TenPhongBan
        FROM N n
        JOIN D d ON d.rn = ((n.rn - 1) % (SELECT COUNT(*) FROM #Dept)) + 1
    )
    INSERT INTO #EmployeesToInsert (rn, TenNhanVien, DeptId, DeptCode, ChucVuId, ChucVuAbbrev, IdNhanVien, NgaySinh, DiaChi, Que, GioiTinh, Email, AnhDaiDien, LoaiNhanVien, DaXoa)
    SELECT
        a.rn,
        a.TenNhanVien,
        a.DeptId,
        a.DeptCode,
        CASE
            WHEN a.DeptCode = 'GD' AND a.rn = 1 THEN cm.ChucVu_Gd_Id
            WHEN a.rn % 11 = 0 THEN cm.ChucVu_Tp_Id
            WHEN a.rn % 13 = 0 AND cm.ChucVu_Tp_Id IS NOT NULL THEN cm.ChucVu_Tp_Id
            ELSE cm.ChucVu_Nv_Id
        END,
        CASE
            WHEN (a.DeptCode = 'GD' AND a.rn = 1) THEN 'GD'
            WHEN a.rn % 11 = 0 THEN 'TP'
            WHEN a.rn % 13 = 0 THEN 'TP'
            ELSE 'NV'
        END,
        NULL,
        DATEFROMPARTS(1985 + ((a.rn * 3) % 25), ((a.rn * 5) % 12) + 1, ((a.rn * 7) % 27) + 1),
        N'TPHCM',
        N'Quê nhà',
        CASE WHEN a.rn % 2 = 0 THEN N'Nam' ELSE N'Nữ' END,
        -- Email = remove diacritics, remove spaces, lowercase + 4-digit seq + @gmail.com
        LOWER(REPLACE(dbo.ufn_RemoveDiacritics(a.TenNhanVien), N' ', N'')) + RIGHT('0000' + CAST(a.rn AS VARCHAR(4)),4) + '@gmail.com',
        -- AnhDaiDien fixed to '1.png' as requested
        '1.png',
        CASE WHEN a.rn % 7 = 0 THEN N'Thử việc' ELSE N'Nhân viên chính thức' END,
        0
    FROM Assign a
    LEFT JOIN #ChucVuMap cm ON cm.DeptId = a.DeptId;

    -- 7) Insert employees into NhanVien ensuring idChucVu is not NULL:
    IF OBJECT_ID('tempdb..#EmployeesToInsert') IS NOT NULL
    BEGIN
        DECLARE cur_emp_seed CURSOR LOCAL FAST_FORWARD FOR
            SELECT rn, TenNhanVien, DeptId, DeptCode, ChucVuId, ChucVuAbbrev, NgaySinh, DiaChi, Que, GioiTinh, Email, AnhDaiDien, LoaiNhanVien, DaXoa
            FROM #EmployeesToInsert
            ORDER BY rn;

        OPEN cur_emp_seed;

        DECLARE @rn_e INT, @ten_e NVARCHAR(255), @deptid_e INT, @deptcode_e NVARCHAR(20), @chucvu_e INT, @chucvuabbr_e NVARCHAR(5),
                @ngaysinh_e DATE, @diachi_e NVARCHAR(255), @que_e NVARCHAR(100), @gioitinh_e NVARCHAR(4), @email_e VARCHAR(100), @anh_e NVARCHAR(255),
                @loai_e NVARCHAR(100), @daxoa_e BIT;

        -- helper vars
        DECLARE @seq_e VARCHAR(4), @id_e NVARCHAR(20), @emailUnique_e VARCHAR(100), @e_i_e INT, @chucvu_final_e INT, @deptname_e NVARCHAR(255),
                @dup_i_e INT, @newId_e VARCHAR(20);

        FETCH NEXT FROM cur_emp_seed INTO @rn_e, @ten_e, @deptid_e, @deptcode_e, @chucvu_e, @chucvuabbr_e, @ngaysinh_e, @diachi_e, @que_e, @gioitinh_e, @email_e, @anh_e, @loai_e, @daxoa_e;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @seq_e = RIGHT('0000' + CAST(@rn_e AS VARCHAR(4)),4);

            IF @chucvuabbr_e = 'GD'
                SET @id_e = 'GD' + @deptcode_e + @seq_e;
            ELSE IF @chucvuabbr_e = 'TP'
                SET @id_e = 'TP' + @deptcode_e + @seq_e;
            ELSE
                SET @id_e = 'NV' + @deptcode_e + @seq_e;

            IF LEN(@id_e) > 10
                SET @id_e = LEFT(@id_e, 10);

            -- ensure unique email: if exists append numeric suffix before @
            SET @emailUnique_e = @email_e;
            SET @e_i_e = 1;
            WHILE EXISTS (SELECT 1 FROM NhanVien WHERE Email = @emailUnique_e)
            BEGIN
                SET @emailUnique_e = LEFT(@email_e, CHARINDEX('@', @email_e)-1) + CAST(@e_i_e AS VARCHAR(3)) + SUBSTRING(@email_e, CHARINDEX('@', @email_e), 100);
                SET @e_i_e = @e_i_e + 1;
            END

            -- ensure age constraint
            IF @ngaysinh_e > DATEADD(YEAR, -16, GETDATE())
                SET @ngaysinh_e = DATEADD(YEAR, -16, GETDATE());

            -- determine final ChucVu id
            SET @chucvu_final_e = @chucvu_e;
            IF @chucvu_final_e IS NULL
                SELECT TOP 1 @chucvu_final_e = id FROM ChucVu WHERE idPhongBan = @deptid_e ORDER BY id;

            IF @chucvu_final_e IS NULL
            BEGIN
                -- create fallback ChucVu for this dept
                SELECT @deptname_e = TenPhongBan FROM PhongBan WHERE id = @deptid_e;
                INSERT INTO ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan)
                VALUES (N'Nhân viên ' + ISNULL(@deptname_e, CAST(@deptid_e AS NVARCHAR(10))), 10000000, 0, N'Auto-created fallback', @deptid_e);
                SET @chucvu_final_e = SCOPE_IDENTITY();
            END

            -- insert employee (unique id)
            IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE id = @id_e)
            BEGIN
                INSERT INTO NhanVien (id, TenNhanVien, NgaySinh, DiaChi, Que, GioiTinh, Email, AnhDaiDien, idChucVu, idPhongBan, LoaiNhanVien, DaXoa)
                VALUES (@id_e, @ten_e, @ngaysinh_e, @diachi_e, @que_e, @gioitinh_e, @emailUnique_e, @anh_e, @chucvu_final_e, @deptid_e, @loai_e, @daxoa_e);
            END
            ELSE
            BEGIN
                SET @dup_i_e = 1;
                SET @newId_e = @id_e + '_' + CAST(@dup_i_e AS VARCHAR(3));
                WHILE EXISTS (SELECT 1 FROM NhanVien WHERE id = @newId_e)
                BEGIN
                    SET @dup_i_e = @dup_i_e + 1;
                    SET @newId_e = @id_e + '_' + CAST(@dup_i_e AS VARCHAR(3));
                END
                INSERT INTO NhanVien (id, TenNhanVien, NgaySinh, DiaChi, Que, GioiTinh, Email, AnhDaiDien, idChucVu, idPhongBan, LoaiNhanVien, DaXoa)
                VALUES (@newId_e, @ten_e, @ngaysinh_e, @diachi_e, @que_e, @gioitinh_e, @emailUnique_e, @anh_e, @chucvu_final_e, @deptid_e, @loai_e, @daxoa_e);
            END

            FETCH NEXT FROM cur_emp_seed INTO @rn_e, @ten_e, @deptid_e, @deptcode_e, @chucvu_e, @chucvuabbr_e, @ngaysinh_e, @diachi_e, @que_e, @gioitinh_e, @email_e, @anh_e, @loai_e, @daxoa_e;
        END

        CLOSE cur_emp_seed;
        DEALLOCATE cur_emp_seed;
    END

    -- 8) Generate TaiKhoan for each NhanVien without account
    DECLARE @pwdhash_seed VARCHAR(255) = '6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b';

    DECLARE cur_acc_seed CURSOR LOCAL FAST_FORWARD FOR
        SELECT id, TenNhanVien FROM NhanVien WHERE id NOT IN (SELECT idNhanVien FROM TaiKhoan);
    OPEN cur_acc_seed;
    DECLARE @eid_acc VARCHAR(20), @ename_acc NVARCHAR(255);

    -- account helper vars
    DECLARE @prefix_acc NVARCHAR(3), @deptcode_acc NVARCHAR(10), @initials_acc NVARCHAR(50), @taiBase_acc VARCHAR(80), @tai_acc VARCHAR(80), @tai_i_acc INT;

    FETCH NEXT FROM cur_acc_seed INTO @eid_acc, @ename_acc;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @prefix_acc = LEFT(@eid_acc,2);
        SET @deptcode_acc = SUBSTRING(@eid_acc,3, CASE WHEN LEN(@eid_acc) - 6 > 0 THEN LEN(@eid_acc) - 6 ELSE 1 END);
        IF @deptcode_acc = '' SET @deptcode_acc = SUBSTRING(@eid_acc,3,3);

        SET @initials_acc = '';
        ;WITH parts_acc AS (
            SELECT value FROM STRING_SPLIT(REPLACE(@ename_acc, N'Đ', N'D'), ' ')
        )
        SELECT @initials_acc = @initials_acc + UPPER(LEFT(value,1))
        FROM parts_acc;

        IF LEN(@initials_acc) = 0 SET @initials_acc = LEFT(REPLACE(@ename_acc, N' ', ''), 3);

        SET @taiBase_acc = @prefix_acc + @deptcode_acc + @initials_acc + RIGHT('0000' + RIGHT(@eid_acc,4),4);
        SET @tai_acc = @taiBase_acc;
        SET @tai_i_acc = 1;
        WHILE EXISTS (SELECT 1 FROM TaiKhoan WHERE taiKhoan = @tai_acc)
        BEGIN
            SET @tai_acc = @taiBase_acc + CAST(@tai_i_acc AS VARCHAR(3));
            SET @tai_i_acc = @tai_i_acc + 1;
        END

        INSERT INTO TaiKhoan (taiKhoan, idNhanVien, matKhau)
        VALUES (@tai_acc, @eid_acc, @pwdhash_seed);

        FETCH NEXT FROM cur_acc_seed INTO @eid_acc, @ename_acc;
    END
    CLOSE cur_acc_seed;
    DEALLOCATE cur_acc_seed;

    -- 9) QuenMatKhau entries for each TaiKhoan if missing
    INSERT INTO QuenMatKhau (taiKhoan, otp, thoiGianHetHan, daXacNhan)
    SELECT t.taiKhoan,
           RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)),6),
           DATEADD(MINUTE, 30, GETDATE()),
           0
    FROM TaiKhoan t
    LEFT JOIN QuenMatKhau q ON q.taiKhoan = t.taiKhoan
    WHERE q.taiKhoan IS NULL;

    -- 10) ChamCong from 2025-10-01 to 2025-11-08 for each active employee (avoid duplicates)
    ;WITH DatesSeed AS (
        SELECT CAST('2025-10-01' AS DATE) AS d
        UNION ALL
        SELECT DATEADD(DAY, 1, d) FROM DatesSeed WHERE d < '2025-11-08'
    ),
    EmpsSeed AS (
        SELECT id AS idNhanVien FROM NhanVien WHERE DaXoa = 0
    )
    INSERT INTO ChamCong (NgayChamCong, GioVao, GioRa, idNhanVien)
    SELECT d.d, CAST('07:00' AS TIME), CAST('17:00' AS TIME), e.idNhanVien
    FROM DatesSeed d
    CROSS JOIN EmpsSeed e
    WHERE NOT EXISTS (SELECT 1 FROM ChamCong cc WHERE cc.NgayChamCong = d.d AND cc.idNhanVien = e.idNhanVien)
    OPTION (MAXRECURSION 0);

    -- 11) HopDongLaoDong: ensure at least one per NV (avoid duplicates where NgayBatDau same)
    DECLARE @hd_start_seed DATE = '2025-09-01';
    INSERT INTO HopDongLaoDong (LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, Luong, HinhAnh, idNhanVien, MoTa)
    SELECT
        CASE WHEN NV.LoaiNhanVien = N'Thử việc' THEN N'hợp đồng thử việc' ELSE N'hợp đồng nhân viên chính thức' END,
        GETDATE(),
        @hd_start_seed,
        CASE
            WHEN NV.LoaiNhanVien = N'Thử việc' THEN DATEADD(MONTH, CASE (ABS(CHECKSUM(NEWID())) % 4) WHEN 0 THEN 3 WHEN 1 THEN 6 WHEN 2 THEN 9 ELSE 12 END, @hd_start_seed)
            ELSE DATEADD(YEAR, (ABS(CHECKSUM(NEWID())) % 3) + 1, @hd_start_seed)
        END,
        ISNULL((SELECT TOP 1 luongCoBan FROM ChucVu WHERE id = NV.idChucVu), 10000000),
        NULL,
        NV.id,
        N'Hợp đồng tự động tạo'
    FROM NhanVien NV
    WHERE NOT EXISTS (SELECT 1 FROM HopDongLaoDong h WHERE h.idNhanVien = NV.id AND h.NgayBatDau = @hd_start_seed);

    -- 12) PhuCap ensure >=30
    DECLARE @pc_count_seed INT = (SELECT COUNT(*) FROM PhuCap);
    IF @pc_count_seed < 30
    BEGIN
        DECLARE @pc_toadd_seed INT = 30 - @pc_count_seed;
        ;WITH nums_pc AS (
            SELECT 1 AS n
            UNION ALL
            SELECT n+1 FROM nums_pc WHERE n < @pc_toadd_seed
        )
        INSERT INTO PhuCap (soTien, lyDoPhuCap)
        SELECT 100000 * (n % 10 + 1), N'Phụ cấp tự động #' + CAST(n AS NVARCHAR(10))
        FROM nums_pc
        OPTION (MAXRECURSION 0);
    END

    -- 13) NhanVien_PhuCap ensure >=30 rows (assign random)
    DECLARE @nvpc_count_seed INT = (SELECT COUNT(*) FROM NhanVien_PhuCap);
    IF @nvpc_count_seed < 30
    BEGIN
        INSERT INTO NhanVien_PhuCap (idNhanVien, idPhuCap, trangThai)
        SELECT TOP (30 - @nvpc_count_seed) NV.id, P.id, N'Hoạt động'
        FROM NhanVien NV
        CROSS APPLY (SELECT TOP 5 id FROM PhuCap ORDER BY NEWID()) P
        LEFT JOIN NhanVien_PhuCap np ON np.idNhanVien = NV.id AND np.idPhuCap = P.id
        WHERE NV.DaXoa = 0 AND np.idNhanVien IS NULL
        ORDER BY NEWID();
    END

    -- 14) KhauTru ensure >=30
    DECLARE @kt_count_seed INT = (SELECT COUNT(*) FROM KhauTru);
    IF @kt_count_seed < 30
    BEGIN
        DECLARE @kt_toadd_seed INT = 30 - @kt_count_seed;
        ;WITH nums_kt AS (
            SELECT 1 AS n
            UNION ALL
            SELECT n+1 FROM nums_kt WHERE n < @kt_toadd_seed
        )
        INSERT INTO KhauTru (loaiKhauTru, soTien, moTa, idNguoiTao)
        SELECT N'Khấu trừ tự động #' + CAST(n AS NVARCHAR(10)), 100000 * (n % 10 + 1), N'Mô tả khấu trừ ' + CAST(n AS NVARCHAR(10)),
               (SELECT TOP 1 id FROM NhanVien WHERE DaXoa = 0 ORDER BY NEWID())
        FROM nums_kt
        OPTION (MAXRECURSION 0);
    END

    -- 15) NhanVien_KhauTru ensure >=30
    IF (SELECT COUNT(*) FROM NhanVien_KhauTru) < 30
    BEGIN
        INSERT INTO NhanVien_KhauTru (idNhanVien, idKhauTru, thangApDung)
        SELECT TOP (30 - (SELECT COUNT(*) FROM NhanVien_KhauTru)) NV.id, K.id, '2025-09-01'
        FROM NhanVien NV
        CROSS JOIN (SELECT TOP 30 id FROM KhauTru ORDER BY NEWID()) K
        LEFT JOIN NhanVien_KhauTru nk ON nk.idNhanVien = NV.id AND nk.idKhauTru = K.id
        WHERE nk.idNhanVien IS NULL AND NV.DaXoa = 0
        ORDER BY NEWID();
    END

    -- 16) ThuongPhat & NhanVien_ThuongPhat ensure >=30
    DECLARE @tp_count_seed INT = (SELECT COUNT(*) FROM ThuongPhat);
    IF @tp_count_seed < 30
    BEGIN
        DECLARE @tp_toadd_seed INT = 30 - @tp_count_seed;
        ;WITH nums_tp AS (
            SELECT 1 AS n
            UNION ALL
            SELECT n+1 FROM nums_tp WHERE n < @tp_toadd_seed
        )
        INSERT INTO ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao)
        SELECT 500000 * (n % 5 + 1),
               CASE WHEN n % 3 = 0 THEN N'Phạt' WHEN n % 3 = 1 THEN N'Thưởng' ELSE N'Kỷ luật' END,
               N'Lý do tự động #' + CAST(n AS NVARCHAR(10)),
               (SELECT TOP 1 id FROM NhanVien WHERE DaXoa = 0 ORDER BY NEWID())
        FROM nums_tp
        OPTION (MAXRECURSION 0);
    END

    IF (SELECT COUNT(*) FROM NhanVien_ThuongPhat) < 30
    BEGIN
        INSERT INTO NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung)
        SELECT TOP (30 - (SELECT COUNT(*) FROM NhanVien_ThuongPhat)) NV.id, TP.id, '2025-09-01'
        FROM NhanVien NV
        CROSS JOIN (SELECT TOP 30 id FROM ThuongPhat ORDER BY NEWID()) TP
        LEFT JOIN NhanVien_ThuongPhat ntp ON ntp.idNhanVien = NV.id AND ntp.idThuongPhat = TP.id
        WHERE ntp.idNhanVien IS NULL AND NV.DaXoa = 0
        ORDER BY NEWID();
    END

    -- 17) KyLuong ensure >=30
    IF (SELECT COUNT(*) FROM KyLuong) < 30
    BEGIN
        DECLARE @base_seed DATE = '2024-01-01';
        DECLARE @ky_i INT = 0;
        WHILE (SELECT COUNT(*) FROM KyLuong) < 30
        BEGIN
            INSERT INTO KyLuong (ngayBatDau, ngayKetThuc, ngayChiTra, trangThai)
            VALUES (DATEADD(MONTH, @ky_i, @base_seed),
                    DATEADD(DAY, -1, DATEADD(MONTH, @ky_i+1, @base_seed)),
                    DATEADD(DAY, 5, DATEADD(MONTH, @ky_i+1, @base_seed)),
                    CASE WHEN @ky_i < 20 THEN N'Đã trả' ELSE N'Chưa giải quyết' END);
            SET @ky_i = @ky_i + 1;
        END
    END

    -- 18) ChiTietLuong ensure >=30
    IF (SELECT COUNT(*) FROM ChiTietLuong) < 30
    BEGIN
        INSERT INTO ChiTietLuong (ngayNhanLuong, luongTruocKhauTru, luongSauKhauTru, tongKhauTru, tongPhuCap, tongKhenThuong, tongTienPhat, trangThai, ghiChu, idNhanVien, idKyLuong)
        SELECT GETDATE(), c.luongCoBan, c.luongCoBan * 0.9, c.luongCoBan * 0.1, 500000, 0, 0, N'Chưa giải quyết', N'Lương mẫu', NV.id,
               (SELECT TOP 1 id FROM KyLuong ORDER BY NEWID())
        FROM (SELECT TOP (30 - (SELECT COUNT(*) FROM ChiTietLuong)) * FROM ChucVu ORDER BY NEWID()) c
        CROSS JOIN (SELECT TOP 30 id FROM NhanVien WHERE DaXoa = 0 ORDER BY NEWID()) NV;
    END

    -- 19) DanhGiaNhanVien ensure >=30
    IF (SELECT COUNT(*) FROM DanhGiaNhanVien) < 30
    BEGIN
        INSERT INTO DanhGiaNhanVien (DiemSo, NhanXet, ngayTao, idNhanVien, idNguoiDanhGia)
        SELECT TOP (30 - (SELECT COUNT(*) FROM DanhGiaNhanVien))
               (ABS(CHECKSUM(NEWID())) % 10) + 1,
               N'Đánh giá tự động #' + CAST(ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS NVARCHAR(10)),
               DATEADD(DAY, -((ABS(CHECKSUM(NEWID())) % 365)), GETDATE()),
               NV.id,
               (SELECT TOP 1 id FROM NhanVien WHERE id <> NV.id AND DaXoa = 0 ORDER BY NEWID())
        FROM NhanVien NV
        WHERE NV.DaXoa = 0;
    END

    -- 20) NghiPhep ensure >=30 with NgayKetThuc >= NgayBatDau
    IF (SELECT COUNT(*) FROM NghiPhep) < 30
    BEGIN
        DECLARE @np_i INT = 1;
        WHILE (SELECT COUNT(*) FROM NghiPhep) < 30
        BEGIN
            DECLARE @nb DATE = DATEADD(DAY, - (ABS(CHECKSUM(NEWID())) % 60), CAST(GETDATE() AS DATE));
            DECLARE @len INT = (ABS(CHECKSUM(NEWID())) % 5);
            DECLARE @nk DATE = DATEADD(DAY, @len, @nb);
            INSERT INTO NghiPhep (NgayBatDau, NgayKetThuc, LyDoNghi, LoaiNghiPhep, idNhanVien, TrangThai)
            VALUES (@nb, @nk, N'Lý do nghỉ mẫu ' + CAST(@np_i AS NVARCHAR(10)),
                    CASE WHEN (ABS(CHECKSUM(NEWID())) % 2) = 0 THEN N'Có lương' ELSE N'Không lương' END,
                    (SELECT TOP 1 id FROM NhanVien WHERE DaXoa = 0 ORDER BY NEWID()),
                    CASE WHEN (ABS(CHECKSUM(NEWID())) % 3) = 0 THEN N'Đã duyệt' ELSE N'Đang yêu cầu' END);
            SET @np_i = @np_i + 1;
        END
    END

    -- 21) TuyenDung ensure >=30
    IF (SELECT COUNT(*) FROM TuyenDung) < 30
    BEGIN
        INSERT INTO TuyenDung (tieuDe, idPhongBan, idChucVu, idNguoiTao, trangThai, ngayTao, ghiChu, xacThucYeuCau, soLuong)
        SELECT TOP (30 - (SELECT COUNT(*) FROM TuyenDung))
               N'Tuyển dụng mẫu #' + CAST(ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS NVARCHAR(10)),
               (SELECT TOP 1 id FROM PhongBan ORDER BY NEWID()),
               (SELECT TOP 1 id FROM ChucVu ORDER BY NEWID()),
               (SELECT TOP 1 id FROM NhanVien WHERE DaXoa = 0 ORDER BY NEWID()),
               N'Đang tuyển',
               GETDATE(),
               N'Ghi chú tuyển dụng',
               CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN N'Đã xác thực' ELSE N'Chưa xác thực' END,
               (ABS(CHECKSUM(NEWID())) % 3) + 1
        FROM sys.objects s1;
    END

    -- 22) UngVien ensure >=30
    IF (SELECT COUNT(*) FROM UngVien) < 30
    BEGIN
        DECLARE @uv_i INT = 1;
        WHILE (SELECT COUNT(*) FROM UngVien) < 30
        BEGIN
            INSERT INTO UngVien (tenNhanVien, ngaySinh, diaChi, que, gioiTinh, email, duongDanCV, idChucVuUngTuyen, idTuyenDung, trangThai, daXoa)
            VALUES (N'Ứng viên ' + CAST(@uv_i AS NVARCHAR(5)),
                    DATEFROMPARTS(1990 + (@uv_i % 10), ((@uv_i*3) % 12) + 1, ((@uv_i*5) % 27) + 1),
                    N'TP HCM', N'Tỉnh', CASE WHEN @uv_i % 2 = 0 THEN N'Nam' ELSE N'Nữ' END,
                    N'uv' + CAST(@uv_i AS NVARCHAR(5)) + '@example.com', '1.png',
                    (SELECT TOP 1 id FROM ChucVu ORDER BY NEWID()), (SELECT TOP 1 id FROM TuyenDung ORDER BY NEWID()), N'Đang xét duyệt', 0);
            SET @uv_i = @uv_i + 1;
        END
    END

    -- Commit transaction after all seed operations
    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRAN;

    DECLARE @ErrNum INT = ERROR_NUMBER();
    DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Seed failed: %d - %s', 16, 1, @ErrNum, @ErrMsg);
END CATCH;
GO

-- =========================
-- Stored procedures (unchanged)
-- =========================

CREATE PROCEDURE sp_XuatDanhGiaNhanVien
    @Thang INT = NULL,
    @Nam INT = NULL,
    @IdPhongBan INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        dg.id,
        dg.DiemSo,
        dg.NhanXet,
        dg.ngayTao,
        nv.id AS idNhanVien,
        nv.TenNhanVien,
        nv.idPhongBan,
        pb.TenPhongBan,
        dg.idNguoiDanhGia,
        nvDG.TenNhanVien AS TenNguoiDanhGia
    FROM DanhGiaNhanVien dg
    INNER JOIN NhanVien nv ON dg.idNhanVien = nv.id
    LEFT JOIN PhongBan pb ON nv.idPhongBan = pb.id
    LEFT JOIN NhanVien nvDG ON dg.idNguoiDanhGia = nvDG.id
    WHERE ( @Thang IS NULL OR MONTH(dg.ngayTao) = @Thang )
      AND ( @Nam IS NULL OR YEAR(dg.ngayTao) = @Nam )
      AND ( @IdPhongBan IS NULL OR nv.idPhongBan = @IdPhongBan )
    ORDER BY nv.TenNhanVien, dg.ngayTao;
END;
GO

CREATE OR ALTER PROCEDURE sp_XuatNhanVien
    @IdPhongBan INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        nv.id AS idNhanVien,
        nv.TenNhanVien,
        nv.NgaySinh,
        nv.DiaChi,
        nv.Que,
        nv.GioiTinh,
        nv.Email,
        nv.idChucVu,
        cv.TenChucVu,
        nv.idPhongBan,
        pb.TenPhongBan
    FROM NhanVien nv
    LEFT JOIN ChucVu cv ON nv.idChucVu = cv.id
    LEFT JOIN PhongBan pb ON nv.idPhongBan = pb.id
    WHERE nv.DaXoa = 0
      AND (@IdPhongBan IS NULL OR nv.idPhongBan = @IdPhongBan)
    ORDER BY pb.TenPhongBan, nv.TenNhanVien;
END;
GO

CREATE OR ALTER PROCEDURE sp_ChamCong
    @Thang INT = NULL,       -- tháng (1-12). Nếu NULL mặc định là tháng hiện tại
    @Nam   INT = NULL,       -- năm. Nếu NULL mặc định là năm hiện tại
    @IdPhongBan INT = -1     -- nếu = -1 => tất cả phòng ban; nếu >=1 => lọc theo idPhongBan
AS
BEGIN
    SET NOCOUNT ON;

    -- Mặc định nếu không truyền
    IF @Thang IS NULL SET @Thang = MONTH(GETDATE());
    IF @Nam   IS NULL SET @Nam   = YEAR(GETDATE());

    -- Validate đơn giản
    IF @Thang < 1 OR @Thang > 12
    BEGIN
        RAISERROR('Tham số @Thang phải nằm trong [1,12].', 16, 1);
        RETURN;
    END

    IF @Nam < 1900 OR @Nam > 9999
    BEGIN
        RAISERROR('Tham số @Nam không hợp lệ.', 16, 1);
        RETURN;
    END

    IF @IdPhongBan < -1
    BEGIN
        RAISERROR('Tham số @IdPhongBan không hợp lệ. Sử dụng -1 cho tất cả hoặc giá trị >= 1 cho phòng cụ thể.', 16, 1);
        RETURN;
    END

    -- Xác định khoảng ngày cho "tháng" yêu cầu
    DECLARE @StartDate DATE = DATEFROMPARTS(@Nam, @Thang, 1);
    DECLARE @EndDate   DATE = DATEADD(MONTH, 1, @StartDate);

    -- Trả về: Ngày chấm công, Giờ vào, Giờ ra, Id nhân viên, Tên nhân viên, Ngày sinh, IdPhòngBan, Tên phòng ban
    SELECT
        c.NgayChamCong       AS NgayChamCong,
        c.GioVao             AS GioVao,
        c.GioRa              AS GioRa,
        nv.id                AS IdNhanVien,
        nv.TenNhanVien       AS TenNhanVien,
        nv.NgaySinh          AS NgaySinh,
        nv.idPhongBan        AS IdPhongBan,
        pb.TenPhongBan       AS TenPhongBan
    FROM ChamCong c
    INNER JOIN NhanVien nv ON c.idNhanVien = nv.id
    LEFT JOIN PhongBan pb ON nv.idPhongBan = pb.id
    WHERE c.NgayChamCong >= @StartDate
      AND c.NgayChamCong <  @EndDate
      AND ( @IdPhongBan = -1 OR nv.idPhongBan = @IdPhongBan )
    ORDER BY c.NgayChamCong, pb.TenPhongBan, nv.TenNhanVien;
END;
go
--EXEC sp_ChamCong @Thang = 10, @Nam = 2025, @IdPhongBan = 1;


CREATE OR ALTER PROCEDURE sp_ChamCongCaNhan
    @IdNhanVien VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate input
    IF @IdNhanVien IS NULL OR LTRIM(RTRIM(@IdNhanVien)) = ''
    BEGIN
        RAISERROR('Tham số @IdNhanVien không được null hoặc rỗng.', 16, 1);
        RETURN;
    END

    -- Xác định khoảng thời gian cho "tháng hiện tại"
    DECLARE @StartDate DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
    DECLARE @EndDate   DATE = DATEADD(MONTH, 1, @StartDate);

    /*
      Trả về:
        - TenNhanVien   : Tên nhân viên
        - IdNhanVien    : Mã nhân viên
        - TenChucVu     : Tên chức vụ
        - TenPhongBan   : Tên phòng ban
        - NgayChamCong  : Ngày chấm công (null nếu chưa có bản ghi trong tháng)
        - GioVao        : Giờ vào (null nếu chưa có)
        - GioRa         : Giờ ra (null nếu chưa có)
      Sử dụng LEFT JOIN trên ChamCong với điều kiện ngày để luôn trả về
      thông tin nhân viên ngay cả khi chưa có bản ghi chấm công trong tháng.
    */
    SELECT
        nv.TenNhanVien    AS TenNhanVien,
        nv.id             AS IdNhanVien,
        cv.TenChucVu      AS TenChucVu,
        pb.TenPhongBan    AS TenPhongBan,
        cc.NgayChamCong   AS NgayChamCong,
        cc.GioVao         AS GioVao,
        cc.GioRa          AS GioRa
    FROM NhanVien nv
    LEFT JOIN ChucVu cv ON nv.idChucVu = cv.id
    LEFT JOIN PhongBan pb ON nv.idPhongBan = pb.id
    LEFT JOIN ChamCong cc
        ON cc.idNhanVien = nv.id
        AND cc.NgayChamCong >= @StartDate
        AND cc.NgayChamCong <  @EndDate
    WHERE nv.id = @IdNhanVien
    ORDER BY cc.NgayChamCong;
END;
go


--USE master;
--ALTER DATABASE PersonnelManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE PersonnelManagement;