--[CLEAR ALL TEST DATA]-------------------------------------------------------------------------------------------------------
delete from Cars;
delete from Locations;
delete from CarsModels;

--[CARS MODELS TEST DATA]-------------------------------------------------------------------------------------------------------

insert into CarsModels values 
(NEWID(), 'Model S Plaid', 600, 2.1, 5, 140),
(NEWID(), 'Model Y Performance', 514, 3.7, 5, 160),
(NEWID(), 'Model 3 Performance', 547, 3.3, 5, 120),
(NEWID(), 'Model X Plaid', 543, 2.6, 6, 200);

select * from CarsModels

--[LOCATIONS TEST DATA]-------------------------------------------------------------------------------------------------------

insert into Locations values 
(NEWID(), 'Palma Airport', 'Cami de Can Pastilla 48', 'Palma', '07611'),
(NEWID(), 'Palma City Center', 'C del General Riera 20', 'Palma', '07003'),
(NEWID(), 'Alcudia', 'Cami de Ronda 15', 'Alcudia', '07400'),
(NEWID(), 'Manacor', 'Carrer En Joa 11', 'Manacor', '07500');

select * from Locations

--[CARS RANDOM TEST DATA]-------------------------------------------------------------------------------------------------------

declare @numberOfCars int = 20;

while (@numberOfCars > 0) 
begin
declare @carModel uniqueidentifier = (SELECT TOP 1 CarModelID from CarsModels ORDER BY NEWID())
declare @location uniqueidentifier = (SELECT TOP 1 LocationID from Locations ORDER BY NEWID())
declare @licensePlate varchar(7) = SUBSTRING(convert(nvarchar(36), NEWID()), 1, 7)
declare @colorNumber int = floor(RAND()*4)
declare @color varchar(max) = case 
when @colorNumber = 0 then 'white'
when @colorNumber = 1 then 'green'
when @colorNumber = 2 then 'black'
when @colorNumber = 3 then 'blue'
end
insert into Cars values
(@licensePlate, @color, @carModel, @location)
set @numberOfCars = @numberOfCars - 1
end
go

select * from Cars




