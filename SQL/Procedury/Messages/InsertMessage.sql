CREATE OR ALTER PROCEDURE [PROJEKT].[InsertMessage]
@SenderId INT,
@ReceiverId INT,
@Message VARCHAR(MAX)
AS
BEGIN
	INSERT INTO [PROJEKT].[Messages]
	(PMS_SenderId,PMS_ReceiverId,PMS_Message,PMS_Date)
	VALUES 
	(@SenderId,@ReceiverId,@Message,GETDATE())
END
GO