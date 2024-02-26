if exists (select 1 from master.dbo.sysdatabases
		where name = 'WFTDA')
BEGIN	
drop database [WFTDA]
		print '' print '*** dropping database [WFTDA]***'
end 
GO

print '' print '*** creating database [WFTDA] ***'
GO
create database [WFTDA]
GO

print '' print '*** using database [WFTDA] ***'
GO

use [WFTDA]
/******************
Create the [dbo].[League] table
***************/
print '' Print '***Create the [dbo].[League] table***' 
 go 


CREATE TABLE [dbo].[League](


[LeagueID]	[nvarchar](100)	not null	
,[Region]	[nvarchar](100)	not null	
,[Gender]	[nvarchar](100)	not null
,[Active]		[bit]									not null	DEFAULT '1'	
,CONSTRAINT [PK_League] PRIMARY KEY ([LeagueID])
)
go
/******************
Create the [dbo].[Location] table
***************/
print '' Print '***Create the [dbo].[Location] table***' 
 go 


CREATE TABLE [dbo].[Location](


[LocationID]	[nvarchar](100)	not null	
,[LeagueID]	[nvarchar](100)	not null	
,[ContactPhone]	[nvarchar](14)	null	
,[City]	[nvarchar](100)	not null	
,[State]	[nvarchar](50)	not null	
,[Zip]	[nvarchar](5)	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Location] PRIMARY KEY ([LocationID])
,CONSTRAINT [fk_Location_League] foreign key ([LeagueID]) references [League]([LeagueID])
)
go
/******************
Create the [dbo].[Team] table
***************/
print '' Print '***Create the [dbo].[Team] table***' 
 go 


CREATE TABLE [dbo].[Team](


[TeamId]	[nvarchar](50)	 not null	
,[LeagueID]	[nvarchar](100)	null	
,[MonthlyDue] money
,[City]	[nvarchar](100)	not null	
,[State]	[nvarchar](50)	not null	
,[Zip]	[nvarchar](5)	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Team] PRIMARY KEY ([TeamId])
,CONSTRAINT [fk_Team_League] foreign key ([LeagueID]) references [League]([LeagueID])
)
go
/******************
Create the [dbo].[Game] table
***************/
print '' Print '***Create the [dbo].[Game] table***' 
 go 


CREATE TABLE [dbo].[Game](


[GameID]	[Int] identity(100000,1) 	not null	
,[LocationID]	[nvarchar](100)	null	
,[Date]	[datetime]	null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Game] PRIMARY KEY ([GameID])
,CONSTRAINT [fk_Game_Location] foreign key ([LocationId]) references [Location]([LocationId])
)
go
/******************
Create the [dbo].[Game_Team_Line] table
***************/
print '' Print '***Create the [dbo].[Game_Team_Line] table***' 
 go 


CREATE TABLE [dbo].[Game_Team_Line](


[GameID]	[Int]	not null	
,[TeamId]	[nvarchar](50)	not null	
,[Points]	[int]	null
,[Active]		[bit]									not null	DEFAULT '1'	
,CONSTRAINT [PK_Game_Team_Line] PRIMARY KEY ([GameID] , [TeamId])
,CONSTRAINT [fk_Game_Team_Line_Game] foreign key ([GameId]) references [Game]([GameId])
,CONSTRAINT [fk_Game_Team_Line_Team] foreign key ([TeamId]) references [Team]([TeamId])
)
go
/******************
Create the [dbo].[Skater] table
***************/
print '' Print '***Create the [dbo].[Skater] table***' 
 go 


CREATE TABLE [dbo].[Skater](


[SkaterID]	[nvarchar](50)	not null	
,[TeamID]	[nvarchar](50)	not null DEFAULT 'Default'	
,[GivenName]	[nvarchar](50)	not null	
,[FamilyName]	[nvarchar](50)	not null	
,[Phone]	[nvarchar](15)	null	
,[email]	[nvarchar](250)	null	
,[passwordhash]	[nvarchar](100)	not null	 DEFAULT '3cfb3b458aad7613b44fcdf00b9a045f254f5e766d1d13b3dbdf1f9bb8eea9af'
,[active]	[bit]	not null	 DEFAULT '1'
,CONSTRAINT [PK_Skater] PRIMARY KEY ([SkaterID])
,CONSTRAINT [fk_Skater_Team] foreign key ([TeamId]) references [Team]([TeamId])
)
go
/******************
Create the [dbo].[Invoice] table
***************/
print '' Print '***Create the [dbo].[Invoice] table***' 
 go 


CREATE TABLE [dbo].[Invoice](


[InvoiceID]	[Int] identity(100000,1) 	not null	
,[SkaterID]	[nvarchar](50)	not null	
,[InvoiceAmount]	[float]	not null	
,[IssueDate]	[datetime]	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Invoice] PRIMARY KEY ([InvoiceID])
,CONSTRAINT [fk_Invoice_Skater] foreign key ([SkaterID]) references [Skater]([SkaterID])
)
go
/******************
Create the [dbo].[Receipts] table
***************/
print '' Print '***Create the [dbo].[Receipts] table***' 
 go 


CREATE TABLE [dbo].[receipts](


[receiptID]	[Int] identity(100000,1) 	not null	
,[InvoiceID]	[int]	not null	
,[receiptDate]	[datetime]	not null	
,[receiptAmount]	[float]	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Receipts] PRIMARY KEY ([ReceiptID])
,CONSTRAINT [fk_Receipts_Invoice] foreign key ([InvoiceID]) references [Invoice]([InvoiceID])
)
go
/******************
Create the [dbo].[Role] table
***************/
print '' Print '***Create the [dbo].[Role] table***' 
 go 


CREATE TABLE [dbo].[Role](


[RoleID]	[nvarchar](50)	not null	
,[Active]		[bit]									not null	DEFAULT '1'	
,CONSTRAINT [PK_Role] PRIMARY KEY ([RoleID])
)
go
/******************
Create the [dbo].[Skater_Role_Line] table
***************/
print '' Print '***Create the [dbo].[Skater_Role_Line] table***' 
 go 


CREATE TABLE [dbo].[Skater_Role_Line](


[RoleID]	[nvarchar](50)	not null	
,[SkaterID]	[nvarchar](50)	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Skater_Role_Line] PRIMARY KEY ([RoleID] , [SkaterID])
,CONSTRAINT [fk_Skater_Role_Line_Role] foreign key ([RoleID]) references [Role]([RoleID])
,CONSTRAINT [fk_Skater_Role_Line_Skater] foreign key ([SkaterID]) references [Skater]([SkaterID])
)
go
/******************
Create the [dbo].[Exercise] table
***************/
print '' Print '***Create the [dbo].[Exercise] table***' 
 go 


CREATE TABLE [dbo].[Exercise](


[ExerciseID]	[nvarchar](50)	not null	
,[Exercise_count]	[int]	null	
,[Exercise_units]	[nvarchar](50)	null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Exercise] PRIMARY KEY ([ExerciseID])
)
go
/******************
Create the [dbo].[Practice] table
***************/
print '' Print '***Create the [dbo].[Practice] table***' 
 go 


CREATE TABLE [dbo].[Practice](


[PracticeID]	[Int]	identity(100000,1)  not null	
,[LocationID]	[nvarchar](100)	not null	
,[DateTime]	[datetime]	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Practice] PRIMARY KEY ([PracticeID])
,CONSTRAINT [fk_Practice_Location] foreign key ([LocationId]) references [Location]([LocationId])
)
go
/******************
Create the [dbo].[exercise_practice_line] table
***************/
print '' Print '***Create the [dbo].[exercise_practice_line] table***' 
 go 


CREATE TABLE [dbo].[exercise_practice_line](


[ExerciseID]	[nvarchar](50)	not null	
,[PracticeID]	[Int]	not null	
,[Count]	[int]	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_exercise_practice_line] PRIMARY KEY ([ExerciseID] , [PracticeID])
,CONSTRAINT [fk_exercise_practice_line_Exercise] foreign key ([ExerciseID]) references [Exercise]([ExerciseID])
,CONSTRAINT [fk_exercise_practice_line_Practice] foreign key ([PracticeId]) references [Practice]([PracticeId])
)
go
/******************
Create the [dbo].[Skater_practice_Line] table
***************/
print '' Print '***Create the [dbo].[Skater_practice_Line] table***' 
 go 


