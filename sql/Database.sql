----Database đồ án quản lý nhân sự
--Create Database
create database PersonnelManagement
go

use PersonnelManagement
go

--Set format date = dd/mm/yyyy
set dateformat dmy
go

create table ChucVu
(
	id int identity(1,1) not null,
	TenChucVu nvarchar(255) not null,
	luongCoBan decimal(18,2) not null,
	tyLeHoaHong decimal(5,2) default 0,
	moTa nvarchar(255),
	idPhongBan int not null,
	primary key(id)
)
go

create table PhongBan
(
	id int identity(1,1) not null,
	TenPhongBan nvarchar(255) not null,
	Mota nvarchar(255),
	primary key(id)
)
go

create table NhanVien
(
	id varchar(10) not null,
	TenNhanVien nvarchar(255) not null,
	NgaySinh date not null,
	DiaChi nvarchar(255) not null,
	Que nvarchar(100) not null,
	GioiTinh nvarchar(4) not null,
	Email varchar(100) not null unique,
	AnhDaiDien varchar(255),
	idChucVu int not null,
	idPhongBan int not null,
	DaXoa bit default 0 not null,
	constraint chk_NgaySinhNV check (NgaySinh <= dateadd(year, -16, getdate())),
	primary key(id)
)
go

create table TaiKhoan
(
	id int identity(1,1) not null,
	taiKhoan varchar(50) unique not null,
	idNhanVien varchar(10) not null, 
	matKhau varchar(255) not null,
	primary key(id)
)
go
CREATE TABLE QuenMatKhau
(
    id int identity(1,1) not null,
    taiKhoan varchar(50) not null,
    otp varchar(6) not null,
    thoiGianHetHan datetime not null,
    daXacNhan bit default 0,
    primary key(id),
    FOREIGN KEY (taiKhoan) REFERENCES TaiKhoan(taiKhoan)
)
GO

create table ChamCong
(
	id int identity(1,1) not null,
	NgayChamCong date not null,
	GioRa time,
	GioVao time,
	idNhanVien varchar(10) not null,
	primary key(id)
)
go

create table HopDongLaoDong
(
	id int identity(1,1) not null,
	LoaiHopDong nvarchar(100) not null,
	NgayKy date not null,
	NgayBatDau date not null,
	NgayKetThuc date,
	Luong decimal(18,2) not null,
	HinhAnh varchar(255),
	idNhanVien varchar(10),
	MoTa nvarchar(255),
	primary key(id),
	constraint chk_NgayKetThuc check (NgayBatDau <= NgayKetThuc)
)
go

create table NghiPhep
(
	id int identity(1,1) not null,
	NgayBatDau date not null,
	NgayKetThuc date not null,
	LyDoNghi nvarchar(500) not null,
	LoaiNghiPhep nvarchar(50) check(LoaiNghiPhep in(N'Có phép', N'Không phép')) not null,
	idNhanVien varchar(10) not null,
	TrangThai nvarchar(50) not null,
	primary key(id),
	constraint chk_NgayBatDau check(NgayBatDau<= ngayKetThuc),
)
go

Create table DanhGiaNhanVien
(
	id int identity(1,1) not null,
	DiemSo int not null,
	NhanXet ntext,
	ngayTao date not null,
	idNhanVien varchar(10) not null,
	idNguoiDanhGia varchar(10) not null,
	primary key(id),
	constraint chk_NguoiDanhGia check (idNhanVien != idNguoiDanhGia),
	constraint chk_DiemSo check (DiemSo between 1 and 10)
)
go

Create table PhuCap
(
	id int identity(1,1) not null,
	soTien decimal(18,2) default 0 not null,
	lyDoPhuCap nvarchar(100) not null,
	primary key(id)
)

Create table ChiTietLuong
(
	id int identity(1,1) not null,
	ngayNhanLuong date not null default getdate(),
	luongTruocKhauTru decimal(18,2) not null,
	luongSauKhauTru decimal(18,2) not null,
	tongKhauTru decimal(18,2) not null,
	tongPhuCap decimal(18,2) not null,
	tongKhenThuong decimal(18,2) default 0 not null,
	tongTienPhat decimal(18,2) default 0 not null,
	trangThai nvarchar(20) default N'Chưa giải quyết' not null,
	ghiChu nvarchar(255),
	idNhanVien varchar(10) not null,
	idKyLuong int not null,
	capNhatLuong bit default 0 not null,
	primary key(id)
)
go

