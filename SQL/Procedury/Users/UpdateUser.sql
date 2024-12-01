CREATE OR ALTER PROCEDURE [PROJEKT].[UpdateUser]
@Id INT,
@UserName VARCHAR(200),
@Email VARCHAR(200),
@Password VARCHAR(200),
@RegistrationDate DATETIME,
@LastLogin DATETIME,
@Bio TEXT,
@Country VARCHAR(100)
AS
BEGIN
	UPDATE [PROJEKT].[Users] SET
	PUS_UserName = @UserName,
	PUS_EMAIL = @Email,
	PUS_Password = @Password,
	PUS_LastLogin = @LastLogin,
	PUS_Bio = @Bio,
	PUS_Country = @Country
	WHERE PUS_Id = @Id
END
GO