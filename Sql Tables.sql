create table PersonalDataDetails
(
Person_ID int primary key identity(1000,1),
Person_Name varchar(50) not null,
Person_Age int not null,
Person_Occupation varchar(100) not null,
Person_Mail varchar(100) not null
)

Create table LoginDetails
(
Login_Userid varchar(50) primary key,
Login_Password varchar(100) unique not null,
)

CREATE TABLE [dbo].[Datafile](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Filerecord] [image] NOT NULL,
	[Filetype] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO