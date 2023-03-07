--[INSERT USERS TYPES]-----------------------------------------------------------------------------------------------------------

delete from UsersTypes

insert into UsersTypes values
(NEWID(), 'Client'), (NEWID(), 'Staff')

select * from UsersTypes
go;