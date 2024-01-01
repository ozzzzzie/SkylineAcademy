/****** Object:  Database [SkylineAcademyIdentity]    Script Date: 1/2/2024 2:03:07 AM ******/
CREATE DATABASE [SkylineAcademyIdentity];
GO
ALTER DATABASE [SkylineAcademyIdentity] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ARITHABORT OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET  MULTI_USER 
GO
ALTER DATABASE [SkylineAcademyIdentity] SET ENCRYPTION ON
GO
ALTER DATABASE [SkylineAcademyIdentity] SET QUERY_STORE = ON
GO

/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[FName] [nvarchar](max) NULL,
	[Lname] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 1/2/2024 2:03:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231216211053_AddIdentityToDb', N'6.0.23')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231216232542_AddIdentityToDbExtraColumns', N'6.0.23')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'487dd845-a1e4-4fb3-900a-0afd75360273', N'Student', N'STUDENT', N'219d074e-da85-4e7d-b08d-e8e2aea5ead9')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9d6995ed-1afa-471a-8500-4c72e5eaca9e', N'Teacher', N'TEACHER', N'0ea7ea5d-6183-46dc-97ee-e26155d69a79')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e29852a2-458a-4cad-a993-0311149fca19', N'Admin', N'ADMIN', N'45e4fc75-1a77-4bc3-9921-97a9ba78cb9d')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e349549b-c548-4508-929f-183f9a85f507', N'SuperAdmin', N'SUPERADMIN', N'bfcbd266-6015-44c1-b86a-023f7f515e6b')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'031fdc36-9fe4-4815-8137-46ed86f3faf3', N'487dd845-a1e4-4fb3-900a-0afd75360273')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8863686e-b680-40be-9fc0-486385922e8b', N'487dd845-a1e4-4fb3-900a-0afd75360273')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c241f230-cc07-4851-a3a2-e27d381d5b61', N'487dd845-a1e4-4fb3-900a-0afd75360273')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8e9c851f-3927-454a-964b-c50f8d2ad46a', N'9d6995ed-1afa-471a-8500-4c72e5eaca9e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'baeb05a6-3f38-431a-8598-2df230d10f0f', N'9d6995ed-1afa-471a-8500-4c72e5eaca9e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd152c5d8-95b8-479d-a49c-ee0f96fa0a88', N'9d6995ed-1afa-471a-8500-4c72e5eaca9e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'19b8552c-d6ac-4c66-a351-65bf56bc4f92', N'e29852a2-458a-4cad-a993-0311149fca19')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'96019653-8aa1-434d-86ad-6c6465518037', N'e29852a2-458a-4cad-a993-0311149fca19')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cea727d8-c7c3-47e8-bd3e-9b14e84cf927', N'e29852a2-458a-4cad-a993-0311149fca19')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9cd17d0d-529f-4d25-88d6-385a48b7e852', N'e349549b-c548-4508-929f-183f9a85f507')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a7a34600-b28c-479a-8c54-aa4bcb1e5d13', N'e349549b-c548-4508-929f-183f9a85f507')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c76fb34c-48a5-4241-951d-dc40a736d50d', N'e349549b-c548-4508-929f-183f9a85f507')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'031fdc36-9fe4-4815-8137-46ed86f3faf3', N'nikola.walczak@skylineacademy.com', N'NIKOLA.WALCZAK@SKYLINEACADEMY.COM', N'nikola.walczak@skylineacademy.com', N'NIKOLA.WALCZAK@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAEDOPcGL9Xauq83iFaw23ahr3Ih2z9cgSMnNuee7P7xK3ydcQeGLFCjQZNkdBONeERQ==', N'A2P4KSWWOR6GM3MYYKTUPV54SGLR4BFZ', N'0b4f6404-50c7-41f1-b075-da392037eef1', NULL, 0, 0, NULL, 1, 0, NULL, N'ApplicationUser', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'19b8552c-d6ac-4c66-a351-65bf56bc4f92', N'admin@test.com', N'ADMIN@TEST.COM', N'admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAEAACcQAAAAEJH00gc1HGLpfprBGeAwBGUxaBRsjOCqBamN8vzOwS+fpCHapMur7w6WaoPG76u6DQ==', N'TRLEZOINDTFHXCOW54UMSUO3FIL7WMQV', N'6a0f5dce-df9b-4205-976b-ea2a80de1243', NULL, 0, 0, NULL, 1, 0, N'Admin Test', N'ApplicationUser', N'Admin', N'Test')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'8863686e-b680-40be-9fc0-486385922e8b', N'maryl.hertwell@skylineacademy.com', N'MARYL.HERTWELL@SKYLINEACADEMY.COM', N'maryl.hertwell@skylineacademy.com', N'MARYL.HERTWELL@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAEOZGd5zogr5UOnRYSmjxJR9ca3rC0Tu3QsaTZAVGNmEZ6BIjPuSpVQBCQuUlKNd0OA==', N'4NGKLWIUBOXNYLPF7NAEFWJP4GVC5U6Z', N'5bd0f2f5-ce1e-40ba-91e6-91599d47ffd4', NULL, 0, 0, NULL, 1, 0, N'12345671', N'ApplicationUser', N'Maryl', N'Hertwell')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'8e9c851f-3927-454a-964b-c50f8d2ad46a', N'akeem.collins@skylineacademy.com', N'AKEEM.COLLINS@SKYLINEACADEMY.COM', N'akeem.collins@skylineacademy.com', N'AKEEM.COLLINS@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAEBOKFVrPSD3hD05pV/1JgXsk67hx5VOQ7jEte1eERQ0JjjEoq3pZaXNcBgiSNU8Ktg==', N'O5XK4GQWZNZVOGZY6JRE5Q5ZB2WW7CZY', N'003fed47-ecaf-4a00-b836-537a556e80b9', NULL, 0, 0, NULL, 1, 0, N'akeem.collins@skylineacademy.com', N'ApplicationUser', N'Akeem', N'Collins')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'96019653-8aa1-434d-86ad-6c6465518037', N'karina.cooke@skylineacademy.com', N'KARINA.COOKE@SKYLINEACADEMY.COM', N'karina.cooke@skylineacademy.com', N'KARINA.COOKE@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAEH8Rk9Hd4mEPFi4jqUPB3BQ4RGvr8HGan4MuID1fhij7l7WYVj7zm3jqfaf8Kty1UQ==', N'XTVEF47HSBZG5MMNTQL73JWPZ64N43OX', N'c6140b2c-12a4-4063-8df1-acf3fd0be728', NULL, 0, 0, NULL, 1, 0, NULL, N'ApplicationUser', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'9cd17d0d-529f-4d25-88d6-385a48b7e852', N'jin.paul@skylineacademy.com', N'JIN.PAUL@SKYLINEACADEMY.COM', N'jin.paul@skylineacademy.com', N'JIN.PAUL@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAEPFm1WSBlJtv1Kj3X7j+ym2RKmmVlLpHWXUsF58T8t3FaOGzfiLn0ltujWE6vPtR7g==', N'DP6CBHUARELSPYRNUZNLIHMW7ZITUD3P', N'4c388bcc-3658-4d50-84a5-390f4b4f210d', NULL, 0, 0, NULL, 1, 0, NULL, N'ApplicationUser', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'a7a34600-b28c-479a-8c54-aa4bcb1e5d13', N'superadmin@test.com', N'SUPERADMIN@TEST.COM', N'superadmin@test.com', N'SUPERADMIN@TEST.COM', 0, N'AQAAAAEAACcQAAAAEJwJp50s0F1Pdyqivm5EUxsa5qoJtD2y1kL2hF6DQVi1bxNI0q03IW3HaRLIplrxoA==', N'FE737W7OUAVVCBYFX7EFV222F6QX62HR', N'52a67eeb-6859-46a9-91e9-3076949cba68', NULL, 0, 0, NULL, 1, 0, N'SuperAdmin Test', N'ApplicationUser', N'SuperAdmin', N'Test')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'baeb05a6-3f38-431a-8598-2df230d10f0f', N'teacher@test.com', N'TEACHER@TEST.COM', N'teacher@test.com', N'TEACHER@TEST.COM', 0, N'AQAAAAEAACcQAAAAEE1/ztt4njCKJ/Yi1SZOOkOB1Ors+BeQpfYyrehICClvZ17cbef+GJZyhe0CPPVDNQ==', N'BQI6OWO74NDMHGFQ7LTZSS7WB7AM4MWN', N'7fca6e57-76e0-49fc-8b3a-9152519063da', NULL, 0, 0, NULL, 1, 0, N'Teacher Test', N'ApplicationUser', N'Teacher', N'Test')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'c241f230-cc07-4851-a3a2-e27d381d5b61', N'student@test.com', N'STUDENT@TEST.COM', N'student@test.com', N'STUDENT@TEST.COM', 0, N'AQAAAAEAACcQAAAAECu97vtDvsY0zb6ECeoPH4mxRoZRlTtXzGxhqwBJ9X6CXeOy34Ckus3qRU+ZGmK8jA==', N'3RWAWGZ5JPR73EC3U5SFA22V2A74MFJQ', N'10fd719f-d2b2-4e26-ae4b-2dad880c7c85', NULL, 0, 0, NULL, 1, 0, N'Student Test', N'ApplicationUser', N'Student', N'Test')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'c38ed15a-2b86-446c-8a9d-c9946f30482f', N'oskaralk@hotmail.com', N'OSKARALK@HOTMAIL.COM', N'oskaralk@hotmail.com', N'OSKARALK@HOTMAIL.COM', 0, N'AQAAAAEAACcQAAAAEA3/N6I2qpuFndnL/z5FznPpv+vsQhJZGvZMcYEdjbPGozv5kg9O3nzvwiVSn13dhw==', N'U2WNYDA6ZIVB5W627THYXI6Q3CPISBAJ', N'e22da952-9ebd-403d-b636-ece85910e7c8', NULL, 0, 0, NULL, 1, 0, NULL, N'', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'c76fb34c-48a5-4241-951d-dc40a736d50d', N'test@gmail.com', N'TEST@GMAIL.COM', N'test@gmail.com', N'TEST@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEE7yA3iTpAI2mU7APxjmV7j1LuCiggFfOtBcTahw0jFBZ94DZiHWCi3lfqLyF8fYBQ==', N'3TTBBY6MJALZSGC2B4V36VBM3MHG3CJ2', N'29deebe1-842b-4132-990e-68b9780b44a2', NULL, 0, 0, NULL, 1, 0, N'The Walls, Room 303', N'ApplicationUser', N'Oskar', N'Alkhateeb')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'cb2b9825-ef48-444e-bf3a-0adeabefac69', N'oskaralkhateeb35@gmail.com', N'OSKARALKHATEEB35@GMAIL.COM', N'oskaralkhateeb35@gmail.com', N'OSKARALKHATEEB35@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEHJGMCodXrw9aBfVYPuw3rRuC/SWlIeA7CaAs6vaS9MSuOiJhl1QPIb4XCy5Wc3CXg==', N'6AMBEMUAWBUBITO6EHCM242RMUMUSAJL', N'4fe8b5c2-0c3e-4450-90ab-52008005bac9', NULL, 0, 0, NULL, 1, 0, NULL, N'', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'cea727d8-c7c3-47e8-bd3e-9b14e84cf927', N'victor.cantu@skylineacademy.com', N'VICTOR.CANTU@SKYLINEACADEMY.COM', N'victor.cantu@skylineacademy.com', N'VICTOR.CANTU@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAEEwrK+dzaXAZihn1zldFLrpwf46AFBV+juxcaoOSEI1jIFuhMMtluccEoWmhqCbRJQ==', N'3R7JCQ6JZZKBO2AYLXD4G6YUYLZEOFNU', N'fb7909cc-dc7f-4cbc-bde7-a6572d682267', NULL, 0, 0, NULL, 1, 0, NULL, N'ApplicationUser', NULL, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [Discriminator], [FName], [Lname]) VALUES (N'd152c5d8-95b8-479d-a49c-ee0f96fa0a88', N'camden.taylor@skylineacademy.com', N'CAMDEN.TAYLOR@SKYLINEACADEMY.COM', N'camden.taylor@skylineacademy.com', N'CAMDEN.TAYLOR@SKYLINEACADEMY.COM', 0, N'AQAAAAEAACcQAAAAECWRyFRA7lBNaB22NnaRJi+X9TIJb2gTwK01P+nRn4rsHV5dHIzz7lppSCif33o17Q==', N'ZNYFHENVKVXYAKH6TZFEFD265J7QRMUY', N'dba6072f-73b9-4613-87ee-814da6f29ce3', NULL, 0, 0, NULL, 1, 0, NULL, N'ApplicationUser', NULL, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 1/2/2024 2:03:18 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER DATABASE [SkylineAcademyIdentity] SET  READ_WRITE 
GO