CREATE TABLE [dbo].[Skater_practice_Line](


[SkaterID]	[nvarchar](50)	not null	
,[PracticeID]	[Int]	not null	
,[Active]		[bit]									not null	DEFAULT '1'
,CONSTRAINT [PK_Skater_practice_Line] PRIMARY KEY ([SkaterID] , [PracticeID])
,CONSTRAINT [fk_Skater_practice_Line_Skater] foreign key ([SkaterId]) references [Skater]([SkaterId])
,CONSTRAINT [fk_Skater_practice_Line_Practice] foreign key ([PracticeId]) references [Practice]([PracticeId])
)
go

/******************
Create the [dbo].[ApplicationStatus] table
***************/
print '' Print '***Create the [dbo].[ApplicationStatus] table***' 
 go 


CREATE TABLE [dbo].[ApplicationStatus](


[ApplicationStatusID]	[nvarchar](50)	not null	
CONSTRAINT [PK_ApplicationStatus] PRIMARY KEY (ApplicationStatusID)
)
go



/******************
Create the [dbo].[TeamApplication] table
***************/
print '' Print '***Create the [dbo].[TeamApplication] table***' 
 go 


CREATE TABLE [dbo].[TeamApplication](


[TeamApplicationID]	[int]	not null	identity(100000,1)
,[SkaterID]	[nvarchar](50)	not null	
,[TeamID]	[nvarchar](50)	not null	
,[ApplicationTime]	[datetime]	not null	DEFAULT GETDATE()
,[ApplicationStatus]	[nvarchar](50)	not null default 'Pending'
,CONSTRAINT [PK_TeamApplication] PRIMARY KEY ([TeamApplicationID])
,CONSTRAINT [fk_TeamApplication_Skater] foreign key ([SkaterId]) references [Skater]([SkaterId])
,CONSTRAINT [fk_TeamApplication_Team] foreign key ([TeamID]) references [Team]([TeamID])
,CONSTRAINT [fk_TeamApplication_ApplicationStatus] foreign key ([ApplicationStatus]) references [ApplicationStatus]([ApplicationStatusID])
)
go


/******************
Create the [dbo].[GearRequest] table
***************/
print '' Print '***Create the [dbo].[GearRequest] table***' 
 go 


CREATE TABLE [dbo].[GearRequest](


[GearRequestID]	[int]	not null	identity(100000,1) 
,[HelmSize]	[nvarchar](25)	not null	default"NA"
,[WristGuardSize]	[nvarchar](25)	not null	default"NA"
,[ElbowPadSize]	[nvarchar](25)	not null	default"NA"
,[KneePadSize]	[nvarchar](25)	not null	default"NA"
,[SkateSize]	[nvarchar](25)	not null	default"NA"
,CONSTRAINT [PK_GearRequest] PRIMARY KEY ([GearRequestID])
)
go

/******************
Create the [dbo].[GearApplication] table
***************/
print '' Print '***Create the [dbo].[GearApplication] table***' 
 go 


CREATE TABLE [dbo].[GearApplication](


[ApplicationID]	[int]	not null	identity(100000,1) 
,[SkaterID]	[nvarchar](50)	not null	
,[GearRequestID]	[int]	not null	
,[ApplicationTime]	[datetime]	not null	default GETDATE()
,[ApplicationStatus]	[nvarchar](50)	not null default 'Pending'	
,CONSTRAINT [PK_GearApplication] PRIMARY KEY ([ApplicationID])
,CONSTRAINT [fk_GearApplication_Skater] foreign key ([SkaterId]) references [Skater]([SkaterId])
,CONSTRAINT [fk_GearApplication_GearRequest] foreign key ([GearRequestID]) references [GearRequest]([GearRequestID])
,CONSTRAINT [fk_GearApplication_ApplicationStatus] foreign key ([ApplicationStatus]) references [ApplicationStatus]([ApplicationStatusID])
)
go



/******************
Create the [dbo].[GearInventory] table
***************/
print '' Print '***Create the [dbo].[GearInventory] table***' 
 go 


CREATE TABLE [dbo].[GearInventory](


[GearPart]	[nvarchar](50)	not null	
,[GearSize]	[nvarchar](50)	not null	
,[GearCount]	[int]	not null	
,CONSTRAINT [PK_GearInventory] PRIMARY KEY ([GearPart] , [GearSize])
)
go

/* insert data */

print '' print '3 league inserts'

Insert into [dbo].[League] (
[LeagueID],[Region],[Gender])
Values 
('Default','Default','Default'),
('WFTDA','Midwest','Female'),
('MFTDA','Midwest','Male'),
('OFTDA','Midwest','Open')
GO

print '' print '6 exercise inserts'

Insert into [dbo].[Exercise] (
[ExerciseID],[Exercise_count],[Exercise_units])
Values 
('Situp','20','Reps'),
('Pushup','20','Reps'),
('Suicide','10','Minutes'),
('Carts','5','Laps'),
('WarmUp','5','minutes'),
('WarmDown','5','minutes')
GO

print '' print ' 7 location inserts'

Insert into [dbo].[Location] (
[LocationID],[LeagueID],[ContactPhone],[City],[State],[Zip])
Values 
('A','Default','(123)454-2345','Cedar Rapids','Iowa','52405'),
('B','MFTDA','(212)455-8745','Iowa City','Iowa','52240'),
('C','OFTDA','(248)875-7845','Vinton','Iowa','52349'),
('D','WFTDA','(452)4561-1546','Platteville','Wisconsin','53818'),
('E','MFTDA','(123)885-9966','Madison','Wisconsin','53562'),
('F','OFTDA','(452)122-4576','Peoria','Illinois','61525'),
('G','WFTDA','(455)899-7541','Ames','Iowa','50010')
GO

print '' print ' 9 practice inserts'
Insert into [dbo].[practice] (
[LocationID],[DateTime])
Values 
('A','10/10/2023'),
('B','9/27/2023'),
('C','10/8/2023'),
('D  ','10/14/2023'),
('E','10/11/2023'),
('F','10/8/2023'),
('G','10/10/2023'),
('F','10/4/2023'),
('G','10/13/2023')
GO

print '' print ' 18 Exercise practice line inserts'

Insert into [dbo].[Exercise_Practice_Line] (
[ExerciseID],[PracticeID],[Count])
Values 
('Situp','100000','2'),
('Pushup','100000','1'),
('Suicide','100000','1'),
('Carts','100000','2'),
('WarmUp','100000','3'),
('WarmDown','100001','2'),
('Situp','100001','4'),
('Pushup','100001','5'),
('Suicide','100001','1'),
('Carts','100001','2'),
('WarmUp','100002','1'),
('WarmDown','100002','4'),
('Situp','100002','6'),
('Pushup','100002','2'),
('Suicide','100002','1'),
('Carts','100003','2'),
('WarmUp','100003','3'),
('WarmDown','100003','4')
GO

print '' print '12 team inserts'
GO

Insert into [dbo].[Team] (
[LeagueID],[TeamID],[MonthlyDue],[City],[State],[Zip])
Values 
('Default','Default','0','Default','Default','NA'),
('WFTDA','CRRD','30','Cedar Rapids','Iowa','52405'),
('MFTDA','OCCRD','30','Iowa City','Iowa','52245'),
('OFTDA','Vikings','30','Vinton','Iowa','52349'),
('WFTDA','Miners','30','Platteville','Wisconsin','53818'),
('Default','Capitols','30','Madison','Wisconsin','53593'),
('OFTDA','Prowlers','30','Peoria','Illinois','61525'),
('WFTDA','Riot','40','Ames','Iowa','50010'),
('MFTDA','KGB','40','Cedar Rapids','Iowa','52406'),
('OFTDA','West Hawks','40','Iowa City','Iowa','52245'),
('WFTDA','Romans','40','Vinton','Iowa','52349'),
('MFTDA','Majors','40','Platteville','Wisconsin','53818'),
('OFTDA','LowerCases','40','Madison','Wisconsin','53593')
GO

print '' print '36 skater inserts'

