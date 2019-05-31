USE [master]
GO
/****** Object:  Database [SLSShippingApp]    Script Date: 3/13/2019 10:13:04 AM ******/
CREATE DATABASE [SLSShippingApp]
GO
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'SLSShippingApp', FILENAME = N'E:\DATA\SLSShippingApp.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'SLSShippingApp_log', FILENAME = N'E:\LOGS\SLSShippingApp.ldf' , SIZE = 5696KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SLSShippingApp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SLSShippingApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SLSShippingApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SLSShippingApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SLSShippingApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SLSShippingApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SLSShippingApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [SLSShippingApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SLSShippingApp] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SLSShippingApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SLSShippingApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SLSShippingApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SLSShippingApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SLSShippingApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SLSShippingApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SLSShippingApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SLSShippingApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SLSShippingApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SLSShippingApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SLSShippingApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SLSShippingApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SLSShippingApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SLSShippingApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SLSShippingApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SLSShippingApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SLSShippingApp] SET RECOVERY FULL 
GO
ALTER DATABASE [SLSShippingApp] SET  MULTI_USER 
GO
ALTER DATABASE [SLSShippingApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SLSShippingApp] SET DB_CHAINING OFF 
GO
--ALTER DATABASE [SLSShippingApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
--GO
--ALTER DATABASE [SLSShippingApp] SET TARGET_RECOVERY_TIME = 0 SECONDS 
--GO
EXEC sys.sp_db_vardecimal_storage_format N'SLSShippingApp', N'ON'
GO
USE [SLSShippingApp]
GO
/****** Object:  User [sls_shipping_app]    Script Date: 3/13/2019 10:13:04 AM ******/
IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE name = 'sls_shipping_app') BEGIN
	CREATE USER [sls_shipping_app] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
	EXEC sp_addrolemember 'db_datareader','sls_shipping_app'
	EXEC sp_addrolemember 'db_datawriter','sls_shipping_app'
END
IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE name = CURRENT_USER) BEGIN
	CREATE USER [CURRENT_USER] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
	EXEC sp_addrolemember 'db_datareader',[CURRENT_USER]
	EXEC sp_addrolemember 'db_datawriter',[CURRENT_USER]
END
IF NOT EXISTS(SELECT * FROM sys.database_principals WHERE name = 'sa') BEGIN
	CREATE USER [sa] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
	EXEC sp_addrolemember 'db_datareader','sa'
	EXEC sp_addrolemember 'db_datawriter','sa'
	EXEC sp_addrolemember 'db_owner','sa'
END
GO
/****** Object:  Schema [MILLMATS\sql_service]    Script Date: 3/13/2019 10:13:04 AM ******/
CREATE SCHEMA [MILLMATS\sql_service]
GO
/****** Object:  StoredProcedure [dbo].[GetRetailerPackingLabel]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
exec dbo.GetRetailerPackingLabel 'keithb','keithb'
*/

