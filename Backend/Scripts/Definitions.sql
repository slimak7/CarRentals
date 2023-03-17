--[CREATE DATABASE]-------------------------------------------------------------------------------------------------------

if not exists (SELECT * FROM sys.databases WHERE name = 'CarRentingDB')
begin 
create database CarRentingDB 
use [CarRentingDB]
end
go

--[DROP TABLES]-----------------------------------------------------------------------------------------------------------

if object_id('Reservations') is not null
begin
drop table Reservations
end
if object_id('Users') is not null
begin
drop table Users
end
if object_id('UsersTypes') is not null
begin
drop table UsersTypes
end
if object_id('Cars') is not null
begin
drop table Cars
end
if object_id('Locations') is not null
begin
drop table Locations
end
if object_id('CarsModels') is not null
begin
drop table CarsModels
end
go

--[CREATE TABLES]-----------------------------------------------------------------------------------------------------------

create table UsersTypes (
UserTypeID uniqueidentifier primary key,
TypeName varchar(20))
go

create table Users (
UserID uniqueidentifier primary key,
Email varchar(50),
Password varchar(50),
FirstName varchar(50),
LastName varchar(50),
PhoneNumber varchar(11),
UserTypeIDFK uniqueidentifier foreign key references UsersTypes(UserTypeID))
go

create table Locations (
LocationID uniqueidentifier primary key,
LocationName varchar(50),
Street varchar(50),
City varchar(50),
Zipcode varchar(5))
go

create table CarsModels (
CarModelID uniqueidentifier primary key,
ModelName varchar(50),
ModelRange integer,
Acceleration real,
MaxNumberOfSeats integer,
PricePerDay real)
go

create table Cars (
LicensePlateID varchar(10) primary key,
Color varchar(20),
ModelIDFK uniqueidentifier foreign key references CarsModels(CarModelID),
LocationIDFK uniqueidentifier null foreign key references Locations(LocationID)
)
go

create table Reservations (
ReservationID uniqueidentifier primary key,
DateFrom Date,
DateTo Date,
isPickedUp bit,
UserIDFK uniqueidentifier foreign key references Users(UserID),
CarIDFK varchar(10) foreign key references Cars(LicensePlateID),
LocationIDFK uniqueidentifier foreign key references Locations(LocationID))
go