Insert into [dbo].[Skater] (
[SkaterID],[TeamID],[GivenName],[FamilyName],[Phone],[email])
Values 
('Alice','Default','Tia','Dot','(641)915-3636','TiaDot@gmail.com'),
('Malfoy','OCCRD','Elenora','Cinda','(258)911-5376','ElenoraCinda@gmail.com'),
('Malice','Vikings','Daniela','Aura','(156)849-6398','DanielaAura@gmail.com'),
('Pppi','Miners','Corinna','Annis','(886)454-1455','CorinnaAnnis@gmail.com'),
('Pain','Capitols','Gifty','Sonia','(198)296-5751','GiftySonia@gmail.com'),
('Avatar','Prowlers','Norma','Elodie','(621)874-4627','NormaElodie@gmail.com'),
('Agatha','Riot','Diane','Monday','(613)725-1195','DianeMonday@gmail.com'),
('Tequila','KGB','Barbie','Tuesday','(761)893-9541','BarbieTuesday@gmail.com'),
('Mother','West Hawks','zoe','Wednesday','(784)917-8181','zoeWednesday@gmail.com'),
('Meanhatters','Romans','Marve','Thursday','(249)148-6144','MarveThursday@gmail.com'),
('Razor','Majors','Sophie','Friday','(788)623-4813','SophieFriday@gmail.com'),
('Stitches','LowerCases','Onyx','Freddie','(461)986-4918','OnyxFreddie@gmail.com'),
('Patches','CRRD','Joanne','Mercury','(793)553-7828','JoanneMercury@gmail.com'),
('Shades','OCCRD','Charleen','Gains','(262)116-5575','CharleenGains@gmail.com'),
('PowerTower','Vikings','RosemaryAnnis','Lockheart','(911)276-7235','RosemaryAnnisLockheart@gmail.com'),
('BloodSoak','Miners','Gerri','Strife','(626)947-5153','GerriStrife@gmail.com'),
('Liberty','Capitols','Justice','Wallace','(426)613-4531','JusticeWallace@gmail.com'),
('BigRed','Prowlers','Liliana','Highwind','(533)231-4663','LilianaHighwind@gmail.com'),
('Nature','Riot','Marisa','Presley','(928)745-6245','MarisaPresley@gmail.com'),
('Danyell','KGB','Maria','Mathers','(911)156-3346','MariaMathers@gmail.com'),
('Wilma','West Hawks','Greg','Redfield','(874)713-2959','GregRedfield@gmail.com'),
('Betsy','Romans','Rob','Kennedy','(339)446-2384','RobKennedy@gmail.com'),
('Misty','Majors','Alex','McDonald','(377)745-4755','AlexMcDonald@gmail.com'),
('Deathrow','Default','Shell','Beck','(568)878-9218','ShellBeck@gmail.com'),
('Internal','CRRD','Niki','Grant','(535)214-8667','NikiGrant@gmail.com'),
('Slammy','OCCRD','Sammy','McDonald','(849)257-8434','SammyMcDonald@gmail.com'),
('BlockingJay','Vikings','Marjory','Anne','(644)522-6829','MarjoryAnne@gmail.com'),
('Choplin','Miners','Paulette','Birdie','(849)818-4879','PauletteBirdie@gmail.com'),
('Pickle','Capitols','Vicky','Fae','(362)984-5641','VickyFae@gmail.com'),
('Cobra','Prowlers','Shelby','Alberts','(995)516-4452','ShelbyAlberts@gmail.com'),
('Roller','Riot','Molly','Charlotte','(796)618-8919','MollyCharlotte@gmail.com'),
('Steel','KGB','Illa','Notaspy','(975)813-2396','IllaNotaspy@gmail.com'),
('mischief','West Hawks','Ruth','Carcillo','(959)327-7161','RuthCarcillo@gmail.com'),
('Kazi','Romans','Carmen','Keith','(811)754-4748','CarmenKeith@gmail.com'),
('Wham','Majors','Willie','seabrook','(988)928-5768','Willieseabrook@gmail.com'),
('Magic','LowerCases','Maddy','Crosby','(626)355-9373','MaddyCrosby@gmail.com')
GO


print '' print '16 skater practice line inserts'

Insert into [dbo].[Skater_practice_Line] (
[SkaterID],[PracticeID])
Values 
('Alice','100000'),
('Malfoy','100000'),
('Malice','100000'),
('Pppi','100000'),
('Pain','100000'),
('Avatar','100001'),
('Agatha','100001'),
('Tequila','100001'),
('Mother','100001'),
('Meanhatters','100001'),
('Razor','100002'),
('Stitches','100002'),
('Patches','100002'),
('Shades','100002'),
('PowerTower','100002'),
('BloodSoak','100003')
GO

print '' print '21 game inserts'
go
Insert into [dbo].[Game] (
[LocationID],[Date])
Values 
('A','8/26/2023'),
('B','9/17/2023'),
('C','7/25/2023'),
('D','9/3/2023'),
('E','9/19/2023'),
('F','7/30/2023'),
('G','7/30/2023'),
('A','8/29/2023'),
('B','6/16/2023'),
('C','7/11/2023'),
('D','8/23/2023'),
('E','7/16/2023'),
('F','10/4/2023'),
('G','8/27/2023'),
('A','9/5/2023'),
('B','8/3/2023'),
('C','8/27/2023'),
('D','9/23/2023'),
('E','6/10/2023'),
('F','8/10/2023'),
('G','6/15/2023')
GO

print '' print '38 game line inserts'
go

Insert into [dbo].[Game_Team_Line] (
[GameID],[TeamId],[Points])
Values 
('100000','CRRD','82'),
('100000','OCCRD','73'),
('100001','Vikings','104'),
('100001','Miners','71'),
('100002','Capitols','75'),
('100002','Prowlers','73'),
('100003','Riot','124'),
('100003','KGB','125'),
('100004','West Hawks','118'),
('100004','Romans','82'),
('100005','Majors','96'),
('100005','LowerCases','117'),
('100006','CRRD','108'),
('100006','OCCRD','124'),
('100007','Vikings','71'),
('100007','Miners','113'),
('100008','Capitols','119'),
('100008','Prowlers','106'),
('100009','Riot','109'),
('100009','KGB','113'),
('100010','West Hawks','108'),
('100010','Romans','127'),
('100011','Majors','75'),
('100011','LowerCases','73'),
('100012','CRRD','71'),
('100012','OCCRD','120'),
('100013','Vikings','104'),
('100013','Miners','106'),
('100014','Capitols','93'),
('100014','Prowlers','127'),
('100015','Riot','128'),
('100015','KGB','97'),
('100016','West Hawks','96'),
('100016','Romans','121'),
('100017','Majors','75'),
('100017','LowerCases','93'),
('100018','West Hawks','79'),
('100018','Romans','117')
GO

print '' print '15 invoice inserts'
GO
Insert into [dbo].[Invoice] (
[SkaterID],[InvoiceAmount],[IssueDate])
Values 
('Alice','40','9/9/23'),
('Malfoy','40','9/14/23'),
('Malice','40','9/5/23'),
('Pppi','40','9/5/23'),
('Pain','40','8/11/23'),
('Avatar','40','10/5/23'),
('Agatha','40','9/21/23'),
('Tequila','40','8/16/23'),
('Mother','40','8/30/23'),
('Meanhatters','40','8/11/23'),
('Razor','40','9/17/23'),
('Stitches','40','9/29/23'),
('Patches','40','9/18/23'),
('Shades','40','9/15/23'),
('PowerTower','40','9/21/23')
GO

print '' print '6 receipts inserts'
GO
Insert into [dbo].[receipts] (
[InvoiceID],[receiptDate],[receiptAmount])
Values 
('100000','10/5/23','40'),
('100001','10/6/23','40'),
('100002','10/7/23','40'),
('100003','10/8/23','30'),
('100004','10/9/23','30'),
('100005','10/10/23','30')
GO

print '' print '4 role inserts'
GO
Insert into [dbo].[Role] (
[RoleID])
Values 
('League_Admin'),
('Team_Admin'),
('Coach'),
('Skater')
GO

