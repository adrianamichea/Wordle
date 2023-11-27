CREATE database WordleDB;

USE WordleDB;


  CREATE TABLE Users (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
);



INSERT INTO Users (UserName, Password) 
VALUES ('user1', 'parola1'), 
       ('user2', 'parola2');


SELECT * FROM Users;

CREATE PROCEDURE AuthenticateUser
    @UserName NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserCount INT;
    SELECT @UserCount = COUNT(*)
    FROM Users
    WHERE UserName = @Username AND Password = @Password;

    SELECT @UserCount AS UserCount;
END


CREATE FUNCTION UserExists
    (@UserName NVARCHAR(50))
RETURNS INT
AS
BEGIN
    DECLARE @UserCount INT;
    SELECT @UserCount = COUNT(*)
    FROM Users
    WHERE UserName = @UserName;

    RETURN @UserCount;
END


CREATE PROCEDURE RegisterUser
    @UserName NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF dbo.UserExists(@UserName) = 0
    BEGIN
        INSERT INTO Users (UserName, Password)
        VALUES (@UserName, @Password);
    END
END

