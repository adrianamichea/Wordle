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


--created table LastGame  (userid, secretWord,date, attempts, codes)

CREATE TABLE LastGame (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    SecretWord NVARCHAR(5) NOT NULL,
    Attempts NVARCHAR(50),
    Codes NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(ID)
);


CREATE PROCEDURE LastGameByID
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM LastGame
    WHERE ID = @ID;
END

CREATE PROCEDURE LastGameByUserID
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM LastGame
    WHERE UserID = @UserID;
END

CREATE PROCEDURE UpdateLastGame
    @UserID INT,
    @SecretWord NVARCHAR(5),
    @Attempts NVARCHAR(50),
    @Codes NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE LastGame
    SET SecretWord = @SecretWord,
        Attempts = @Attempts,
        Codes = @Codes
    WHERE UserID = @UserID;
END

//get user id by username
CREATE PROCEDURE GetUserID
    @UserName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID FROM Users
    WHERE UserName = @UserName;
END


Insert into LastGame (UserID, SecretWord, Attempts, Codes)
Values (1, 'CRANE', 'SALSA,TWIRL', 'UUPUU,UUUPU');



