
CREATE database DBVENTAS_TEST
USE DBVENTAS_TEST
CREATE SCHEMA SAL
CREATE SCHEMA SEG



create table SEG.ROL(
ROL_ID int primary key identity(1,1),
ROL_NAME varchar(50) NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


create table SEG.MODULE(
MODULE_ID int primary key identity(1,1),
MODULE_NAME varchar(50),
MODULE_ICON varchar(50),
MODULE_ROUTE varchar(50),
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

CREATE TABLE SEG.MODULE_ROL(
MODULE_ROL_ID int primary key identity(1,1),
MODULE_ID int references SEG.MODULE(MODULE_ID) NOT NULL,
ROL_ID int references SEG.ROL(ROL_ID) NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)



CREATE TABLE SEG.[USER](
USER_ID int primary key identity(1,1),
USER_CODE VARCHAR(20),
USER_NAME varchar(100),
USER_USERNAME varchar(40) NOT NULL,
USER_PASSWORD varchar(100) NOT NULL,
ROL_ID int references SEG.ROL(ROL_ID) NOT NULL,
USER_MAIL varchar(40),
USER_PHONE_NUMBER varchar(40),
USER_STATUS bit DEFAULT 1,
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


CREATE TABLE SAL.CATEGORY(
CATEGORY_ID int primary key identity(1,1),
CATEGORY_NAME varchar(80) NOT NULL,
CATEGORY_STATUS bit DEFAULT 1,
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


CREATE TABLE SAL.PRODUCT (
PRODUCT_ID int primary key identity(1,1),
PRODUCT_SKU varchar(20) NOT NULL,
PRODUCT_NAME varchar(100) NOT NULL,
CATEGORY_ID int references SAL.CATEGORY(CATEGORY_ID) NOT NULL,
PRODUCT_STOCK int NOT NULL,
PRODUCT_PRICE decimal(10,2),
PRODUCT_STATUS bit DEFAULT 1,
PRODUCT_IMAGE varchar(50),
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.NUMBERING_TYPE(
NUM_TYPE_ID int primary key identity(1,1),
NUM_TYPE_NAME VARCHAR(2) NOT NULL,
NUM_TYPE_DESC VARCHAR(100) NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.NUMBERING(
NUM_ID int primary key identity(1,1),
NUM_TYPE_ID int references SAL.NUMBERING_TYPE(NUM_TYPE_ID),
NUM_SERIAL VARCHAR(4) NOT NULL,
NUM_NAME VARCHAR(50) NOT NULL,
NUM_NOW INT NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.STATUS_ORDER(
STATUS_ORDER_ID int primary key identity(1,1),
STATUS_ORDER_NAME VARCHAR(100) NOT NULL,
STATUS_ORDER_DESC VARCHAR(400),
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.SALE(
SALE_ID int primary key identity(1,1),
NUM_ID int references SAL.NUMBERING(NUM_ID),
NUM_NUMBER int,
SALE_TYPE_PAYMENT VARCHAR(50),
STATUS_ORDER_ID int references SAL.STATUS_ORDER(STATUS_ORDER_ID) NOT NULL,
SALE_DATE DATETIME NOT NULL,
SALE_NET decimal(10,2),
SALE_BALANCE decimal(10,2),
USER_DELIVER varchar(40),
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


create table SAL.SALE_DETAIL(
SALE_DETAIL_ID int primary key identity(1,1),
SALE_ID int references SAL.SALE(SALE_ID),
PRODUCT_ID int references SAL.PRODUCT(PRODUCT_ID),
SALE_DETAIL_QUANTITY int,
SALE_DETAIL_NET decimal(10,2),
SALE_DETAIL_TOTAL decimal(10,2),
SALE_DETAIL_BALANCE decimal(10,2),
LOG_DATE_CREATE DATETIME NOT NULL DEFAULT GETDATE(),
LOG_USER_CREATE VARCHAR(50) NOT NULL,
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)
ALTER AUTHORIZATION ON DATABASE::DBVENTAS_TEST TO sa;

ALTER PROC UspVentas_ValidarUsuarioExiste 
    @USUARIO VARCHAR(150),
    @CLAVE VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) 
    FROM SEG.[USER] 
    WHERE USER_USERNAME = @USUARIO 
      AND USER_PASSWORD = @CLAVE 
      AND USER_STATUS = 1
	  AND ENDS IS NULL;
END


CREATE PROC UspVentas_InsertarUsuario
    @USER_CODE VARCHAR(20),
    @USER_NAME VARCHAR(100),
    @USER_USERNAME VARCHAR(40),
    @USER_PASSWORD VARCHAR(100),
    @ROL_ID INT,
    @USER_MAIL VARCHAR(40),
    @USER_PHONE_NUMBER VARCHAR(40),
    @LOG_USER_CREATE VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SEG.[USER] (
        USER_CODE,
        USER_NAME,
        USER_USERNAME,
        USER_PASSWORD,
        ROL_ID,
        USER_MAIL,
        USER_PHONE_NUMBER,
        LOG_USER_CREATE
    )
    VALUES (
        @USER_CODE,
        @USER_NAME,
        @USER_USERNAME,
        @USER_PASSWORD,
        @ROL_ID,
        @USER_MAIL,
        @USER_PHONE_NUMBER,
        @LOG_USER_CREATE
    );
    SELECT SCOPE_IDENTITY() AS NewUserID;
END


CREATE PROCEDURE UspVentas_ValidarUsuarioLogin
    @UserName VARCHAR(40),
    @UserPassword VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        USER_CODE,
        USER_NAME,
        USER_USERNAME,
        ROL_ID,
        USER_MAIL,
        USER_PHONE_NUMBER,
        USER_STATUS
    FROM SEG.[USER]
    WHERE USER_USERNAME = @UserName 
      AND USER_PASSWORD = @UserPassword 
      AND USER_STATUS = 1;
END


--LISTAR ROLES DE USUARIO
CREATE PROCEDURE UspVentas_ListarRoles 
AS  
BEGIN  
    SET NOCOUNT ON  

	SELECT ROL_ID, ROL_NAME FROM
	SEG.ROL WHERE ENDS IS NULL
	ORDER BY ROL_ID ASC
END


CREATE PROCEDURE UspVentas_ListarUsuarios 
AS  
BEGIN  
    SET NOCOUNT ON  

	SELECT A.USER_ID,
	A.USER_CODE, 
	A.USER_NAME,
	A.USER_USERNAME,
	B.ROL_ID,
	B.ROL_NAME,
	ISNULL(A.USER_MAIL,'') AS USER_MAIL,
	ISNULL(A.USER_PHONE_NUMBER,'') AS USER_PHONE_NUMBER ,
	A.USER_STATUS
	FROM
	SEG.[USER] A 
	INNER JOIN SEG.ROL B ON A.ROL_ID = B.ROL_ID
	WHERE A.ENDS IS NULL
	ORDER BY USER_NAME ASC
END

CREATE PROCEDURE UspVentas_ListarCategoriaProductos
AS  
BEGIN  
    SET NOCOUNT ON  

	SELECT CATEGORY_ID,
	CATEGORY_NAME
	FROM
	SAL.CATEGORY WHERE ENDS IS NULL
	ORDER BY CATEGORY_NAME ASC
END

CREATE PROCEDURE UspVentas_ListarProductos
AS  
BEGIN  
    SET NOCOUNT ON  

	SELECT A.PRODUCT_ID,
	A.PRODUCT_SKU,
	A.PRODUCT_NAME,
	B.CATEGORY_ID,
	B.CATEGORY_NAME,
	A.PRODUCT_STOCK,
	A.PRODUCT_PRICE,
	A.PRODUCT_STATUS
	FROM
	SAL.PRODUCT A
	INNER JOIN SAL.CATEGORY B ON A.CATEGORY_ID=B.CATEGORY_ID
	WHERE A.ENDS IS NULL
	ORDER BY A.PRODUCT_NAME ASC
END


CREATE PROCEDURE UspVentas_EliminarUsuario
@USER_ID INT
AS  
BEGIN  
UPDATE SEG.[USER] SET ENDS=GETDATE() WHERE USER_ID = @USER_ID
END



CREATE PROCEDURE UspVentas_ActualizarUsuario
@USER_ID INT,
@USER_CODE VARCHAR(200),
@USER_NAME VARCHAR(200),
@USER_USERNAME VARCHAR(200),
@USER_MAIL  VARCHAR(200),
@USER_PHONE_NUMBER  VARCHAR(200),
@USER_STATUS BIT,
@ROL_ID int ,
@LOG_USER_UPDATE VARCHAR(50)
AS  
BEGIN  
UPDATE SEG.[USER] SET
        USER_NAME = @USER_NAME,
		USER_CODE = @USER_CODE,
        USER_USERNAME = @USER_USERNAME,
        ROL_ID = @ROL_ID,
		USER_STATUS = @USER_STATUS,
        USER_MAIL = @USER_MAIL,
        USER_PHONE_NUMBER = @USER_PHONE_NUMBER,
		LOG_DATE_UPDATE =GETDATE(),
		LOG_USER_UPDATE = @LOG_USER_UPDATE
WHERE USER_ID= @USER_ID
END



ALTER PROCEDURE UspVentas_InsertarProducto
    @PRODUCT_NAME VARCHAR(100),
    @CATEGORY_ID INT,
    @PRODUCT_STOCK INT,
    @PRODUCT_PRICE DECIMAL(10,2),
    @LOG_USER_CREATE VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SAL.PRODUCT (
        PRODUCT_NAME,
        CATEGORY_ID,
        PRODUCT_STOCK,
        PRODUCT_PRICE,
        LOG_USER_CREATE
    )
    VALUES (
        @PRODUCT_NAME,
        @CATEGORY_ID,
        @PRODUCT_STOCK,
        @PRODUCT_PRICE,
        @LOG_USER_CREATE
    );

    DECLARE @NewProductID INT = SCOPE_IDENTITY();

    UPDATE SAL.PRODUCT
    SET PRODUCT_SKU = 'SKU00' + CAST(@NewProductID AS VARCHAR)
    WHERE PRODUCT_ID = @NewProductID;

    SELECT @NewProductID AS NewProductID;
END


CREATE PROCEDURE UspVentas_EliminarProducto
@PRODUCT_ID INT
AS  
BEGIN  
UPDATE SAL.PRODUCT SET ENDS=GETDATE() WHERE PRODUCT_ID = @PRODUCT_ID
END


CREATE PROCEDURE UspVentas_ActualizarProducto
@PRODUCT_ID INT,
@PRODUCT_NAME VARCHAR(100),
@PRODUCT_STOCK INT,
@CATEGORY_ID INT,
@PRODUCT_PRICE DECIMAL(10,2),
@PRODUCT_STATUS BIT,
@LOG_USER_UPDATE VARCHAR(50)
AS  
BEGIN  
UPDATE SAL.PRODUCT SET
        PRODUCT_NAME = @PRODUCT_NAME,
		CATEGORY_ID=@CATEGORY_ID,
		PRODUCT_STOCK  = @PRODUCT_STOCK,
		PRODUCT_PRICE =  @PRODUCT_PRICE,
		PRODUCT_STATUS = @PRODUCT_STATUS,
		LOG_DATE_UPDATE =GETDATE(),
		LOG_USER_UPDATE = @LOG_USER_UPDATE
WHERE PRODUCT_ID= @PRODUCT_ID
END



ALTER PROCEDURE UspVentas_InsertarCabeceraVenta
    @NUM_ID INT,
    @NUM_NUMBER INT,
    @SALE_TYPE_PAYMENT VARCHAR(50),
    @STATUS_ORDER_ID INT,
    @SALE_NET DECIMAL(10, 2),
    @LOG_USER_CREATE VARCHAR(50),
    @SALE_ID INT OUTPUT  
AS
BEGIN

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO SAL.SALE (
            NUM_ID, 
            NUM_NUMBER, 
            SALE_TYPE_PAYMENT, 
            STATUS_ORDER_ID, 
            SALE_DATE, 
            SALE_NET, 
            SALE_BALANCE, 
            LOG_USER_CREATE
        )
        VALUES (
            @NUM_ID, 
            @NUM_NUMBER, 
            @SALE_TYPE_PAYMENT, 
            @STATUS_ORDER_ID, 
            GETDATE(), 
            @SALE_NET, 
            @SALE_NET, 
            @LOG_USER_CREATE
        );

        SET @SALE_ID = SCOPE_IDENTITY();
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH
END;



CREATE PROCEDURE UspVentas_InsertarDetalleVenta
    @SALE_ID INT,
    @PRODUCT_ID INT,
    @SALE_DETAIL_QUANTITY INT,
    @SALE_DETAIL_NET DECIMAL(10,2),
    @LOG_USER_CREATE VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO SAL.SALE_DETAIL(
            SALE_ID, 
            PRODUCT_ID, 
            SALE_DETAIL_QUANTITY, 
            SALE_DETAIL_NET, 
            LOG_USER_CREATE            
        )
        VALUES (
            @SALE_ID, 
            @PRODUCT_ID, 
            @SALE_DETAIL_QUANTITY,
            @SALE_DETAIL_NET,
            @LOG_USER_CREATE
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH
END;



ALTER PROCEDURE	UspVentas_ListarVentas --'','','1','F1011'
    @fechaInicio VARCHAR(10),        
    @fechaFin VARCHAR(10),              
    @filtroBuscar VARCHAR(1),        
    @numero VARCHAR(20)     
AS  
BEGIN  
    SET NOCOUNT ON  

    SELECT
		A.SALE_ID,
        (C.NUM_SERIAL + CAST(A.SALE_ID AS VARCHAR)) AS NUMERO,
        A.SALE_DATE,
		b.STATUS_ORDER_ID,
        B.STATUS_ORDER_NAME,
        ISNULL(A.SALE_TYPE_PAYMENT, '') AS SALE_TYPE_PAYMENT,
        A.SALE_NET
    FROM
        SAL.SALE A
    INNER JOIN
        SAL.STATUS_ORDER B ON A.STATUS_ORDER_ID = B.STATUS_ORDER_ID
    INNER JOIN
        SAL.NUMBERING C ON A.NUM_ID = C.NUM_ID
    WHERE 
        (@filtroBuscar = '1' AND (C.NUM_SERIAL + CAST(A.SALE_ID AS VARCHAR)) LIKE '%' + @numero + '%')
        OR (@filtroBuscar = '0' AND CAST(A.SALE_DATE AS DATE) BETWEEN CAST(@fechaInicio AS DATE) AND CAST(@fechaFin AS DATE))
END



CREATE PROCEDURE UspVentas_ListarDetalleVenta
    @saleId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        D.PRODUCT_ID AS PRODUCT_ID,
		P.PRODUCT_SKU AS PRODUCT_SKU,
        P.PRODUCT_NAME AS PRODUCT_NAME,
        ISNULL(D.SALE_DETAIL_QUANTITY,0) AS SALE_DETAIL_QUANTITY,
        D.SALE_DETAIL_NET AS SALE_DETAIL_NET,
        D.SALE_DETAIL_QUANTITY * D.SALE_DETAIL_NET as TOTAL
    FROM
        SAL.SALE_DETAIL D
    INNER JOIN
        SAL.PRODUCT P ON D.PRODUCT_ID = P.PRODUCT_ID
    WHERE 
        D.SALE_ID = @saleId;
END



CREATE PROCEDURE UspVentas_ModuloXRol
    @rolId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
	A.ROL_NAME,
	C.MODULE_ID,
	C.MODULE_NAME,
	C.MODULE_ICON,
	C.MODULE_ROUTE
    FROM
        SEG.ROL A
    INNER JOIN
        SEG.MODULE_ROL B ON A.ROL_ID = B.ROL_ID
	INNER JOIN
		SEG.MODULE C ON B.MODULE_ID = C.MODULE_ID
    WHERE 
        A.ROL_ID = @rolId AND B.ENDS IS NULL
END


ALTER PROCEDURE UspVentas_ListarEstadosReparto 
AS  
BEGIN  
    SET NOCOUNT ON  

	SELECT STATUS_ORDER_ID,
	STATUS_ORDER_NAME,
	STATUS_ORDER_DESC 
	FROM
	[SAL].[STATUS_ORDER] WHERE ENDS IS NULL
	ORDER BY STATUS_ORDER_ID ASC
END


CREATE PROCEDURE UspVentas_CambiarEstadoVenta
@saleId INT,
@statusOrderId INT
AS  
BEGIN  
UPDATE  SAL.SALE set STATUS_ORDER_ID =@statusOrderId  where SALE_ID= @saleId
END