print '' print 'Skater role inserts'
Insert into [dbo].[Skater_Role_Line] (
[RoleID],[SkaterID])
Values 
('League_Admin','Alice'),
('Team_Admin','Malfoy'),
('Coach','Malice'),
('Skater','Pppi'),
('League_Admin','Pain'),
('Team_Admin','Avatar'),
('Coach','Agatha'),
('Skater','Tequila'),
('League_Admin','Mother'),
('Team_Admin','Meanhatters'),
('Coach','Razor'),
('Skater','Stitches'),
('League_Admin','Patches'),
('Team_Admin','Shades'),
('Coach','PowerTower'),
('Skater','BloodSoak'),
('Skater','Liberty'),
('Skater','BigRed'),
('Skater','Nature'),
('Skater','Danyell'),
('Skater','Wilma'),
('Skater','Betsy'),
('Skater','Misty'),
('Skater','Deathrow'),
('Skater','Internal'),
('Skater','Slammy'),
('Skater','BlockingJay'),
('Skater','Choplin'),
('Skater','Pickle'),
('Skater','Cobra'),
('Skater','Steel'),
('Skater','mischief'),
('Skater','Kazi'),
('Skater','Wham'),
('Skater','Magic')

GO

print '' print 'Application Status  inserts'
Insert into [dbo].[ApplicationStatus] (
[ApplicationStatusID])
Values 
('Pending'),
('Approved'),
('Rejected')

go

print '' print 'TeamApplication  inserts'
Insert into [dbo].[TeamApplication] (
[SkaterID],[TeamID])
Values 
('Pickle','West Hawks'),
('Roller','Capitols'),
('Betsy','Miners'),
('Mother','Vikings')

go

print '' print 'GearInventory  inserts'
Insert into [dbo].[GearInventory] (
[GearPart],[GearSize],[GearCount])
Values 
('Helm','ExtraLarge','5'),
('Helm','Large','5'),
('Helm','Medium','5'),
('Helm','Small','5'),
('Helm','ExtraSmall','5'),
('Wrist','ExtraLarge','5'),
('Wrist','Large','5'),
('Wrist','Medium','5'),
('Wrist','Small','5'),
('Wrist','ExtraSmall','5'),
('Elbow','ExtraLarge','5'),
('Elbow','Large','5'),
('Elbow','Medium','5'),
('Elbow','Small','5'),
('Elbow','ExtraSmall','5'),
('Knee','ExtraLarge','5'),
('Knee','Large','5'),
('Knee','Medium','5'),
('Knee','Small','5'),
('Knee','ExtraSmall','5'),
('Skate','6','5'),
('Skate','7','5'),
('Skate','8','5'),
('Skate','9','5'),
('Skate','10','5'),
('Skate','11','5'),
('Skate','12','5'),
('Skate','13','5'),
('Skate','14','5')


go

print '' print 'GearRequest  inserts'
Insert into [dbo].[GearRequest] (
[HelmSize],[WristGuardSize],[ElbowPadSize],[KneePadSize],[SkateSize])
Values 
('Large','Large','Large','NA','9'),
('Medium','Small','NA','Large','9'),
('Small','NA','Large','Large','10'),
('NA','Large','Medium','Large','7'),
('Small','Large','Large','Medium','8'),
('Large','Small','Medium','Large','11'),
('Large','Medium','Large','Large','12')



go

print '' print 'GearApplication  inserts'
Insert into [dbo].[GearApplication] (
[SkaterID],[GearRequestID])
Values 
('Agatha','100000'),
('Razor','100001'),
('Liberty','100002'),
('Cobra','100003')

go







/* login related stored procedures */
print '' print '*** Create sp_auth_skater***'
GO


create procedure [dbo].[sp_auth_skater]
(
@SkaterID	[nvarchar](50),
@PasswordHash [nvarchar](100)
) 
AS
	BEGIN
		select count([SkaterID]) as 'authenticated'
		from [skater]
		where @SkaterID	 = [SkaterId]
		and @passwordHash = [PasswordHash]
		and [active]=1
	end
	
	go
	
/* get employee name by email */
print '' print '*** Create get_skater_name_by_email***'
GO
create procedure [dbo].[sp_get_skater_name_by_SkaterID]	
(
@SkaterID [nvarchar](50)
)
as 
begin
select 
[SkaterID]	
,[GivenName]		
,[FamilyName]		
,[Phone]		
,[Email]
,[TeamID]	
,[PasswordHash]	
,[Active]	
from [skater]
where @SkaterID = [SkaterID]
and [active]=1
end 
GO

print '' print 'Create sp_select_roles_by_Skaterid'
go
create procedure [dbo].[sp_select_roles_by_SkaterID]
(
@SkaterID [nvarchar](50)
)
as
	BEGIN
		select [roleid]
		from [Skater_Role_Line]
		where @SkaterID = [SkaterID]
		end
go

print '' print 'create sp_update_passwordhash'
GO 
create procedure [dbo].[sp_update_passwordhash]
(
@SkaterID  [nvarchar](50),
@Newpasswordhash [nvarchar](100),
@oldpasswordhash [nvarchar](100)
)
as 
begin
update [Skater]
set passwordhash = @newpasswordhash 
where @SkaterID  = SkaterID 
and @oldpasswordhash = passwordhash
and [active]=1
return @@rowcount
end
GO

print '' Print '***Create the retreive by key script for the League table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_League]
(@LeagueID [nvarchar](100)
)
as
 Begin 
 select 
[LeagueID] 
,[Region] 
,[Gender] 

 FROM League
where [LeagueID]=@LeagueID
and [active]=1
 END 

/******************
Create the retreive by key script for the Location table
***************/
print '' Print '***Create the retreive by key script for the Location table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Location]
(@LocationID [nvarchar](100)
)
as
 Begin 
 select 
[LocationID] 
,[LeagueID] 
,[ContactPhone] 
,[City] 
,[State] 
,[Zip] 

 FROM Location
where [LocationID]=@LocationID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Team table
***************/
print '' Print '***Create the retreive by key script for the Team table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Team]
(@TeamId [nvarchar](50)
)
as
 Begin 
 select 
[TeamId] 
,[LeagueID] 
,[MonthlyDue] 
,[City] 
,[State] 
,[Zip] 

 FROM Team
where [TeamId]=@TeamId
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Game table
***************/
print '' Print '***Create the retreive by key script for the Game table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Game]
(@GameID [Int]
)
as
 Begin 
 select 
[GameID] 
,[LocationID] 
,[Date] 

 FROM Game
where [GameID]=@GameID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Game_Team_Line table
***************/
print '' Print '***Create the retreive by key script for the Game_Team_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Game_Team_Line]
(@GameID [Int]
,@TeamId [nvarchar](50)
)
as
 Begin 
 select 
[GameID] 
,[TeamId] 
,[Points] 

 FROM Game_Team_Line
where [GameID]=@GameID
AND [TeamId]=@TeamId
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Skater table
***************/
print '' Print '***Create the retreive by key script for the Skater table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Skater]
(@SkaterID [nvarchar](50)
)
as
 Begin 
 select 
[SkaterID] 
,[TeamID] 
,[GivenName] 
,[FamilyName] 
,[Phone] 
,[email] 
,[passwordhash] 
,[active] 

 FROM Skater
where [SkaterID]=@SkaterID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Invoice table
***************/
print '' Print '***Create the retreive by key script for the Invoice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Invoice]
(@InvoiceID [Int]
)
as
 Begin 
 select 
[InvoiceID] 
,[SkaterID] 
,[InvoiceAmount] 
,[IssueDate] 

 FROM Invoice
where [InvoiceID]=@InvoiceID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Receipts table
***************/
print '' Print '***Create the retreive by key script for the Receipts table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Receipts]
(@ReceiptID [Int]
)
as
 Begin 
 select 
[ReceiptID] 
,[InvoiceID] 
,[ReceiptDate] 
,[ReceiptAmount] 

 FROM Receipts
where [ReceiptID]=@ReceiptID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Role table
***************/
print '' Print '***Create the retreive by key script for the Role table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Role]
(@RoleID [nvarchar](50)
)
as
 Begin 
 select 
[RoleID] 

 FROM Role