CREATE TABLE TuyenDung (
    id INT PRIMARY KEY IDENTITY(1,1),
    tieuDe NVARCHAR(150) NOT NULL,
    idPhongBan int NOT NULL,
    idChucVu int NOT NULL,
    idNguoiTao varchar(10) NOT NULL, -- Trưởng phòng nhân sự
    trangThai NVARCHAR(50) DEFAULT N'Đang tuyển', 
    ngayTao DATETIME DEFAULT GETDATE(),
	ghiChu nvarchar(50),
	xacThucYeuCau nvarchar(50),
	soLuong int not null default 1
)
go

CREATE TABLE UngVien (
    id INT PRIMARY KEY IDENTITY(1,1),
    tenNhanVien nvarchar(255) not null,
	ngaySinh date not null,
	diaChi nvarchar(255) not null,
	que nvarchar(100) not null,
	gioiTinh nvarchar(4) not null,
	email varchar(100) not null unique,
    duongDanCV NVARCHAR(255), -- Lưu file CV của ứng viên
    idChucVuUngTuyen int NOT NULL,
	idTuyenDung int not null,
    ngayUngTuyen DATE DEFAULT GETDATE(),
    trangThai NVARCHAR(50) DEFAULT N'Đang xét duyệt',
	daXoa bit not null default 0
)
go
create table NhanVien_PhuCap
(
	idNhanVien varchar(10) not null,
	idPhuCap int not null,
	trangThai nvarchar(20) not null default N'Hoạt động',
	constraint check_TrangThai check(trangThai in (N'Hoạt động', N'Ngưng hoạt động')),
	primary key(idNhanVien, idPhuCap)
)
go

Create table KhauTru
(
	id int identity(1,1) not null,
	loaiKhauTru nvarchar(50) not null,
	soTien decimal(18,2) default 0 not null,
	moTa nvarchar(255),
	idNguoiTao varchar(10) not null,
	primary key(id)
)
go

Create table NhanVien_KhauTru
(
	id int identity(1,1) not null,
	idNhanVien varchar(10),
	idKhauTru int,
	thangApDung date,
	primary key(id)
)

create table ThuongPhat
(
	id int identity not null,
	tienThuongPhat decimal(18,2) default 0 not null,
	loai nvarchar(20) default N'Thưởng',
	lyDo nvarchar(255) not null,
	idNguoiTao varchar(10) not null,
	constraint check_loai_ThuongPhat check(loai in(N'Thưởng', N'Phạt', N'Kỷ luật')),
	primary key(id)
)
go

Create table NhanVien_ThuongPhat
(
	id int identity(1,1) not null,
	idNhanVien varchar(10) not null,
	idThuongPhat int not null,
	thangApDung date,
	primary key(id)
)
go

Create table KyLuong
(
	id int identity(1,1) not null,
	ngayBatDau date, --Ví dụ 1/9/2025
	ngayKetThuc date, --Ví dụ 31/9/2025
	ngayChiTra date,
	trangThai nvarchar(20) default N'Chưa giải quyết' not null,
	constraint check_TT_KyLuong check(trangThai in(N'Chưa giải quyết', N'Đang giải quyết', N'Đã trả')),
	primary key(id)
)
go

alter table NghiPhep
add TrangThai nvarchar(50);
go
alter table NghiPhep
add constraint df_NghiPhep_TrangThai default N'Đang yêu cầu' for TrangThai;
go

----Create Foreign Key constraint

alter table ThuongPhat
add constraint fk_NV_ThuongPhat foreign key(idNguoiTao) references NhanVien(id)
go

alter table ChucVu
add constraint fk_PB_ChucVu foreign key(idPhongBan) references PhongBan(id)
go

alter table KhauTru
add constraint fk_NV_KhauTru foreign key(idNguoiTao) references NhanVien(id)
go
--Table NhanVien_KhauTru
alter Table NhanVien_ThuongPhat
add constraint fk_NV_NVTP foreign key(idNhanVien) references NhanVien(id),
    constraint fk_TP_NVTP foreign key(idThuongPhat) references ThuongPhat(id)
go
--Table ChiTietLuong
alter table ChiTietLuong
add constraint fk_NhanVien_ChiTietLuong foreign key(idNhanVien) references NhanVien(id),
	constraint fk_KyLuong_ChiTietLuong foreign key(idKyLuong) references KyLuong(id)
go

