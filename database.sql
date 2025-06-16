-- database.sql
CREATE DATABASE SafeVault;
GO
USE SafeVault;
GO
CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(100),
    Email VARCHAR(100)
);