CREATE PROC [dbo].[GetRetailerPackingLabel](@Operator VARCHAR(50), @EnvironUser VARCHAR(50),@OrderNumber VARCHAR(25))
AS
SET NOCOUNT ON; 
BEGIN 
	SELECT 
		PickedDate, 
		LTRIM(RTRIM(OrderOrSONumber)) as OrderOrSONumber,  
		 CustomerNumber, 	
		 CustomerName,
		PKTK, 
		 OrderLineNumber, 	
		 StockNumber, 
		 StockDescription1, 
		 StockDescription2,  	
		StockDescription3,
		 QuantityToShip, 
		PONumber, 	
		ShipToNumber,
		ShiptoName, 
		ShiptoAddress1,  
		ShiptoAddress2,
		ShipToAddress3,
		ShiptoCity, 
		ShipToCountry, 
		ShipViaCode, 
		RecToday, 
		CustomerProfile, 	
		BayLocation, 
		QtyFROMStock,
		QtyShippedTodate, 
		BilltoName,
		BilltoAddress1,
		BilltoAddress2, 
		BilltoAddress3,
		BilltoCity, 
		BillToCountry,  
		OrderDate,
		ShipDate,
		ShipInstructions1,
		ShipInstructions2,
		CustShipperAcct, 
		CustNote1,  
		CustNote2,
		CustNote3, 
		CustNote4,
		CustNote5,
		RetailerName,
		RetailerFld1, 
		RetailerFld2,
		RetailerFld3,
		RetailerFld4, 
		RetailerFld5,
		OrderWeight,
		SearchDesc, 
		UnitPrice,
		ShipCost,
		UnitTaxAmt,  	
		TotalOrderCost,
		CustomerOrderNo,
		ShipToPhone, 
		OrderedByName,
		OrderedByAddr1,
		OrderedByAddr2, 
		OrderedByAddr3,
		OrderedByCity,
		OrderedByState,
		OrderedByZipCode,
		OrderedByCountry,
		RetailerItemDesc, 
		LTRIM(RTRIM(TCNumber)) as TCNumber,
		UPC, 	
		  [ASNNumber],
		  [DPCI],
		  [GiftWrap],
		  [MFGID],
		  [ReturnMethod],
		  [ReturnPolicy],
		  [GiftMessage],
		UnitPrice*[QuantityToShip] as ItemTotal,
		(SELECT SUM([UnitPrice]*[QuantityToShip]) 
			FROM tblRetailerPackingLabel 
			GROUP BY [OrderOrSONumber]) AS SubTotal,  
		(SELECT SUM([QuantityToShip]*[ShipCost]) 
			FROM tblRetailerPackingLabel 
			GROUP BY  [OrderOrSONumber]) AS Shipping,  
		(SELECT SUM([QuantityToShip]*[UnitTaxAmt]) 
			FROM tblRetailerPackingLabel 
			GROUP BY  [OrderOrSONumber]) AS Tax,              
		(SELECT SUM([UnitPrice]*[QuantityToShip])+SUM([QuantityToShip]*[ShipCost])+SUM([QuantityToShip]*[UnitTaxAmt]) 
				FROM tblRetailerPackingLabel 
				GROUP BY  [OrderOrSONumber]) AS Total 
	FROM tblRetailerPackingLabel
	WHERE OperatorName = @Operator
	AND EnvironUser = @EnvironUser
	AND OrderOrSoNumber = @OrderNumber
