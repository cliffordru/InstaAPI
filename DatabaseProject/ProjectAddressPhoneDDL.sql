CREATE TABLE dbo.Country (
    CountryId INTEGER NOT NULL IdENTITY(1, 1),
    CountryName NVARCHAR(100) NOT NULL unique,
    IsoCode CHAR(2) NOT NULL unique,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (CountryId)
);
GO

CREATE TABLE dbo.Region (
    RegionId INTEGER NOT NULL IdENTITY(1, 1),
    RegionName NVARCHAR(50) NOT NULL,
    RegionAbbrv NVARCHAR(5) NULL,
    CountryId INTEGER NOT NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (RegionId)
);
GO

ALTER TABLE dbo.Region  WITH CHECK ADD  CONSTRAINT FK_Region_Country FOREIGN KEY(CountryId)
REFERENCES dbo.Country (CountryId);
GO

ALTER TABLE dbo.Region
ADD CONSTRAINT UC_Region_Name UNIQUE(RegionName, CountryId);
GO

CREATE NONCLUSTERED INDEX IX_RegionName_CountryId ON Region(RegionName, CountryId);
GO

CREATE TABLE dbo.AddressType (
	AddressTypeId INTEGER NOT NULL IdENTITY(1, 1),
	AddressType NVARCHAR(25) NOT NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
	PRIMARY KEY (AddressTypeId)
)

CREATE TABLE dbo.Address (
    AddressId INTEGER NOT NULL IdENTITY(1, 1),
    AddressLine1 NVARCHAR(255) NOT NULL,
    AddressLine2 NVARCHAR(255) NULL,
    AddressLine3 NVARCHAR(255) NULL,
    City VARCHAR(255) NOT NULL,
    RegionId INTEGER NOT NULL,
    PostalCode VARCHAR(10) NOT NULL,
    CountryId INTEGER NOT NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (AddressId)
);
GO

CREATE TABLE dbo.Person (
    PersonId UNIQUEIdENTIFIER NOT NULL,
    FirstName NVARCHAR(255) NOT NULL,
    MIddleName NVARCHAR(255) NULL,
    LastName NVARCHAR(255) NOT NULL,
	PreferredName NVARCHAR(255) NULL,
    BirthDate DATE NOT NULL,
    Gender CHAR(1) NOT NULL,
    EmailAddress1 VARCHAR(255) NOT NULL,
    EmailAddress2 VARCHAR(255) NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (PersonId)
);
GO

CREATE TABLE dbo.PersonAddress(
	PersonId UNIQUEIdENTIFIER NOT NULL,
	AddressId INTEGER NOT NULL,
	AddressTypeId INTEGER NOT NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (PersonId, AddressId, AddressTypeId)
)
GO

ALTER TABLE dbo.PersonAddress  WITH CHECK ADD  CONSTRAINT FK_PersonAddress_AddressType FOREIGN KEY(AddressTypeId)
REFERENCES dbo.AddressType (AddressTypeId);
GO

ALTER TABLE dbo.PersonAddress  WITH CHECK ADD  CONSTRAINT FK_PersonAddress_Address FOREIGN KEY(AddressId)
REFERENCES dbo.Address (AddressId);
GO

CREATE TABLE dbo.Phone (
    PhoneId INTEGER NOT NULL IdENTITY(1, 1),
    CountryCode TINYINT NOT NULL,
    AreaCode CHAR(6) NOT NULL,
    Number VARCHAR(100) NULL,
    Extension VARCHAR(10) NULL,    
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (PhoneId)
);
GO

ALTER TABLE dbo.Phone
ADD CONSTRAINT UC_Phone_Number UNIQUE(CountryCode, AreaCode, Number, Extension);
GO

CREATE TABLE dbo.PhoneType (
	PhoneTypeId INTEGER NOT NULL IdENTITY(1, 1),
	PhoneType NVARCHAR(25) NOT NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
	PRIMARY KEY (PhoneTypeId)
);
GO

CREATE TABLE dbo.PersonPhone(
	PersonId UNIQUEIdENTIFIER NOT NULL,
	PhoneId INTEGER NOT NULL,
	PhoneTypeId INTEGER NOT NULL,
	Created datetime NOT NULL DEFAULT (GETDATE()),
	Updated datetime NULL,
	Timestamp,
    PRIMARY KEY (PersonId, PhoneId, PhoneTypeId)
)
GO

ALTER TABLE dbo.PersonPhone WITH CHECK ADD  CONSTRAINT FK_PersonPhone_PhoneType FOREIGN KEY(PhoneTypeId)
REFERENCES dbo.PhoneType (PhoneTypeId);
GO

ALTER TABLE dbo.PersonPhone  WITH CHECK ADD  CONSTRAINT FK_PersonPhone_Phone FOREIGN KEY(PhoneId)
REFERENCES dbo.Phone (PhoneId);
GO