where [RoleID]=@RoleID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Skater_Role_Line table
***************/
print '' Print '***Create the retreive by key script for the Skater_Role_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Skater_Role_Line]
(@RoleID [nvarchar](50)
,@SkaterID [nvarchar](50)
)
as
 Begin 
 select 
[RoleID] 
,[SkaterID] 

 FROM Skater_Role_Line
where [RoleID]=@RoleID
AND [SkaterID]=@SkaterID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Exercise table
***************/
print '' Print '***Create the retreive by key script for the Exercise table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Exercise]
(@ExerciseID [nvarchar](50)
)
as
 Begin 
 select 
[ExerciseID] 
,[Exercise_count] 
,[Exercise_units] 

 FROM Exercise
where [ExerciseID]=@ExerciseID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Practice table
***************/
print '' Print '***Create the retreive by key script for the Practice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Practice]
(@PracticeID [Int]
)
as
 Begin 
 select 
[PracticeID] 
,[LocationID] 
,[DateTime] 

 FROM Practice
where [PracticeID]=@PracticeID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the exercise_practice_line table
***************/
print '' Print '***Create the retreive by key script for the exercise_practice_line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_exercise_practice_line]
(@ExerciseID [nvarchar](50)
,@PracticeID [Int]
)
as
 Begin 
 select 
[ExerciseID] 
,[PracticeID] 
,[Count] 

 FROM exercise_practice_line
where [ExerciseID]=@ExerciseID
AND [PracticeID]=@PracticeID
and [active]=1
 END 
 GO

/******************
Create the retreive by key script for the Skater_practice_Line table
***************/
print '' Print '***Create the retreive by key script for the Skater_practice_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_Skater_practice_Line]
(@SkaterID [nvarchar](50)
,@PracticeID [Int]
)
as
 Begin 
 select 
[SkaterID] 
,[PracticeID] 

 FROM Skater_practice_Line
where [SkaterID]=@SkaterID
AND [PracticeID]=@PracticeID
and [active]=1
 END 
 GO





/******************
Create the retreive by key script for the GearRequest table
***************/
print '' Print '***Create the retreive by key script for the GearRequest table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_GearRequest]
(@GearRequestID [int]
)
as
 Begin 
 select 
[GearRequestID] 
,[HelmSize] 
,[WristGuardSize] 
,[ElbowPadSize] 
,[KneePadSize] 
,[SkateSize] 

 FROM GearRequest
where [GearRequestID]=@GearRequestID 
 END 
 GO
 
 
/******************
Create the retreive by key script for the GearApplication table
***************/
print '' Print '***Create the retreive by key script for the GearApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_GearApplication]
(@ApplicationID [int]
)
as
 Begin 
 select 
[ApplicationID] 
,[SkaterID] 
,[GearRequestID] 
,[ApplicationTime] 
,[ApplicationStatus] 

 FROM GearApplication
where [ApplicationID]=@ApplicationID 
 END 
 GO
 
/******************
Create the retreive by all script for the League table
***************/
print '' Print '***Create the retreive by all script for the League table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_League]
AS
begin 
 SELECT 

[LeagueID]
,[Region]
,[Gender]
 FROM League
 Where  [active]=1
 
 END  
 GO

/******************
Create the retreive by all script for the Location table
***************/
print '' Print '***Create the retreive by all script for the Location table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Location]
AS
begin 
 SELECT 

[LocationID]
,[LeagueID]
,[ContactPhone]
,[City]
,[State]
,[Zip]
 FROM Location
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Team table
***************/
print '' Print '***Create the retreive by all script for the Team table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Team]
AS
begin 
 SELECT 

[TeamId]
,[LeagueID]
,[MonthlyDue]
,[City]
,[State]
,[Zip]
 FROM Team
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Game table
***************/
print '' Print '***Create the retreive by all script for the Game table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Game]
AS
begin 
 SELECT 

[GameID]
,[LocationID]
,[Date]
 FROM Game
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Game_Team_Line table
***************/
print '' Print '***Create the retreive by all script for the Game_Team_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Game_Team_Line]
AS
begin 
 SELECT 

[GameID]
,[TeamId]
,[Points]
 FROM Game_Team_Line
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Skater table
***************/
print '' Print '***Create the retreive by all script for the Skater table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Skater]
AS
begin 
 SELECT 

[SkaterID]
,[TeamID]
,[GivenName]
,[FamilyName]
,[Phone]
,[email]
,[passwordhash]
,[active]
 FROM Skater
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Invoice table
***************/
print '' Print '***Create the retreive by all script for the Invoice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Invoice]
AS
begin 
 SELECT 

[InvoiceID]
,[SkaterID]
,[InvoiceAmount]
,[IssueDate]
 FROM Invoice
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Receipts table
***************/
print '' Print '***Create the retreive by all script for the Receipts table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Receipts]
AS
begin 
 SELECT 

[ReceiptID]
,[InvoiceID]
,[ReceiptDate]
,[ReceiptAmount]
 FROM Receipts
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Role table
***************/
print '' Print '***Create the retreive by all script for the Role table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Role]
AS
begin 
 SELECT 

[RoleID]
 FROM Role
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Skater_Role_Line table
***************/
print '' Print '***Create the retreive by all script for the Skater_Role_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Skater_Role_Line]
AS
begin 
 SELECT 

[RoleID]
,[SkaterID]
 FROM Skater_Role_Line
  Where  [active]=1
 ;
 END  
 GO



/******************
Create the retreive by all script for the Practice table
***************/
print '' Print '***Create the retreive by all script for the Practice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Practice]
AS
begin 
 SELECT 

[PracticeID]
,[LocationID]
,[DateTime]
 FROM Practice
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the exercise_practice_line table
***************/
print '' Print '***Create the retreive by all script for the exercise_practice_line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_exercise_practice_line]
AS
begin 
 SELECT 

[ExerciseID]
,[PracticeID]
,[Count]
 FROM exercise_practice_line
  Where  [active]=1
 ;
 END  
 GO

/******************
Create the retreive by all script for the Skater_practice_Line table
***************/
print '' Print '***Create the retreive by all script for the Skater_practice_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_Skater_practice_Line]
AS
begin 
 SELECT 

[SkaterID]
,[PracticeID]
 FROM Skater_practice_Line
  Where  [active]=1
 ;
 END  
 GO
 

/******************
Create the retreive by all script for the ApplicationStatus table
***************/
print '' Print '***Create the retreive by all script for the ApplicationStatus table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_ApplicationStatus]
AS
begin 
 SELECT 

[ApplicationStatusID]
 FROM ApplicationStatus
 ;
 END  
 GO
 
 /******************
Create the retreive by all script for the GearApplication table
***************/
print '' Print '***Create the retreive by all script for the GearApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_GearApplication]
AS
begin 
 SELECT 

[ApplicationID]
,[SkaterID]
,[GearRequestID]
,[ApplicationTime]
,[ApplicationStatus]
 FROM GearApplication
 ;
 END  
 GO
 
 
/******************
Create the retreive by all script for the GearInventory table
***************/
print '' Print '***Create the retreive by all script for the GearInventory table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_GearInventory]
AS
begin 
 SELECT 

[GearPart]
,[GearSize]
,[GearCount]
 FROM GearInventory
 ;
 END  
 GO
 


/******************
Create the delete script for the League table
***************/
print '' Print '***Create the delete script for the League table***' 
 go 
create procedure [dbo].[sp_delete_League]
(
@LeagueID [nvarchar](100)
)
as
begin
update [League]
set active = 0
where @LeagueID=LeagueID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Location table
***************/
print '' Print '***Create the delete script for the Location table***' 
 go 
create procedure [dbo].[sp_delete_Location]
(
@LocationID [nvarchar](100)
)
as
begin
update [Location]
set active = 0
where @LocationID=LocationID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Team table
***************/
print '' Print '***Create the delete script for the Team table***' 
 go 
create procedure [dbo].[sp_delete_Team]
(
@TeamId [nvarchar](50)
)
as
begin
update [Team]
set active = 0
where @TeamId=TeamId
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Game table
***************/
print '' Print '***Create the delete script for the Game table***' 
 go 
create procedure [dbo].[sp_delete_Game]
(
@GameID [Int]
)
as
begin
update [Game]
set active = 0
where @GameID=GameID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Game_Team_Line table
***************/
print '' Print '***Create the delete script for the Game_Team_Line table***' 
 go 
create procedure [dbo].[sp_delete_Game_Team_Line]
(
@GameID [Int]
,@TeamId [nvarchar](50)
)
as
begin
update [Game_Team_Line]
set active = 0
where @GameID=GameID
and @TeamId=TeamId
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Skater table
***************/
print '' Print '***Create the delete script for the Skater table***' 
 go 
create procedure [dbo].[sp_delete_Skater]
(
@SkaterID [nvarchar](50)
)
as
begin
update [Skater]
set active = 0
where @SkaterID=SkaterID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Invoice table
***************/
print '' Print '***Create the delete script for the Invoice table***' 
 go 
create procedure [dbo].[sp_delete_Invoice]
(
@InvoiceID [Int]
)
as
begin
update [Invoice]
set active = 0
where @InvoiceID=InvoiceID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Receipts table
***************/
print '' Print '***Create the delete script for the Receipts table***' 
 go 
create procedure [dbo].[sp_delete_Receipts]
(
@ReceiptID [Int]
)
as
begin
update [Receipts]
set active = 0
where @ReceiptID=ReceiptID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Role table
***************/
print '' Print '***Create the delete script for the Role table***' 
 go 
create procedure [dbo].[sp_delete_Role]
(
@RoleID [nvarchar](50)
)
as
begin
update [Role]
set active = 0
where @RoleID=RoleID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Skater_Role_Line table
***************/
print '' Print '***Create the delete script for the Skater_Role_Line table***' 
 go 
create procedure [dbo].[sp_delete_Skater_Role_Line]
(
@RoleID [nvarchar](50)
,@SkaterID [nvarchar](50)
)
as
begin
update [Skater_Role_Line]
set active = 0
where @RoleID=RoleID
and @SkaterID=SkaterID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Exercise table
***************/
print '' Print '***Create the delete script for the Exercise table***' 
 go 
create procedure [dbo].[sp_delete_Exercise]
(
@ExerciseID [nvarchar](50)
)
as
begin
update [Exercise]
set active = 0
where @ExerciseID=ExerciseID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Practice table
***************/
print '' Print '***Create the delete script for the Practice table***' 
 go 
create procedure [dbo].[sp_delete_Practice]
(
@PracticeID [Int]
)
as
begin
update [Practice]
set active = 0
where @PracticeID=PracticeID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the exercise_practice_line table
***************/
print '' Print '***Create the delete script for the exercise_practice_line table***' 
 go 
create procedure [dbo].[sp_delete_exercise_practice_line]
(
@ExerciseID [nvarchar](50)
,@PracticeID [Int]
)
as
begin
update [exercise_practice_line]
set active = 0
where @ExerciseID=ExerciseID
and @PracticeID=PracticeID
return @@rowcount 
 end 
 go
/******************
Create the delete script for the Skater_practice_Line table
***************/
print '' Print '***Create the delete script for the Skater_practice_Line table***' 
 go 
create procedure [dbo].[sp_delete_Skater_practice_Line]
(
@SkaterID [nvarchar](50)
,@PracticeID [Int]
)
as
begin
update [Skater_practice_Line]
set active = 0
where @SkaterID=SkaterID
and @PracticeID=PracticeID
return @@rowcount 
 end 
 go
 
 

/******************
Create the insert script for the League table
***************/
print '' Print '***Create the insert script for the League table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_League]
(@LeagueID [nvarchar](100)
,@Region [nvarchar](100)
,@Gender [nvarchar](100)
)as 
 begin
 insert into [dbo].League(
[LeagueID]
,[Region]
,[Gender]
)
 VALUES (
@LeagueID
,@Region
,@Gender
)
return @@rowcount
end
Go

/******************
Create the insert script for the Location table
***************/
print '' Print '***Create the insert script for the Location table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Location]
(@LocationID [nvarchar](100)
,@LeagueID [nvarchar](100)
,@ContactPhone [nvarchar](13)
,@City [nvarchar](100)
,@State [nvarchar](50)
,@Zip [nvarchar](5)
)as 
 begin
 insert into [dbo].Location(
[LocationID]
,[LeagueID]
,[ContactPhone]
,[City]
,[State]
,[Zip]
)
 VALUES (
@LocationID
,@LeagueID
,@ContactPhone
,@City
,@State
,@Zip
)
return @@rowcount
end
Go


/******************
Create the insert script for the Team table
***************/
print '' Print '***Create the insert script for the Team table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Team]
(@TeamId [nvarchar](50)
,@LeagueID [nvarchar](100)
,@MonthlyDue [money]
,@City [nvarchar](100)
,@State [nvarchar](50)
,@Zip [nvarchar](5)
)as 
 begin
 insert into [dbo].Team(
[TeamId]
,[LeagueID]
,[MonthlyDue] 
,[City]
,[State]
,[Zip]
)
 VALUES (
@TeamId
,@LeagueID
,@MonthlyDue
,@City
,@State
,@Zip
)
return @@rowcount
end
Go


/******************
Create the insert script for the Game table
***************/
print '' Print '***Create the insert script for the Game table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Game]
(@GameID [Int]
,@LocationID [nvarchar](100)
,@Date [datetime]
)as 
 begin
 insert into [dbo].Game(
[GameID]
,[LocationID]
,[Date]
)
 VALUES (
@GameID
,@LocationID
,@Date
)
return @@rowcount
end
Go


/******************
Create the insert script for the Game_Team_Line table
***************/
print '' Print '***Create the insert script for the Game_Team_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Game_Team_Line]
(@GameID [Int]
,@TeamId [nvarchar](50)
,@Points [int]
)as 
 begin
 insert into [dbo].Game_Team_Line(
[GameID]
,[TeamId]
,[Points]
)
 VALUES (
@GameID
,@TeamId
,@Points
)
return @@rowcount
end
Go


/******************
Create the insert script for the Skater table
***************/
print '' Print '***Create the insert script for the Skater table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Skater]
(@SkaterID [nvarchar](50)

,@GivenName [nvarchar](50)
,@FamilyName [nvarchar](50)
,@Phone [nvarchar](13)
,@email [nvarchar](250)

)as 
 begin
 insert into [dbo].Skater(
[SkaterID]

,[GivenName]
,[FamilyName]
,[Phone]
,[email]
)
 VALUES (
@SkaterID

,@GivenName
,@FamilyName
,@Phone
,@email
)
return @@rowcount

end
Go


/******************
Create the insert script for the Invoice table
***************/
print '' Print '***Create the insert script for the Invoice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Invoice]
(@InvoiceID [Int]
,@SkaterID [nvarchar](50)
,@InvoiceAmount [float]
,@IssueDate [datetime]
)as 
 begin
 insert into [dbo].Invoice(
[InvoiceID]
,[SkaterID]
,[InvoiceAmount]
,[IssueDate]
)
 VALUES (
@InvoiceID
,@SkaterID
,@InvoiceAmount
,@IssueDate
)
return @@rowcount
end
Go

/******************
Create the insert script for the Receipts table
***************/
print '' Print '***Create the insert script for the Receipts table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Receipts]
(@ReceiptID [Int]
,@InvoiceID [int]
,@ReceipttDate [datetime]
,@ReceiptAmount [float]
)as 
 begin
 insert into [dbo].Receipts(
[ReceiptID]
,[InvoiceID]
,[receiptDate]
,[ReceiptAmount]
)
 VALUES (
@ReceiptID
,@InvoiceID
,@ReceipttDate
,@ReceiptAmount
)
return @@rowcount
end
Go


/******************
Create the insert script for the Role table
***************/
print '' Print '***Create the insert script for the Role table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Role]
(@RoleID [nvarchar](50)
)as 
 begin
 insert into [dbo].Role(
[RoleID]
)
 VALUES (
@RoleID
)
return @@rowcount
end
Go


/******************
Create the insert script for the Skater_Role_Line table
***************/
print '' Print '***Create the insert script for the Skater_Role_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Skater_Role_Line]
(@RoleID [nvarchar](50)
,@SkaterID [nvarchar](50)
)as 
 begin
 insert into [dbo].Skater_Role_Line(
[RoleID]
,[SkaterID]
)
 VALUES (
@RoleID
,@SkaterID
)
return @@rowcount
end
Go