--Table NhanVien_PhuCap
alter table NhanVien_PhuCap
add constraint fk_NhanVien_NVPC foreign key(idNhanVien) references NhanVien(id),
    constraint fk_PhuCap_NVPC foreign key(idPhuCap) references PhuCap(id)
go
--Table NhanVien
alter table NhanVien
add constraint fk_ChucVu_NhanVien foreign key(idChucVu) references ChucVu(id),
	constraint	fk_PhongBan_NhanVien foreign key(idPhongBan) references PhongBan(id)
go

--Table ChamCong
alter table ChamCong
add constraint fk_NhanVien_ChamCong foreign key(idNhanVien) references NhanVien(id)
go

--Table TaiKhoan
alter table TaiKhoan
add constraint fk_NhanVien_TaiKhoan foreign key(idNhanVien) references NhanVien(id)
go

--Table NghiPhep
alter table NghiPhep
add constraint fk_NhanVien_NghiPhep foreign key(idNhanVien) references NhanVien(id)
go

--Table HopDongLaoDong
alter table HopDongLaoDong
add constraint fk_NhanVien_HopDongLaoDong foreign key(idNhanVien) references NhanVien(id)
go


--Table DanhGiaNhanVien
alter table DanhGiaNhanVien
add constraint fk_NV1_DGNV foreign key(idNhanVien) references NhanVien(id),
	constraint fk_NV2_DGNV foreign key(idNguoiDanhGia) references NhanVien(id)
go
alter table UngVien
add CONSTRAINT FK_UngVien_ChucVu FOREIGN KEY (idChucVuUngTuyen) REFERENCES ChucVu(id)
go

alter table TuyenDung
add CONSTRAINT FK_TuyenDung_PhongBan FOREIGN KEY (idPhongBan) REFERENCES PhongBan(id),
    CONSTRAINT FK_TuyenDung_ChucVu FOREIGN KEY (idChucVu) REFERENCES ChucVu(id),
    CONSTRAINT FK_TuyenDung_NhanVien FOREIGN KEY (idNguoiTao) REFERENCES NhanVien(id)
go

alter table NhanVien_KhauTru
add constraint fk_NhanVien_NVKT foreign key(idNhanVien) references NhanVien(id),
constraint fk_KhauTru_NVKT foreign key(idKhauTru) references KhauTru(id)
go

-- Seed data for PersonnelManagement (based on Database.sql)
-- Set dateformat same as schema
use PersonnelManagement
go
set dateformat dmy
go

-- 1) PhongBan (3 rows)
insert into PhongBan (TenPhongBan, Mota) values
(N'Công nghệ thông tin', N'Phòng ban chịu trách nhiệm CNTT'),
(N'Nhân sự', N'Phòng ban phụ trách nhân sự'),
(N'Giám đốc', N'Phòng ban Giám đốc');
go

-- 2) ChucVu (at least 3 rows; ensure idPhongBan references existing PhongBan)
insert into ChucVu (TenChucVu, luongCoBan, tyLeHoaHong, moTa, idPhongBan) values
(N'Nhân viên công nghệ thông tin', 10000000, 0, N'Nhân viên CNTT', 1),
(N'Trưởng phòng công nghệ thông tin', 20000000, 0, N'Trưởng phòng CNTT', 1),
(N'Nhân viên nhân sự', 10000000, 0, N'Nhân viên phòng nhân sự', 2),
(N'Trưởng phòng nhân sự', 20000000, 0, N'Trưởng phòng nhân sự', 2),
(N'Giám đốc', 50000000, 10, N'Người điều hành cao nhất', 3);
go

-- 3) NhanVien (3 rows as requested)
-- IDs follow pattern: position abbrev + dept abbrev + 4-digit seq
insert into NhanVien (id, TenNhanVien, NgaySinh, DiaChi, Que, GioiTinh, Email, AnhDaiDien, idChucVu, idPhongBan, DaXoa) values
('NVCNTT0001', N'Đặng Thanh Tùng', '20/08/2005', N'Thủ Đức', N'Vũng Tàu', N'Nam', 'dangthanhtungdtt@gmail.com', 'tung.png', 1, 1, 0),
('TPCNTT0001', N'Nguyễn Thành Tuấn', '20/08/2005', N'Thủ Đức', N'Gia Lai', N'Nam', 'nguyenthanhtuankrp1@gmail.com', 'tuan.png', 2, 1, 0),
('GDGD0001', N'Lý Thị Thanh Ngân', '20/08/2005', N'Thủ Đức', N'Bến Tre', N'Nữ', '23211tt0154@gmail.tdc.edu.vn', 'ngan.png', 5, 3, 0);
go

