USE master
GO
DROP DATABASE PetSpaManagement
Go
CREATE DATABASE PetSpaManagement
Go
USE PetSpaManagement
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 4/20/2024 10:54:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Table: Role
CREATE TABLE Role (
    id INT PRIMARY KEY,
    name VARCHAR(20) NOT NULL
);

-- Table: Voucher
CREATE TABLE Voucher (
    id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    started DATETIME NOT NULL,
    expired DATETIME NOT NULL,
    reach INT NOT NULL,
	status BIT NOT NULL
);

-- Table: Account
CREATE TABLE Account (
    id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
	pass varchar(15) Not Null,
    status BIT NOT NULL,
    countVoucher INT NOT NULL,
    roleId INT NOT NULL,
    voucherId INT,
    FOREIGN KEY (roleId) REFERENCES Role(id),
    FOREIGN KEY (voucherId) REFERENCES Voucher(id)
);

-- Table: Service
CREATE TABLE Service (
    id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    duration INT NOT NULL,
    price FLOAT NOT NULL,
    status BIT NOT NULL
);

-- Table: Spot
CREATE TABLE Spot (
    id INT PRIMARY KEY,
    name VARCHAR(10) NOT NULL,
    status BIT NOT NULL
);

-- Table: Available
CREATE TABLE Available (
    id INT PRIMARY KEY,
    serviceId INT NOT NULL,
    spotId INT NOT NULL,
    FOREIGN KEY (serviceId) REFERENCES Service(id),
    FOREIGN KEY (spotId) REFERENCES Spot(id)
);

-- Table: Booking
CREATE TABLE Booking (
    id INT PRIMARY KEY,
    started DATETIME NOT NULL,
    ended DATETIME NOT NULL,
    created DATETIME NOT NULL,
    availableId INT NOT NULL,
    accountId INT NOT NULL,
	status BIT NOT NULL,
    FOREIGN KEY (availableId) REFERENCES Available(id),
    FOREIGN KEY (accountId) REFERENCES Account(id)
);

-- Table: Feedback
CREATE TABLE Feedback (
    id INT PRIMARY KEY,
    information VARCHAR(50) NOT NULL,
    status BIT NOT NULL,
    rating INT NOT NULL CHECK (rating BETWEEN 0 AND 5),
    created DATETIME NOT NULL,
    updated DATETIME NOT NULL,
    accId INT NOT NULL,
    serviceId INT NOT NULL,
    FOREIGN KEY (accId) REFERENCES Account(id),
    FOREIGN KEY (serviceId) REFERENCES Service(id)
);

-- Table: Bill
CREATE TABLE Bill (
    id INT PRIMARY KEY,
    started DATETIME NOT NULL,
    total FLOAT NOT NULL,
    voucherId INT NOT NULL,
    FOREIGN KEY (voucherId) REFERENCES Voucher(id),
);

-- Table: BillDetailed
CREATE TABLE BillDetailed (
    id INT PRIMARY KEY,
    billId INT NOT NULL,
	cost float NOT NULL,
	bookingId INT NOT NULL,
	FOREIGN KEY (bookingId) REFERENCES Booking(id),
    FOREIGN KEY (billId) REFERENCES Bill(id)
);

-- Insert data into Role
INSERT INTO Role (id, name) VALUES 
(1, 'Admin'),
(2, 'Member'),
(3, 'Staff');

-- Insert data into Voucher
INSERT INTO Voucher (id, name, started, expired, reach, status) VALUES
(1, '10% Off', '2024-01-01', '2024-12-31', 3, 1),
(2, '20% Off', '2024-01-01', '2024-06-30', 5, 1),
(3, '50% Off', '2024-05-05', '2024-06-30', 10, 1);


-- Insert data into Account
INSERT INTO Account (id, name, email, pass, status, countVoucher, roleId, voucherId) VALUES 
(1, 'John Doe', 'john.doe@example.com',1, 1, 3, 2, 1),
(2, 'Jane Smith', 'jane.smith@example.com',1, 1, 1, 2,2),
(3, 'Tran Van A', 'ATV@gmail.com',1, 1, 2, 2, null),
(4, 'Tran Van B', 'BTV@gmail.com',1, 1, 3, 2, null),
(5, 'Tran Van C', 'CTV@gmail.com',1, 1, 4, 2, null),
(6, 'Pham Tran Hoang Minh', 'mphamtran8@gmail.com',1, 1, 0, 1, null),
(7, 'Hoang Trung Thong', 'ThongHT@gmail.com',1, 1, 0, 1, null),
(8, 'admin', 'admin@gmail.com',1, 1, 0, 1, null);

