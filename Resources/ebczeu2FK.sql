GO
ALTER TABLE akce_produkcni_listy 
ADD CONSTRAINT FK_akce_produkcni_listy_akce FOREIGN KEY (akce_id) 
    REFERENCES akce (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE akce_produkcni_listy 
ADD CONSTRAINT FK_akce_produkcni_listy_produkcni_listy FOREIGN KEY (produkcni_listy_id) 
    REFERENCES produkcni_listy (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE akce_naklady 
ADD CONSTRAINT FK_akce_naklady_akce FOREIGN KEY (akce_id) 
    REFERENCES akce (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE akce_naklady 
ADD CONSTRAINT FK_akce_naklady_naklady FOREIGN KEY (naklady_id) 
    REFERENCES naklady (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE osoby_akce 
ADD CONSTRAINT FK_osoby_akce_akce FOREIGN KEY (akce_id) 
    REFERENCES akce (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE osoby_akce 
ADD CONSTRAINT FK_osoby_akce_osoby FOREIGN KEY (osoby_id) 
    REFERENCES osoby (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE osoby_akce 
ADD CONSTRAINT FK_osoby_akce_nastroje FOREIGN KEY (nastroje_id) 
    REFERENCES nastroje (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE osoby_nastroje 
ADD CONSTRAINT FK_osoby_nastroje_nastroje FOREIGN KEY (nastroje_id) 
    REFERENCES nastroje (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE osoby_nastroje 
ADD CONSTRAINT FK_osoby_nastroje_osoby FOREIGN KEY (osoby_id) 
    REFERENCES osoby (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE fermany 
ADD CONSTRAINT FK_fermany_akce FOREIGN KEY (akce_id) 
    REFERENCES akce (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE fermany 
ADD CONSTRAINT FK_fermany_lokace FOREIGN KEY (lokace_id) 
    REFERENCES lokace (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE ubytovani 
ADD CONSTRAINT FK_ubytovani_osoby FOREIGN KEY (osoby_id) 
    REFERENCES osoby (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE ubytovani 
ADD CONSTRAINT FK_ubytovani_lokace FOREIGN KEY (lokace_id) 
    REFERENCES lokace (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE ubytovani 
ADD CONSTRAINT FK_ubytovani_akce FOREIGN KEY (akce_id) 
    REFERENCES akce (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE smlouvy 
ADD CONSTRAINT FK_smlouvy_osoby FOREIGN KEY (osoby_id) 
    REFERENCES osoby (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE smlouvy 
ADD CONSTRAINT FK_smlouvy_text_smlouvy FOREIGN KEY (text_smlouvy_id) 
    REFERENCES text_smlouvy (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO
GO
ALTER TABLE akce
ADD CONSTRAINT FK_akce_titul FOREIGN KEY (titul_id) 
    REFERENCES titul (pk_id) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
GO