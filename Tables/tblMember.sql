USE [FullerTV]
GO

/****** Object:  Table [dbo].[tblMember]    Script Date: 3/6/2018 9:09:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMember](
	[mbID] [int] NOT NULL,
	[mbFirstName] [varchar](30) NOT NULL,
	[mbLastName] [varchar](30) NOT NULL,
	[mbEmail] [varchar](75) NOT NULL,
	[mbPassword] [varchar](50) NOT NULL,
	[mbAddress] [varchar](30) NOT NULL,
	[mbCity] [varchar](30) NOT NULL,
	[mbState] [varchar](2) NOT NULL,
	[mbZipCode] [varchar](10) NOT NULL,
	[mbDOB] [date] NOT NULL,
	[mbSex] [varchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[mbID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
