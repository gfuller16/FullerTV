USE [FullerTV]
GO

/****** Object:  Table [dbo].[tblLog]    Script Date: 3/6/2018 9:08:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLog](
	[lgID] [int] NOT NULL,
	[lgEmail] [varchar](75) NOT NULL,
	[lgDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[lgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblLog] ADD  DEFAULT (getdate()) FOR [lgDateTime]
GO
