USE master
GO

DROP DATABASE SignalR
GO

CREATE DATABASE SignalR
GO

USE SignalR
GO

-- Create the AppUsers table
CREATE TABLE AppUsers (
    UserID INT PRIMARY KEY,
    Fullname VARCHAR(100),
    Address VARCHAR(255),
    Email VARCHAR(100),
    Password VARCHAR(100)
);

-- Create the PostCategories table
CREATE TABLE PostCategories (
    CategoryID INT PRIMARY KEY,
    CategoryName VARCHAR(100),
    Description TEXT
);

-- Create the Posts table
CREATE TABLE Posts (
    PostID INT PRIMARY KEY,
    AuthorID INT,
    CreatedDate DATE,
    UpdatedDate DATE,
    Title VARCHAR(255),
    Content TEXT,
    PublishStatus VARCHAR(50),
    CategoryID INT,
    FOREIGN KEY (AuthorID) REFERENCES AppUsers(UserID),
    FOREIGN KEY (CategoryID) REFERENCES PostCategories(CategoryID)
);

-- Insert example data into AppUsers table
INSERT INTO AppUsers (UserID, Fullname, Address, Email, Password) VALUES
(1, 'Alice Johnson', '789 Pine St', 'alice@example.com', 'password789'),
(2, 'Bob Brown', '321 Elm St', 'bob@example.com', 'password321');

-- Insert example data into PostCategories table
INSERT INTO PostCategories (CategoryID, CategoryName, Description) VALUES
(1, 'Science', 'Posts about scientific discoveries and research'),
(2, 'Health', 'Posts about health and wellness');

-- Insert example data into Posts table
INSERT INTO Posts (PostID, AuthorID, CreatedDate, UpdatedDate, Title, Content, PublishStatus, CategoryID) VALUES
(1, 1, '2024-01-01', '2024-01-02', 'Science Post', 'This is the content of the science post.', 'Published', 1),
(2, 2, '2024-01-03', '2024-01-04', 'Health Post', 'This is the content of the health post.', 'Draft', 2);
