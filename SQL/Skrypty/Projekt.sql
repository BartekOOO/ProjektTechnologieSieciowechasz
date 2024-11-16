CREATE SCHEMA PROJEKT;
GO
CREATE LOGIN NowyUzytkownik WITH PASSWORD = 'TwojeHaslo';
CREATE USER NowyUzytkownik FOR LOGIN NowyUzytkownik;
ALTER ROLE db_owner ADD MEMBER NowyUzytkownik;
