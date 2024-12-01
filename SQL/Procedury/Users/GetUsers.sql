CREATE OR ALTER PROCEDURE [PROJEKT].[GetUsers] 
@Id INT = NULL
AS
BEGIN
	IF @Id IS NULL
		BEGIN
			SELECT PUS_Id, ISNULL(PUS_UserName,'') AS PUS_UserName, ISNULL(PUS_EMAIL,'') AS PUS_EMAIL,PUS_Password,PUS_RegistrationDate,
	PUS_LastLogin,ISNULL(PUS_Bio,'') AS PUS_Bio, ISNULL(PUS_Country,'') AS PUS_Country
	FROM [PROJEKT].[Users]
		END
		ELSE
			SELECT PUS_Id, ISNULL(PUS_UserName,'') AS PUS_UserName, ISNULL(PUS_EMAIL,'') AS PUS_EMAIL, PUS_Password,PUS_RegistrationDate,
	PUS_LastLogin,ISNULL(PUS_Bio,'') AS PUS_Bio, ISNULL(PUS_Country,'') AS PUS_Country
	FROM [PROJEKT].[Users] WHERE PUS_Id = @Id
END
GO
EXEC [PROJEKT].[GetUsers]