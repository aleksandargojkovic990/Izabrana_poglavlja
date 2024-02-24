USE [master]
GO
/****** Object:  Database [IP_projekat]    Script Date: 24.2.2024. 20:13:07 ******/
CREATE DATABASE [IP_projekat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IP_projekat', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\IP_projekat.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IP_projekat_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\IP_projekat_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [IP_projekat] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IP_projekat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IP_projekat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IP_projekat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IP_projekat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IP_projekat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IP_projekat] SET ARITHABORT OFF 
GO
ALTER DATABASE [IP_projekat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IP_projekat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IP_projekat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IP_projekat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IP_projekat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IP_projekat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IP_projekat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IP_projekat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IP_projekat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IP_projekat] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IP_projekat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IP_projekat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IP_projekat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IP_projekat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IP_projekat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IP_projekat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IP_projekat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IP_projekat] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IP_projekat] SET  MULTI_USER 
GO
ALTER DATABASE [IP_projekat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IP_projekat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IP_projekat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IP_projekat] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IP_projekat] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IP_projekat] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [IP_projekat] SET QUERY_STORE = ON
GO
ALTER DATABASE [IP_projekat] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [IP_projekat]
GO
/****** Object:  Table [dbo].[Cenovnik]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cenovnik](
	[Sifra] [int] IDENTITY(1,1) NOT NULL,
	[SifraUsluge] [int] NOT NULL,
	[DatumOd] [datetime] NOT NULL,
	[DatumDo] [datetime] NULL,
	[Cena] [money] NOT NULL,
 CONSTRAINT [PK_Cenovnik] PRIMARY KEY CLUSTERED 
(
	[Sifra] ASC,
	[SifraUsluge] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Korisnik]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Korisnik](
	[Sifra] [int] IDENTITY(1,1) NOT NULL,
	[Jmbg] [varchar](13) NOT NULL,
	[Ime] [varchar](50) NOT NULL,
	[Prezime] [varchar](50) NOT NULL,
	[BrojTelefona] [varchar](30) NOT NULL,
	[BrojLicneKarte] [varchar](9) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paket]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paket](
	[Sifra] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaketUsluga]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaketUsluga](
	[SifraUsluge] [int] NOT NULL,
	[SifraPaketa] [int] NOT NULL,
	[Popust] [money] NOT NULL,
 CONSTRAINT [PK_PaketUsluga] PRIMARY KEY CLUSTERED 
(
	[SifraUsluge] ASC,
	[SifraPaketa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pretplata]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pretplata](
	[SifraUsluge] [int] NOT NULL,
	[BrojUgovora] [int] NOT NULL,
	[JeAktivna] [bit] NOT NULL,
 CONSTRAINT [PK_Pretplata] PRIMARY KEY CLUSTERED 
(
	[SifraUsluge] ASC,
	[BrojUgovora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ugovor]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ugovor](
	[BrojUgovora] [int] IDENTITY(1,1) NOT NULL,
	[DatumOd] [datetime] NOT NULL,
	[DatumDo] [datetime] NOT NULL,
	[SifraKorisnika] [int] NOT NULL,
	[SifraPaketa] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BrojUgovora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usluga]    Script Date: 24.2.2024. 20:13:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usluga](
	[Sifra] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Sifra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cenovnik] ON 

INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (72, 36, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL, 500.0000)
INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (73, 37, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL, 1200.0000)
INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (74, 38, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL, 500.0000)
INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (76, 36, CAST(N'2009-01-01T00:00:00.000' AS DateTime), CAST(N'2010-01-01T00:00:00.000' AS DateTime), 400.0000)
INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (77, 39, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL, 600.0000)
INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (78, 40, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL, 600.0000)
INSERT [dbo].[Cenovnik] ([Sifra], [SifraUsluge], [DatumOd], [DatumDo], [Cena]) VALUES (79, 41, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL, 1200.0000)
SET IDENTITY_INSERT [dbo].[Cenovnik] OFF
GO
SET IDENTITY_INSERT [dbo].[Korisnik] ON 

INSERT [dbo].[Korisnik] ([Sifra], [Jmbg], [Ime], [Prezime], [BrojTelefona], [BrojLicneKarte]) VALUES (45, N'1234567890123', N'Pera', N'Perić', N'+381611234567', N'123456789')
INSERT [dbo].[Korisnik] ([Sifra], [Jmbg], [Ime], [Prezime], [BrojTelefona], [BrojLicneKarte]) VALUES (47, N'1234567890124', N'Laza', N'Lazić', N'+381611234568', N'123456780')
SET IDENTITY_INSERT [dbo].[Korisnik] OFF
GO
SET IDENTITY_INSERT [dbo].[Paket] ON 

INSERT [dbo].[Paket] ([Sifra], [Naziv]) VALUES (28, N'KTV')
INSERT [dbo].[Paket] ([Sifra], [Naziv]) VALUES (29, N'KTV+NET')
INSERT [dbo].[Paket] ([Sifra], [Naziv]) VALUES (30, N'KTV+FIX')
INSERT [dbo].[Paket] ([Sifra], [Naziv]) VALUES (32, N'KTV+NET+FIX')
SET IDENTITY_INSERT [dbo].[Paket] OFF
GO
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (36, 28, 0.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (36, 29, 5.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (36, 30, 5.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (36, 32, 10.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (37, 29, 5.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (37, 32, 10.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (38, 30, 10.0000)
INSERT [dbo].[PaketUsluga] ([SifraUsluge], [SifraPaketa], [Popust]) VALUES (38, 32, 15.0000)
GO
INSERT [dbo].[Pretplata] ([SifraUsluge], [BrojUgovora], [JeAktivna]) VALUES (39, 5, 0)
INSERT [dbo].[Pretplata] ([SifraUsluge], [BrojUgovora], [JeAktivna]) VALUES (40, 7, 0)
INSERT [dbo].[Pretplata] ([SifraUsluge], [BrojUgovora], [JeAktivna]) VALUES (41, 5, 1)
GO
SET IDENTITY_INSERT [dbo].[Ugovor] ON 

INSERT [dbo].[Ugovor] ([BrojUgovora], [DatumOd], [DatumDo], [SifraKorisnika], [SifraPaketa]) VALUES (5, CAST(N'2011-01-01T00:00:00.000' AS DateTime), CAST(N'2013-01-01T00:00:00.000' AS DateTime), 45, 32)
INSERT [dbo].[Ugovor] ([BrojUgovora], [DatumOd], [DatumDo], [SifraKorisnika], [SifraPaketa]) VALUES (6, CAST(N'2013-01-01T00:00:00.000' AS DateTime), CAST(N'2015-01-01T00:00:00.000' AS DateTime), 45, 28)
INSERT [dbo].[Ugovor] ([BrojUgovora], [DatumOd], [DatumDo], [SifraKorisnika], [SifraPaketa]) VALUES (7, CAST(N'2024-02-14T00:00:00.000' AS DateTime), CAST(N'2026-02-14T00:00:00.000' AS DateTime), 47, 30)
SET IDENTITY_INSERT [dbo].[Ugovor] OFF
GO
SET IDENTITY_INSERT [dbo].[Usluga] ON 

INSERT [dbo].[Usluga] ([Sifra], [Naziv]) VALUES (36, N'KTV')
INSERT [dbo].[Usluga] ([Sifra], [Naziv]) VALUES (37, N'NET')
INSERT [dbo].[Usluga] ([Sifra], [Naziv]) VALUES (38, N'FIX')
INSERT [dbo].[Usluga] ([Sifra], [Naziv]) VALUES (39, N'Javna IP adresa')
INSERT [dbo].[Usluga] ([Sifra], [Naziv]) VALUES (40, N'Arena sport')
INSERT [dbo].[Usluga] ([Sifra], [Naziv]) VALUES (41, N'Disney kanali')
SET IDENTITY_INSERT [dbo].[Usluga] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Korisnik__78A3F55441DEE275]    Script Date: 24.2.2024. 20:13:07 ******/
ALTER TABLE [dbo].[Korisnik] ADD UNIQUE NONCLUSTERED 
(
	[BrojLicneKarte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Korisnik__9EF480F6EF04CACF]    Script Date: 24.2.2024. 20:13:07 ******/
ALTER TABLE [dbo].[Korisnik] ADD UNIQUE NONCLUSTERED 
(
	[Jmbg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cenovnik]  WITH CHECK ADD  CONSTRAINT [FK_CenovnikUsluga] FOREIGN KEY([SifraUsluge])
REFERENCES [dbo].[Usluga] ([Sifra])
GO
ALTER TABLE [dbo].[Cenovnik] CHECK CONSTRAINT [FK_CenovnikUsluga]
GO
ALTER TABLE [dbo].[PaketUsluga]  WITH CHECK ADD  CONSTRAINT [FK_PaketUslugaPaket] FOREIGN KEY([SifraPaketa])
REFERENCES [dbo].[Paket] ([Sifra])
GO
ALTER TABLE [dbo].[PaketUsluga] CHECK CONSTRAINT [FK_PaketUslugaPaket]
GO
ALTER TABLE [dbo].[PaketUsluga]  WITH CHECK ADD  CONSTRAINT [FK_PaketUslugaUsluga] FOREIGN KEY([SifraUsluge])
REFERENCES [dbo].[Usluga] ([Sifra])
GO
ALTER TABLE [dbo].[PaketUsluga] CHECK CONSTRAINT [FK_PaketUslugaUsluga]
GO
ALTER TABLE [dbo].[Pretplata]  WITH CHECK ADD  CONSTRAINT [FK_PretplataUgovor] FOREIGN KEY([BrojUgovora])
REFERENCES [dbo].[Ugovor] ([BrojUgovora])
GO
ALTER TABLE [dbo].[Pretplata] CHECK CONSTRAINT [FK_PretplataUgovor]
GO
ALTER TABLE [dbo].[Pretplata]  WITH CHECK ADD  CONSTRAINT [FK_PretplataUsluga] FOREIGN KEY([SifraUsluge])
REFERENCES [dbo].[Usluga] ([Sifra])
GO
ALTER TABLE [dbo].[Pretplata] CHECK CONSTRAINT [FK_PretplataUsluga]
GO
ALTER TABLE [dbo].[Ugovor]  WITH CHECK ADD  CONSTRAINT [FK_UgovorKorisnik] FOREIGN KEY([SifraKorisnika])
REFERENCES [dbo].[Korisnik] ([Sifra])
GO
ALTER TABLE [dbo].[Ugovor] CHECK CONSTRAINT [FK_UgovorKorisnik]
GO
ALTER TABLE [dbo].[Ugovor]  WITH CHECK ADD  CONSTRAINT [FK_UgovorPaket] FOREIGN KEY([SifraPaketa])
REFERENCES [dbo].[Paket] ([Sifra])
GO
ALTER TABLE [dbo].[Ugovor] CHECK CONSTRAINT [FK_UgovorPaket]
GO
USE [master]
GO
ALTER DATABASE [IP_projekat] SET  READ_WRITE 
GO
