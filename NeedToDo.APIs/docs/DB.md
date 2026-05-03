```sql
SQl Server localDB
===================

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<>" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2019-latest

DB Schema
=========
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](max) NULL,
	[is_completed] [bit] NULL,
	[created_at] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[todos] ADD  CONSTRAINT [PK_todos] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[todos] ADD  CONSTRAINT [DEFAULT_todos_is_completed]  DEFAULT ((0)) FOR [is_completed]
GO
```