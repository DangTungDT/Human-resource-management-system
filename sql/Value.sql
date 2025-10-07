use PersonnelManagement
go

--Set format date = dd/mm/yyyy
set dateformat dmy
go

insert into PhongBan(TenPhongBan, Mota) values
(N'Admin',N'Quản lý toàn bộ hệ thống.'),
(N'Giám đốc',N'Phòng ban có quyền quản lý và điều hành cao nhất.'),
(N'Công nghệ thông tin',N'Phòng ban sử lý các vấn đề về công nghệ, làm các sản phẩm về công nghệ như website,...'),
(N'Marketing',N'Phòng Marketing là bộ phận nghiên cứu thị trường, xây dựng thương hiệu và hỗ trợ bán hàng, tạo ra nhu cầu để bộ phận kinh doanh dễ dàng bán được sản phẩm/dịch vụ.'),
(N'Kinh doanh',N'Phòng Kinh doanh là nơi chốt đơn, mang doanh thu về cho công ty, là bộ phận trực tiếp tạo ra lợi nhuận.'),
(N'Nhân sự',N'Phòng ban chịu trách nhiệm và thực hiện các chức năng quản lý nhân sự trong công ty')
go

insert into ChucVu(TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan) values
(N'Giám đốc', 5000000, 10, N'Là người điều hành và quản lý cao nhất của công ty, có vai trò định hướng đi cho công ty', 2),
(N'Trưởng phòng nhân sự', 20000000, 0, N'Là người đứng đầu của phòng ban nhân sự',6),
(N'Nhân viên nhân sự', 10000000, 0, N'Là người của phòng ban nhân sự',6),
(N'Trưởng phòng kinh doanh', 20000000, 5, N'Là người đứng đầu của phòng ban nhân sự',5),
(N'Nhân viên kinh doanh', 10000000, 3, N'Là nhân viên của phòng ban nhân sự',5),
(N'Trưởng phòng marketing', 20000000, 0, N'Là người đứng đầu của phòng ban nhân sự',4),
(N'Nhân viên marketing', 10000000, 0, N'Là Nhân viên của phòng ban nhân sự',4),
(N'Trưởng phòng công nghệ thông tin', 20000000, 0, N'Là người đứng đầu của phòng ban nhân sự',3),
(N'Nhân viên công nghệ thông tin', 10000000, 0, N'Là nhân viên của phòng ban nhân sự',3)
go

