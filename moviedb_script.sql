USE [MoviesDB]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 21/08/2022 09:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[Genre] [varchar](100) NULL,
	[Rating] [decimal](18, 2) NULL,
	[YearReleased] [int] NULL,
	[Director] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 21/08/2022 09:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserInformation] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 

INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (1, N'Shawshank Redemption', N'Drama', CAST(9.25 AS Decimal(18, 2)), 1994, N'Frank Darabont')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (2, N'Looper', N'Thriller', CAST(8.00 AS Decimal(18, 2)), 2012, N'Rian Johnson')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (3, N'Fight Club', N'Thriller', CAST(8.50 AS Decimal(18, 2)), 1999, N'David Fincher')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (4, N'Shrek', N'Comedy', CAST(7.60 AS Decimal(18, 2)), 2001, N'Andrew Adamson')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (5, N'A Beautiful Mind', N'Drama', CAST(8.80 AS Decimal(18, 2)), 2000, N'Ron Howard')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (6, N'Room', N'Thriller', CAST(7.20 AS Decimal(18, 2)), 2015, N'Lenny Abrahamson')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (7, N'The Hangover', N'Comedy', CAST(6.20 AS Decimal(18, 2)), 2008, N'Todd Phillips')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (8, N'Home Alone', N'Comedy', CAST(7.40 AS Decimal(18, 2)), 1990, N'Chris Columbus')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (9, N'Shutter Island', N'Thriller', CAST(8.10 AS Decimal(18, 2)), 2010, N'Martin Scorcese')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (10, N'Inception', N'Thriller', CAST(8.90 AS Decimal(18, 2)), 2010, N'Christopher Nolan')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (11, N'Se7en', N'Thriller', CAST(8.40 AS Decimal(18, 2)), 1995, N'David Fincher')
INSERT [dbo].[Movie] ([Id], [Name], [Genre], [Rating], [YearReleased], [Director]) VALUES (40, N'Space Jam', N'Family', CAST(6.50 AS Decimal(18, 2)), 1996, N'Joe Pytka')
SET IDENTITY_INSERT [dbo].[Movie] OFF
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([UserId], [DisplayName], [UserName], [Email], [Password], [Role], [CreatedDate]) VALUES (1, N'Super Admin', N'SA', N'admin@abc.com', N'da62753634d8fc7b33fa67cb7d6585f1859de019b96c78962f1dae181e083c03', N'Admin', CAST(N'2022-08-20T14:47:58.207' AS DateTime))
INSERT [dbo].[UserInfo] ([UserId], [DisplayName], [UserName], [Email], [Password], [Role], [CreatedDate]) VALUES (2, N'John Doe', N'john_doe', N'johndoe@abc.com', N'13bd2166a6b9338dafb49e24e7003fad637d50cd4df98d3bd81bccf846a414e0', N'User', CAST(N'2022-08-20T14:47:58.207' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO
