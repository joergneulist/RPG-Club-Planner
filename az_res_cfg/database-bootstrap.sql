-- EXECUTE IN DATABASE QUERY EDITOR

-- 1. Create Tables
CREATE TABLE Roles (
    RoleName NVARCHAR(20) PRIMARY KEY
);

INSERT INTO Roles (RoleName) VALUES ('Pending'), ('Player'), ('GM'), ('Admin');

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ExternalId NVARCHAR(255) UNIQUE NOT NULL, -- The unique ID from Google/GitHub
    Provider NVARCHAR(50) NOT NULL,          -- 'google', 'github', etc.
    Email NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Phone NVARCHAR(20),
    ImageUrl NVARCHAR(MAX),
    RoleName NVARCHAR(20) DEFAULT 'Pending' REFERENCES Roles(RoleName),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);

CREATE TABLE Events (
    Id INT PRIMARY KEY IDENTITY(1,1),
    GmId INT REFERENCES Users(Id),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    EventDate DATETIMEOFFSET NOT NULL,
    MaxPlayers INT NOT NULL,
    ImageUrl NVARCHAR(MAX),
    CreatedAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);

CREATE TABLE RSVPs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EventId INT REFERENCES Events(Id) ON DELETE CASCADE,
    UserId INT NULL REFERENCES Users(Id), -- NULL allowed for Placeholders
    PlaceholderName NVARCHAR(100) NULL,
    SignedUpAt DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET(),
    RsvpStatus INT NOT NULL DEFAULT 2 -- 1 = Declined, 2 = Confirmed, etc.
);

-- 2. Initial Admin Seed (Example)
-- TODO

