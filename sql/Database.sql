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
<<<<<<< HEAD
	LoaiHopDong nvarchar(100) not null,
=======
	LoaiHopDong nvarchar(100) check(LoaiHopDong in(N'Xác định thời hạn', N'Không xác định thời hạn')) not null,
>>>>>>> origin/develop
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
	LoaiNghiPhep nvarchar(50) check(LoaiNghiPhep in(N'Nghỉ phép không lương', N'Nghỉ phép có lương')) not null,
	idNhanVien varchar(10) not null,
	primary key(id),
	constraint chk_NgayBatDau check(NgayBatDau<= ngayKetThuc)
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
	primary key(id)
)
go

CREATE TABLE TuyenDung (
    id INT PRIMARY KEY IDENTITY(1,1),
    tieuDe NVARCHAR(150) NOT NULL,
	soLuong int not null default 1,
    idPhongBan int NOT NULL,
    idChucVu int NOT NULL,
    idNguoiTao varchar(10) NOT NULL, -- Trưởng phòng nhân sự
    trangThai NVARCHAR(50) DEFAULT N'Đang tuyển', 
    ngayTao DATETIME DEFAULT GETDATE(),
	ghiChu nvarchar(50),
	xacThucYeuCau nvarchar(50),
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
--USE master;
--ALTER DATABASE PersonnelManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE PersonnelManagement;

