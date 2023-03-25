use master
go
create database Project_PRN221
go
use Project_PRN221
go
create table [Users](
	id int primary key identity(1,1),
	
	username varchar(255),
	[password] varchar(255),

	user_code varchar(255),
	email varchar(255),
	phone varchar(63),
	address nvarchar(255),
	user_id_number varchar(63)
)

create table [AssetStatuses](
	id int primary key identity(1,1),
	[name] nvarchar(255),
	available_to_use int,
	active int,
	currently_in_use int
)

create table AssetVendors(
	id int primary key identity(1,1),
	[name] nvarchar(255),
	contact_name nvarchar(255),
	phone varchar(63),
	email varchar(255),
	website varchar(255),
	address nvarchar(255),
	description text
)

create table AssetLocations(
	id int primary key identity(1,1),
	[name] nvarchar(255),
	description text
)

create table [AssetTypes](
	id int primary key identity(1,1),
	[name] nvarchar(255),
	code varchar(255)
)

create table [AssetTransactions](
	id int primary key identity(1,1),
	[name] nvarchar(255),

	transaction_date date,
	transaction_type varchar(255),
	transaction_cost float,
	created_at int
)

create table Assets(
	id int primary key identity(1,1),
	[name] nvarchar(255),
	code varchar(255),
	[type_id] int,

	location_id int,
	status_id int,

	assignee_id int,
	acquisition_date date,
	disposal_date date,
	assign_date date,

	vendor_id int,
	description nvarchar(255),
	transaction_id int,

	create_by_user int,

	foreign key (type_id) references [AssetTypes](id),
	foreign key (location_id) references [AssetLocations](id),
	foreign key (status_id) references [AssetStatuses](id),
	foreign key (assignee_id) references [Users](id),
	foreign key (vendor_id) references AssetVendors(id),
	foreign key (transaction_id) references [AssetTransactions](id),
	foreign key (create_by_user) references [Users](id)
)

Insert into [AssetStatuses](
	[name],
	available_to_use,
	active,
	currently_in_use
) values 
('Inactive', 0, 0, 0),
('Useable', 0, 1, 0),
('In Use', 1, 1, 1),
('Available', 1, 1, 0)

INSERT INTO [dbo].[Users](
	[username]
	,[password]
	,[user_code]
	,[email]
	,[phone]
	,[address]
	,[user_id_number]
) VALUES
('admin', '123456789','Admin','admin@gmail.com','0123456789',N'Hà Nội','1234567890')