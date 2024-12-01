CREATE OR ALTER PROCEDURE [PROJEKT].[InsertUser]
@UserName VARCHAR(200),
@Email VARCHAR(200),
@Password VARCHAR(200),
@RegistrationDate DATETIME,
@LastLogin DATETIME,
@Bio TEXT,
@Country VARCHAR(100)
AS
BEGIN
	INSERT INTO [PROJEKT].[Users] 
	(PUS_UserName,PUS_EMAIL,PUS_Password,PUS_RegistrationDate,
	PUS_LastLogin,PUS_Bio,PUS_Country)
	VALUES 
	(@UserName,@Email,@Password,@RegistrationDate,@LastLogin,@Bio,@Country)
END
GO
--EXEC [PROJEKT].InsertUser 'debil','email','haslo'