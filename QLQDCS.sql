IF EXISTS (SELECT * FROM master.DBO.SYSDATABASES WHERE NAME='QLQDCS')   
    DROP DATABASE QLQDCS
--TAO CSDL MOI
CREATE DATABASE QLQDCS
--SU DUNG CSDL VUA TAO
USE QLQDCS
--TAOBANG
DROP TABLE NVCS
DROP TABLE ChinhSach

CREATE TABLE ChinhSach(
     MaChinhSach nchar(10) PRIMARY KEY,
	 ChinhSach nvarchar(100),
	 NgayApDung DateTime,
	 NgayHetHan DateTime,
	 DuongDan nvarchar(255),
	 Mota nvarchar(300))

ALTER TABLE ChinhSach
ADD PhienBan nchar(10)

DROP TABLE PHONGBAN
CREATE TABLE PhongBan(
     MaPhong nchar(10) PRIMARY KEY,
	 TenPhong nvarchar(20),
	 Mota nvarchar(300)
)
DROP TABLE NHANVIEN
CREATE TABLE NHANVIEN(
     MaNV nchar(10) PRIMARY KEY,
	 MatKhau nvarchar(20),
	 HoTen nvarchar(50),
	 NgaySinh DateTime,
	 GioiTinh nvarchar(10),
	 ChucVu nvarchar(30),
	 DiaChi nvarchar(100),
	 Email nvarchar(30),
	 SDT nchar(20),
	 TruyCap DateTime,
	 Luong float,
	 Thuong float,
	 Phat float,
	 TangCa float,
	 Nghi float,
	 KinhNghiem float,
	 Loi nvarchar(300),
	 HoTro nvarchar(300),
	 MaNguoiSua nchar(10),
	 NguoiSua nvarchar(100),
	 NgaySua DateTime,
	 MaPhong nchar(10) foreign key (MaPhong) references PhongBan(MaPhong)
)


CREATE TABLE NVCS(
     MaNvcs INT IDENTITY(1,1) NOT NULL,
     MaNV nchar(10) foreign key (MaNV) references NhanVien(MaNV),
	 MaChinhSach nchar(10) foreign key (MaChinhSach) references ChinhSach(MaChinhSach),
	 PRIMARY KEY (MaNvcs)
)


DROP TABLE NgQL
CREATE TABLE NgQL(
     MaAdm nchar(10) PRIMARY KEY,
	 TenAdmin nvarchar(50),
	 TenDangNhap nchar(50),
	 MatKhau nchar(50),
	 SDT nchar(20),
	 Email nchar(50)
)
DROP TABLE CTCS
CREATE TABLE CTCS(
     MaCT INT IDENTITY(1,1) NOT NULL primary key,
	 MaCS nchar(10) foreign key (MaCS) references ChinhSach(MaChinhSach),
	 DoiTuong nvarchar(200),
	 KetLuan nvarchar(300)
)

INSERT INTO ChinhSach VALUES('CS01',N'Chính sách Lương','01-01-2000','12-31-2029',N'D:\TTCSN\FileCS\CSQD_Luong.docx',N'Chính sách về lương nhân viên đang có hợp đồng với công ty'),
                             ('CS02',N'Chính sách Thưởng','01-12-2001','12-31-2029',N'D:\TTCSN\FileCS\CSQD_Thuong.docx',N'Chính sách về thưởng nhân viên có thành tích tốt'),
							 ('CS03',N'Chính sách Nghỉ','02-20-2000','12-31-2029',N'D:\TTCSN\FileCS\CSQD_Nghi.docx',N'Chính sách về nghỉ của nhân viên với lý do cá nhân'),
							 ('CS04',N'Chính sách Hỗ trợ với nhân viên tăng ca','05-05-2000','12-31-2029',N'D:\TTCSN\FileCS\CSQD_TangCa.docx',N'Chính sách hỗ trợ lương, thưởng, ăn uống, đi lại cho nhân viên tăng ca ngoài giờ'),
							 ('CS05',N'Chính sách đối với Nhân viên chính thức','01-01-2000','12-31-2029',N'D:\TTCSN\FileCS\CSQD_NVCT.docx',N'Chính sách về hoạt động, công việc, lương thưởng, giờ làm việc, đánh giá đối với nhân viên chính thức'),
							 ('CS06',N'Chính sách đối với Thực tập sinh','01-01-2000','12-31-2029',N'D:\TTCSN\FileCS\CSQD_TTS.docx',N'Chính sách về hoạt động, công việc, lương thưởng, giờ làm việc, đánh giá đối với thực tập sinh')
