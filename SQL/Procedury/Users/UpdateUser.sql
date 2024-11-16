CREATE OR ALTER PROCEDURE [PROJEKT].[UpdateUser]
@Id INT,
@UserName VARCHAR(200),
@Email VARCHAR(200),
@Password VARCHAR(200)
AS
BEGIN
	UPDATE [PROJEKT].[Users] SET
	PUS_UserName = @UserName,
	PUS_EMAIL = @Email,
	PUS_Password = @Password
	WHERE PUS_Id = @Id
END
GO