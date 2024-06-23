USE master
GO
ALTER DATABASE PetSpaManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
DROP DATABASE PetSpaManagement
GO
CREATE DATABASE PetSpaManagement
GO
USE PetSpaManagement
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Table: Role
CREATE TABLE Role (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(20) NOT NULL
);

-- Table: Voucher
CREATE TABLE Voucher (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    started DATETIME NOT NULL,
    expired DATETIME NOT NULL,
    reach INT NOT NULL,
    status BIT NOT NULL
);

-- Table: Account
CREATE TABLE Account (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    pass VARCHAR(15) NOT NULL,
    phone VARCHAR(11) NOT NULL,
    status BIT NOT NULL,
    countVoucher INT NOT NULL,
    roleId INT NOT NULL,
    voucherId INT,
    FOREIGN KEY (roleId) REFERENCES Role(id),
    FOREIGN KEY (voucherId) REFERENCES Voucher(id)
);

-- Table: Service
CREATE TABLE Service (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
	description VARCHAR(500) NOT NULL,
    duration INT NOT NULL,
    price FLOAT NOT NULL,
    status BIT NOT NULL
);

-- Table: Spot
CREATE TABLE Spot (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(10) NOT NULL,
    status BIT NOT NULL
);

-- Table: Available
CREATE TABLE Available (
    id INT IDENTITY(1,1) PRIMARY KEY,
    serviceId INT NOT NULL,
    spotId INT NOT NULL,
    FOREIGN KEY (serviceId) REFERENCES Service(id),
    FOREIGN KEY (spotId) REFERENCES Spot(id)
);

-- Table: Booking
CREATE TABLE Booking (
    id INT IDENTITY(1,1) PRIMARY KEY,
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
    id INT IDENTITY(1,1) PRIMARY KEY,
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
    id INT IDENTITY(1,1) PRIMARY KEY,
    started DATETIME NOT NULL,
    total FLOAT NOT NULL,
    voucherId INT,
    FOREIGN KEY (voucherId) REFERENCES Voucher(id)
);

-- Table: BillDetailed
CREATE TABLE BillDetailed (
    id INT IDENTITY(1,1) PRIMARY KEY,
    billId INT NOT NULL,
    cost FLOAT NOT NULL,
    bookingId INT NOT NULL,
    FOREIGN KEY (bookingId) REFERENCES Booking(id),
    FOREIGN KEY (billId) REFERENCES Bill(id)
);

-- Insert data into Role
INSERT INTO Role (name) VALUES 
('Admin'),
('Member'),
('Staff');

-- Insert data into Voucher
INSERT INTO Voucher (name, started, expired, reach, status) VALUES
('10% Off', '2024-01-01', '2024-12-31', 3, 1),
('20% Off', '2024-01-01', '2024-06-30', 5, 1),
('50% Off', '2024-05-05', '2024-06-30', 10, 1);

-- Insert data into Account
INSERT INTO Account (name, email, pass, phone, status, countVoucher, roleId, voucherId) VALUES 
('John Doe', 'john.doe@example.com', '1', '1', 1, 3, 2, 1),
('Jane Smith', 'jane.smith@example.com', '1', '1', 1, 1, 2, 2),
('Tran Van A', 'ATV@gmail.com', '1', '1', 1, 2, 2, NULL),
('Tran Van B', 'BTV@gmail.com', '1', '1', 1, 3, 2, NULL),
('Tran Van C', 'CTV@gmail.com', '1', '1', 1, 4, 2, NULL),
('Pham Tran Hoang Minh', 'mphamtran8@gmail.com', '1', '1', 1, 0, 1, NULL),
('Hoang Trung Thong', 'ThongHT@gmail.com', '1', '1', 1, 0, 1, NULL),
('admin', 'admin@gmail.com', '1', '1', 1, 0, 1, NULL);

