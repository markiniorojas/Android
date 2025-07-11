CREATE TABLE Person (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    LastName NVARCHAR(100),
    TypeDocument NVARCHAR(50),
    DocumentNumber NVARCHAR(50),
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE [User] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    Active BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    PersonId INT,
    FOREIGN KEY (PersonId) REFERENCES Person(Id)
);

CREATE TABLE Rol (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE Permission (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE Form (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Url NVARCHAR(255),
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE Module (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    IsDeleted BIT DEFAULT 0
);

CREATE TABLE FormModule (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FormId INT NOT NULL,
    ModuleId INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (FormId) REFERENCES Form(Id),
    FOREIGN KEY (ModuleId) REFERENCES Module(Id)
);

CREATE TABLE RolFormPermission (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RolId INT NOT NULL,
    FormId INT NOT NULL,
    PermissionId INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (RolId) REFERENCES Rol(Id),
    FOREIGN KEY (FormId) REFERENCES Form(Id),
    FOREIGN KEY (PermissionId) REFERENCES Permission(Id)
);

CREATE TABLE RolUser (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RolId INT NOT NULL,
    UserId INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (RolId) REFERENCES Rol(Id),
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
