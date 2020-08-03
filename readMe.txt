Scaffold: 
Scaffold-DbContext "Server=(LocalDB)\MSSQLLocalDB;Database=Ecommerce;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Repository\Ecommerce -f

login username: malik-geechi
login password: malik-geechi

Admin Table

CREATE TABLE [dbo].[Admin](
	[adminId] [uniqueidentifier] NOT NULL,
	[username] [varchar](100) NOT NULL,
	[passwordHash] [varbinary](max) NOT NULL,
	[passwordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[adminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

----------
Cart Table

CREATE TABLE [dbo].[Cart](
	[cartId] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

----------
Cart product

CREATE TABLE [dbo].[CartProduct](
	[cartProductId] [uniqueidentifier] NOT NULL,
	[cartId] [uniqueidentifier] NOT NULL,
	[productId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NULL,
 CONSTRAINT [PK_CartProduct] PRIMARY KEY CLUSTERED 
(
	[cartProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CartProduct]  WITH CHECK ADD  CONSTRAINT [FK_CartProduct_Cart] FOREIGN KEY([cartId])
REFERENCES [dbo].[Cart] ([cartId])
GO

ALTER TABLE [dbo].[CartProduct] CHECK CONSTRAINT [FK_CartProduct_Cart]
GO

ALTER TABLE [dbo].[CartProduct]  WITH CHECK ADD  CONSTRAINT [FK_CartProduct_Product] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([productId])
GO

ALTER TABLE [dbo].[CartProduct] CHECK CONSTRAINT [FK_CartProduct_Product]
GO

----------
Category Table

CREATE TABLE [dbo].[Category](
	[categoryId] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-----------

Customer Table

CREATE TABLE [dbo].[Customer](
	[customerId] [uniqueidentifier] NOT NULL,
	[fullName] [varchar](100) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[city] [varchar](80) NOT NULL,
	[zipcode] [varchar](50) NOT NULL,
	[personalIdentityNumber] [varchar](15) NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO

ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_User]
GO

----------
Order Table

CREATE TABLE [dbo].[Order](
	[orderId] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[orderSessionId] [uniqueidentifier] NOT NULL,
	[orderDate] [datetime] NOT NULL,
	[status] [int] NOT NULL,
	[total] [decimal](7, 2) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderSession] FOREIGN KEY([orderSessionId])
REFERENCES [dbo].[OrderSession] ([orderSessionId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderSession]
GO

----------
OrderSession Table

CREATE TABLE [dbo].[OrderSession](
	[orderSessionId] [uniqueidentifier] NOT NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderSession] PRIMARY KEY CLUSTERED 
(
	[orderSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


-----------
OrderSessionProduct

CREATE TABLE [dbo].[OrderSessionProduct](
	[orderSessionProductId] [uniqueidentifier] NOT NULL,
	[orderSessionId] [uniqueidentifier] NOT NULL,
	[productId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NOT NULL,
	[createdDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderSessionProduct] PRIMARY KEY CLUSTERED 
(
	[orderSessionProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderSessionProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderSessionProduct_OrderSession] FOREIGN KEY([orderSessionId])
REFERENCES [dbo].[OrderSession] ([orderSessionId])
GO

ALTER TABLE [dbo].[OrderSessionProduct] CHECK CONSTRAINT [FK_OrderSessionProduct_OrderSession]
GO

ALTER TABLE [dbo].[OrderSessionProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderSessionProduct_Product] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([productId])
GO

ALTER TABLE [dbo].[OrderSessionProduct] CHECK CONSTRAINT [FK_OrderSessionProduct_Product]
GO

-----------
Product Table


CREATE TABLE [dbo].[Product](
	[productId] [uniqueidentifier] NOT NULL,
	[categoryId] [uniqueidentifier] NOT NULL,
	[title] [varchar](100) NOT NULL,
	[description] [varchar](max) NOT NULL,
	[price] [float] NOT NULL,
	[image] [varchar](max) NOT NULL,
	[stock] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([categoryId])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO

-------

User Table

CREATE TABLE [dbo].[User](
	[userId] [uniqueidentifier] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[passwordHash] [varbinary](max) NOT NULL,
	[passwordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
