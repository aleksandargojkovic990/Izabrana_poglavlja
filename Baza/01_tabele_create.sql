-- KORISNIK
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Korisnik'))
BEGIN
    PRINT('Tabela dbo.Korisnik već postoji.')
END
ELSE
BEGIN
	CREATE TABLE Korisnik
	(
		Sifra INT IDENTITY(1, 1) PRIMARY KEY
		, Jmbg VARCHAR(13) UNIQUE NOT NULL
		, Ime VARCHAR(50) NOT NULL
		, Prezime VARCHAR(50) NOT NULL
		, BrojTelefona VARCHAR(30) NOT NULL
		, BrojLicneKarte VARCHAR(9) UNIQUE NOT NULL
	)
END



-- PAKET
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Paket'))
BEGIN
    PRINT('Tabela dbo.Paket već postoji.')
END
ELSE
BEGIN
	CREATE TABLE Paket
	(
		Sifra INT IDENTITY(1, 1) PRIMARY KEY
		, Naziv NVARCHAR(50) NOT NULL
	) 
END



-- UGOVOR
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Ugovor'))
BEGIN
    PRINT('Tabela dbo.Ugovor već postoji.')
END
ELSE
BEGIN
	CREATE TABLE Ugovor
	(
		BrojUgovora INT IDENTITY(1, 1) PRIMARY KEY
		, DatumOd DATETIME NOT NULL
		, DatumDo DATETIME NOT NULL
		, SifraKorisnika INT NOT NULL
		, SifraPaketa INT NOT NULL
		, CONSTRAINT FK_UgovorKorisnik FOREIGN KEY (SifraKorisnika) REFERENCES Korisnik (Sifra)
		, CONSTRAINT FK_UgovorPaket FOREIGN KEY (SifraPaketa) REFERENCES Paket (Sifra)
	) 
END



-- USLUGA
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Usluga'))
BEGIN
    PRINT('Tabela dbo.Usluga već postoji.')
END
ELSE
BEGIN
	CREATE TABLE Usluga
	(
		Sifra INT IDENTITY(1, 1) PRIMARY KEY
		, Naziv NVARCHAR(50) NOT NULL
	) 
END



-- CENOVNIK
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Cenovnik'))
BEGIN
    PRINT('Tabela dbo.Cenovnik već postoji.')
END
ELSE
BEGIN
	CREATE TABLE Cenovnik
	(
		Sifra INT IDENTITY(1, 1)
		, SifraUsluge INT
		, DatumOd DATETIME NOT NULL
		, DatumDo DATETIME NULL
		, Cena MONEY NOT NULL
		, CONSTRAINT PK_Cenovnik PRIMARY KEY (Sifra, SifraUsluge)
		, CONSTRAINT FK_CenovnikUsluga FOREIGN KEY (SifraUsluge) REFERENCES Usluga (Sifra)
	) 
END



-- PAKETUSLUGA
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PaketUsluga'))
BEGIN
    PRINT('Tabela dbo.PaketUsluga već postoji.')
END
ELSE
BEGIN
	CREATE TABLE PaketUsluga
	(
		SifraUsluge INT
		, SifraPaketa INT
		, Popust MONEY NOT NULL
		, CONSTRAINT PK_PaketUsluga PRIMARY KEY (SifraUsluge, SifraPaketa)
		, CONSTRAINT FK_PaketUslugaUsluga FOREIGN KEY (SifraUsluge) REFERENCES Usluga (Sifra)
		, CONSTRAINT FK_PaketUslugaPaket FOREIGN KEY (SifraPaketa) REFERENCES Paket (Sifra)
	) 
END



-- PRETPLATA
IF (EXISTS (SELECT * 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Pretplata'))
BEGIN
    PRINT('Tabela dbo.Pretplata već postoji.')
END
ELSE
BEGIN
	CREATE TABLE Pretplata
	(
		SifraUsluge INT
		, BrojUgovora INT
		, JeAktivna BIT NOT NULL
		, CONSTRAINT PK_Pretplata PRIMARY KEY (SifraUsluge, BrojUgovora)
		, CONSTRAINT FK_PretplataUsluga FOREIGN KEY (SifraUsluge) REFERENCES Usluga (Sifra)
		, CONSTRAINT FK_PretplataUgovor FOREIGN KEY (BrojUgovora) REFERENCES Ugovor (BrojUgovora)
	) 
END