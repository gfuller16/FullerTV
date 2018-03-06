USE [FullerTV]
GO

/****** Object:  Table [dbo].[tblPackage]    Script Date: 3/6/2018 9:09:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPackage](
	[pkgID] [int] NOT NULL,
	[pkgName] [varchar](50) NOT NULL,
	[pkgPrice] [money] NULL,
	[pkgStartDate] [date] NOT NULL,
	[pkgEndDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[pkgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
