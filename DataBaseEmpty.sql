USE [master]
GO
/****** Object:  Database [Cinema]    Script Date: 13.12.2024 9:28:55 ******/
CREATE DATABASE [Cinema]
GO
ALTER DATABASE [Cinema] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cinema].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cinema] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cinema] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cinema] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cinema] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cinema] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cinema] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cinema] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cinema] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cinema] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cinema] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cinema] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cinema] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cinema] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cinema] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cinema] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cinema] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cinema] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cinema] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cinema] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cinema] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cinema] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cinema] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cinema] SET RECOVERY FULL 
GO
ALTER DATABASE [Cinema] SET  MULTI_USER 
GO
ALTER DATABASE [Cinema] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cinema] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cinema] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cinema] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Cinema] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Cinema]
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Actor](
	[IDActor] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Patronymic] [nvarchar](max) NULL,
	[Nickname] [nvarchar](max) NULL,
	[Image] [varbinary](max) NULL,
 CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED 
(
	[IDActor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ActorsInMovies]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorsInMovies](
	[IDActorsInMovies] [int] IDENTITY(1,1) NOT NULL,
	[IDMovie] [int] NOT NULL,
	[IDActor] [int] NOT NULL,
	[Character] [nvarchar](max) NULL,
 CONSTRAINT [PK_ActorsInMovies] PRIMARY KEY CLUSTERED 
(
	[IDActorsInMovies] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[IDCountry] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[IDCountry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[IDEmployee] [int] IDENTITY(1,1) NOT NULL,
	[IDRole] [int] NOT NULL,
	[Surname] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Patronymic] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](16) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Login] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[IDEmployee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[IDGenre] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Genere] PRIMARY KEY CLUSTERED 
