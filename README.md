Web app for car renting

Update #1![DBDiagram](https://user-images.githubusercontent.com/38327738/223462639-d13bc2cc-546f-44b5-afac-144a6627fb2c.png)

First version of SQL server DB. Users table is for storage of some basic info of clients and also of company staff. To distinguish these two groups of users every Users entry has reference to predefinied user type from UsersTypes table. CarsModel table stores basic info about every model available in app, these way we can reference particular car form Cars table to concrete model and avoid storing the same info many times. Every car from Cars table has also a reference to location from Locations table to determine in what location it is currently available (it is important to note that this reference can be empty if some car is already rented). Reservations table stores info about particular reservations referencing the user and a the car. Field isPickedUp can be marked as true (0 or 1 - its a bit type) when someone after reservations gets to particular location and gets his car.

Update #2
Small changes in DB: PricePerDay added into the CarsModels table. 
Also I have decided to use bootstrap to create some acceptable view. 
While working on backend I assumed that accounts with type "Staff" will be added to the database through SQL scripts and appropriate endpoints will only serve purpose to add clients. The right way may be to create some admin console and create different endpoints to add clients and staff but it will be too time consuming and right now I want to focus more on car renting and reservations system.

Update #3
Basic assumptions on car renting system:
1. Client can choose location and see what cars are currently available to book them starting from the next day.
2. After choosing location client can also see all cars even if they aren't currently in this particular location and book one of them but there will be one day delay to transport this car to the chosen location. So in this case the booking period starts in second day counting from the day he tries to do reservation. 
3. Between every reservation there has to be one day off to make sure that every car will be delivered for time to particular location. 
4. Also client can rent a car which is currently rented if periods of time don't collide with each other and there is a one day off between them.
5. Reservations can be made by clients for 2 next months starting from current month (for example: if it is March, reservations are available for March, April and May).
6. There is no need to show clients lots of cars of the same model. For client only necessary info will be the fact that for chosen period of time the car will be available. The decision which concrete car has to be booked is made by server with the focus on to reserve firstly these cars which have some reservations but not for chosen time period. So the server tries to keep available as many cars as possible.

Update #4
![reservation_algorithm_example](https://user-images.githubusercontent.com/38327738/226184681-e5875339-6000-4151-a171-6fb94a248199.png)
Small example to show how reservation algorithm works. Car ID is a car plate so it identifies particulary one car, so phrase: "different cars" means cars with different plates. We can see that 1st reservation and 3rd reservation are for the same model but due to the fact that date periods intersects with each other, different cars have been reserved. On 4th row we can see that another reservation has been made for this car model and once again another car has been reserved (we have to assure one day off beetween two reservation so the reservation can not be made for CarID = 977C5C6). On the 5th row we can see that another reservation has been made to the same car mentioned in previous sentence due to the fact that date ranges do not collide with each other. 

How to run project:
1. Backend -> Scripts -> Run scripts in given order: Definitions, Predefined, TestData
2. Backend -> appsettings.json -> Modify entry DBCarRenting to connect to created on step 1 DB.
3. App -> .env -> change backend URL to the one You will use.
4. Run project

To log in to staff account see script TestData. One example entry is given there. 