/******************
Create the insert script for the Exercise table
***************/
print '' Print '***Create the insert script for the Exercise table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Exercise]
(@ExerciseID [nvarchar](50)
,@Exercise_count [int]
,@Exercise_units [nvarchar](50)
)as 
 begin
 insert into [dbo].Exercise(
[ExerciseID]
,[Exercise_count]
,[Exercise_units]
)
 VALUES (
@ExerciseID
,@Exercise_count
,@Exercise_units
)
return @@rowcount
end
Go


/******************
Create the insert script for the Practice table
***************/
print '' Print '***Create the insert script for the Practice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Practice]
(@PracticeID [Int]
,@LocationID [nvarchar](100)
,@DateTime [datetime]
)as 
 begin
 insert into [dbo].Practice(
[PracticeID]
,[LocationID]
,[DateTime]
)
 VALUES (
@PracticeID
,@LocationID
,@DateTime
)
return @@rowcount
end
Go


/******************
Create the insert script for the exercise_practice_line table
***************/
print '' Print '***Create the insert script for the exercise_practice_line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_exercise_practice_line]
(@ExerciseID [nvarchar](50)
,@PracticeID [Int]
,@Count [int]
)as 
 begin
 insert into [dbo].exercise_practice_line(
[ExerciseID]
,[PracticeID]
,[Count]
)
 VALUES (
@ExerciseID
,@PracticeID
,@Count
)
return @@rowcount
end
Go


/******************
Create the insert script for the Skater_practice_Line table
***************/
print '' Print '***Create the insert script for the Skater_practice_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_Skater_practice_Line]
(@SkaterID [nvarchar](50)
,@PracticeID [Int]
)as 
 begin
 insert into [dbo].Skater_practice_Line(
[SkaterID]
,[PracticeID]
)
 VALUES (
@SkaterID
,@PracticeID
)
return @@rowcount
end
Go



/******************
Create the insert script for the GearRequest table
***************/
print '' Print '***Create the insert script for the GearRequest table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insertGearRequest]
(
@HelmSize [nvarchar](25)
,@WristGuardSize [nvarchar](25)
,@ElbowPadSize [nvarchar](25)
,@KneePadSize [nvarchar](25)
,@SkateSize [nvarchar](25)
)as 
 begin
 insert into [dbo].GearRequest(

[HelmSize]
,[WristGuardSize]
,[ElbowPadSize]
,[KneePadSize]
,[SkateSize]
)
 VALUES (
@HelmSize
,@WristGuardSize
,@ElbowPadSize
,@KneePadSize
,@SkateSize
)
SELECT SCOPE_IDENTITY()
end
Go

/******************
Create the insert script for the GearApplication table
***************/
print '' Print '***Create the insert script for the GearApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insertGearApplication]
(
@SkaterID [nvarchar](50)
,@GearRequestID [int]

)as 
 begin
 insert into [dbo].GearApplication(

[SkaterID]
,[GearRequestID]

)
 VALUES (

@SkaterID
,@GearRequestID

)
return @@rowcount
end
Go

/******************
Create the update script for the League table
***************/
print '' Print '***Create the update script for the League table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_League]
(@oldLeagueID[nvarchar](100)
,@oldRegion[nvarchar](100)
,@newRegion[nvarchar](100)
,@oldGender[nvarchar](100)
,@newGender[nvarchar](100)
)
as
BEGIN
UPDATE League
SET
Region = @newRegion
,Gender = @newGender
WHERE
LeagueID = @oldLeagueID
and Region = @oldRegion
and Gender = @oldGender
return @@rowcount
end
go
/******************
Create the update script for the Location table
***************/
print '' Print '***Create the update script for the Location table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Location]
(@oldLocationID[nvarchar](100)

,@oldLeagueID[nvarchar](100)
,@newLeagueID[nvarchar](100)
,@oldContactPhone[nvarchar](14)
,@newContactPhone[nvarchar](14)
,@oldCity[nvarchar](100)
,@newCity[nvarchar](100)
,@oldState[nvarchar](50)
,@newState[nvarchar](50)
,@oldZip[nvarchar](5)
,@newZip[nvarchar](5)
)
as
BEGIN
UPDATE Location
SET
LeagueID=@newLeagueID
,ContactPhone = @newContactPhone
,City = @newCity
,State = @newState
,Zip = @newZip
WHERE
LocationID = @oldLocationID
and LeagueID = @oldLeagueID
and ContactPhone = @oldContactPhone
and City = @oldCity
and State = @oldState
and Zip = @oldZip
return @@rowcount
end
go
/******************
Create the update script for the Team table
***************/
print '' Print '***Create the update script for the Team table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Team]
(@oldTeamId[nvarchar](50)

,@oldLeagueID[nvarchar](100)
,@newLeagueID[nvarchar](100)
,@oldCity[nvarchar](100)
,@newCity[nvarchar](100)
,@oldMonthlyDue money
,@newMonthlyDue money
,@oldState[nvarchar](50)
,@newState[nvarchar](50)
,@oldZip[nvarchar](5)
,@newZip[nvarchar](5)
)
as
BEGIN
UPDATE Team
SET
LeagueID = @newLeagueID
,MonthlyDue = @newMonthlyDue
,City = @newCity
,State = @newState
,Zip = @newZip
WHERE
TeamId = @oldTeamId
and LeagueID = @oldLeagueID
and MonthlyDue = @oldMonthlyDue
and City = @oldCity
and State = @oldState
and Zip = @oldZip
return @@rowcount
end
go
/******************
Create the update script for the Game table
***************/
print '' Print '***Create the update script for the Game table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Game]
(@oldGameID[Int]
,@oldLocationID[nvarchar](100)
,@newLocationID[nvarchar](100)
,@oldDate[datetime]
,@newDate[datetime]
)
as
BEGIN
UPDATE Game
SET
LocationID = @newLocationID
,Date = @newDate
WHERE
GameID = @oldGameID
and LocationID = @oldLocationID
and Date = @oldDate
return @@rowcount
end
go
/******************
Create the update script for the Game_Team_Line table
***************/
print '' Print '***Create the update script for the Game_Team_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Game_Team_Line]
(@oldGameID[Int]
,@newGameID[Int]
,@oldTeamId[nvarchar](50)
,@newTeamId[nvarchar](50)
,@oldPoints[int]
,@newPoints[int]
)
as
BEGIN
UPDATE Game_Team_Line
SET
Points = @newPoints
WHERE
GameID = @oldGameID
and TeamId = @oldTeamId
and Points = @oldPoints
return @@rowcount
end
go
/******************
Create the update script for the Skater table
***************/
print '' Print '***Create the update script for the Skater table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Skater]
(@oldSkaterID[nvarchar](50)
,@oldTeamID[nvarchar](50)
,@newTeamID[nvarchar](50)
,@oldGivenName[nvarchar](50)
,@newGivenName[nvarchar](50)
,@oldFamilyName[nvarchar](50)
,@newFamilyName[nvarchar](50)
,@oldPhone[nvarchar](13)
,@newPhone[nvarchar](13)
,@oldemail[nvarchar](250)
,@newemail[nvarchar](250)

)
as
BEGIN
UPDATE Skater
SET
TeamID = @newTeamID
,GivenName = @newGivenName
,FamilyName = @newFamilyName
,Phone = @newPhone
,email = @newemail

WHERE
SkaterID = @oldSkaterID
and TeamID = @oldTeamID
and GivenName = @oldGivenName
and FamilyName = @oldFamilyName
and Phone = @oldPhone
and email = @oldemail

