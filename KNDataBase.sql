USE [master]
GO

CREATE DATABASE [KNDataBase]
GO

USE [KNDataBase]
GO

CREATE TABLE [dbo].[TUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](10) NOT NULL,
 CONSTRAINT [PK_TUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[TUsuario] ON 
GO
INSERT [dbo].[TUsuario] ([IdUsuario], [Identificacion], [Nombre], [Correo], [Contrasenna]) VALUES (2, N'304590415', N'Eduardo Calvo', N'ecalvo90415@ufide.ac.cr', N'90415')
GO
SET IDENTITY_INSERT [dbo].[TUsuario] OFF
GO

CREATE PROCEDURE [dbo].[RegistroUsuario]
	@Identificacion varchar(20),
	@Nombre varchar(255),
	@Correo varchar(100),
	@Contrasenna varchar(10)
AS
BEGIN

	INSERT INTO dbo.TUsuario (Identificacion,Nombre,Correo,Contrasenna)
	VALUES (@Identificacion, @Nombre, @Correo, @Contrasenna)

END
GO

CREATE PROCEDURE [dbo].[ValidarInicioSesion]
	@Correo varchar(100),
	@Contrasenna varchar(10)
AS
BEGIN
	
	SELECT	IdUsuario,
			Identificacion,
			Nombre,
			Correo,
			Contrasenna
	  FROM	dbo.TUsuario
	  WHERE Correo = @Correo
		AND Contrasenna = @Contrasenna

END
GO