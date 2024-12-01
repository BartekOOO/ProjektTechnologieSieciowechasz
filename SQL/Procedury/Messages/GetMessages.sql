CREATE OR ALTER PROCEDURE [PROJEKT].[GetMessages]
@SenderId INT,
@ReceiverId INT
AS
BEGIN
	SELECT PMS_Id, PMS_SenderId, PMS_ReceiverId,
	PMS_Message, PMS_Date, PUS_UserName
	FROM [PROJEKT].[Messages]
	JOIN [PROJEKT].[Users] ON PUS_Id = PMS_SenderId
	WHERE PMS_SenderId IN (@SenderId,@ReceiverId)
	OR PMS_ReceiverId IN (@SenderId,@ReceiverId)
END
GO
[PROJEKT].[GetMessages] 35, 0