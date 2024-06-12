CREATE DATABASE ShipRocket;
GO

-- Use the newly created database
USE ShipRocket;
GO

CREATE TABLE Events
(
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AggregateId UNIQUEIDENTIFIER NOT NULL,
    Timestamp DATETIME2 NOT NULL,
    Data NVARCHAR(MAX) NOT NULL
);

CREATE TABLE ParcelReadModel
(
    ParcelId UNIQUEIDENTIFIER PRIMARY KEY,
    Weight DECIMAL(18, 2),
    Dimensions DECIMAL(18, 2),
    CurrentFacility NVARCHAR(100),
	ContainerId NVARCHAR(100),
    BOL NVARCHAR(100)
);