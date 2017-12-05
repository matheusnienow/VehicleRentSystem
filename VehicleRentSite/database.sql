CREATE TABLE [Role] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50)
);
--DROP TABLE [Role]

CREATE TABLE [Brand] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50)
);
--DROP TABLE [Brand]

CREATE TABLE [VehicleType] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50)
);
--DROP TABLE [VehicleType]

CREATE TABLE [GasType] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Description] VARCHAR(50)
);
--DROP TABLE [GasType]

CREATE TABLE [User] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Login] VARCHAR(20),
	[Salt] BINARY(256),
	[Hash] BINARY(32),
	[RoleId] INT,
    FOREIGN KEY ([RoleId]) REFERENCES [Role](Id)
);
--DROP TABLE [User]

CREATE TABLE [Client] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50),
	[Surname] VARCHAR(50),
	[Passport] VARCHAR(10),
	[CPF] INT NULL,
	[Sex] VARCHAR(1) NULL,
	[Phone] VARCHAR(15),
	[BirthDate] DATE NULL,
	[Address] VARCHAR(150),
	[City] VARCHAR(50),
	[Country] VARCHAR(50),
	[UserId] INT,
    FOREIGN KEY ([UserId]) REFERENCES [User](Id)
);
--DROP TABLE [Client]

CREATE TABLE [VehicleModel] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Description] VARCHAR(100),
	[Year] INT,
	[BrandId] INT,
	[VehicleTypeId] INT,
	[GasTypeId] INT,
    FOREIGN KEY ([BrandId]) REFERENCES [Brand](Id),
    FOREIGN KEY ([VehicleTypeId]) REFERENCES [VehicleType](Id),
    FOREIGN KEY ([GasTypeId]) REFERENCES [GasType](Id)
);
--DROP TABLE [VehicleModel]

CREATE TABLE [Vehicle] (
    [Id] INT PRIMARY KEY IDENTITY,
	[Description] VARCHAR(100),
	[Plate] VARCHAR(7),
	[Mileage] INT NULL,
	[InUse] BIT,
	[VehicleModelId] INT,
    FOREIGN KEY ([VehicleModelId]) REFERENCES [VehicleModel](Id)
);
--DROP TABLE [Vehicle]

CREATE TABLE [Rent] (
    [Id] INT PRIMARY KEY IDENTITY,
	[ClientId] INT,
	[VehicleId] INT,
	[StartDate] DATE,
	[FinishDate] DATE NULL,
	[Price] FLOAT,
	[Finished] BIT NOT NULL,
    FOREIGN KEY ([ClientId]) REFERENCES [Client](Id),
    FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle](Id)
);
--DROP TABLE RENT