SELECT * FROM ChinhSach
INSERT INTO PhongBan VALUES ('P01',N'Phòng Kinh doanh',N'Phòng kinh doanh'),
                            ('P02',N'Phòng Kế toán',N'Phòng tài chính - kế toán'),
							('P03',N'Phòng Marketing',N'Phòng Marketing'),
							('P04',N'Phòng nhân sự',N'Phòng nhân sự'),
							('P05',N'Phòng CNTT',N'Phòng công nghệ thông tin'),
							('P06',N'Phòng CSKH',N'Phòng chăm sóc khách hàng'),
							('P07',N'Phòng giám đốc',N'Phòng giám đốc')
SELECT * FROM PhongBan
INSERT INTO NHANVIEN VALUES ('nv001','123456',N'Nguyễn Đức Xuân','08-28-2003','Nam',N'Quản lý',N'Bắc Từ Liêm, Hà Nội','xuan@gmail.com','0987654321','','20000000','1000000','500000','30','2','3',N'Đi muộn x1',N'Tiền xăng, Tiền gửi xe, Tiền ăn trưa','AD01',N'Nguyễn Đức Xuân','','P05'),
                            ('nv002','123456',N'Nguyễn Văn Trường','01-01-2003','Nam',N'Quản lý',N'Bắc Từ Liêm, Hà Nội','truong1@gmail.com','012345678987','','20000000','1000000','500000','30','2','3',N'Đi muộn x1',N'Tiền xăng, Tiền gửi xe, Tiền ăn trưa','AD01',N'Nguyễn Đức Xuân','','P03'),
							('nv003','123456',N'Nguyễn Văn Trường','02-02-2003','Nam',N'Nhân viên',N'Bắc Từ Liêm, Hà Nội','truong2@gmail.com','0357986421','','20000000','1000000','500000','30','2','3',N'Đi muộn x1',N'Tiền xăng, Tiền gửi xe, Tiền ăn trưa','AD01',N'Nguyễn Đức Xuân','','P05'),
							('nv004','123456',N'Nguyễn Anh Tú','03-03-2003','Nam',N'Nhân viên',N'Bắc Từ Liêm, Hà Nội','tu@gmail.com','0246897531','','20000000','1000000','500000','30','2','3',N'Đi muộn x1',N'Tiền xăng, Tiền gửi xe, Tiền ăn trưa','AD01',N'Nguyễn Đức Xuân','','P02'),
							('nv005','123456',N'Luyện Xuân Tùng','04-04-2003','Nam',N'Nhân viên',N'Bắc Từ Liêm, Hà Nội','tung@gmail.com','0369875421','','20000000','1000000','500000','30','2','3',N'Đi muộn x1',N'Tiền xăng, Tiền gửi xe, Tiền ăn trưa','AD01',N'Nguyễn Đức Xuân','','P01'),
							('nv006','123456',N'Phạm Nhật Vượng','05-05-1965','Nam',N'Giám đốc',N'Bắc Từ Liêm, Hà Nội','vuong@gmail.com','0369875421','','90000000','1000000','0','30','2','3',N'Không',N'Xe riêng đưa đón, Nhà ở, Tiền ăn trưa','AD01',N'Nguyễn Đức Xuân','','P07')
