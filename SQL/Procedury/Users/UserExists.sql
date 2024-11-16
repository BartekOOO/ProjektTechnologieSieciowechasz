CREATE OR ALTER PROCEDURE [PROJEKT].[UserExists]
@UserName VARCHAR(200),
@Password VARCHAR(200) = NULL
AS
BEGIN
	IF @Password IS NULL
	BEGIN
		SELECT PUS_Id FROM [PROJEKT].Users WHERE PUS_UserName = @UserName;
	END
	ELSE
		SELECT PUS_Id FROM [PROJEKT].Users WHERE PUS_UserName = @UserName AND PUS_Password = @Password;
END
GO
EXEC [PROJEKT].UserExists 'user1', 'pxj3iBAoLmWxJ/cJzO6P4g=='