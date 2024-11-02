
create database DBVENTAS_TEST
use DBVENTAS_TEST

CREATE SCHEMA SAL
CREATE SCHEMA SEG



create table SEG.ROL(
ROL_ID int primary key identity(1,1),
ROL_NAME varchar(50) NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


create table SEG.MODULE(
MODULE_ID int primary key identity(1,1),
MODULE_NAME varchar(50),
MODULE_ICON varchar(50),
MODULE_ROUTE varchar(50),
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

CREATE TABLE SEG.MODULE_ROL(
MODULE_ROL_ID int primary key identity(1,1),
MODULE_ID int references SEG.MODULE(MODULE_ID),
ROL_ID int references SEG.ROL(ROL_ID),
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)



CREATE TABLE SEG.[USER](
USER_ID int primary key identity(1,1),
USER_NAME varchar(100),
USER_USERNAME varchar(40),
USER_PASSWORD varchar(100),
ROL_ID int references SEG.ROL(ROL_ID),
USER_MAIL varchar(40),
USER_STATUS bit,
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


CREATE TABLE SAL.CATEGORY(
CATEGORY_ID int primary key identity(1,1),
CATEGORY_NAME varchar(80),
CATEGORY_STATUS bit,
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)


CREATE TABLE SAL.PRODUCT (
PRODUCT_ID int primary key identity(1,1),
PRODUCT_NAME varchar(100),
CATEGORY_ID int references SAL.CATEGORY(CATEGORY_ID),
PRODUCT_STOCK int,
PRODUCT_PRICE decimal(10,2),
PRODUCT_STATUS bit,
PRODUCT_IMAGE varchar(50),
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.NUMBERING_TYPE(
NUM_TYPE_ID int primary key identity(1,1),
NUM_TYPE_NAME VARCHAR(2) NOT NULL,
NUM_TYPE_DESCRIPTION VARCHAR(100) NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.NUMBERING(
NUM_ID int primary key identity(1,1),
NUM_TYPE_ID int references SAL.NUMBERING_TYPE(NUM_TYPE_ID),
NUM_SERIAL VARCHAR(4),
NUM_NAME VARCHAR(50),
NUM_NOW INT NOT NULL,
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)

create table SAL.SALE(
SALE_ID int primary key identity(1,1),
NUM_ID int references SAL.NUMBERING(NUM_ID),
NUM_NUMBER int,
SALE_TYPE_PAYMENT VARCHAR(50),
SALE_DATE DATETIME NOT NULL,
SALE_NET decimal(10,2),
SALE_BALANCE decimal(10,2),
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
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
LOG_DATE_CREATE DATETIME NOT NULL,
LOG_USER_CREATE VARCHAR(50),
LOG_DATE_UPDATE DATETIME,
LOG_USER_UPDATE VARCHAR(50),
ENDS DATETIME
)