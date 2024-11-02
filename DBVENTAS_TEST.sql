
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
go

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