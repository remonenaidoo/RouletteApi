USE [master]
GO
/****** Object:  Database [RouletteTransactions]    Script Date: 9/12/2022 8:19:11 PM ******/
CREATE DATABASE [RouletteTransactions]
GO
ALTER DATABASE [RouletteTransactions] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RouletteTransactions].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RouletteTransactions] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RouletteTransactions] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RouletteTransactions] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RouletteTransactions] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RouletteTransactions] SET ARITHABORT OFF 
GO
ALTER DATABASE [RouletteTransactions] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RouletteTransactions] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RouletteTransactions] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RouletteTransactions] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RouletteTransactions] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RouletteTransactions] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RouletteTransactions] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RouletteTransactions] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RouletteTransactions] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RouletteTransactions] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RouletteTransactions] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RouletteTransactions] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RouletteTransactions] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RouletteTransactions] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RouletteTransactions] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RouletteTransactions] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RouletteTransactions] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RouletteTransactions] SET RECOVERY FULL 
GO
ALTER DATABASE [RouletteTransactions] SET  MULTI_USER 
GO
ALTER DATABASE [RouletteTransactions] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RouletteTransactions] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RouletteTransactions] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RouletteTransactions] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RouletteTransactions] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RouletteTransactions] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RouletteTransactions', N'ON'
GO
ALTER DATABASE [RouletteTransactions] SET QUERY_STORE = OFF
GO
USE [RouletteTransactions]
GO
/****** Object:  Table [dbo].[BalanceLog]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BalanceLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BetRefence] [varchar](50) NULL,
	[BalanceBefore] [decimal](18, 2) NULL,
	[BalanceAfter] [decimal](18, 2) NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionType] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bet]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bet](
	[BetID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NULL,
	[Betdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BetsPayout]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BetsPayout](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BetReference] [varchar](50) NULL,
	[TransactionType] [int] NULL,
	[Payout] [decimal](18, 2) NULL,
	[PayoutInfo] [varchar](250) NULL,
	[TransactionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BetsPlaced]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BetsPlaced](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BetID] [int] NULL,
	[BetReference] [varchar](50) NULL,
	[TransactionType] [int] NULL,
	[Stake] [decimal](18, 2) NULL,
	[Bet] [varchar](50) NULL,
	[TransactionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[EmailAddress] [varchar](100) NULL,
	[Balance] [decimal](18, 2) NULL,
	[PasswordHash] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rounds]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rounds](
	[RoundID] [int] IDENTITY(1,1) NOT NULL,
	[BetID] [int] NULL,
	[Number] [int] NULL,
	[Colour] [varchar](50) NULL,
	[Parity] [varchar](50) NULL,
	[BetRange] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoundID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionType]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionType](
	[ID] [int] NOT NULL,
	[TransactionType] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser]
    @Username NVARCHAR(50), 
	@EmailAddress NVARCHAR(100), 
	@Balance decimal(18,2) = 0,
    @Password NVARCHAR(50)
AS
BEGIN
declare @responseMessage NVARCHAR(250)

    SET NOCOUNT ON

	 IF EXISTS (SELECT TOP 1 [ClientID] FROM [dbo].[Client]  WHERE EmailAddress = @EmailAddress)
	 SET @responseMessage= 'User Exists'
	 else
	 begin
    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.Client (Username,EmailAddress,Balance,PasswordHash,Salt,IsActive)
        VALUES(@Username,@EmailAddress,@Balance, HASHBYTES('SHA2_512', @Password+CAST(@salt AS NVARCHAR(36))), @salt,1)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

	select @responseMessage as responseMessage
	end
END
GO
/****** Object:  StoredProcedure [dbo].[GetOriginalBet]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetOriginalBet]
(
@BetReference varchar(100)
)
As
Begin
SELECT BP.[BetID]
      ,[BetReference]
      ,[TransactionType]
      ,[Stake] as Amount
      ,BP.[Bet]
      ,[TransactionDate]
	  ,C.ClientID
	  ,C.Balance
FROM [dbo].[BetsPlaced] BP
JOIN [dbo].[Bet] B
ON BP.BetID = B.BetID
JOIN [dbo].[Client] C
ON B.ClientID = C.ClientID
where BetReference = @BetReference
END
GO
/****** Object:  StoredProcedure [dbo].[InsertBets]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertBets]
(
@ClientID int,
@BetReference varchar(50),
@TransactionType int,
@Amount decimal(18,2),
@Bet varchar(50)
)
as
begin

BEGIN TRY
BEGIN TRANSACTION;


Insert into [dbo].[Bet]
VALUES (@ClientID,GETDATE())


declare @BetID int

set @BetID = (SELECT SCOPE_IDENTITY())

Insert into [dbo].[BetsPlaced]
values(@BetID,@BetReference,@TransactionType,@Amount,@Bet,GETDATE())

COMMIT;

Select 1 as IsSuccess , @BetReference as BetReference
END TRY

BEGIN CATCH
ROLLBACK;
Select 0 as IsSuccess , @BetReference as BetReference
END CATCH;

end
GO
/****** Object:  StoredProcedure [dbo].[InsertPayout]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertPayout]
(
@BetReference varchar(50),
@TransactionType int,
@Amount decimal(18,2),
@PayoutInfo varchar(250)
)
as
begin

BEGIN TRY
BEGIN TRANSACTION;

Insert into [dbo].[BetsPayout]
values(@BetReference,@TransactionType,@Amount,@PayoutInfo,GETDATE())

COMMIT;

Select 1 as IsSuccess , @BetReference as BetReference
END TRY

BEGIN CATCH
ROLLBACK;
Select 0 as IsSuccess , @BetReference as BetReference
END CATCH;
end
GO
/****** Object:  StoredProcedure [dbo].[InsertRounds]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertRounds]
(
@BetID int,
@Number int,
@Colour varchar(50),
@Parity varchar(50),
@BetRange varchar(50)
)
as
begin

BEGIN TRY
BEGIN TRANSACTION;

Insert into [dbo].[Rounds]
values(@BetID,@Number,@Colour,@Parity,@BetRange)

COMMIT;
END TRY

BEGIN CATCH
ROLLBACK;
END CATCH;


end

 
GO
/****** Object:  StoredProcedure [dbo].[LoginUser]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoginUser]
    @EmailAddress NVARCHAR(100), 
    @Password NVARCHAR(50)
AS
BEGIN
declare  @responseMessage bit = 0 
    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 [ClientID] FROM [dbo].[Client]  WHERE EmailAddress = @EmailAddress)
    BEGIN
        SET @userID=(SELECT [ClientID] FROM [dbo].[Client] WHERE EmailAddress = @EmailAddress AND PasswordHash=HASHBYTES('SHA2_512', @Password+CAST(Salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @responseMessage= 0
       ELSE 
           SET @responseMessage= 1
    END
    ELSE
       SET @responseMessage= 0

	   select @responseMessage as responseMessage, @EmailAddress as EmailAddress 
END
GO
/****** Object:  StoredProcedure [dbo].[RetrieveSpins]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RetrieveSpins]
AS
BEGIN
SELECT [SpinsID]
      ,[Number]
      ,[Colour]
      ,[Parity]
      ,[BetRange]
  FROM [RouletteTransactions].[dbo].[Spins]
end

 
GO
/****** Object:  StoredProcedure [dbo].[RetrivePayout]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RetrivePayout]
(
@BetReference varchar(50)
)
as
begin
if(exists(select top 1 * FROM [dbo].[BetsPayout] where BetReference = @BetReference))
  begin
  Select 'true' as IsSuccess , @BetReference as BetReference
  end
  else
  Select 'false' as IsSuccess , @BetReference as BetReference
  end
GO
/****** Object:  StoredProcedure [dbo].[UpdateBalances]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateBalances]
(
@ClientID int,
@BetReference varchar(50),
@TransactionType int,
@Amount decimal(18,2),
@Balance decimal(18,2)
)
AS
Begin

BEGIN TRY
BEGIN TRANSACTION;

declare @BalanceAfter decimal(18,2)

Set @BalanceAfter = (Select 
Case 
when @TransactionType = 1 
Then @Balance - @Amount  
When @TransactionType = 2 
Then @Balance + @Amount 
End
)

Insert into [dbo].[BalanceLog]
values(@BetReference,@Balance,@BalanceAfter,GETDATE(),@TransactionType
)

Update [dbo].[Client]
set [Balance] = @BalanceAfter
where ClientID = @ClientID

COMMIT;
END TRY
BEGIN CATCH
ROLLBACK;
END CATCH;

End
GO
/****** Object:  StoredProcedure [dbo].[ValidateUserExists]    Script Date: 9/12/2022 8:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ValidateUserExists]
    @EmailAddress NVARCHAR(100)
AS
BEGIN
declare  @Exists bit = 0 , @Balance decimal(18,2) = 0 , @ClientID int = 0
    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 [ClientID] FROM [dbo].[Client]  WHERE EmailAddress = @EmailAddress)
    BEGIN
           SET @Exists= 1
		   SET @Balance = (SELECT TOP 1 Balance FROM [dbo].[Client]  WHERE EmailAddress = @EmailAddress)
		   SET @ClientID = (SELECT TOP 1 ClientID FROM [dbo].[Client]  WHERE EmailAddress = @EmailAddress)
	End
       ELSE 
           SET @Exists= 0

	   select @Exists as UserExists, @EmailAddress as EmailAddress, @Balance as balance , @ClientID as ClientID
END
GO
USE [master]
GO
ALTER DATABASE [RouletteTransactions] SET  READ_WRITE 
GO
