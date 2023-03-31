Create Database QLSSNDK
Go
Use QLSSNDK
Go
Create Table	KhachHang
(
	IDCus	Int	Identity(1,1),
	CodeCus	int,
	HoTen	Nvarchar(50),
	Email	Varchar(100)	Unique,
	DiaChiKH	Nvarchar(200),
	DienThoaiKH	Varchar(50),
	Constraint	Pk_KhachHang	Primary Key(CodeCus)
)
Go
--Drop table Parts
--Go
Create Table	Parts
(
	MaParts	Int	Identity(1,1),
	TenParts	Nvarchar(50)	Not Null,
	Constraint Pk_Parts Primary Key(MaParts)
)
Go
--Drop table Brand
--Go
Create Table	Brand
(
	MaBrand	Int Identity(1,1),
	TenBrand	Nvarchar(50)	Not Null,
	XuatXu	Nvarchar(200),
	Constraint	Pk_Brand Primary Key(MaBrand),
)
Go
--Drop table SanPham
--Go
Create Table	SanPham
(
	MaSP	Int	Identity(1,1),
	TenSP	Nvarchar(100)	Not Null,
	GiaBan	Decimal,
	NoiDung	Nvarchar(Max),
	AnhDD	Varchar(50),
	SoLuongTon	Int,
	MaParts	Int,
	MaBrand	Int,
	Constraint	Pk_SanPham	Primary Key(MaSP),
	Constraint	Fk_Parts	Foreign	Key(MaParts) References	Parts(MaParts),
	Constraint	Fk_Brand	Foreign Key(MaBrand) References	Brand(MaBrand)
)
Go
--Table DonDatHang
Create Table	DonDatHang
(
	IDOrder	Int Identity(1,1),
	NgayDH	DateTime,
	DCGH	varchar(200),
	CodeCus int,
	Constraint	Pk_DonDatHang	Primary Key(IDOrder),
	Constraint	Fk_KhachHang	Foreign	Key(CodeCus)	References	KhachHang(CodeCus)
)
Go
--Table ChiTietDatHang
Create Table	ChiTietDatHang
(
	ID	Int Identity(1,1),
	IDOrder	Int,
	MaSP Int,
	SoLuong	Int,
	DonGia Decimal,
	Constraint	Pk_ChiTietDatHang	Primary Key(IDOrder,MaSP),
	Constraint	Fk_DonHang	Foreign	Key(IDOrder)	References	DonDatHang(IDOrder),
	Constraint	Fk_SanPham	Foreign Key(MaSP)	References	SanPham(MaSP)
)
Go
--Thêm dữ liệu:
---Parts
	Insert into Parts Values (N' Deck')
	Insert into Parts Values (N' Trucks')
	Insert into Parts values (N' Wheels')
	Insert into Parts values (N' Bearings')
	Insert into Parts values (N' Grip Tape')
	Insert into Parts values (N' Completes')
select *from Parts
---Brand
	Insert into Brand values (N' BDSKATECO',N' Tây Ban Nha')
	Insert into Brand values (N' 187 KILLER PADS',N' Hoa Kỳ')
	Insert into Brand values (N' CHOCOLATE',N' Hoa Kỳ')
	Insert into Brand values (N' BAKER',N' Hoa Kỳ')
	Insert into Brand values (N' NOMAD',N' Tây Ban Nha')
	Insert into Brand values (N' TOY MACHINE',N' Hoa Kỳ')
	Insert into Brand values (N' SANTA CRUZ' ,N' Hoa Kỳ')
	Insert into Brand values (N' THUNDER' ,N' Hoa Kỳ')
	Insert into Brand values (N' KRUX' ,N' Hoa Kỳ')
	Insert into Brand values (N' PRIMITIVE' ,N' Hoa Kỳ')
	Insert into Brand values (N' SHAKE JUNT',N' Hoa Kỳ')
	Insert into Brand Values (N' BRONSON SPEED CO' ,N' Hoa Kỳ')
	Insert into Brand Values (N' DO BY HEART' ,N' Hoa Kỳ')