insert into NhanVien(id, TenNhanVien, NgaySinh, DiaChi, Que, GioiTinh, Email, idChucVu, idPhongBan) values
('GD00000001', N'Nguyễn Thanh Hiếu', '10/10/1990', N'Thành phố Hồ Chí Minh', N'Huế', N'Nam', 'hieunt@gmail.com', 1, 2),
('TPNS000001', N'Nguyễn Hải Phong', '10/10/1990', N'TP. Hồ Chí Minh', N'Vũng Tàu', N'Nam', 'phongnh@gmail.com', 2, 6),
('NVNS000001', N'Nguyễn Văn A', '20/05/1991', N'TP. Hồ Chí Minh', N'Bến Tre', N'Nam', 'anv@gmail.com', 3, 6),
('NVNS000002', N'Nguyễn Văn B', '15/07/1992', N'TP. Hồ Chí Minh', N'Vũng Tàu', N'Nam', 'bvn@gmail.com', 3, 6),
('NVNS000003', N'Nguyễn Thanh C', '12/09/1993', N'TP. Hồ Chí Minh', N'Nghệ An', N'Nữ', 'cnt@gmail.com', 3, 6),
('NVNS000004', N'Nguyễn Thanh D', '18/11/1994', N'TP. Hồ Chí Minh', N'Hà Nội', N'Nữ', 'dnt@gmail.com', 3, 6),
('NVNS000005', N'Nguyễn Thị T', '22/02/1995', N'TP. Hồ Chí Minh', N'Gia Lai', N'Nữ', 'tnt@gmail.com', 3, 6),
('NVNS000006', N'Lê Văn E', '10/03/1990', N'TP. Hồ Chí Minh', N'Đà Nẵng', N'Nam', 'levne@gmail.com', 3, 6),
('NVNS000007', N'Lê Văn F', '25/06/1991', N'TP. Hồ Chí Minh', N'Bình Định', N'Nam', 'levnf@gmail.com', 3, 6),
('NVNS000008', N'Lê Thị G', '14/08/1992', N'TP. Hồ Chí Minh', N'Quảng Nam', N'Nữ', 'lethg@gmail.com', 3, 6),
('NVNS000009', N'Phạm Văn H', '30/12/1993', N'TP. Hồ Chí Minh', N'Thanh Hóa', N'Nam', 'phamvh@gmail.com', 3, 6),
('NVNS000010', N'Phạm Thị I', '05/04/1994', N'TP. Hồ Chí Minh', N'Nghệ An', N'Nữ', 'phamti@gmail.com', 3, 6),
('TPKD000001', N'Trần Quốc Khánh', '12/03/1989', N'TP. Hồ Chí Minh', N'Hà Nội', N'Nam', 'khanhtq@gmail.com', 4, 5),
('NVKD000001', N'Nguyễn Thị Hoa', '21/07/1992', N'TP. Hồ Chí Minh', N'Đồng Nai', N'Nữ', 'hoant@gmail.com', 5, 5),
('NVKD000002', N'Lê Văn Minh', '11/04/1993', N'TP. Hồ Chí Minh', N'Long An', N'Nam', 'minhlv@gmail.com', 5, 5),
('NVKD000003', N'Phạm Thị Mai', '30/09/1990', N'TP. Hồ Chí Minh', N'Tiền Giang', N'Nữ', 'maipt@gmail.com', 5, 5),
('NVKD000004', N'Hoàng Anh Tuấn', '05/12/1995', N'TP. Hồ Chí Minh', N'Đà Nẵng', N'Nam', 'tuanha@gmail.com', 5, 5),
('NVKD000005', N'Đỗ Văn Cường', '18/08/1991', N'TP. Hồ Chí Minh', N'Hải Phòng', N'Nam', 'cuongdv@gmail.com', 5, 5),
('NVKD000006', N'Nguyễn Thị Hạnh', '25/01/1996', N'TP. Hồ Chí Minh', N'Nghệ An', N'Nữ', 'hanhnt@gmail.com', 5, 5),
('NVKD000007', N'Trần Văn Phát', '15/11/1994', N'TP. Hồ Chí Minh', N'Bến Tre', N'Nam', 'phattv@gmail.com', 5, 5),
('NVKD000008', N'Võ Thị Lan', '10/06/1992', N'TP. Hồ Chí Minh', N'Bình Thuận', N'Nữ', 'lanvt@gmail.com', 5, 5),
('NVKD000009', N'Nguyễn Văn Tùng', '02/02/1990', N'TP. Hồ Chí Minh', N'Hà Tĩnh', N'Nam', 'tungnv@gmail.com', 5, 5),
('NVKD000010', N'Nguyễn Thị Nhung', '09/03/1993', N'TP. Hồ Chí Minh', N'Quảng Ninh', N'Nữ', 'nhungnt@gmail.com', 5, 5),
('TPMKT00001', N'Nguyễn Văn Khoa', '15/02/1988', N'TP. Hồ Chí Minh', N'Huế', N'Nam', 'khoanv@gmail.com', 6, 4),
('NVMKT00001', N'Nguyễn Thị Huyền', '11/05/1992', N'TP. Hồ Chí Minh', N'Thanh Hóa', N'Nữ', 'huyennv@gmail.com', 7, 4),
('NVMKT00002', N'Phạm Văn Lâm', '20/06/1991', N'TP. Hồ Chí Minh', N'Hà Nam', N'Nam', 'lampv@gmail.com', 7, 4),
('NVMKT00003', N'Hoàng Thị Thúy', '09/10/1994', N'TP. Hồ Chí Minh', N'Quảng Ngãi', N'Nữ', 'thuyht@gmail.com', 7, 4),
('NVMKT00004', N'Nguyễn Văn Đạt', '19/07/1993', N'TP. Hồ Chí Minh', N'Hà Tĩnh', N'Nam', 'datnv@gmail.com', 7, 4),
('NVMKT00005', N'Trần Thị Mỹ', '25/11/1995', N'TP. Hồ Chí Minh', N'Quảng Nam', N'Nữ', 'mytt@gmail.com', 7, 4),
('NVMKT00006', N'Lê Thị Hòa', '22/04/1996', N'TP. Hồ Chí Minh', N'Hà Nội', N'Nữ', 'hoalt@gmail.com', 7, 4),
('NVMKT00007', N'Võ Văn Bình', '16/08/1992', N'TP. Hồ Chí Minh', N'Nghệ An', N'Nam', 'binhvv@gmail.com', 7, 4),
('NVMKT00008', N'Nguyễn Thị Nga', '03/12/1990', N'TP. Hồ Chí Minh', N'Hà Giang', N'Nữ', 'ngant@gmail.com', 7, 4),
('NVMKT00009', N'Phạm Văn Hùng', '28/03/1993', N'TP. Hồ Chí Minh', N'Hòa Bình', N'Nam', 'hungpv@gmail.com', 7, 4),
('NVMKT00010', N'Trần Thị Vân', '14/09/1994', N'TP. Hồ Chí Minh', N'Lào Cai', N'Nữ', 'vantr@gmail.com', 7, 4),
('TPCNTT0001', N'Nguyễn Văn Long', '20/01/1987', N'TP. Hồ Chí Minh', N'Hà Nội', N'Nam', 'longnv@gmail.com', 8, 3),
('NVCNTT0001', N'Trần Văn Sơn', '10/02/1991', N'TP. Hồ Chí Minh', N'Hải Phòng', N'Nam', 'sontr@gmail.com', 9, 3),
('NVCNTT0002', N'Nguyễn Văn Quân', '18/04/1992', N'TP. Hồ Chí Minh', N'Nghệ An', N'Nam', 'quannv@gmail.com', 9, 3),
('NVCNTT0003', N'Lê Thị Mai', '12/07/1994', N'TP. Hồ Chí Minh', N'Hà Tĩnh', N'Nữ', 'maile@gmail.com', 9, 3),
('NVCNTT0004', N'Phạm Văn Thắng', '09/08/1990', N'TP. Hồ Chí Minh', N'Hà Nội', N'Nam', 'thangpv@gmail.com', 9, 3),
('NVCNTT0005', N'Nguyễn Văn Duy', '23/05/1993', N'TP. Hồ Chí Minh', N'Thanh Hóa', N'Nam', 'duynv@gmail.com', 9, 3),
('NVCNTT0006', N'Hoàng Thị Lan', '30/11/1995', N'TP. Hồ Chí Minh', N'Hải Dương', N'Nữ', 'lanht@gmail.com', 9, 3),
('NVCNTT0007', N'Võ Văn Khải', '15/06/1996', N'TP. Hồ Chí Minh', N'Bình Định', N'Nam', 'khaivv@gmail.com', 9, 3),
('NVCNTT0008', N'Trần Thị Oanh', '25/03/1992', N'TP. Hồ Chí Minh', N'Hà Nam', N'Nữ', 'oanhtr@gmail.com', 9, 3),
('NVCNTT0009', N'Nguyễn Văn Hào', '08/12/1991', N'TP. Hồ Chí Minh', N'Bắc Giang', N'Nam', 'haonv@gmail.com', 9, 3),
('NVCNTT0010', N'Nguyễn Thị Yến', '01/09/1994', N'TP. Hồ Chí Minh', N'Nam Định', N'Nữ', 'yennv@gmail.com', 9, 3)
go

