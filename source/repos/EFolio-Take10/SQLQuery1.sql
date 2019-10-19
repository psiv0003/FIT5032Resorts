-- Creating table 'Booking'
CREATE TABLE [dbo].[Bookings] (
 [Id] int IDENTITY(1,1) NOT NULL,
 [BookingDateTime] DateTime NOT NULL,
 [RoomID] int NOT NULL,
 [GuestID] nvarchar(128) NOT NULL,
 [CheckInDate] date NOT NULL,
 [CheckOutDate] date NOT NULL,
 [NoOfAdults] int NOT NULL,
 [NoOfChildren] int NOT NULL,
 [TotalCharge] numeric(5,2) NOT NULL,
 [Rating] int,
 [Comment] nvarchar(max),
PRIMARY KEY (Id),
FOREIGN KEY (RoomID) REFERENCES Rooms(Id),
FOREIGN KEY (GuestID) REFERENCES ASPNETUSERS(Id)

);
GO