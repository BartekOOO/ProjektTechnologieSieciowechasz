CREATE OR ALTER PROCEDURE [PROJEKT].[GetUsers] 
@Id INT = NULL
AS
BEGIN
	IF @Id IS NULL
		BEGIN
			SELECT PUS_Id, PUS_UserName,PUS_EMAIL,PUS_Password,PUS_RegistrationDate,
	PUS_LastLogin,ISNULL(PUS_Bio,'') AS PUS_Bio,PUS_Country
	FROM [PROJEKT].[Users]
		END
		ELSE
			SELECT PUS_Id, PUS_UserName,PUS_EMAIL,PUS_Password,PUS_RegistrationDate,
	PUS_LastLogin,ISNULL(PUS_Bio,'') AS PUS_Bio,PUS_Country
	FROM [PROJEKT].[Users] WHERE PUS_Id = @Id
END
GO
EXEC [PROJEKT].[GetUsers]