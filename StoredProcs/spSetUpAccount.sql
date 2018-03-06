USE [FullerTV]
GO
/****** Object:  StoredProcedure [dbo].[spSetUpAccount]    Script Date: 3/6/2018 9:04:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSetUpAccount] --'graysonfuller16@gmail.com','Animal'
@Email VARCHAR(75),
@ChannelName VARCHAR(10)
AS
BEGIN

	BEGIN TRY
	
		BEGIN TRANSACTION SetUpAccount
			
			DECLARE @mbID INT = (SELECT mbID FROM tblMember WHERE mbEmail = @Email)
			DECLARE @stID INT = (SELECT stID FROM tblStation WHERE st_pkgID = 0 AND stShortName = @ChannelName)

			INSERT INTO tblAccount(actID,act_mbID,act_stID)
			VALUES
			((SELECT ISNULL(MAX(actID),-1) + 1 FROM tblAccount),@mbID,@stID)

		COMMIT TRANSACTION SetUpAccount

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION SetUpAccount
		PRINT ERROR_MESSAGE()

	END CATCH

END