return @@rowcount
end
go
/******************
Create the update script for the Invoice table
***************/
print '' Print '***Create the update script for the Invoice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Invoice]
(@oldInvoiceID[Int]
,@oldSkaterID[nvarchar](50)
,@newSkaterID[nvarchar](50)
,@oldInvoiceAmount[float]
,@newInvoiceAmount[float]
,@oldIssueDate[datetime]
,@newIssueDate[datetime]
)
as
BEGIN
UPDATE Invoice
SET
SkaterID = @newSkaterID
,InvoiceAmount = @newInvoiceAmount
,IssueDate = @newIssueDate
WHERE
InvoiceID = @oldInvoiceID
and SkaterID = @oldSkaterID
and InvoiceAmount = @oldInvoiceAmount
and IssueDate = @oldIssueDate
return @@rowcount
end
go
/******************
Create the update script for the Receipts table
***************/
print '' Print '***Create the update script for the Receipts table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Receipts]
(@oldReceiptID[Int]
,@oldInvoiceID[int]
,@newInvoiceID[int]
,@oldReceipttDate[datetime]
,@newReceipttDate[datetime]
,@oldReceiptAmount[float]
,@newReceiptAmount[float]
)
as
BEGIN
UPDATE Receipts
SET
InvoiceID = @newInvoiceID
,ReceiptDate = @newReceipttDate
,ReceiptAmount = @newReceiptAmount
WHERE
ReceiptID = @oldReceiptID
and InvoiceID = @oldInvoiceID
and receiptDate = @oldReceipttDate
and ReceiptAmount = @oldReceiptAmount
return @@rowcount
end
go
/******************
Create the update script for the Role table
***************/
print '' Print '***Create the update script for the Role table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Role]
(@oldRoleID[nvarchar](50)
,@newRoleID[nvarchar](50)
)
as
BEGIN
UPDATE Role
SET
RoleID=@newRoleID
WHERE
RoleID = @oldRoleID
return @@rowcount
end
go
/******************
Create the update script for the Skater_Role_Line table
***************/
/******
print '' Print '***Create the update script for the Skater_Role_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Skater_Role_Line]
(@oldRoleID[nvarchar](50)
,@newRoleID[nvarchar](50)
,@oldSkaterID[nvarchar](50)
,@newSkaterID[nvarchar](50)
)
as
BEGIN
UPDATE Skater_Role_Line
SET
WHERE
RoleID = @oldRoleID
and SkaterID = @oldSkaterID
return @@rowcount
end
go
**********/
/******************
Create the update script for the Exercise table
***************/
print '' Print '***Create the update script for the Exercise table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Exercise]
(@oldExerciseID[nvarchar](50)
,@oldExercise_count[int]
,@newExercise_count[int]
,@oldExercise_units[nvarchar](50)
,@newExercise_units[nvarchar](50)
)
as
BEGIN
UPDATE Exercise
SET
Exercise_count = @newExercise_count
,Exercise_units = @newExercise_units
WHERE
ExerciseID = @oldExerciseID
and Exercise_count = @oldExercise_count
and Exercise_units = @oldExercise_units
return @@rowcount
end
go
/******************
Create the update script for the Practice table
***************/
print '' Print '***Create the update script for the Practice table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Practice]
(@oldPracticeID[Int]
,@newPracticeID[Int]
,@oldLocationID[nvarchar](100)
,@newLocationID[nvarchar](100)
,@oldDateTime[datetime]
,@newDateTime[datetime]
)
as
BEGIN
UPDATE Practice
SET
LocationID = @newLocationID
,DateTime = @newDateTime
WHERE
PracticeID = @oldPracticeID
and LocationID = @oldLocationID
and DateTime = @oldDateTime
return @@rowcount
end
go
/******************
Create the update script for the exercise_practice_line table
***************/
print '' Print '***Create the update script for the exercise_practice_line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_exercise_practice_line]
(@oldExerciseID[nvarchar](50)
,@newExerciseID[nvarchar](50)
,@oldPracticeID[Int]
,@newPracticeID[Int]
,@oldCount[int]
,@newCount[int]
)
as
BEGIN
UPDATE exercise_practice_line
SET
Count = @newCount
WHERE
ExerciseID = @oldExerciseID
and PracticeID = @oldPracticeID
and Count = @oldCount
return @@rowcount
end
go
/******************
Create the update script for the Skater_practice_Line table
***************/
print '' Print '***Create the update script for the Skater_practice_Line table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_Skater_practice_Line]
(@oldSkaterID[nvarchar](50)
,@newSkaterID[nvarchar](50)
,@oldPracticeID[Int]
,@newPracticeID[Int]
)
as
BEGIN
UPDATE Skater_practice_Line
SET
skaterID=@newSkaterID
, PracticeID=@newPracticeID
WHERE
SkaterID = @oldSkaterID
and PracticeID = @oldPracticeID
return @@rowcount
end
go


/******************
Create the update script for the GearApplication table
***************/
print '' Print '***Create the update script for the GearApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_GearApplication]
(@oldApplicationID[int]

,@oldSkaterID[nvarchar](50)

,@oldGearRequestID[int]

,@oldApplicationTime[datetime]

,@oldApplicationStatus[nvarchar](50)
,@newApplicationStatus[nvarchar](50)
)
as
BEGIN
UPDATE GearApplication
SET

ApplicationStatus = @newApplicationStatus
WHERE
ApplicationID = @oldApplicationID
and SkaterID = @oldSkaterID
and GearRequestID = @oldGearRequestID
and ApplicationTime = @oldApplicationTime
and ApplicationStatus = @oldApplicationStatus
return @@rowcount
end
go



/******************
Create the update script for the GearInventory table
***************/
print '' Print '***Create the update script for the GearInventory table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_GearInventory]
(@oldGearPart[nvarchar](50)

,@oldGearSize[nvarchar](50)

,@oldGearCount[int]
,@newGearCount[int]
)
as
BEGIN
UPDATE GearInventory
SET
GearCount = @newGearCount
WHERE
GearPart = @oldGearPart
and GearSize = @oldGearSize
and GearCount = @oldGearCount
return @@rowcount
end
go




/******************
Create the update script for the TeamApplication table
***************/
print '' Print '***Create the update script for the TeamApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_TeamApplication]
(@oldTeamApplicationID[int]
,@oldSkaterID[nvarchar](50)
,@oldTeamID[nvarchar](50)
,@oldApplicationTime[datetime]
,@oldApplicationStatus[nvarchar](50)
,@newApplicationStatus[nvarchar](50)
)
as
BEGIN
UPDATE TeamApplication
SET

ApplicationStatus = @newApplicationStatus
WHERE
 TeamApplicationID = @oldTeamApplicationID
and SkaterID = @oldSkaterID
and TeamID = @oldTeamID
and ApplicationTime = @oldApplicationTime
and ApplicationStatus = @oldApplicationStatus
return @@rowcount
end
go
/******************
Create the retreive by key script for the TeamApplication table
***************/
print '' Print '***Create the retreive by key script for the TeamApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_pk_TeamApplication]
(@TeamApplicationID [int]
)
as
 Begin 
 select 
[TeamApplicationID] 
,[SkaterID] 
,[TeamID] 
,[ApplicationTime] 
,[ApplicationStatus] 

 FROM TeamApplication
where [TeamApplicationID]=@TeamApplicationID 
 END 
 GO
 
 /******************
Create the retreive by all script for the TeamApplication table
***************/
print '' Print '***Create the retreive by all script for the TeamApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_all_TeamApplication]
AS
begin 
 SELECT 

[TeamApplicationID]
,[SkaterID]
,[TeamID]
,[ApplicationTime]
,[ApplicationStatus]
 FROM TeamApplication
 ;
 END  
 GO
 
 /******************
Create the insert script for the TeamApplication table
***************/
print '' Print '***Create the insert script for the TeamApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insertTeamApplication]
(
@SkaterID [nvarchar](50)
,@TeamID [nvarchar](50)

)as 
 begin
 insert into [dbo].TeamApplication(

[SkaterID]
,[TeamID]

)
 VALUES (

@SkaterID
,@TeamID

)
return @@rowcount
end
Go

/******************
Create the retreive by Skater script for the TeamApplication table
***************/
print '' Print '***Create the retreive by key script for the TeamApplication table***' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_TeamApplication_by_SkaterID]
(@SkaterID [nvarchar](50)
)
as
 Begin 
 select 
[TeamApplicationID] 
,[SkaterID] 
,[TeamID] 
,[ApplicationTime] 
,[ApplicationStatus] 

 FROM TeamApplication
where [SkaterID]=@SkaterID 
 END 
 GO