-- Insert data into Service
INSERT INTO Service (id, name, duration, price, status) VALUES 
(1, 'Basic Combo', 90, 49.99, 1),
(2, 'Full Combo', 180, 89.99, 1),
(3, 'Bath', 30, 14.99, 1),
(4, 'Nail Trimming', 30, 9.99, 1),
(5, 'Ear Cleaning', 30, 14.99, 1),
(6, 'Hair Grooming', 30, 19.99, 1),
(7, 'Massage Therapy', 30, 24.99, 1),
(8, 'Teeth Brushing', 30, 14.99, 1);

-- Insert data into Spot
INSERT INTO Spot (id, name, status) VALUES 
(1, 'Room 1', 1),
(2, 'Room 2', 1),
(3, 'Room 3', 1),
(4, 'Room 4', 1);
-- Insert data into Available
INSERT INTO Available (id, serviceId, spotId) VALUES 
(1, 1, 1),
(2, 2, 2),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1),
(6, 6, 2),
(7, 7, 2),
(8, 8, 2),
(9, 1, 3),
(10, 2, 3),
(11, 3, 3),
(12, 4, 3),
(13, 5, 4),
(14, 6, 4),
(15, 7, 4),
(16, 8, 4);
-- Insert data into Booking
INSERT INTO Booking (id, started, ended, created, availableId, accountId, status) VALUES 
(1, '2024-05-01 09:00:00', '2024-05-01 10:00:00', '2024-04-25 14:00:00', 1, 1, 1),
(2, '2024-05-02 11:00:00', '2024-05-02 13:00:00', '2024-04-26 15:00:00', 2, 2, 1),
(3, '2024-05-03 10:00:00', '2024-05-03 11:30:00', '2024-04-27 16:00:00', 3, 3, 1),
(4, '2024-05-04 13:00:00', '2024-05-04 15:00:00', '2024-04-28 17:00:00', 4, 4, 1),
(5, '2024-05-05 09:00:00', '2024-05-05 09:30:00', '2024-04-29 10:00:00', 5, 5, 1),
(6, '2024-05-06 14:00:00', '2024-05-06 14:30:00', '2024-04-30 11:00:00', 6, 6, 1),
(7, '2024-05-07 11:00:00', '2024-05-07 11:30:00', '2024-05-01 12:00:00', 7, 7, 1),
(8, '2024-05-08 12:00:00', '2024-05-08 13:30:00', '2024-05-02 13:00:00', 8, 8, 1);

-- Insert data into Feedback
INSERT INTO Feedback (id, information, status, rating, created, updated, accId, serviceId) VALUES 
(1, 'Great service!', 1, 5, '2024-05-01 12:00:00', '2024-05-01 12:00:00', 1, 1),
(2, 'Very satisfied', 1, 4, '2024-05-02 14:00:00', '2024-05-02 14:00:00', 2, 2),
(3, 'Not bad', 1, 3, '2024-05-03 12:00:00', '2024-05-03 12:00:00', 3, 3),
(4, 'Could be better', 1, 2, '2024-05-04 16:00:00', '2024-05-04 16:00:00', 4, 4),
(5, 'Excellent service!', 1, 5, '2024-05-05 10:00:00', '2024-05-05 10:00:00', 5, 5),
(6, 'Good value for money', 1, 4, '2024-05-06 15:00:00', '2024-05-06 15:00:00', 6, 6),
(7, 'Satisfied with the service', 1, 4, '2024-05-07 12:00:00', '2024-05-07 12:00:00', 7, 7),
(8, 'Highly recommended', 1, 5, '2024-05-08 14:00:00', '2024-05-08 14:00:00', 8, 8);


-- Insert data into Bill
INSERT INTO Bill (id, started, total, voucherId) VALUES
(1, '2024-05-01 09:00:00', 26.99, 1),
(2, '2024-05-02 11:00:00', 47.99, 2),
(3, '2024-05-03 10:00:00', 39.99, NULL),
(4, '2024-05-04 13:00:00', 59.99, NULL),
(5, '2024-05-05 09:00:00', 14.99, NULL),
(6, '2024-05-06 14:00:00', 19.99, NULL),
(7, '2024-05-07 11:00:00', 24.99, NULL),
(8, '2024-05-08 12:00:00', 29.99, NULL);
-- Insert data into BillDetailed
INSERT INTO BillDetailed (id, billId, cost, bookingId) VALUES 
(3, 3, 39.99, 3),
(4, 4, 59.99, 4),
(5, 5, 14.99, 5),
(6, 6, 19.99, 6),
(7, 7, 24.99, 7),
(8, 8, 29.99, 8);