USE [FullerTV]
GO
/****** Object:  StoredProcedure [dbo].[spInsertNewMember]    Script Date: 3/6/2018 9:03:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertNewMember]
--'Grayson','Fuller','gfuller@fbinsmi.com','pass','900','lansing','mi','48911','11-18-1992','m'
@FirstName VARCHAR(30),
@LastName VARCHAR(30),
@Email VARCHAR(75),
@Password VARCHAR(50),
@Address VARCHAR(30),
@City VARCHAR(30),
@State VARCHAR(2),
@ZipCode VARCHAR(10),
@DOB DATE,
@Sex VARCHAR(1)
AS
BEGIN
	
	IF NOT EXISTS(SELECT mbID FROM tblMember WHERE mbEmail = @Email)
	BEGIN

		BEGIN TRY

			BEGIN TRANSACTION NewMember

			INSERT INTO tblMember(mbID,mbFirstName,mbLastName,mbEmail,mbPassword,mbAddress,mbCity,mbState,mbZipCode,mbDOB,mbSex)
			VALUES
			((SELECT ISNULL(MAX(mbID),-1) + 1 FROM tblMember),
			@FirstName,
			@LastName,
			@Email,
			@Password,
			@Address,
			@City,
			@State,
			@ZipCode,
			@DOB,
			@Sex)

			COMMIT TRANSACTION NewMember

		END TRY

		BEGIN CATCH
		
			ROLLBACK TRANSACTION NewMember

			RAISERROR('Error inserting new member',16,1)

		END CATCH

	END

	ELSE
	BEGIN
		
		RAISERROR('Error: Member with this email already exists!',16,1)

	END

END
