-- Vytvorenie databázy
CREATE DATABASE CestovnePrikazyDB;
GO

USE CestovnePrikazyDB;

-- Entita Zamestnanec
CREATE TABLE Zamestnanec (
    osobne_cislo VARCHAR(10) PRIMARY KEY,
    krstne_meno VARCHAR(50) NOT NULL,
    priezvisko VARCHAR(50) NOT NULL,
    datum_narodenia DATE NOT NULL,
    rodne_cislo VARCHAR(11) NOT NULL
);

-- Create unique index
CREATE UNIQUE INDEX UX_Zamestnanec_OsobneCislo ON Zamestnanec(osobne_cislo);

-- Create non-unique indexes for other columns
CREATE INDEX IX_Zamestnanec_KrstneMeno ON Zamestnanec(krstne_meno);
CREATE INDEX IX_Zamestnanec_Priezvisko ON Zamestnanec(priezvisko);
CREATE INDEX IX_Zamestnanec_DatumNarodenia ON Zamestnanec(datum_narodenia);
CREATE INDEX IX_Zamestnanec_RodneCislo ON Zamestnanec(rodne_cislo);


-- Create a Full-Text Catalog
CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;
GO

-- Create a Full-Text Index
CREATE FULLTEXT INDEX ON Zamestnanec
(
--The LCID for Slovak is 1051.
    osobne_cislo Language 1051, 
    krstne_meno Language 1051,
    priezvisko Language 1051,
    rodne_cislo Language 1051
)
KEY INDEX UX_Zamestnanec_OsobneCislo -- This should be the primary key index of the Zamestnanec table
ON ftCatalog;
GO




-- Entita Mesto
CREATE TABLE Mesto (
    mesto_id INT IDENTITY(1,1) PRIMARY KEY,
    nazov_mesta VARCHAR(100) NOT NULL,
    stat VARCHAR(50) NOT NULL,
    zemepisna_sirka DECIMAL(9,6) NOT NULL,
    zemepisna_dlzka DECIMAL(9,6) NOT NULL
);

-- Číselníková entita Stav
CREATE TABLE Stav (
    stav_id tinyint IDENTITY(1,1) PRIMARY KEY,
    kod_stavu VARCHAR(10) NOT NULL UNIQUE,
    nazov_stavu VARCHAR(20) NOT NULL UNIQUE
);

-- Číselníková entita DopravaTyp
CREATE TABLE DopravaTyp (
    doprava_typ_id tinyint IDENTITY(1,1) PRIMARY KEY,
    kod_typu VARCHAR(10) NOT NULL UNIQUE,
    nazov_typu VARCHAR(20) NOT NULL UNIQUE
);

-- Entita CestovnyPrikaz
CREATE TABLE CestovnyPrikaz (
    cp_id INT IDENTITY(1,1) PRIMARY KEY,
    datum_vytvorenia DATETIME DEFAULT GETDATE(),
    ucastnik VARCHAR(10) NOT NULL,
    miesto_zaciatku INT NOT NULL,
    miesto_konca INT NOT NULL,
    datum_cas_zaciatku DATETIME NOT NULL,
    datum_cas_konca DATETIME NOT NULL,
    stav_id INT NOT NULL,
    FOREIGN KEY (ucastnik) REFERENCES Zamestnanec(osobne_cislo),
    FOREIGN KEY (miesto_zaciatku) REFERENCES Mesto(mesto_id),
    FOREIGN KEY (miesto_konca) REFERENCES Mesto(mesto_id),
    FOREIGN KEY (stav_id) REFERENCES Stav(stav_id)
);

-- Entita Doprava pre Cestovny Prikaz
CREATE TABLE Doprava (
    doprava_id INT IDENTITY(1,1) PRIMARY KEY,
    cp_id INT NOT NULL,
    doprava_typ_id INT NOT NULL,
    FOREIGN KEY (cp_id) REFERENCES CestovnyPrikaz(cp_id),
    FOREIGN KEY (doprava_typ_id) REFERENCES DopravaTyp(doprava_typ_id)
);

-- Vytvorenie indexov pre cudzie kľúče
CREATE INDEX idx_cp_ucastnik ON CestovnyPrikaz(ucastnik);
CREATE INDEX idx_cp_miesto_zaciatku ON CestovnyPrikaz(miesto_zaciatku);
CREATE INDEX idx_cp_miesto_konca ON CestovnyPrikaz(miesto_konca);
CREATE INDEX idx_cp_stav_id ON CestovnyPrikaz(stav_id);
CREATE INDEX idx_doprava_cp_id ON Doprava(cp_id);
CREATE INDEX idx_doprava_doprava_typ_id ON Doprava(doprava_typ_id);

-- Naplnenie číselníkovej tabulky Stav
INSERT INTO Stav (kod_stavu, nazov_stavu) VALUES 
('VYT', 'Vytvoreny'), 
('SCH', 'Schvaleny'), 
('VYC', 'Vyuctovany'), 
('ZRU', 'Zruseny');

-- Naplnenie číselníkovej tabulky DopravaTyp
INSERT INTO DopravaTyp (kod_typu, nazov_typu) VALUES 
('AUTO', 'Sluzobne auto'), 
('BUS', 'Autobus'), 
('MHD', 'MHD'), 
('PESO', 'Peso'), 
('VLAK', 'Vlak'), 
('TAXI', 'Taxi'), 
('LIET', 'Lietadlo');

-- Inicializačné dáta pre Zamestnanec
INSERT INTO Zamestnanec (osobne_cislo, krstne_meno, priezvisko, datum_narodenia, rodne_cislo) VALUES
('EMP001', 'Ján', 'Novák', '1985-01-15', '8501151234'),
('EMP002', 'Eva', 'Kováčová', '1990-05-22', '9005225432'),
('EMP003', 'Peter', 'Horváth', '1978-03-11', '7803117890');

-- Inicializačné dáta pre Mesto
INSERT INTO Mesto (nazov_mesta, stat, zemepisna_sirka, zemepisna_dlzka) VALUES
('Bratislava', 'Slovensko', 48.148598, 17.107748),
('Košice', 'Slovensko', 48.716385, 21.261074),
('Žilina', 'Slovensko', 49.223146, 18.739491);

-- Inicializačné dáta pre CestovnyPrikaz
INSERT INTO CestovnyPrikaz (datum_vytvorenia, ucastnik, miesto_zaciatku, miesto_konca, datum_cas_zaciatku, datum_cas_konca, stav_id) VALUES
(GETDATE(), 'EMP001', 1, 2, '2024-07-01 08:00', '2024-07-01 17:00', 1),
(GETDATE(), 'EMP002', 2, 3, '2024-07-02 09:00', '2024-07-02 18:00', 2),
(GETDATE(), 'EMP003', 3, 1, '2024-07-03 07:00', '2024-07-03 16:00', 3);

-- Inicializačné dáta pre Doprava
INSERT INTO Doprava (cp_id, doprava_typ_id) VALUES
(1, 1),
(1, 2),
(2, 3),
(3, 4);