insert into TaiKhoan(taiKhoan, idNhanVien, matKhau)
select
-- Lấy chữ cái đầu của mỗi từ trong tên nhân viên (không dấu)
    upper(
        left(parsename(replace(TenNhanVien, ' ', '.'), 3), 1) +
        isnull(left(parsename(replace(TenNhanVien, ' ', '.'), 2), 1), '') +
        isnull(left(parsename(replace(TenNhanVien, ' ', '.'), 1), 1), '')
    )
    + '_' +
-- Viết tắt phòng ban
    case pb.TenPhongBan
        when N'Công nghệ thông tin' then 'CNTT'
        when N'Nhân sự' then 'NS'
        when N'Kinh doanh' then 'KD'
        when N'Marketing' then 'MKT'
        when N'Giám đốc' then 'GD'
        when N'Admin' then 'AD'
        else 'KHAC'
    end
    + '_' +
-- Vị trí + số thứ tự
    case 
        when cv.TenChucVu like N'Trưởng phòng%' then 'TP'
        when cv.TenChucVu like N'Giám đốc%' then 'GD'
        else 'NV'
    end +
    right('00' + cast(row_number() over (
        partition by cv.TenChucVu, pb.TenPhongBan
        order by nv.id
    ) as varchar), 2),

    nv.id,
    '6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b' as matKhau
from NhanVien nv
join ChucVu cv on nv.idChucVu = cv.id
join PhongBan pb on nv.idPhongBan = pb.id
go

--------------------------------------------------------------------------------
-- Bảng KyLuong (tạo kỳ lương tháng 9/2025)
--------------------------------------------------------------------------------
insert into KyLuong(ngayBatDau, ngayKetThuc, ngayChiTra, trangThai) values
('01/09/2025', '30/09/2025', '05/10/2025', N'Đã trả');
go

--------------------------------------------------------------------------------
-- Bảng HopDongLaoDong (mỗi nhân viên 1 hợp đồng)
--------------------------------------------------------------------------------
insert into HopDongLaoDong(LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, idNhanVien, MoTa)
select
    case when id like 'GD%' or id like 'TP%' then N'Hợp đồng lao động không xác định thời hạn'
         else N'Hợp đồng lao động xác định thời hạn' end,
    '01/01/2024', '01/01/2024',
    case when id like 'GD%' or id like 'TP%' then null else '31/12/2026' end,
    id,
    N'Hợp đồng chính thức'
from NhanVien;
go

--------------------------------------------------------------------------------
-- Bảng ChamCong (mỗi nhân viên 2 bản ghi tháng 9/2025)
--------------------------------------------------------------------------------
insert into ChamCong(NgayChamCong, idNhanVien)
select '01/09/2025', id from NhanVien
union all
select '02/09/2025', id from NhanVien;
go

--------------------------------------------------------------------------------
-- Bảng PhuCap (2 loại phụ cấp)
--------------------------------------------------------------------------------
insert into PhuCap(soTien, lyDoPhuCap) values
(500000, N'Phụ cấp xăng xe'),
(1000000, N'Phụ cấp ăn trưa');
go

--------------------------------------------------------------------------------
-- Bảng NhanVien_PhuCap (mỗi NV nhận cả 2 phụ cấp)
--------------------------------------------------------------------------------
insert into NhanVien_PhuCap(idNhanVien, idPhuCap)
select id, 1 from NhanVien
union all
select id, 2 from NhanVien;
go

--------------------------------------------------------------------------------
-- Bảng KhauTru (1 loại khấu trừ bảo hiểm, tạo bởi Giám đốc)
--------------------------------------------------------------------------------
insert into KhauTru(id, loaiKhauTru, soTien, moTa, idNguoiTao) values
(1, N'Bảo hiểm xã hội', 1000000, N'Khấu trừ BHXH hàng tháng', 'GD00000001');
go