-- Insert data into Service
INSERT INTO Service (name, duration, description, price, status) VALUES 
('Basic Combo', 90, 'Includes a bath, nail trimming, and ear cleaning. Ideal for regular grooming maintenance.', 49.99, 1),
('Full Combo', 180, 'Comprehensive package with all services. Perfect for a thorough grooming session.', 89.99, 1),
('Bath', 30, 'Refreshing bath using pet-friendly products to cleanse and deodorize your pet''s coat.', 14.99, 1),
('Nail Trimming', 30, 'Gentle trimming of your pet''s nails to maintain comfort and prevent overgrowth.', 9.99, 1),
('Ear Cleaning', 30, 'Careful cleaning of ears to remove dirt and debris, promoting ear health.', 14.99, 1),
('Hair Grooming', 30, 'Trimming and styling of fur to keep your pet looking neat and tidy.', 19.99, 1),
('Massage Therapy', 30, 'Relaxing massage to soothe muscles and improve circulation, promoting overall well-being.', 24.99, 1),
('Teeth Brushing', 30, 'Brushing with pet-safe toothpaste to maintain dental hygiene and freshen breath.', 14.99, 1);


-- Insert data into Spot
INSERT INTO Spot (name, status) VALUES 
('Room 1', 1),
('Room 2', 1),
('Room 3', 1),
('Room 4', 1);

-- Insert data into Available
INSERT INTO Available (serviceId, spotId) VALUES 
(1, 1),
(2, 2),
(3, 1),
(4, 1),
(5, 1),
(6, 2),
(7, 2),
(8, 2)
-- Insert data into Booking
INSERT INTO Booking (started, ended, created, availableId, accountId, status) VALUES 
('2024-05-01 09:00:00', '2024-05-01 10:00:00', '2024-04-25 14:00:00', 1, 1, 1),
('2024-05-02 11:00:00', '2024-05-02 13:00:00', '2024-04-26 15:00:00', 2, 2, 1),
('2024-05-03 10:00:00', '2024-05-03 11:30:00', '2024-04-27 16:00:00', 3, 3, 1),
('2024-05-04 13:00:00', '2024-05-04 15:00:00', '2024-04-28 17:00:00', 4, 4, 1),
('2024-05-05 09:00:00', '2024-05-05 09:30:00', '2024-04-29 10:00:00', 5, 5, 1),
('2024-05-06 14:00:00', '2024-05-06 14:30:00', '2024-04-30 11:00:00', 6, 6, 1),
('2024-05-07 11:00:00', '2024-05-07 11:30:00', '2024-05-01 12:00:00', 7, 7, 1),
('2024-05-08 12:00:00', '2024-05-08 13:30:00', '2024-05-02 13:00:00', 8, 8, 1);

-- Insert data into Feedback
INSERT INTO Feedback (information, status, rating, created, updated, accId, serviceId) VALUES 
('Great service!', 1, 5, '2024-05-01 12:00:00', '2024-05-01 12:00:00', 1, 1),
('Very satisfied', 1, 4, '2024-05-02 14:00:00', '2024-05-02 14:00:00', 2, 2),
('Not bad', 1, 3, '2024-05-03 12:00:00', '2024-05-03 12:00:00', 3, 3),
('Could be better', 1, 2, '2024-05-04 16:00:00', '2024-05-04 16:00:00', 4, 4),
('Excellent service!', 1, 5, '2024-05-05 10:00:00', '2024-05-05 10:00:00', 5, 5),
('Good value for money', 1, 4, '2024-05-06 15:00:00', '2024-05-06 15:00:00', 6, 6),
('Satisfied with the service', 1, 4, '2024-05-07 12:00:00', '2024-05-07 12:00:00', 7, 7),
('Highly recommended', 1, 5, '2024-05-08 14:00:00', '2024-05-08 14:00:00', 8, 8);

-- Insert data into Bill
INSERT INTO Bill (started, total, voucherId) VALUES
('2024-05-01 09:00:00', 26.99, 1),
('2024-05-02 11:00:00', 47.99, 2),
('2024-05-03 10:00:00', 39.99, NULL),
('2024-05-04 13:00:00', 59.99, NULL),
('2024-05-05 09:00:00', 14.99, NULL),
('2024-05-06 14:00:00', 19.99, NULL),
('2024-05-07 11:00:00', 24.99, NULL),
('2024-05-08 12:00:00', 29.99, NULL);

-- Insert data into BillDetailed
INSERT INTO BillDetailed (billId, cost, bookingId) VALUES 
(3, 39.99, 3),
(4, 59.99, 4),
(5, 14.99, 5),
(6, 19.99, 6),
(7, 24.99, 7),
(8, 29.99, 8);
