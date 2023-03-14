CREATE TRIGGER [dbo].[users_UPDATE] ON [dbo].[users]
    AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

    DECLARE @Id UNIQUEIDENTIFIER

    SELECT @Id = INSERTED.id
    FROM INSERTED

    UPDATE dbo.users
    SET [Timestamp] = GETDATE()
    WHERE id = @Id
END