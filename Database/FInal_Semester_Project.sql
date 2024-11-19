USE [SuperMarketInventoryDb]
GO

/****** Object:  Database [SuperMarketInventoryDb]    Script Date: 26-05-2024 18:44:53 ******/
CREATE DATABASE [SuperMarketInventoryDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SuperMarketInventoryDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SuperMarketInventoryDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SuperMarketInventoryDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SuperMarketInventoryDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SuperMarketInventoryDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ARITHABORT OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET  ENABLE_BROKER 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET  MULTI_USER 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [SuperMarketInventoryDb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [SuperMarketInventoryDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [SuperMarketInventoryDb] SET QUERY_STORE = ON
GO

ALTER DATABASE [SuperMarketInventoryDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [SuperMarketInventoryDb] SET  READ_WRITE 
GO
