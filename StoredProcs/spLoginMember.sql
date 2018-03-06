USE [FullerTV]
GO
/****** Object:  StoredProcedure [dbo].[spLoginMember]    Script Date: 3/6/2018 9:04:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spLoginMember]
@Email VARCHAR(75),
@Password VARCHAR(50)
AS
BEGIN

	IF EXISTS(SELECT mbID FROM tblMember WHERE mbEmail = @Email AND mbPassword = @Password)
	BEGIN

		BEGIN TRY
		
			BEGIN TRANSACTION [Login]

				INSERT INTO tblLog(lgID,lgEmail)
				VALUES
				((SELECT ISNULL(MAX(lgID),-1) + 1 FROM tblLog),
				@Email)

			COMMIT TRANSACTION [Login]

		END TRY

		BEGIN CATCH
			
			ROLLBACK TRANSACTION [Login]

			RAISERROR('There was a problem inserting log data',16,1)

		END CATCH

	END

	ELSE
	BEGIN

		RAISERROR('Invalid Email or Password',16,1)

	END

END