(
	[IDGenre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Movie]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Movie](
	[IDMovie] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IDCountry] [int] NOT NULL,
	[YearOfPublication] [int] NOT NULL,
	[Timing] [float] NOT NULL,
	[AgeRating] [int] NOT NULL,
	[Cover] [varbinary](max) NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[IDMovie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MovieGenre]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieGenre](
	[IDMovieGenre] [int] IDENTITY(1,1) NOT NULL,
	[IDMovie] [int] NOT NULL,
	[IDGenre] [int] NOT NULL,
 CONSTRAINT [PK_MovieGenere] PRIMARY KEY CLUSTERED 
(
	[IDMovieGenre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IDRole] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[IDSession] [int] IDENTITY(1,1) NOT NULL,
	[IDMovie] [int] NOT NULL,
	[DateAndTimeSession] [datetime] NOT NULL,
	[TicketPrice] [money] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[IDSession] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[IDSettings] [int] IDENTITY(1,1) NOT NULL,
	[RowHall] [int] NOT NULL,
	[PlaceHall] [int] NOT NULL,
	[HiddenPlaces] [nvarchar](max) NULL,
	[DateTimeChange] [datetime] NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[IDSettings] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 13.12.2024 9:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[IDTicket] [int] IDENTITY(1,1) NOT NULL,
	[IDSession] [int] NOT NULL,
	[RowNumber] [int] NOT NULL,
	[PlaceNumber] [int] NOT NULL,
	[IDEmployee] [int] NOT NULL,
	[DateTimeBooking] [datetime] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[IDTicket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Country] ON 
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (1, N'Австралия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (2, N'Австрия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (3, N'Азербайджан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (4, N'Албания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (5, N'Алжир')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (6, N'Ангола')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (7, N'Андорра')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (8, N'Антигуа и Барбуда')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (9, N'Аргентина')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (10, N'Армения')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (11, N'Афганистан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (12, N'Багамы')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (13, N'Бангладеш')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (14, N'Барбадос')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (15, N'Бахрейн')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (16, N'Белиз')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (17, N'Белоруссия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (18, N'Бельгия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (19, N'Бенин')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (20, N'Болгария')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (21, N'Боливия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (22, N'Босния и Герцеговина')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (23, N'Ботсвана')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (24, N'Бразилия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (25, N'Бруней')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (26, N'Буркина-Фасо')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (27, N'Бурунди')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (28, N'Бутан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (29, N'Вануату')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (30, N'Великобритания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (31, N'Венгрия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (32, N'Венесуэла')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (33, N'Восточный Тимор')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (34, N'Вьетнам')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (35, N'Габон')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (36, N'Гаити')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (37, N'Гайана')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (38, N'Гамбия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (39, N'Гана')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (40, N'Гватемала')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (41, N'Гвинея')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (42, N'Гвинея-Бисау')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (43, N'Германия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (44, N'Гондурас')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (45, N'Гренада')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (46, N'Греция')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (47, N'Грузия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (48, N'Дания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (49, N'Джибути')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (50, N'Доминика')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (51, N'Доминикана')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (52, N'Египет')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (53, N'Замбия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (54, N'Зимбабве')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (55, N'Израиль')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (56, N'Индия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (57, N'Индонезия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (58, N'Иордания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (59, N'Ирак')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (60, N'Иран')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (61, N'Ирландия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (62, N'Исландия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (63, N'Испания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (64, N'Италия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (65, N'Йемен')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (66, N'Кабо-Верде')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (67, N'Казахстан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (68, N'Камбоджа')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (69, N'Камерун')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (70, N'Канада')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (71, N'Катар')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (72, N'Кения')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (73, N'Кипр')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (74, N'Кирибати')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (75, N'Китай')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (76, N'Колумбия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (77, N'Коморы')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (78, N'Конго')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (79, N'ДР Конго')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (80, N'КНДР')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (81, N'Корея')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (82, N'Коста-Рика')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (83, N'Кот-д’Ивуар')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (84, N'Куба')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (85, N'Кувейт')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (86, N'Кыргызстан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (87, N'Лаос')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (88, N'Латвия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (89, N'Лесото')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (90, N'Либерия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (91, N'Ливан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (92, N'Ливия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (93, N'Литва')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (94, N'Лихтенштейн')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (95, N'Люксембург')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (96, N'Маврикий')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (97, N'Мавритания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (98, N'Мадагаскар')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (99, N'Малави')
GO
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (100, N'Малайзия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (101, N'Мали')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (102, N'Мальдивы')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (103, N'Мальта')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (104, N'Марокко')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (105, N'Маршалловы Острова')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (106, N'Мексика')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (107, N'Микронезия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (108, N'Мозамбик')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (109, N'Молдавия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (110, N'Монако')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (111, N'Монголия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (112, N'Мьянма')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (113, N'Намибия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (114, N'Науру')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (115, N'Непал')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (116, N'Нигер')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (117, N'Нигерия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (118, N'Нидерланды')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (119, N'Никарагуа')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (120, N'Новая Зеландия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (121, N'Норвегия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (122, N'ОАЭ')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (123, N'Оман')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (124, N'Пакистан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (125, N'Палау')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (126, N'Панама')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (127, N'Папуа — Новая Гвинея')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (128, N'Парагвай')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (129, N'Перу')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (130, N'Польша')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (131, N'Португалия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (132, N'Россия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (133, N'Руанда')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (134, N'Румыния')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (135, N'Сальвадор')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (136, N'Самоа')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (137, N'Сан-Марино')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (138, N'Сан-Томе и Принсипи')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (139, N'Саудовская Аравия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (140, N'Северная Македония')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (141, N'Сейшелы')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (142, N'Сенегал')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (143, N'Сент-Винсент и Гренадины')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (144, N'Сент-Китс и Невис')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (145, N'Сент-Люсия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (146, N'Сербия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (147, N'Сингапур')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (148, N'Сирия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (149, N'Словакия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (150, N'Словения')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (151, N'США')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (152, N'Соломоновы Острова')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (153, N'Сомали')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (154, N'Судан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (155, N'Суринам')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (156, N'Сьерра-Леоне')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (157, N'Таджикистан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (158, N'Таиланд')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (159, N'Танзания')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (160, N'Того')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (161, N'Тонга')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (162, N'Тринидад и Тобаго')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (163, N'Тувалу')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (164, N'Тунис')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (165, N'Туркменистан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (166, N'Турция')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (167, N'Уганда')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (168, N'Узбекистан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (169, N'Украина')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (170, N'Уругвай')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (171, N'Фиджи')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (172, N'Филиппины')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (173, N'Финляндия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (174, N'Франция')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (175, N'Хорватия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (176, N'ЦАР')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (177, N'Чад')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (178, N'Черногория')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (179, N'Чехия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (180, N'Чили')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (181, N'Швейцария')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (182, N'Швеция')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (183, N'Шри-Ланка')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (184, N'Эквадор')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (185, N'Экваториальная Гвинея')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (186, N'Эритрея')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (187, N'Эсватини')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (188, N'Эстония')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (189, N'Эфиопия')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (190, N'ЮАР')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (191, N'Южный Судан')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (192, N'Ямайка')
INSERT [dbo].[Country] ([IDCountry], [Title]) VALUES (193, N'Япония')
SET IDENTITY_INSERT [dbo].[Country] OFF

SET IDENTITY_INSERT [dbo].[Role] ON 
INSERT [dbo].[Role] ([IDRole], [Title]) VALUES (1, N'Букер')
INSERT [dbo].[Role] ([IDRole], [Title]) VALUES (2, N'Кассир')
INSERT [dbo].[Role] ([IDRole], [Title]) VALUES (3, N'Директор')
INSERT [dbo].[Role] ([IDRole], [Title]) VALUES (4, N'Администратор')
SET IDENTITY_INSERT [dbo].[Role] OFF

GO
ALTER TABLE [dbo].[ActorsInMovies]  WITH CHECK ADD  CONSTRAINT [FK_ActorsInMovies_Actor] FOREIGN KEY([IDActor])
REFERENCES [dbo].[Actor] ([IDActor])
GO
ALTER TABLE [dbo].[ActorsInMovies] CHECK CONSTRAINT [FK_ActorsInMovies_Actor]
GO
ALTER TABLE [dbo].[ActorsInMovies]  WITH CHECK ADD  CONSTRAINT [FK_ActorsInMovies_Movie] FOREIGN KEY([IDMovie])
REFERENCES [dbo].[Movie] ([IDMovie])
GO
ALTER TABLE [dbo].[ActorsInMovies] CHECK CONSTRAINT [FK_ActorsInMovies_Movie]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Role] FOREIGN KEY([IDRole])
REFERENCES [dbo].[Role] ([IDRole])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Role]
GO
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_Movie_Country] FOREIGN KEY([IDCountry])
REFERENCES [dbo].[Country] ([IDCountry])
GO
ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_Movie_Country]
GO
ALTER TABLE [dbo].[MovieGenre]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenre_Genre] FOREIGN KEY([IDGenre])
REFERENCES [dbo].[Genre] ([IDGenre])
GO
ALTER TABLE [dbo].[MovieGenre] CHECK CONSTRAINT [FK_MovieGenre_Genre]
GO
ALTER TABLE [dbo].[MovieGenre]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenre_Movie] FOREIGN KEY([IDMovie])
REFERENCES [dbo].[Movie] ([IDMovie])
GO
ALTER TABLE [dbo].[MovieGenre] CHECK CONSTRAINT [FK_MovieGenre_Movie]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_Movie] FOREIGN KEY([IDMovie])
REFERENCES [dbo].[Movie] ([IDMovie])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_Movie]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Employee] FOREIGN KEY([IDEmployee])
REFERENCES [dbo].[Employee] ([IDEmployee])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Employee]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Session] FOREIGN KEY([IDSession])
REFERENCES [dbo].[Session] ([IDSession])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Session]
GO
USE [master]
GO
ALTER DATABASE [Cinema] SET  READ_WRITE 
GO
