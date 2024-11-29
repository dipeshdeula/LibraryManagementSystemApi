
CREATE TABLE Users (
    UserId INT IDENTITY(1, 1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT('User') CHECK (Role IN ('Admin', 'Editor', 'User')), -- Editor -> Author
    UserProfile NVARCHAR(MAX) NOT NULL,
    LoginStatus BIT NOT NULL DEFAULT (1),
    FullName NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(20) NULL UNIQUE
);
-- Alter the Users table to allow NULL for FullName, UserProfile, etc.
ALTER TABLE Users
ALTER COLUMN FullName NVARCHAR(50) NULL;

ALTER TABLE Users
ALTER COLUMN UserProfile NVARCHAR(MAX) NULL;

ALTER TABLE Users
ALTER COLUMN Phone NVARCHAR(20) NULL;

-- Create Authors Table
CREATE TABLE Authors (
    AuthorId INT IDENTITY(1, 1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Biography NVARCHAR(MAX) NULL,
    AuthorProfile NVARCHAR(MAX) NOT NULL
);


-- Create Books Table
CREATE TABLE Books (
    BookId INT IDENTITY(1, 1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    AuthorId INT NOT NULL FOREIGN KEY REFERENCES Authors(AuthorId),
    Genre NVARCHAR(50) NULL,
    ISBN NVARCHAR(20) NOT NULL UNIQUE,
    PublishedDate DATE NULL,
    AvailabilityStatus NVARCHAR(20) NOT NULL DEFAULT 'Available'
);

-- Create BookBorrow Table
CREATE TABLE BookBorrow (
    BorrowId INT IDENTITY(1, 1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
    BorrowDate DATE NOT NULL DEFAULT GETDATE(),
    ReturnDate DATE NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Borrowed'
);


-- Create Reviews Table
CREATE TABLE Reviews (
    ReviewId INT IDENTITY(1, 1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comments NVARCHAR(MAX) NULL,
    ReviewDate DATE NOT NULL DEFAULT GETDATE()
);
