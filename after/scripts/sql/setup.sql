CREATE DATABASE Flights
GO

USE Flights
GO

CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesAgent] [nvarchar](max) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[RecordLocator] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

CREATE TABLE [dbo].[Journey](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Departure] [nvarchar](max) NOT NULL,
	[Arrival] [nvarchar](max) NOT NULL,
	[DepartureDate] [datetime] NOT NULL,
	[ArrivalDate] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[BookingId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Journey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)

GO

ALTER TABLE [dbo].[Journey] ADD  CONSTRAINT [FK_dbo.Journey_dbo.Booking_BookingId] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Journey] CHECK CONSTRAINT [FK_dbo.Journey_dbo.Booking_BookingId]
GO