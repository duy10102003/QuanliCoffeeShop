
Create database QuanLiCoffeShop
go
use QuanLiCoffeShop
go
create table GenreProduct
(
	ID int identity(1,1),
	DisplayName Nvarchar(max),
	Constraint PK_Genre Primary key(ID),
)
go
create table Product
(
	ID int identity(1,1),
	DisplayName Nvarchar(max),
	Price money default 0,
	IDGenre int,
	Count int default 0,
	Description Nvarchar(max),
	Image varchar(max),
	IsDeleted bit default 0,
	Constraint PK_PrD Primary key(ID),
	Constraint FK_PrD_Genre Foreign Key (IDGenre) references GenreProduct(ID),
)
go
create table Staff
(
	ID int identity(1,1),
	DisplayName nvarchar(max),
	StartDate smalldatetime default GetDate(),
	UserName varchar(max),
	PassWord varchar(max),
	PhoneNumber varchar(20),
	BirthDay smalldatetime,
	Wage money,
	Status nvarchar(100) default N'Đang làm',
	Email varchar(max),
	Gender nchar(3) not null,
	Role nvarchar(100) default N'Nhân Viên',
	IsDeleted bit default 0,
	Constraint PK_ST Primary key (ID),
	Constraint CHECK_STAFF Check((Year(StartDate)-Year(BirthDay))>=16 and Role in (N'Nhân Viên', N'Quản lí') and Gender in (N'Nam', N'Nữ') and Status in (N'Đang làm', N'Xin nghỉ'))
)
go
create table GenreSeat
(
	ID int identity (1,1),
	DisplayName nvarchar(max),
	Constraint PK_GENRESEAT Primary key(ID)
)
go
create table Seat
(
	ID int identity(1,1),
	IDGenre int,
	Status nvarchar(100) default N'Còn trống',
	IsDeleted bit default 0,
	Constraint PK_SEAT Primary key (ID),
	Constraint FK_SEAT_GENRESEAT Foreign key (IDGenre) references GenreSeat(ID),
	Constraint CHECK_STATUS Check (Status in (N'Còn trống', N'Đã Đặt',N'Đang sửa chữa'))
)
go
create table Customer
(
	ID int identity(1,1),
	IDSeat int,
	DisplayName nvarchar(max),
	Email nvarchar(max),
	PhoneNumber varchar(20),
	Description nvarchar(max),
	Spend money,
	IsDeleted bit default 0,
	Constraint PK_Cus Primary key (ID),
	Constraint FK_Cus_Seat Foreign key(IDSeat) references Seat(ID), 
)
go
create table Error
(
	ID int identity(1,1),
	DisplayName nvarchar(max),
	Status nvarchar(100) default N'Đang sửa chữa',
	Description nvarchar(max),
	Constraint PK_ER Primary key(ID),
	Constraint CHECK_ER Check (Status in (N'Đang sửa chữa', N'Đã sửa'))
)
go
create table Bill
(
	ID int identity(1,1),
	IDCus int,
	IDStaff int,
	CreateAt smalldatetime default GetDate(),
	TotalPrice money default 0,
	Constraint PK_Bill Primary key(ID),
	Constraint FK_Bill_Cus Foreign key (IDCus) references Customer(ID),
	Constraint FK_Bill_Staff Foreign key(IDStaff) references Staff(ID)
)
go
create table BillInfo
(
	IDBill int,
	IDProduct int,
	PriceItem money default 0,
	Count int default 0,
	Constraint PK_BillInfo Primary key (IDBill,IDProduct),
	Constraint FK_BillInfo_Bill Foreign key(IDBill) references Bill(ID),
	Constraint FK_BillInfo_PrD Foreign key(IDProduct) references Product(ID),
)
go
ALter table [dbo].[Error] add IsDeleted Bit default 0
alter table [dbo].[Bill] add IsDeleted bit default 0
alter table [dbo].[BillInfo] add IsDeleted bit default 0
alter table [dbo].[Bill] add IDSeat int
go 
alter table [dbo].[Bill] add constraint FK_Bill_Seat foreign key(IDSeat) references [dbo].[Seat]
alter table [dbo].[BillInfo] add Description nvarchar(max)
go
insert into Staff (PassWord, UserName, Gender, Role) values ('admin','admin', 'Nam', N'Quản lí')