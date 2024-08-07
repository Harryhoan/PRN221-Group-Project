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
	discount int NOT NULL,
    expired DATETIME NOT NULL,
    reach INT NOT NULL,
	created DATETIME NOT NULL,
    status BIT NOT NULL
);

-- Table: Account
CREATE TABLE Account (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    pass VARCHAR(100) NOT NULL,
    phone VARCHAR(11) NOT NULL,
	created DATETIME NOT NULL,
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
	created DATETIME NOT NULL,
    status BIT NOT NULL
);

-- Table: Spot
CREATE TABLE Spot (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(10) NOT NULL,
	created DATETIME NOT NULL,
    status BIT NOT NULL
);

-- Table: Available
CREATE TABLE Available (
    id INT IDENTITY(1,1) PRIMARY KEY,
    serviceId INT NOT NULL,
    spotId INT NOT NULL,
	status bit NOT NULL,
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
	created DATETIME NOT NULL,
    total FLOAT NOT NULL,
    voucherId INT,
	accId INT NOT NULL,
    FOREIGN KEY (voucherId) REFERENCES Voucher(id),
	FOREIGN KEY (accId) REFERENCES Account(id)

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
INSERT INTO Voucher (name, discount, expired, reach, created, status) VALUES
('10% Off', 10, '2024-12-31', 20, '2024-05-25 11:00:00', 1),
('20% Off', 20, '2024-07-30', 5, '2024-05-24 10:00:00', 1),
('20% Off', 20, '2024-07-30', 5, '2024-05-24 10:00:00', 1),
('20% Off', 20, '2024-06-20', 10, '2024-05-24 10:00:00', 0),
('70% Off', 70, '2024-08-30', 3, '2024-05-24 10:00:00', 1),
('50% Off', 50, '2024-05-30', 10, '2024-05-23 09:00:00', 0),
('50% Off', 50, '2024-07-30', 10, '2024-05-23 09:00:00', 1);

-- Insert data into Account
INSERT INTO Account (name, email, pass, phone, status, countVoucher, roleId, voucherId, created) VALUES 
('John Doe', 'john.doe@example.com', '$2b$10$A4wpCO8EvvT310QG8k6Qleyrd9HDm83nOm4s0rsUEu7uy.fQ6ZRCy', '1', 1, 3, 2, 1, '2024-05-25 11:00:00'),
('Jane Smith', 'jane.smith@example.com', '$2b$10$A4wpCO8EvvT310QG8k6Qleyrd9HDm83nOm4s0rsUEu7uy.fQ6ZRCy', '1', 1, 1, 2, 2, '2024-05-24 10:00:00'),
('Tran Van A', 'ATV@gmail.com', '$2b$10$A4wpCO8EvvT310QG8k6Qleyrd9HDm83nOm4s0rsUEu7uy.fQ6ZRCy', '1', 1, 2, 2, NULL, '2024-05-23 09:00:00'),
('Tran Van B', 'BTV@gmail.com', '$2b$10$0yiXJ37HLG7IyxJ9p41Yqe1C/Q0bL/QXwjv9R/BcWr15ywAH7H89u', '1', 1, 3, 2, NULL, '2024-05-22 08:00:00'),
('Tran Van C', 'CTV@gmail.com', '$2b$10$A4wpCO8EvvT310QG8k6Qleyrd9HDm83nOm4s0rsUEu7uy.fQ6ZRCy', '1', 1, 4, 2, NULL, '2024-05-21 07:00:00'),
('Pham Tran Hoang Minh', 'mphamtran8@gmail.com', '$2b$10$0yiXJ37HLG7IyxJ9p41Yqe1C/Q0bL/QXwjv9R/BcWr15ywAH7H89u', '1', 1, 0, 1, NULL, '2024-05-20 06:00:00'),
('Hoang Trung Thong', 'ThongHT@gmail.com', '$2b$10$A4wpCO8EvvT310QG8k6Qleyrd9HDm83nOm4s0rsUEu7uy.fQ6ZRCy', '1', 1, 0, 1, NULL, '2024-05-19 05:00:00'),
('admin', 'admin@gmail.com', '$2b$10$A4wpCO8EvvT310QG8k6Qleyrd9HDm83nOm4s0rsUEu7uy.fQ6ZRCy', '1', 1, 0, 1, NULL, '2024-05-18 04:00:00'),
('User1', 'user1@example.com', '$2b$10$mJrQ6ByRoascbInC8Wx9UOKvARrOU6n25ft1AMo322bgEuNtsuBQe', '1234567890', 1, 3, 1, NULL, '2024-05-17 03:00:00'),
('User2', 'user2@example.com', '$2b$10$x2QQUTusuAdnHWYEGGLXLespAC967ntiiKCcSPJmDoT5DT1wGLAHK', '1234567891', 1, 4, 2, 1, '2024-05-16 02:00:00'),
('User3', 'user3@example.com', '$2b$10$kfJvNPOxzdL23KqZ4nHap.WUYvGis9fHYF/ZQzuPWFDJFV8AU0gw.', '1234567892', 1, 5, 3, 2, '2024-05-15 01:00:00'),
('User4', 'user4@example.com', '$2b$10$EcvAupapII.ZUQdP6Cw3gehEKFdNHBgFHGy3NGk4KHLymfuyy2kRW', '1234567893', 1, 6, 2, 3, '2024-05-14 12:00:00'),
('User5', 'user5@example.com', '$2b$10$W3gT3VQ9X5JaEZkhNi9UzeGnxptKbpC4sdVKPKP7j9p8ZIGDM8js.', '1234567894', 1, 7, 3, 1, '2024-05-13 11:00:00'),
('User6', 'user6@example.com', '$2b$10$8PcqEUdU/KrZdMj0XB9/FOINQeXE3La0B9TC.7amrZ5YCBr2BQfzy', '1234567895', 1, 8, 2, 2, '2024-05-12 10:00:00'),
('User7', 'user7@example.com', '$2b$10$XJMZeVZDHrqC7UofK2NTLefwrbrF6.Gr1kIoir1buCdu2lhWrEbIW', '1234567896', 1, 9, 3, 3, '2024-05-11 09:00:00'),
('User8', 'user8@example.com', '$2b$10$.K89kcpGhuccmxeFbiTVoe0Do6Qa0HFVkoqP1AYemdnrICfgiwsI.', '1234567897', 1, 10, 2, 1, '2024-05-10 08:00:00'),
('User9', 'user9@example.com', '$2b$10$LRr88CMHOV5cUirJzVJepeTDv0qn11.7OwZnC7VeT7gviSDPzW3AK', '1234567898', 1, 11, 3, 2, '2024-05-09 07:00:00'),
('User10', 'user10@example.com', '$2b$10$1GzxXOa0LjklN2H22iGvve8HowG6e2W202SMZavntAOXch4ubXp.W', '1234567899', 1, 12, 2, 3, '2024-05-08 06:00:00'),
('User11', 'user11@example.com', '$2b$10$80KZQLvOnkq2sFLrd1A1oeccgWcPh.bkN.iZz6B6MMjzwIYy8m3OG', '1234567800', 1, 13, 2, NULL, '2024-05-07 05:00:00');

UPDATE Account
SET voucherId = 
    CASE
        WHEN countVoucher >= 10 THEN 3
        WHEN countVoucher >= 5 AND countVoucher < 10 THEN 2
        WHEN countVoucher >= 3 AND countVoucher < 5 THEN 1
        ELSE NULL
    END;

-- Insert data into Service
INSERT INTO Service (name, duration, description, price, created, status) VALUES 
('Basic Combo', 90, 'Includes a bath, nail trimming, and ear cleaning. Ideal for regular grooming maintenance.', 49.99, '2024-05-25 12:00:00', 1),
('Full Combo', 180, 'Comprehensive package with all services. Perfect for a thorough grooming session.', 89.99, '2024-05-24 12:00:00', 1),
('Bath', 30, 'Refreshing bath using pet-friendly products to cleanse and deodorize your pet''s coat.', 14.99, '2024-05-23 12:00:00', 1),
('Nail Trimming', 30, 'Gentle trimming of your pet''s nails to maintain comfort and prevent overgrowth.', 9.99, '2024-05-22 12:00:00', 1),
('Ear Cleaning', 30, 'Careful cleaning of ears to remove dirt and debris, promoting ear health.', 14.99, '2024-05-21 12:00:00', 1),
('Hair Grooming', 30, 'Trimming and styling of fur to keep your pet looking neat and tidy.', 19.99, '2024-05-20 12:00:00', 1),
('Massage Therapy', 30, 'Relaxing massage to soothe muscles and improve circulation, promoting overall well-being.', 24.99, '2024-05-19 12:00:00', 1),
('Teeth Brushing', 30, 'Brushing with pet-safe toothpaste to maintain dental hygiene and freshen breath.', 14.99, '2024-05-18 12:00:00', 1);


-- Insert data into Spot
INSERT INTO Spot (name, created, status) VALUES 
('Room 1', '2024-05-22 08:00:00', 1),
('Room 2', '2024-05-21 07:00:00', 1),
('Room 3', '2024-05-20 06:00:00', 1),
('Room 4', '2024-05-19 05:00:00', 1);

-- Insert data into Available

-- Inserting data for Spot 1
INSERT INTO Available (serviceId, spotId,status)
SELECT TOP 8 id, 1,1
FROM Service;

-- Inserting data for Spot 2
INSERT INTO Available (serviceId, spotId,status)
SELECT TOP 8 id, 2,1
FROM Service;

-- Inserting data for Spot 3
INSERT INTO Available (serviceId, spotId,status)
SELECT TOP 8 id, 3,1
FROM Service;

-- Inserting data for Spot 4
INSERT INTO Available (serviceId, spotId,status)
SELECT TOP 8 id, 4,1
FROM Service;

     INSERT INTO Booking (started, ended, created, availableId, accountId, status) VALUES
('2024-06-01 09:00:00', '2024-06-01 10:00:00', '2024-05-26 14:00:00', 1, 1, 1),
('2024-06-02 11:00:00', '2024-06-02 13:00:00', '2024-05-27 15:00:00', 2, 2, 1),
('2024-06-03 10:00:00', '2024-06-03 11:30:00', '2024-05-28 16:00:00', 3, 3, 1),
('2024-06-04 13:00:00', '2024-06-04 15:00:00', '2024-05-29 17:00:00', 4, 4, 1),
('2024-06-05 09:00:00', '2024-06-05 09:30:00', '2024-05-30 10:00:00', 5, 5, 1),
('2024-06-06 10:00:00', '2024-06-06 11:00:00', '2024-05-31 11:00:00', 6, 6, 1),
('2024-06-07 11:00:00', '2024-06-07 12:30:00', '2024-06-01 12:00:00', 7, 7, 1),
('2024-06-08 12:00:00', '2024-06-08 13:30:00', '2024-06-02 13:00:00', 8, 8, 1),
('2024-06-09 13:00:00', '2024-06-09 14:30:00', '2024-06-03 14:00:00', 9, 9, 1),
('2024-06-10 14:00:00', '2024-06-10 15:00:00', '2024-06-04 15:00:00', 10, 10, 1),
('2024-06-11 15:00:00', '2024-06-11 16:30:00', '2024-06-05 16:00:00', 11, 11, 1),
('2024-06-12 16:00:00', '2024-06-12 17:00:00', '2024-06-06 17:00:00', 12, 12, 1),
('2024-06-13 09:00:00', '2024-06-13 10:00:00', '2024-06-07 10:00:00', 13, 13, 1),
('2024-06-14 10:00:00', '2024-06-14 11:30:00', '2024-06-08 11:00:00', 14, 14, 1),
('2024-06-15 11:00:00', '2024-06-15 12:30:00', '2024-06-09 12:00:00', 15, 15, 1),
('2024-06-16 12:00:00', '2024-06-16 13:30:00', '2024-06-10 13:00:00', 16, 16, 1),
('2024-06-17 13:00:00', '2024-06-17 14:30:00', '2024-06-11 14:00:00', 17, 17, 1),
('2024-06-18 14:00:00', '2024-06-18 15:00:00', '2024-06-12 15:00:00', 18, 18, 1),
('2024-06-19 15:00:00', '2024-06-19 16:30:00', '2024-06-13 16:00:00', 19, 19, 1),
('2024-06-20 16:00:00', '2024-06-20 17:00:00', '2024-06-14 17:00:00', 20, 1, 1),
('2024-06-21 09:00:00', '2024-06-21 10:00:00', '2024-06-15 10:00:00', 21, 2, 1),
('2024-06-22 10:00:00', '2024-06-22 11:30:00', '2024-06-16 11:00:00', 22, 3, 1),
('2024-06-23 11:00:00', '2024-06-23 12:30:00', '2024-06-17 12:00:00', 23, 4, 1),
('2024-06-24 12:00:00', '2024-06-24 13:30:00', '2024-06-18 13:00:00', 24, 5, 1),
('2024-06-25 13:00:00', '2024-06-25 14:30:00', '2024-06-19 14:00:00', 25, 6, 1),
('2024-06-26 14:00:00', '2024-06-26 15:00:00', '2024-06-20 15:00:00', 26, 7, 1),
('2024-06-27 15:00:00', '2024-06-27 16:30:00', '2024-06-21 16:00:00', 27, 8, 1),
('2024-06-28 16:00:00', '2024-06-28 17:00:00', '2024-06-22 17:00:00', 28, 9, 1),
('2024-06-29 09:00:00', '2024-06-29 10:00:00', '2024-06-23 10:00:00', 29, 10, 1),
('2024-06-30 10:00:00', '2024-06-30 11:30:00', '2024-06-24 11:00:00', 30, 11, 1),
('2024-07-01 11:00:00', '2024-07-01 12:30:00', '2024-06-25 12:00:00', 31, 12, 1),
('2024-07-02 12:00:00', '2024-07-02 13:30:00', '2024-06-26 13:00:00', 32, 13, 1),
('2024-07-03 13:00:00', '2024-07-03 14:30:00', '2024-06-27 14:00:00', 1, 14, 1),
('2024-07-04 14:00:00', '2024-07-04 15:00:00', '2024-06-28 15:00:00', 2, 15, 1),
('2024-07-05 15:00:00', '2024-07-05 16:30:00', '2024-06-29 16:00:00', 3, 16, 1),
('2024-07-06 16:00:00', '2024-07-06 17:00:00', '2024-06-30 17:00:00', 4, 17, 1),
('2024-07-07 09:00:00', '2024-07-07 10:00:00', '2024-07-01 10:00:00', 5, 18, 1),
('2024-07-08 10:00:00', '2024-07-08 11:30:00', '2024-07-02 11:00:00', 6, 19, 1),
('2024-07-09 11:00:00', '2024-07-09 12:30:00', '2024-07-03 12:00:00', 7, 1, 1),
('2024-07-10 12:00:00', '2024-07-10 13:30:00', '2024-07-04 13:00:00', 8, 2, 1),
('2024-07-11 13:00:00', '2024-07-11 14:30:00', '2024-07-05 14:00:00', 9, 3, 1),
('2024-07-12 14:00:00', '2024-07-12 15:00:00', '2024-07-06 15:00:00', 10, 4, 1),
('2024-07-13 15:00:00', '2024-07-13 16:30:00', '2024-07-07 16:00:00', 11, 5, 1),
('2024-07-14 16:00:00', '2024-07-14 17:00:00', '2024-07-08 17:00:00', 12, 6, 1),
('2024-07-15 09:00:00', '2024-07-15 10:00:00', '2024-07-09 10:00:00', 13, 7, 1),
('2024-07-16 10:00:00', '2024-07-16 11:30:00', '2024-07-10 11:00:00', 14, 8, 1),
('2024-07-17 11:00:00', '2024-07-17 12:30:00', '2024-07-11 12:00:00', 15, 9, 1),
('2024-07-18 12:00:00', '2024-07-18 13:30:00', '2024-07-12 13:00:00', 16, 10, 1),
('2024-07-19 13:00:00', '2024-07-19 14:30:00', '2024-07-13 14:00:00', 17, 11, 1),
('2024-07-20 14:00:00', '2024-07-20 15:00:00', '2024-07-14 15:00:00', 18, 12, 1),
('2024-07-21 15:00:00', '2024-07-21 16:30:00', '2024-07-15 16:00:00', 19, 13, 1),
('2024-07-22 16:00:00', '2024-07-22 17:00:00', '2024-07-16 17:00:00', 20, 14, 1),
('2024-07-23 09:00:00', '2024-07-23 10:00:00', '2024-07-17 10:00:00', 21, 15, 1),
('2024-07-24 10:00:00', '2024-07-24 11:30:00', '2024-07-18 11:00:00', 22, 16, 1),
('2024-07-25 11:00:00', '2024-07-25 12:30:00', '2024-07-19 12:00:00', 23, 17, 1),
('2024-07-26 12:00:00', '2024-07-26 13:30:00', '2024-07-20 13:00:00', 24, 18, 1),
('2024-07-27 13:00:00', '2024-07-27 14:30:00', '2024-07-21 14:00:00', 25, 19, 1),
('2024-07-28 14:00:00', '2024-07-28 15:00:00', '2024-07-22 15:00:00', 26, 1, 1),
('2024-07-29 15:00:00', '2024-07-29 16:30:00', '2024-07-23 16:00:00', 27, 2, 1),
('2024-07-30 16:00:00', '2024-07-30 17:00:00', '2024-07-24 17:00:00', 28, 3, 1),
('2024-07-31 09:00:00', '2024-07-31 10:00:00', '2024-07-25 10:00:00', 29, 4, 1),
('2024-08-01 10:00:00', '2024-08-01 11:30:00', '2024-07-26 11:00:00', 30, 5, 1),
('2024-08-02 11:00:00', '2024-08-02 12:30:00', '2024-07-27 12:00:00', 31, 6, 1),
('2024-08-03 12:00:00', '2024-08-03 13:30:00', '2024-07-28 13:00:00', 32, 7, 1),
('2024-08-03 12:00:00', '2024-07-30 13:30:00', '2024-07-28 13:00:00', 32, 7, 1);

-- Insert data into Feedback
INSERT INTO Feedback (information, status, rating, created, updated, accId, serviceId) VALUES 
('Great service!', 1, 5, '2024-05-01 12:00:00', '2024-05-01 12:00:00', 1, 1),
('Very satisfied', 1, 4, '2024-05-02 14:00:00', '2024-05-02 14:00:00', 2, 2),
('Not bad', 1, 3, '2024-05-03 12:00:00', '2024-05-03 12:00:00', 3, 3),
('Could be better', 1, 2, '2024-05-04 16:00:00', '2024-05-04 16:00:00', 4, 4),
('Excellent service!', 1, 5, '2024-05-05 10:00:00', '2024-05-05 10:00:00', 5, 5),
('Good value for money', 1, 4, '2024-05-06 15:00:00', '2024-05-06 15:00:00', 6, 6),
('Satisfied with the service', 1, 4, '2024-05-07 12:00:00', '2024-05-07 12:00:00', 7, 7),
('Highly recommended', 1, 5, '2024-05-08 14:00:00', '2024-05-08 14:00:00', 8, 8),
('Average experience', 1, 3, '2024-05-09 11:00:00', '2024-05-09 11:00:00', 9, 1),
('Fantastic staff!', 1, 5, '2024-05-10 13:00:00', '2024-05-10 13:00:00', 10, 2),
('Could improve the quality', 1, 2, '2024-05-11 14:30:00', '2024-05-11 14:30:00', 11, 3),
('Great ambiance!', 1, 4, '2024-05-12 17:00:00', '2024-05-12 17:00:00', 12, 4),
('Service was a bit slow', 1, 3, '2024-05-13 12:45:00', '2024-05-13 12:45:00', 13, 5),
('Amazing service!', 1, 5, '2024-05-14 18:00:00', '2024-05-14 18:00:00', 17, 6),
('Will visit again', 1, 4, '2024-05-15 13:00:00', '2024-05-15 13:00:00', 9, 7),
('Not worth the price', 1, 2, '2024-05-16 19:00:00', '2024-05-16 19:00:00', 5, 8),
('Loved the service', 1, 5, '2024-05-17 16:00:00', '2024-05-17 16:00:00', 3, 1),
('Disappointed with the experience', 1, 1, '2024-05-18 14:00:00', '2024-05-18 14:00:00', 15, 2),
('Excellent hospitality', 1, 5, '2024-05-19 15:00:00', '2024-05-19 15:00:00', 10, 3),
('Very good', 1, 4, '2024-05-20 10:00:00', '2024-05-20 10:00:00', 11, 4);

-- Insert data into Bill
INSERT INTO Bill (started,created, total,accId, voucherId) VALUES
('2024-05-01 09:00:00','2024-05-01 08:00:00', 26.99,1, NULL),
('2024-05-02 11:00:00','2024-05-01 08:00:00', 47.99,2, NULL),
('2024-05-03 10:00:00','2024-05-01 08:00:00',39.99,3, NULL),
('2024-05-04 13:00:00','2024-05-01 08:00:00', 59.99,10, NULL),
('2024-05-05 09:00:00','2024-05-01 08:00:00', 14.99,12, NULL),
('2024-05-06 14:00:00','2024-05-01 08:00:00', 19.99,12, NULL),
('2024-05-07 11:00:00','2024-05-01 08:00:00', 24.99,6, NULL),
('2024-05-08 12:00:00','2024-05-01 08:00:00', 29.99,7, NULL),
('2024-08-03 12:00:00','2024-07-28 13:30:00', 29.99,7, NULL),
('2024-08-01 10:00:00','2024-07-26 11:00:00', 29.99,7, NULL),
('2024-07-23 09:00:00','2024-07-17 10:00:00', 29.99,15, NULL),
('2024-07-15 09:00:00','2024-07-09 10:00:00', 39.99,7, NULL),
('2024-06-20 16:00:00','2024-06-14 17:00:00', 49.99,1, NULL),
('2024-07-08 10:00:00','2024-07-02 11:00:00', 89.99,19, NULL),
('2024-06-18 14:00:00','2024-06-12 15:00:00', 49.99,18, NULL),
('2024-07-06 16:00:00','2024-06-30 17:00:00', 29.99,17, NULL),
('2024-07-18 12:00:00','2024-07-12 13:00:00', 49.99,10, NULL),
('2024-07-28 14:00:00','2024-07-22 15:00:00', 14.99,1, NULL);

-- Insert data into BillDetailed
INSERT INTO BillDetailed (billId, cost, bookingId) 
VALUES
  (1, 50.00, 1),
  (1, 30.00, 2),
  (2, 25.00, 3),
  (2, 40.00, 4),
  (3, 35.00, 5),
  (3, 55.00, 6),
  (4, 20.00, 7),
  (4, 45.00, 8),
  (5, 60.00, 9),
  (5, 28.00, 10),
  (6, 38.00, 11),
  (6, 42.00, 12),
  (7, 33.00, 13),
  (7, 52.00, 14),
  (8, 22.00, 15),
  (8, 48.00, 16),
  (9, 30.00, 17),
  (10, 30.00, 18),
  (11, 30.00, 19),
  (12, 39.00, 20),
  (13, 50.00, 21),
  (14, 90.00, 22),
  (15, 50.00, 23),
  (16, 30.00, 24),
  (17, 50.00, 25),
  (18, 15.00, 26);

-- Update BillDetailed to match Service price
UPDATE bd
SET bd.cost = s.price
FROM BillDetailed bd
JOIN Booking b ON bd.bookingId = b.id
Join Available a on b.accountId = a.id
JOIN Service s ON a.serviceId = s.id;
-- Update Bill to calculate total based on BillDetailed costs
UPDATE Bill
SET total = (
    SELECT SUM(cost)
    FROM BillDetailed
    WHERE BillDetailed.billId = Bill.id
);

-- Update Bill started column to match the started column of the earliest Booking record associated through BillDetailed
UPDATE Bill
SET started = (
    SELECT MIN(b.started)
    FROM Booking b
    JOIN BillDetailed bd ON b.id = bd.bookingId
    WHERE bd.billId = Bill.id
);

-- Step 1: Find the earliest datetime for each EntityA
WITH EarliestDateTimes AS (
    SELECT
        a.id,
        MIN(c.started) AS EarliestDateTime
    FROM
        Bill a
        INNER JOIN BillDetailed b ON a.id = b.billId
        INNER JOIN Booking c ON b.bookingId = c.id
    GROUP BY
        a.id
)
-- Step 2: Update EntityA's DateTimeColumn with the earliest datetime
UPDATE a
SET a.started = e.EarliestDateTime
FROM Bill a
INNER JOIN EarliestDateTimes e ON a.id = e.id;