END
GO
/****** Object:  StoredProcedure [dbo].[GetWalmartPackingLabel]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[GetWalmartPackingLabel](@Operator VARCHAR(50), @EnvironUser VARCHAR(50),@OrderNumber VARCHAR(25))
AS
SET NOCOUNT ON; 
BEGIN 
	SELECT 
		PickedDate, 
		LTRIM(RTRIM(OrderOrSONumber)) as OrderOrSONumber,  
		 CustomerNumber, 	
		 CustomerName,
		PKTK, 
		 OrderLineNumber, 	
		 StockNumber, 
		 StockDescription1, 
		 StockDescription2,  	
		StockDescription3,
		 QuantityToShip, 
		PONumber, 	
		ShipToNumber,
		ShiptoName, 
		ShiptoAddress1,  
		ShiptoAddress2,
		ShiptoCity, 
		ShipToCountry, 
		ShipViaCode, 
		RecToday, 
		CustomerProfile, 	
		BayLocation, 
		QtyFROMStock,
		QtyShippedTodate, 
		BilltoName,
		BilltoAddress1,
		BilltoAddress2, 
		BilltoAddress3,
		BilltoCity, 
		BillToCountry,  
		OrderDate,
		ShipDate,
		ShipInstructions1,
		ShipInstructions2,
		CustShipperAcct, 
		CustNote1,  
		CustNote2,
		CustNote3, 
		CustNote4,
		CustNote5,
		RetailerName,
		RetailerFld1, 
		RetailerFld2,
		RetailerFld3,
		RetailerFld4, 
		RetailerFld5,
		OrderWeight,
		SearchDesc, 
		UnitPrice,
		ShipCost,
		UnitTaxAmt,  	
		TotalOrderCost,
		CustomerOrderNo,
		ShipToPhone, 
		OrderedByName,
		OrderedByAddr1,
		OrderedByAddr2, 
		OrderedByAddr3,
		OrderedByCity,
		OrderedByState,
		OrderedByZipCode,
		OrderedByCountry,
		RetailerItemDesc, 
		LTRIM(RTRIM(TCNumber)) as TCNumber,
		UPC, 
		UnitPrice*[QuantityToShip] as ItemTotal,
		(SELECT SUM([UnitPrice]*[QuantityToShip]) 
			FROM tblRetailerPackingLabel 
			GROUP BY [OrderOrSONumber]) AS SubTotal,  
		(SELECT SUM([QuantityToShip]*[ShipCost]) 
			FROM tblRetailerPackingLabel 
			GROUP BY  [OrderOrSONumber]) AS Shipping,  
		(SELECT SUM([QuantityToShip]*[UnitTaxAmt]) 
			FROM tblRetailerPackingLabel 
			GROUP BY  [OrderOrSONumber]) AS Tax,              
		(SELECT SUM([UnitPrice]*[QuantityToShip])+SUM([QuantityToShip]*[ShipCost])+SUM([QuantityToShip]*[UnitTaxAmt]) 
				FROM tblRetailerPackingLabel 
				GROUP BY  [OrderOrSONumber]) AS Total 
	FROM tblRetailerPackingLabel
	WHERE OperatorName = @Operator
	AND EnvironUser = @EnvironUser
	AND OrderOrSONumber = @OrderNumber
END
GO
/****** Object:  StoredProcedure [dbo].[qryArchTableTickets]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[qryArchTableTickets]
AS
SET NOCOUNT ON; 
BEGIN

INSERT INTO tblTicketsArch ( OrderorSONumber, PT, 
PickingTicket, PickedDate, CustomerNumber, CustomerName, 
PONumber, ShipViaDescription, BoxCount, Scanned, OperatorName )
SELECT LTRIM(RTRIM(tblTickets.OrderorSONumber)) AS [Order], 
tblTickets.PT, tblTickets.PickingTicket, tblTickets.PickedDate
, tblTickets.CustomerNumber, tblTickets.CustomerName, 
tblTickets.PONumber, tblTickets.ShipViaDescription, 
tblTickets.BoxCount, GETDATE() AS Scanned, OperatorName
FROM tblTickets
END
GO
/****** Object:  StoredProcedure [dbo].[qryFreezeBay]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[qryFreezeBay](@OrderNumber VARCHAR(25))
AS
SET NOCOUNT ON; 
BEGIN
/*
	DECLARE @sameOrderNumber VARCHAR(25)
	SET @SameOrderNumber = @ORderNumber
UPDATE MMMACSQL01.FMShipping.dbo.tblBay 
SET tblBay.PickedDateTime = GETDATE()
WHERE tblBay.OrderNumber = RIGHT(REPLICATE('0',8) + @ORderNumber,8)
*/
SELECT 1
END
GO
/****** Object:  StoredProcedure [dbo].[qryGetPackingLabelSums]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[qryGetPackingLabelSums]
AS
SET NOCOUNT ON; 
BEGIN

SELECT Sum([UnitPrice]*[QuantityToShip]) AS SubTotal, 
SUM([QuantityToShip]*[ShipCost]) AS Shipping,
 SUM([QuantityToShip]*[UnitTaxAmt]) AS Tax,
  SUM([UnitPrice]*[QuantityToShip])+Sum([QuantityToShip]*[ShipCost])+Sum([QuantityToShip]*[UnitTaxAmt]) AS Total
  
FROM tblRetailerPackingLabel
END
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetItemImage]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fnGetItemImage](@ItemNo VARCHAR(10)) 

RETURNS  VARCHAR(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ImagePath VARCHAR(100)
	
	SET @ImagePath = '\\fs10\public\Graphic\FANMATS\F' + RIGHT(REPLICATE('0',8) + LTRIM(RTRIM(@ItemNo)),8) + '.gif'

   --sFileName = "F" + sNewStockNumber.PadLeft(8, '0') + ".gif";
   --         sFile = ConfigurationManager.AppSettings["ImagePath"].ToString() + sFileName;
   --         //sFile = @"\\fs10\public\Graphic\FANMATS\" + sNewStockNumber + ".gif";	

	-- Return the result of the function
	RETURN @ImagePath
END
GO
/****** Object:  Table [dbo].[tblLabel]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLabel](
	[OrderNumber] [varchar](25) NULL,
	[ShippingDate] [date] NULL,
	[StockNumber] [varchar](25) NULL,
	[BayLocation] [varchar](25) NULL,
	[OrderSize] [int] NULL,
	[BayLocationOnly] [varchar](50) NULL,
	[CustomerNum] [varchar](25) NULL,
	[CustomerName] [varchar](75) NULL,
	[IsKit] [bit] NULL,
	[ItemDesc1] [varchar](30) NULL,
	[ItemDesc2] [varchar](30) NULL,
	[ComponentItemNumber] [varchar](25) NULL,
	[OperatorName] [varchar](25) NULL,
	[EnvironUser] [varchar](50) NULL,
	[OrderWeight] [decimal](6, 2) NULL,
	[Image] [varchar](250) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLabelFanBrands]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLabelFanBrands](
	[StockNumber] [varchar](25) NULL,
	[OperatorName] [varchar](25) NULL,
	[EnvironUser] [varchar](50) NULL,
	[txtImageFile] [varchar](1000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLabelQuickShip]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLabelQuickShip](
	[StockNumber] [varchar](25) NULL,
	[PO] [varchar](25) NULL,
	[Operator] [varchar](25) NULL,
	[EnvironUser] [varchar](50) NULL,
	[txtImageFile] [varchar](1000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrderItemMismatchMacola]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblOrderItemMismatchMacola](
	[OrderNumber] [varchar](25) NULL,
	[BayLocation] [int] NULL,
	[StockNumber] [varchar](25) NULL,
	[OrderedQty] [int] NULL,
	[qty_ordered] [int] NULL,
	[OperatorName] [varchar](25) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrderItemsNotInMacola]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblOrderItemsNotInMacola](
	[OrderNumber] [varchar](25) NULL,
	[BayLocation] [int] NULL,
	[StockNumber] [varchar](25) NULL,
	[OrderedQty] [int] NULL,
	[qty_ordered] [int] NULL,
	[ReceivedQty] [int] NULL,
	[OperatorName] [varchar](25) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPickingFile]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPickingFile](
	[PickedDate] [datetime] NULL,
	[OrderorSONumber] [varchar](25) NULL,
	[PKTK] [varchar](25) NULL,
	[OrderLineNumber] [int] NULL,
	[StockNumber] [int] NULL,
	[StockDescription1] [varchar](30) NULL,
	[StockDescription2] [varchar](30) NULL,
	[StockDescription3] [varchar](30) NULL,
	[QuantityToShip] [int] NULL,
	[RecToday] [int] NULL,
	[txtImageName] [varchar](100) NULL,
	[CustomerNumber] [varchar](25) NULL,
	[CustomerName] [varchar](75) NULL,
	[PONumber] [varchar](25) NULL,
	[ShipToNumber] [varchar](25) NULL,
	[ShipToName] [varchar](50) NULL,
	[ShipToAddress1] [varchar](100) NULL,
	[ShipToAddress2] [varchar](100) NULL,
	[ShipToAddress3] [varchar](100) NULL,
	[ShipToCity] [varchar](100) NULL,
	[ShipToState] [varchar](25) NULL,
	[ShipToZipCode] [varchar](10) NULL,
	[ShipViaCode] [varchar](50) NULL,
	[CustomerProfile] [varchar](max) NULL,
	[BayLocation] [varchar](25) NULL,
	[QtyFromStock] [int] NULL,
	[QtyShippedTodate] [int] NULL,
	[BillToName] [varchar](50) NULL,
	[BillToAddress1] [varchar](100) NULL,
	[BillToAddress2] [varchar](100) NULL,
	[BIllToAddress3] [varchar](100) NULL,
	[BIllToCity] [varchar](100) NULL,
	[BillToState] [varchar](25) NULL,
	[OrderDate] [date] NULL,
	[ShipDate] [date] NULL,
	[ShipInstructions1] [varchar](100) NULL,
	[ShipInstructions2] [varchar](100) NULL,
	[CustShipperAcct] [varchar](30) NULL,
	[CustNote1] [varchar](100) NULL,
	[CustNote2] [varchar](100) NULL,
	[CustNote3] [varchar](100) NULL,
	[CustNote4] [varchar](100) NULL,
	[CustNote5] [varchar](100) NULL,
	[RetailerName] [varchar](100) NULL,
	[RetailerFld1] [varchar](50) NULL,
	[RetailerFld2] [varchar](50) NULL,
	[RetailerFld3] [varchar](50) NULL,
	[RetailerFld4] [varchar](50) NULL,
	[RetailerFld5] [varchar](50) NULL,
	[OrderWeight] [decimal](6, 2) NULL,
	[OperatorName] [varchar](25) NULL,
	[EnvironUser] [varchar](50) NULL,
	[BillToZipCode] [varchar](10) NULL,
	[Hold] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPostingLog]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPostingLog](
	[ORDER#] [int] NULL,
	[LN#] [int] NULL,
	[ORDDATE] [datetime] NULL,
	[[TEM#] [int] NULL,
	[DESCRIPTION] [varchar](30) NULL,
	[QTYTORECV] [int] NULL,
	[QTYRCVD] [int] NULL,
	[UOM] [varchar](100) NULL,
	[QTYDEFECT] [int] NULL,
	[Field1] [int] NULL,
	[CUST#] [int] NULL,
	[OperatorName] [varchar](25) NULL,
	[EnvironUser] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuickShipReport]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuickShipReport](
	[OrderNumber] [varchar](25) NULL,
	[StockNumber] [varchar](25) NULL,
	[cus_name] [varchar](100) NULL,
	[ShippingDate] [datetime] NULL,
	[item_desc_1] [varchar](30) NULL,
	[item_desc_2] [varchar](30) NULL,
	[QuantityOrdered] [int] NULL,
	[StockNumberNumeric] [int] NULL,
	[OperatorName] [varchar](25) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblRetailerPackingLabel]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRetailerPackingLabel](
	[PickedDate] [datetime] NULL,
	[OrderorSONumber] [varchar](25) NULL,
	[PKTK] [varchar](25) NULL,
	[OrderLineNumber] [int] NULL,
	[StockNumber] [int] NULL,
	[StockDescription1] [varchar](250) NULL,
	[StockDescription2] [varchar](30) NULL,
	[StockDescription3] [varchar](30) NULL,
	[QuantityToShip] [int] NULL,
	[RecToday] [int] NULL,
	[txtImageName] [varchar](100) NULL,
	[CustomerNumber] [varchar](25) NULL,
	[CustomerName] [varchar](75) NULL,
	[PONumber] [varchar](25) NULL,
	[ShipToNumber] [varchar](25) NULL,
	[ShipToName] [varchar](100) NULL,
	[ShipToAddress1] [varchar](100) NULL,
	[ShipToAddress2] [varchar](100) NULL,
	[ShipToAddress3] [varchar](100) NULL,
	[ShipToCity] [varchar](100) NULL,
	[ShipToState] [varchar](25) NULL,
	[ShipToZipCode] [varchar](10) NULL,
	[ShipToCountry] [varchar](100) NULL,
	[ShipViaCode] [varchar](25) NULL,
	[CustomerProfile] [varchar](250) NULL,
	[BayLocation] [varchar](25) NULL,
	[QtyFromStock] [int] NULL,
	[QtyShippedTodate] [int] NULL,
	[BillToName] [varchar](50) NULL,
	[BillToAddress1] [varchar](100) NULL,
	[BillToAddress2] [varchar](100) NULL,
	[BIllToAddress3] [varchar](100) NULL,
	[BIllToCity] [varchar](100) NULL,
	[BillToState] [varchar](25) NULL,
	[BillToZipCode] [varchar](15) NULL,
	[BillToCountry] [varchar](100) NULL,
	[OrderDate] [date] NULL,
	[ShipDate] [date] NULL,
	[ShipInstructions1] [varchar](100) NULL,
	[ShipInstructions2] [varchar](100) NULL,
	[CustShipperAcct] [varchar](25) NULL,
	[CustNote1] [varchar](100) NULL,
	[CustNote2] [varchar](100) NULL,
	[CustNote3] [varchar](100) NULL,
	[CustNote4] [varchar](100) NULL,
	[CustNote5] [varchar](100) NULL,
	[RetailerName] [varchar](100) NULL,
	[RetailerFld1] [varchar](50) NULL,
	[RetailerFld2] [varchar](50) NULL,
	[RetailerFld3] [varchar](50) NULL,
	[RetailerFld4] [varchar](50) NULL,
	[RetailerFld5] [varchar](50) NULL,
	[OrderWeight] [decimal](6, 2) NULL,
	[SearchDesc] [varchar](30) NULL,
	[UnitPrice] [decimal](6, 2) NULL,
	[ShipCost] [decimal](6, 2) NULL,
	[UnitTaxAmt] [decimal](6, 2) NULL,
	[TotalOrderCost] [decimal](6, 2) NULL,
	[CustomerOrderNo] [varchar](25) NULL,
	[ShipToPhone] [varchar](10) NULL,
	[OrderedByName] [varchar](30) NULL,
	[OrderedByAddr1] [varchar](100) NULL,
	[OrderedByAddr2] [varchar](100) NULL,
	[OrderedByAddr3] [varchar](100) NULL,
	[OrderedByCity] [varchar](50) NULL,
	[OrderedByState] [varchar](15) NULL,
	[OrderedByZipCode] [varchar](10) NULL,
	[OrderedByCountry] [varchar](20) NULL,
	[RetailerItemDesc] [varchar](250) NULL,
	[TCNumber] [varchar](30) NULL,
	[UPC] [varchar](50) NULL,
	[OperatorName] [varchar](25) NULL,
	[EnvironUser] [varchar](50) NULL,
	[ASNNumber] [varchar](30) NULL,
	[DPCI] [varchar](30) NULL,
	[GiftWrap] [varchar](10) NULL,
	[MFGID] [varchar](30) NULL,
	[ReturnMethod] [varchar](250) NULL,
	[ReturnPolicy] [varchar](250) NULL,
	[GiftMessage] [varchar](250) NULL,
	[OrderMessageText] [varchar](250) NULL,
	[OrderMessaging] [varchar](250) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTickets]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTickets](
	[OrderOrSONumber] [varchar](25) NULL,
	[PT] [varchar](25) NULL,
	[PickingTicket] [varchar](100) NULL,
	[PickedDate] [datetime] NULL,
	[CustomerNumber] [varchar](25) NULL,
	[CustomerName] [varchar](100) NULL,
	[PONumber] [varchar](30) NULL,
	[ShipViaDescription] [varchar](30) NULL,
	[BoxCount] [int] NULL,
	[OperatorName] [varchar](25) NULL,
	[NumberOfBoxes] [int] NULL,
	[EnvironUser] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTicketsArch]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTicketsArch](
	[OrderOrSONumber] [varchar](25) NULL,
	[PT] [varchar](25) NULL,
	[PickingTicket] [varchar](100) NULL,
	[PickedDate] [datetime] NULL,
	[CustomerNumber] [varchar](25) NULL,
	[CustomerName] [varchar](100) NULL,
	[PONumber] [varchar](30) NULL,
	[ShipViaDescription] [varchar](30) NULL,
	[BoxCount] [int] NULL,
	[Scanned] [datetime] NULL,
	[OperatorName] [varchar](25) NULL,
	[NumberOfBoxes] [int] NULL,
	[EnvironUser] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblWindowsUsers]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblWindowsUsers](
	[WindowsLogin] [varchar](25) NOT NULL,
	[DefaultScanScreen] [varchar](25) NOT NULL,
	[DefaultInputType] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[WindowsLogin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[vw_tblPickingFile]    Script Date: 3/13/2019 10:13:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_tblPickingFile]
AS
SELECT [PickedDate] as PickedDate
      ,[OrderorSONumber] as OrderOrSoNumber
      ,[PKTK]
      ,[OrderLineNumber] as OrderLineNumber
      ,[StockNumber] as StockNumber
      ,[StockDescription1] as StockDescription1
      ,[StockDescription2] as StockDescription2
      ,[StockDescription3] as StockDescription3
      ,[QuantityToShip] as QuantityToShip
      ,[RecToday]
      ,[txtImageName]
      ,[CustomerNumber] as CustomerNumber
      ,[CustomerName] as CustomerName
      ,[PONumber]
      ,[ShipToNumber]
      ,[ShipToName]
      ,[ShipToAddress1]
      ,[ShipToAddress2]
      ,[ShipToAddress3]
      ,[ShipToCity]
      ,[ShipToState]
      ,[ShipToZipCode]
      ,[ShipViaCode]
      ,[CustomerProfile]
      ,[BayLocation]
      ,[QtyFromStock]
      ,[QtyShippedTodate] as QtyShippedTodate
      ,[BillToName]
      ,[BillToAddress1]
      ,[BillToAddress2]
      ,[BIllToAddress3]
      ,[BIllToCity]
      ,[BillToState]
      ,[OrderDate]
      ,[ShipDate]
      ,[ShipInstructions1]
      ,[ShipInstructions2]
      ,[CustShipperAcct]
      ,[CustNote1]
      ,[CustNote2]
      ,[CustNote3]
      ,[CustNote4]
      ,[CustNote5]
      ,[RetailerName]
      ,[RetailerFld1]
      ,[RetailerFld2]
      ,[RetailerFld3]
      ,[RetailerFld4]
      ,[RetailerFld5]
      ,[OrderWeight]
      ,[OperatorName]
      ,[EnvironUser]
      ,[BillToZipCode]
  FROM [SLSShippingApp].[dbo].[tblPickingFile]
GO
ALTER TABLE [dbo].[tblLabel] ADD  DEFAULT ((0)) FOR [IsKit]
GO
ALTER TABLE [dbo].[tblPickingFile] ADD  DEFAULT ((0)) FOR [Hold]
GO
USE [master]
GO
ALTER DATABASE [SLSShippingApp] SET  READ_WRITE 
GO