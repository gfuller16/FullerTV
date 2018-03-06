USE [FullerTV]
GO

/****** Object:  Table [dbo].[tblStation]    Script Date: 3/6/2018 9:10:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblStation](
	[stID] [int] NOT NULL,
	[stNumber] [int] NOT NULL,
	[stFullName] [varchar](100) NOT NULL,
	[stShortName] [varchar](10) NOT NULL,
	[st_catID] [int] NULL,
	[st_pkgID] [int] NULL,
	[stPrice] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[stID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblStation]  WITH CHECK ADD FOREIGN KEY([st_catID])
REFERENCES [dbo].[tblCategory] ([catID])
GO

ALTER TABLE [dbo].[tblStation]  WITH CHECK ADD FOREIGN KEY([st_pkgID])
REFERENCES [dbo].[tblPackage] ([pkgID])
GO
