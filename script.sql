USE [master]
GO
/****** Object:  Database [DbBears]    Script Date: 12/9/2019 5:57:59 AM ******/
CREATE DATABASE [DbBears]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DbBears', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DbBears.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DbBears_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DbBears_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DbBears] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DbBears].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DbBears] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DbBears] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DbBears] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DbBears] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DbBears] SET ARITHABORT OFF 
GO
ALTER DATABASE [DbBears] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DbBears] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DbBears] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DbBears] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DbBears] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DbBears] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DbBears] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DbBears] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DbBears] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DbBears] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DbBears] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DbBears] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DbBears] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DbBears] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DbBears] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DbBears] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DbBears] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DbBears] SET RECOVERY FULL 
GO
ALTER DATABASE [DbBears] SET  MULTI_USER 
GO
ALTER DATABASE [DbBears] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DbBears] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DbBears] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DbBears] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DbBears] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DbBears', N'ON'
GO
ALTER DATABASE [DbBears] SET QUERY_STORE = OFF
GO
USE [DbBears]
GO
/****** Object:  Table [dbo].[Bear]    Script Date: 12/9/2019 5:58:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bear](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Bear] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddBear]    Script Date: 12/9/2019 5:58:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bahman Masarratbakhsh
-- Create date: 12/06/2019
-- Description:	Add bear to list
-- =============================================
CREATE PROCEDURE [dbo].[AddBear] 
	 @name nvarchar(50),
	 @typeName nvarchar(50)
AS
BEGIN
	
INSERT INTO Bear(Name, TypeName)
VALUES (@name, @typeName); 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteBear]    Script Date: 12/9/2019 5:58:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Bahman Masarratbakhsh
-- Create date: 12/06/2019
-- Description:	Delete bear
-- =============================================
CREATE PROCEDURE [dbo].[DeleteBear] 
	@Id int
AS
BEGIN
	DELETE FROM bear WHERE id=@Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllBears]    Script Date: 12/9/2019 5:58:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllBears]
	
AS
BEGIN
   select * from Bear	
END
GO
/****** Object:  StoredProcedure [dbo].[GetBears]    Script Date: 12/9/2019 5:58:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetBears] 
 @page INT,
 @size INT,
 @sort nvarchar(50) ,
 @totalrow INT  OUTPUT
AS
BEGIN
	 DECLARE @offset INT
    DECLARE @newsize INT
    DECLARE @sql NVARCHAR(MAX)

    IF(@page=0)
      BEGIN
        SET @offset = @page
        SET @newsize = @size
       END
    ELSE 
      BEGIN
        SET @offset = @page*@size
        SET @newsize = @size-1
      END
    SET NOCOUNT ON
    SET @sql = '
     WITH OrderedSet AS
    (
      SELECT *, ROW_NUMBER() OVER (ORDER BY ' + @sort + ') AS ''Index''
      FROM [dbo].[Bear] 
    )
   SELECT * FROM OrderedSet WHERE [Index] BETWEEN ' + CONVERT(NVARCHAR(12), @offset) + ' AND ' + CONVERT(NVARCHAR(12), (@offset + @newsize)) 
   EXECUTE (@sql)
   SET @totalrow = (SELECT COUNT(*) FROM Bear)
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateBear]    Script Date: 12/9/2019 5:58:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateBear] 
	 @Id int,
	 @name nvarchar(50),
     @TypeName nvarchar(50)
as
BEGIN
	UPDATE
     Bear

SET
    Name=@name,
	TypeName=@TypeName
    
WHERE
    ID = @ID
END
GO
USE [master]
GO
ALTER DATABASE [DbBears] SET  READ_WRITE 
GO