--------------------------------------------------------------------------------
-- Bảng NhanVien_KhauTru (tất cả NV đều áp dụng khấu trừ tháng 9/2025)
--------------------------------------------------------------------------------
insert into NhanVien_KhauTru(idNhanVien, idKhauTru, thangApDung)
select id, 1, '01/09/2025' from NhanVien;
go

--------------------------------------------------------------------------------
-- Bảng ThuongPhat (1 thưởng, 1 phạt do giám đốc tạo)
--------------------------------------------------------------------------------
insert into ThuongPhat(tienThuongPhat, loai, lyDo, idNguoiTao) values
(2000000, N'Thưởng', N'Thưởng năng suất tháng 9/2025', 'GD00000001'),
(500000, N'Phạt', N'Đi trễ nhiều lần', 'GD00000001');
go

--------------------------------------------------------------------------------
-- Bảng NhanVien_ThuongPhat (áp dụng ngẫu nhiên cho vài NV)
--------------------------------------------------------------------------------
insert into NhanVien_ThuongPhat(idNhanVien, idThuongPhat, thangApDung) values
('NVNS000001', 1, '01/09/2025'),
('NVKD000002', 1, '01/09/2025'),
('NVCNTT0003', 2, '01/09/2025');
go

--------------------------------------------------------------------------------
-- Bảng ChiTietLuong (tạo lương tháng 9/2025 cho toàn bộ nhân viên)
--------------------------------------------------------------------------------
insert into ChiTietLuong(luongTruocKhauTru, luongSauKhauTru, tongKhauTru, tongPhuCap, tongKhenThuong, tongTienPhat, idNhanVien, idKyLuong)
select 
    cv.luongCoBan,
    cv.luongCoBan - 1000000 + 1500000,  -- lương sau khấu trừ + phụ cấp
    1000000,                            -- BHXH
    1500000,                            -- phụ cấp
    0,                                  -- mặc định chưa thưởng
    0,                                  -- mặc định chưa phạt
    nv.id,
    1
from NhanVien nv
join ChucVu cv on nv.idChucVu = cv.id;
go

--------------------------------------------------------------------------------
-- Bảng DanhGiaNhanVien (mỗi nhân viên được giám đốc đánh giá 1 lần)
--------------------------------------------------------------------------------
insert into DanhGiaNhanVien(DiemSo, NhanXet, ngayTao, idNhanVien, idNguoiDanhGia)
select 8, N'Hoàn thành công việc tốt', '30/09/2025', id, 'GD00000001'
from NhanVien
where id <> 'GD00000001';
go

--------------------------------------------------------------------------------
-- Bảng NghiPhep (một vài nhân viên xin nghỉ phép)
--------------------------------------------------------------------------------
insert into NghiPhep(NgayBatDau, NgayKetThuc, LyDoNghi, LoaiNghiPhep, idNhanVien)
values
('10/09/2025','12/09/2025',N'Nghỉ phép về quê',N'Nghỉ phép có lương','NVNS000002'),
('20/09/2025','22/09/2025',N'Bệnh cảm cúm',N'Nghỉ phép không lương','NVKD000004');
go

--------------------------------------------------------------------------------
-- Bảng TuyenDung (giám đốc nhân sự tạo 1 tin tuyển dụng)
--------------------------------------------------------------------------------
insert into TuyenDung(tieuDe, idPhongBan, idChucVu, idNguoiTao)
values
(N'Tuyển dụng nhân viên CNTT', 3, 9, 'TPNS000001');
go

--------------------------------------------------------------------------------
-- Bảng UngVien (1 ứng viên nộp CV)
--------------------------------------------------------------------------------
insert into UngVien(hoTen, email, soDienThoai, duongDanCV, idChucVuUngTuyen)
values
(N'Lê Thị Thu', 'thult@gmail.com','0909123456','/cv/thult.pdf',9);
go
