
-- Struktura tabulky "akce"
--
CREATE TABLE "akce" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "jmeno" varchar(40) NOT NULL,
  "popis" varchar(160),
  "titul_id" int NOT NULL,
  "datum" datetime,
  "cas" varchar(13),
  CONSTRAINT PK_akce_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "akce_naklady"
--
CREATE TABLE "akce_naklady" (
  "akce_id" int NOT NULL,
  "naklady_id" int NOT NULL,
  "cena" int NOT NULL,
  CONSTRAINT PK_akce_naklady_pk_id PRIMARY KEY CLUSTERED (akce_id, naklady_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "akce_produkcni_listy"
--
CREATE TABLE "akce_produkcni_listy" (
  "akce_id" int NOT NULL,
  "produkcni_listy_id" int NOT NULL,
  "poznamka" varchar(40),
  "termin" varchar(25),
  "status" int,
  "zodpovedna_osoba" varchar(35),
  CONSTRAINT PK_akce_produkcni_listy_pk_id PRIMARY KEY CLUSTERED (akce_id, produkcni_listy_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "fermany"
--
CREATE TABLE "fermany" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "akce_id" int NOT NULL,
  "lokace_id" int NOT NULL,
  "datum" datetime,
  "cas" varchar(13),
  "orchestr" varchar(40),
  "sbor" varchar(40),
  "solisti" varchar(40),
  CONSTRAINT PK_fermany_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "kontakty"
--
CREATE TABLE "kontakty" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "jmeno" varchar(15) NOT NULL,
  "prijmeni" varchar(20) NOT NULL,
  "email" varchar(40),
  "telefon" varchar(13),
  CONSTRAINT PK_kontakty_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "lokace"
--
CREATE TABLE "lokace" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "jmeno" varchar(40) NOT NULL,
  "adresa" varchar(50),
  "gps" varchar(40),
  "link" varchar(200),
  CONSTRAINT PK_lokace_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "naklady"
--
CREATE TABLE "naklady" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "jmeno" varchar(40) NOT NULL,
  "typ" int NOT NULL,
  CONSTRAINT PK_naklady_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "nastroje"
--
CREATE TABLE "nastroje" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "jmeno" varchar(40) NOT NULL,
  "typ" int NOT NULL,
  CONSTRAINT PK_nastroje_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "osoby"
--
CREATE TABLE "osoby" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "id" int NOT NULL UNIQUE,
  "jmeno" varchar(15) NOT NULL,
  "prijmeni" varchar(20) NOT NULL,
  "telefon" varchar(13),
  "email" varchar(40),
  "mesto" varchar(20),
  "psc" varchar(10),
  "narodnost" varchar(25),
  "cislo_uctu" varchar(22),
  "adresa" varchar(30),
  "datum_narozeni" datetime,
  "misto_narozeni" varchar(20),
  "ic_dic" varchar(25),
  "poznamka" varchar(160),
  CONSTRAINT PK_osoby_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "osoby_akce"
--
CREATE TABLE "osoby_akce" (
  "osoby_id" int NOT NULL,
  "akce_id" int NOT NULL,
  "nastroje_id" int NOT NULL,
  "poznamka" varchar(160),
  "honorar" int NOT NULL,
  "doprava" int NOT NULL,
  "srazkova_dan" int NOT NULL,
  "vyplaceno" int NOT NULL,
  "stav" int NOT NULL,
  CONSTRAINT PK_osoby_akce_pk_id PRIMARY KEY CLUSTERED (osoby_id, akce_id, nastroje_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "osoby_nastroje"
--
CREATE TABLE "osoby_nastroje" (
  "osoby_id" int NOT NULL,
  "nastroje_id" int NOT NULL,
  CONSTRAINT PK_osoby_nastroje_pk_id PRIMARY KEY CLUSTERED (osoby_id, nastroje_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "produkcni_listy"
--
CREATE TABLE "produkcni_listy" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "jmeno_aktivity" varchar(100) NOT NULL,
  "typ" int NOT NULL,
  CONSTRAINT PK_produkcni_listy_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "smlouvy"
--
CREATE TABLE "smlouvy" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "spolecnost" varchar(40) NOT NULL,
  "ic" varchar(60) NOT NULL,
  "sidlo" varchar(60) NOT NULL,
  "zastoupeni" varchar(110) NOT NULL,
  "jmeno" varchar(35) NOT NULL,
  "bytem" varchar(50) NOT NULL,
  "telefon" varchar(13) NOT NULL,
  "email" varchar(40) NOT NULL,
  "cislo_uctu" varchar(22) NOT NULL,
  "datum_narozeni" datetime NOT NULL,
  "datum_spolecnost" datetime NOT NULL,
  "datum_osoba" datetime NOT NULL,
  "osoby_id" int NOT NULL,
  "text_smlouvy_id" int NOT NULL,
  CONSTRAINT PK_smlouvy_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "text_smlouvy"
--
CREATE TABLE "text_smlouvy" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "text1" varchar(450) NOT NULL,
  "text2" varchar(2000) NOT NULL,
  "text3" varchar(1000) NOT NULL,
  "text4" varchar(1000) NOT NULL,
  "text5" varchar(3000) NOT NULL,
  "text6" varchar(2000) NOT NULL,
  CONSTRAINT PK_text_smlouvy_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "titul"
--
CREATE TABLE "titul" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "nazev" varchar(40) NOT NULL,
  "autor" varchar(35),
  "poznamka" varchar(160),
  CONSTRAINT PK_titul_pk_id PRIMARY KEY CLUSTERED (pk_id)
);
-- --------------------------------------------------------
--
-- Struktura tabulky "ubytovani"
--
CREATE TABLE "ubytovani" (
  "pk_id" int IDENTITY(1,1) NOT NULL UNIQUE,
  "osoby_id" int NOT NULL,
  "lokace_id" int NOT NULL,
  "akce_id" int NOT NULL,
  "pokoj" int,
  "cena1" int NOT NULL,
  "cena2" int NOT NULL,
  "cena3" int NOT NULL,
  CONSTRAINT PK_ubytovani_pk_id PRIMARY KEY CLUSTERED (pk_id)
);