-- 4) TaiKhoan (3 rows)
-- taiKhoan format: roleAbbrev + deptAbbrev + shortName + seq, password = sha256('1')
-- SHA-256('1') = 6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b
insert into TaiKhoan (taiKhoan, idNhanVien, matKhau) values
('NVCNTTDTT0001','NVCNTT0001','6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b'),
('TPCNTTTTN0001','TPCNTT0001','6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b'),
('GDGDLTN0001','GDGD0001','6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b');
go

-- 5) QuenMatKhau (3 rows) - sample OTP entries referencing TaiKhoan.taiKhoan
insert into QuenMatKhau (taiKhoan, otp, thoiGianHetHan, daXacNhan) values
('NVCNTTDTT0001', '123456', dateadd(minute, 30, getdate()), 0),
('TPCNTTTTN0001', '234567', dateadd(minute, 30, getdate()), 0),
('GDGDLTN0001', '345678', dateadd(minute, 30, getdate()), 0);
go

-- 6) ChamCong (at least 3 rows) - use multiple dates for the employees
insert into ChamCong (NgayChamCong, GioVao, GioRa, idNhanVien) values
('01/09/2025','08:00','17:00','NVCNTT0001'),
('01/09/2025','08:05','17:10','TPCNTT0001'),
('02/09/2025','08:00','16:50','GDGD0001');
go

-- 7) HopDongLaoDong (3 rows) - use LoaiHopDong free text allowed by Database.sql
insert into HopDongLaoDong (LoaiHopDong, NgayKy, NgayBatDau, NgayKetThuc, Luong, HinhAnh, idNhanVien, MoTa) values
(N'xác định thời hạn', '01/01/2024', '01/01/2024', '31/12/2026', 10000000, NULL, 'NVCNTT0001', N'HĐ NV CNTT'),
(N'không xác định thời hạn', '01/01/2024', '01/01/2024', NULL, 20000000, NULL, 'TPCNTT0001', N'HĐ TP CNTT'),
(N'không xác định thời hạn', '01/01/2024', '01/01/2024', NULL, 50000000, NULL, 'GDGD0001', N'HĐ Giám đốc');
go

-- 8) PhuCap (3 rows)
insert into PhuCap (soTien, lyDoPhuCap) values
(500000, N'Phụ cấp xăng xe'),
(1000000, N'Phụ cấp ăn trưa'),
(300000, N'Phụ cấp điện thoại');
go

-- 9) NhanVien_PhuCap (assign each employee at least one, ensure 3 rows)
insert into NhanVien_PhuCap (idNhanVien, idPhuCap) values
('NVCNTT0001', 1),
('TPCNTT0001', 2),
('GDGD0001', 3);
go

-- 10) KhauTru (3 rows)
insert into KhauTru (loaiKhauTru, soTien, moTa, idNguoiTao) values
(N'Bảo hiểm xã hội', 1000000, N'Khấu trừ BHXH', 'GDGD0001'),
(N'Thuế thu nhập', 500000, N'Khấu trừ thuế TNCN', 'GDGD0001'),
(N'Khấu trừ khác', 200000, N'Khấu trừ nội bộ', 'TPCNTT0001');
go

-- 11) NhanVien_KhauTru (3 rows)
insert into NhanVien_KhauTru (idNhanVien, idKhauTru, thangApDung) values
('NVCNTT0001', 1, '01/09/2025'),
('TPCNTT0001', 1, '01/09/2025'),
('GDGD0001', 2, '01/09/2025');
go

-- 12) ThuongPhat (3 rows)
insert into ThuongPhat (tienThuongPhat, loai, lyDo, idNguoiTao) values
(2000000, N'Thưởng', N'Thưởng năng suất', 'GDGD0001'),
(500000, N'Phạt', N'Đi trễ', 'GDGD0001'),
(1000000, N'Thưởng', N'Hoàn thành dự án', 'TPCNTT0001');
go

-- 13) NhanVien_ThuongPhat (3 rows)
insert into NhanVien_ThuongPhat (idNhanVien, idThuongPhat, thangApDung) values
('NVCNTT0001', 1, '01/09/2025'),
('TPCNTT0001', 3, '01/09/2025'),
('GDGD0001', 2, '01/09/2025');
go

