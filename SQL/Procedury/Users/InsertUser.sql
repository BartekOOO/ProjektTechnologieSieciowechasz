CREATE OR ALTER PROCEDURE [PROJEKT].[InsertUser]
@UserName VARCHAR(200),
@Email VARCHAR(200),
@Password VARCHAR(200)
AS
BEGIN
	INSERT INTO [PROJEKT].[Users] 
	(PUS_UserName,PUS_EMAIL,PUS_Password)
	VALUES 
	(@UserName,@Email,@Password)
END
GO
--EXEC [PROJEKT].InsertUser 'debil','email','haslo'