select *from Brand
---Sản phẩm
	---Complete
	Insert into SanPham values (N' NOMAD O.L TRY AGAIN DECK 8.0',850000,N' Có tặng kèm mặt nhám','/Asset/Images/complete1a.jpg',25,6,5)
	Insert into SanPham values (N' BDSKATECO 26" PINAPPLE CRUISER COMPLETE - 7.5X26.5"',1950000,N' Có tặng kèm mặt nhám','/Asset/Images/complete2a.jpg',15,6,1)
	Insert into SanPham values (N' SANTA CRUZ GARTLAND SWEET DREAMS VX DECK 8.0',2200000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/complete3a.jpg',8,6,7)
	Insert into SanPham values (N' BDSKATECO 26" FLAMINGO CRUISER COMPLETE - 7.5X26.5"',1950000,N' Có tặng kèm mặt nhám','/Asset/Images/complete4a.jpg',19,6,1)
	---Deck
	Insert into SanPham values (N'TOY MACHINE SECT EYE DECK - 7.87 BLUE',1450000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/deck1a.jpg',30,1,6)
	Insert into SanPham values (N' CHOCOLATE PEREZ SECRET SOCIETY DECK 8.0',1450000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/deck2a.jpg',6,1,2)
	Insert into SanPham values (N' BDSKATECO INK BLACK DECK 8.0',85000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/deck3a.jpg',12,1,1)
	Insert into SanPham values (N' BAKER TEAM BRAND LOGO BLACK DECK 8.0',1550000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/deck4a.png',23,1,4)
	---Truck
	Insert into SanPham values (N' BDSKATECO SOFTRUCKS TRUCK KIT',1100000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/truck1a.jpg',5,2,1)
	Insert into SanPham values (N' THUNDER STANDARD POLISHED TRUCKS',1200000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/truck2a.jpg',21,2,8)
	Insert into SanPham values (N' KRUX K5 DLK 90S STANDARD TRUCKS 8.5',1390000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/truck3a.jpg',12,2,9)
	Insert into SanPham values (N' BDSKATECO BLUE LIGHT TRUCKS 5.25',700000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/truck4a.jpg',15,2,1)
	---Wheel
	Insert into SanPham values (N' BDSKATECO OG LOGO BLACK 100A WHEELS 52MM',550000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/wheel1a.jpg',10,3,1)
	Insert into SanPham values (N' NOMAD FILMER 101A WHEELS 52MM',450000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/wheel2a.jpg',12,3,5)
	Insert into SanPham values (N' PRIMTIVE X MARVEL WOLVERINE WHEEL 54MM',650000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/wheel3a.jpg',6,3,10)
	Insert into SanPham values (N' BDSKATECO ROSETTE RED 101A WHEELS 52MM',450000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/wheel4a.jpg',6,3,1)
	---Bearing
	Insert into SanPham values (N' NOMAD ABEC-5 BEARINGS',350000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/bearing1a.jpg',9,4,5)
	Insert into SanPham values (N' SHAKE JUNT LOW RIDERS ABEC-3 BEARINGS',400000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/bearing2a.jpg',14,4,11)
	Insert into SanPham values (N' BRONSON SPEED CO.  PRO G3 BEARINGS',650000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/bearing3a.jpg',5,4,12)
	Insert into SanPham values (N' DO BY HEART FLIP BEARINGS',400000,N'Thông tin sản phẩm đang được cập nhật','/Asset/Images/bearing4a.jpg',6,4,13)
	---Grip Tape
	Insert into SanPham values (N' SHAKE JUNT CLEAR GRIPTAPE',280000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/grip1a.png',24,5,11)
	Insert into SanPham values (N' DO BY HEART LAGUNA CUTOUT PURPLE GRIPTAPE',220000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/grip2a.png',12,5,13)
	Insert into SanPham values (N' BDSKATECO BLACK GRIPTAPE 9"',140000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/grip3a.jpg',30,5,1)
	Insert into SanPham values (N' DO BY HEART LAGUNA CUTOUT ORANGE GRIPTAPE',220000,N' Thông tin sản phẩm đang được cập nhật','/Asset/Images/grip4a.jpg',18,5,13)
select *from SanPham
--Dữ liệu cập nhật: Tài khoản quản trị
Create table Admin
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nVarchar(50)
)
Insert into Admin values('admin','123456','Nhom5')
Insert into Admin values('user','654321','Group5')
select *from Admin