-- 14) KyLuong (3 rows)
insert into KyLuong (ngayBatDau, ngayKetThuc, ngayChiTra, trangThai) values
('01/09/2025','30/09/2025','05/10/2025', N'Đã trả'),
('01/10/2025','31/10/2025','05/11/2025', N'Chưa giải quyết'),
('01/11/2025','30/11/2025','05/12/2025', N'Chưa giải quyết');
go

-- 15) ChiTietLuong (3 rows) - reference KyLuong.id and NhanVien.id
insert into ChiTietLuong (ngayNhanLuong, luongTruocKhauTru, luongSauKhauTru, tongKhauTru, tongPhuCap, tongKhenThuong, tongTienPhat, trangThai, ghiChu, idNhanVien, idKyLuong) values
('05/10/2025', 10000000, 9000000, 1000000, 500000, 0, 0, N'Đã giải quyết', N'Lương tháng 9', 'NVCNTT0001', 1),
('05/10/2025', 20000000, 19000000, 1000000, 1000000, 1000000, 0, N'Đã giải quyết', N'Lương TP CNTT', 'TPCNTT0001', 1),
('05/10/2025', 50000000, 49000000, 1000000, 300000, 2000000, 500000, N'Đã giải quyết', N'Lương Giám đốc', 'GDGD0001', 1);
go

-- 16) DanhGiaNhanVien (3 rows)
insert into DanhGiaNhanVien (DiemSo, NhanXet, ngayTao, idNhanVien, idNguoiDanhGia) values
(8, N'Hoàn thành tốt nhiệm vụ', '30/09/2025', 'NVCNTT0001', 'GDGD0001'),
(9, N'Lãnh đạo tốt', '30/09/2025', 'TPCNTT0001', 'GDGD0001'),
(7, N'Cần cải thiện giao tiếp', '30/09/2025', 'GDGD0001', 'TPCNTT0001');
go

-- 17) NghiPhep (3 rows)
insert into NghiPhep (NgayBatDau, NgayKetThuc, LyDoNghi, LoaiNghiPhep, idNhanVien, TrangThai) values
('10/09/2025','12/09/2025', N'Nghỉ phép cá nhân', N'Có phép', 'NVCNTT0001', N'Đã duyệt'),
('20/09/2025','22/09/2025', N'Bệnh', N'Không phép', 'TPCNTT0001', N'Đang yêu cầu'),
('25/09/2025','25/09/2025', N'Việc riêng', N'Có phép', 'GDGD0001', N'Đã duyệt');
go

-- 18) TuyenDung (3 rows)
insert into TuyenDung (tieuDe, idPhongBan, idChucVu, idNguoiTao, trangThai, ghiChu, xacThucYeuCau) values
(N'Tuyển kỹ sư phần mềm', 1, 1, 'TPCNTT0001', N'Đang tuyển', N'Tuyển fulltime', N'Đã xác thực'),
(N'Tuyển nhân viên nhân sự', 2, 3, 'TPCNTT0001', N'Đang tuyển', N'Cần 2 người', N'Chưa xác thực'),
(N'Tuyển trợ lý giám đốc', 3, 5, 'GDGD0001', N'Đang tuyển', N'Yêu cầu kinh nghiệm', N'Đã xác thực');
go

-- 19) UngVien (3 rows)
insert into UngVien (tenNhanVien, ngaySinh, diaChi, que, gioiTinh, email, duongDanCV, idChucVuUngTuyen, idTuyenDung, trangThai, daXoa) values
(N'Lê Thị A', '01/01/1998', N'Quận 1', N'Bình Dương', N'Nữ', 'letha@example.com', '1.png', 1, 1, N'Đang xét duyệt', 0),
(N'Ngô Văn B', '05/05/1995', N'Quận 3', N'Đồng Nai', N'Nam', 'ngovanb@example.com', '1.png', 3, 2, N'Đang xét duyệt', 0),
(N'Trần Thị C', '10/10/1996', N'Quận 7', N'Long An', N'Nữ', 'tranthic@example.com', '1.png', 5, 3, N'Đang xét duyệt', 0);
go
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
END
go
--EXEC sp_XuatDanhGiaNhanVien @Thang=10, @Nam=2025, @IdPhongBan=NULL

CREATE OR ALTER PROCEDURE sp_XuatNhanVien
    @IdPhongBan INT = NULL  -- NULL = tất cả phòng ban
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
END

--EXEC sp_XuatNhanVien @IdPhongBan = null;

--USE master;
--ALTER DATABASE PersonnelManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE PersonnelManagement;

