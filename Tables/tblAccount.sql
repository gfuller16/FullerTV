USE [FullerTV]
GO

/****** Object:  Table [dbo].[tblAccount]    Script Date: 3/6/2018 9:07:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAccount](
	[actID] [int] NOT NULL,
	[act_mbID] [int] NULL,
	[act_stID] [int] NULL,
	[act_pkgID] [int] NULL,
	[actDateAdded] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[actID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblAccount] ADD  DEFAULT (getdate()) FOR [actDateAdded]
GO

ALTER TABLE [dbo].[tblAccount]  WITH CHECK ADD FOREIGN KEY([act_mbID])
REFERENCES [dbo].[tblMember] ([mbID])
GO

ALTER TABLE [dbo].[tblAccount]  WITH CHECK ADD FOREIGN KEY([act_pkgID])
REFERENCES [dbo].[tblPackage] ([pkgID])
GO

ALTER TABLE [dbo].[tblAccount]  WITH CHECK ADD FOREIGN KEY([act_stID])
REFERENCES [dbo].[tblStation] ([stID])
GO