SELECT * FROM NHANVIEN
INSERT INTO NVCS VALUES('nv001','CS01'),
                       ('nv001','CS02'),
					   ('nv001','CS03'),
					   ('nv001','CS05'),
					   ('nv002','CS01'),
					   ('nv002','CS02'),
					   ('nv002','CS05'),
					   ('nv003','CS01'),
					   ('nv003','CS06'),
					   ('nv004','CS01'),
					   ('nv004','CS03'),
					   ('nv004','CS04'),
					   ('nv005','CS01'),
					   ('nv005','CS02'),
					   ('nv005','CS03')
					   
SELECT * FROM NVCS		

INSERT INTO CTCS VALUES('CS01',N'Nhân viên chưa có kinh nghiệm',N'dưới 8.000.000 đồng'),
                        ('CS01',N'Nhân viên dưới 1 năm kinh nghiệm',N'dưới 12.000.000 đồng'),
						('CS01',N'Nhân viên dưới 3 năm kinh nghiệm',N'dưới 20.000.000 đồng'),
						('CS01',N'Nhân viên dưới 5 năm kinh nghiệm',N'dưới 30.000.000 đồng'),
						('CS01',N'Nhân viên dưới 8 năm kinh nghiệm',N'dưới 50.000.000 đồng'),
						('CS01',N'Nhân viên trên 8 năm kinh nghiệm',N'theo thoả thuận'),
						('CS02',N'Thưởng tháng',N'Toàn bộ nhân viên, do quản lý thưởng'),
						('CS02',N'Thưởng quý',N'Toàn bộ nhân viên, do quản lý thưởng'),
						('CS02',N'Thưởng năm',N'Toàn bộ nhân viên, do quản lý thưởng'),
						('CS02',N'Thưởng thành tích đặc biệt',N'Toàn bộ nhân viên có thành tích đặc biệt, do quản lý thưởng'),
						('CS02',N'Thưởng khuyến khích',N'Toàn bộ nhân viên có thành tích tốt, do quản lý thưởng')

INSERT INTO NgQL VALUES ('AD01',N'Nguyễn Đức Xuân','xuan','123456','0123456798','x@gmail.com'),
                         ('AD02',N'Nguyễn Văn A','admina','123456','0987654312','a@gmail.com'),
						 ('AD03',N'Trần Văn B','adminb','123456','0987654312','b@gmail.com')
						 
DROP TABLE QuyDinh
CREATE TABLE QuyDinh(
     MaQuyDinh nchar(10) PRIMARY KEY,
	 QuyDinh nvarchar(100),
	 NgayApDung DateTime,
	 NgayHetHan DateTime,
	 DuongDan nvarchar(255),
	 Mota nvarchar(300),
	 PhienBan nchar(100))

DROP TABLE NVQD
CREATE TABLE NVQD(
     MaNvqd INT IDENTITY(1,1) NOT NULL,
     MaNV nchar(10) foreign key (MaNV) references NhanVien(MaNV),
	 MaQuyDinh nchar(10) foreign key (MaQuyDinh) references QuyDinh(MaQuyDinh),
	 PRIMARY KEY (MaNvqd))
DROP TABLE DUONGDAN
CREATE TABLE DUONGDAN(
     MaDuongDan nchar(10) PRIMARY KEY,
	 DuongDan nvarchar(100),
	 MaChinhSach nchar(10) foreign key (MaChinhSach) references ChinhSach(MaChinhSach),
	 MaQuyDinh nchar(10) foreign key (MaQuyDinh) references QuyDinh(MaQuyDinh))
DROP TABLE CTQD
CREATE TABLE CTQD(
     MaCT INT IDENTITY(1,1) NOT NULL primary key,
	 MaQD nchar(10) foreign key (MaQD) references QuyDinh(MaQuyDinh),
	 DoiTuong nvarchar(200),
	 KetLuan nvarchar(300)
)