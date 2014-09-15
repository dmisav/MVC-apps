USE [TestTaskDB]
GO
BEGIN TRANSACTION;

INSERT INTO [Province](NAME) VALUES ('Drenthe')
INSERT INTO [Province](NAME) VALUES ('Flevoland')
INSERT INTO [Province](NAME) VALUES ('Kiev')
INSERT INTO [Province](NAME) VALUES ('Odessa')

insert into [DatabaseVersion](VersionNumber, VersionDate, VersionUpdater) values (1, SYSDATETIME(), 'dmitriy.savchenko');
COMMIT;
GO

BEGIN TRANSACTION;

INSERT INTO [Country](NAME) VALUES ('Netherlands')
INSERT INTO [Country](NAME) VALUES ('Ukraine')

insert into [DatabaseVersion](VersionNumber, VersionDate, VersionUpdater) values (1, SYSDATETIME(), 'dmitriy.savchenko');
COMMIT;
GO

BEGIN TRANSACTION;

INSERT INTO [CountryProvince](COUNTRY_ID, PROVINCE_ID) VALUES ((SELECT TOP(1) ID FROM Country WHERE NAME LIKE '%Ukraine%'), (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Kiev%'))
INSERT INTO [CountryProvince](COUNTRY_ID, PROVINCE_ID) VALUES ((SELECT TOP(1) ID FROM Country WHERE NAME LIKE '%Ukraine%'), (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Odessa%'))
INSERT INTO [CountryProvince](COUNTRY_ID, PROVINCE_ID) VALUES ((SELECT TOP(1) ID FROM Country WHERE NAME LIKE '%Netherlands%'), (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Drenthe%'))
INSERT INTO [CountryProvince](COUNTRY_ID, PROVINCE_ID) VALUES ((SELECT TOP(1) ID FROM Country WHERE NAME LIKE '%Netherlands%'), (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Flevoland%'))

insert into [DatabaseVersion](VersionNumber, VersionDate, VersionUpdater) values (1, SYSDATETIME(), 'dmitriy.savchenko');
COMMIT;
GO

BEGIN TRANSACTION;

INSERT INTO [User](EMAIL, [PASSWORD], PROVINCE) VALUES ('testUser1@email.com', 'abc1@', (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Kiev%'))
INSERT INTO [User](EMAIL, [PASSWORD], PROVINCE) VALUES ('testUser2@email.com', 'abc2@', (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Odessa%'))
INSERT INTO [User](EMAIL, [PASSWORD], PROVINCE) VALUES ('testUser3@email.com', 'abc3@', (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Drenthe%'))
INSERT INTO [User](EMAIL, [PASSWORD], PROVINCE) VALUES ('testUser4@email.com', 'abc4@', (SELECT TOP(1) ID FROM Province WHERE NAME LIKE '%Flevoland%'))

insert into [DatabaseVersion](VersionNumber, VersionDate, VersionUpdater) values (1, SYSDATETIME(), 'dmitriy.savchenko');
COMMIT;
GO