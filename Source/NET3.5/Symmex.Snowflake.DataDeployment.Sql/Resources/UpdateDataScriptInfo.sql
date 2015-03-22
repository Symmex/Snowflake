UPDATE dbo.DataScriptInfo
SET ExecutedDate = @ExecutedDate, [Hash] = @Hash
WHERE Id = @Id