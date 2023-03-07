# HappyTeamRecruitmentTask
Web app for car renting

Update #1![DBDiagram](https://user-images.githubusercontent.com/38327738/223462639-d13bc2cc-546f-44b5-afac-144a6627fb2c.png)

First version of DB. Users table is for storage of some basic info of clients and also of company staff. To distinguish these two groups of users every Users entry has reference to predefinied user type from UsersTypes table. CarsModel table stores basic info about every model available in app, these way we can reference particular car form Cars table to concrete model and avoid storing the same info many times. Every car from Cars table has also a reference to location from Locations table to determine in what location it is currently available (it is important to note that this reference can be empty if some car is already rented). Reservations table stores info about particular reservations referencing the user and a the car. Field isPickedUp can be marked as true (0 or 1 - its a bit type) when someone after reservations gets to particular location and gets his car.
