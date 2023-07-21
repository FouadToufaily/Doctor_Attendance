/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2021                    */
/* Created on:     20/7/2023  5:28:17 PM                        */
/*==============================================================*/


/* Creating the database */

USE master

GO

CREATE DATABASE Doctor_Attendance

go




/* Creating tables and indices */

Use Doctor_Attendance
go

/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     7/21/2023 1:50:24 AM                         */
/*==============================================================*/


/*==============================================================*/
/* Table: CATEGORY                                              */
/*==============================================================*/
create table CATEGORY (
   CATEGORY_ID          int       IDENTITY(1,1)           not null,
   TYPE                 varchar(20)          null,
   constraint PK_CATEGORY primary key nonclustered (CATEGORY_ID)
)
go

/*==============================================================*/
/* Table: DOCTOR                                                */
/*==============================================================*/
create table DOCTOR (
   DOCTOR_ID            int       IDENTITY(1,1)           not null,
   CATEGORY_ID          int                  null,
   FIRSTNAME            varchar(20)          null,
   LASTNAME             varchar(20)          null,
   AGE                  int                  null,
   EMAIL                varchar(30)          null,
   CITY                 varchar(30)          null,
   constraint PK_DOCTOR primary key nonclustered (DOCTOR_ID),
   constraint FK_DOCTOR_IS_OF_TYP_CATEGORY foreign key (CATEGORY_ID)
      references CATEGORY (CATEGORY_ID)
)
go

/*==============================================================*/
/* Table: DEPARTMENT                                            */
/*==============================================================*/
create table DEPARTMENT (
   DEP_ID               int         IDENTITY(1,1)         not null,
   DOCTOR_ID            int                  null,
   DEP_NAME             varchar(30)          null,
   NUMBER               int                  null,
   NBDOCTORS            int                  null,
   constraint PK_DEPARTMENT primary key nonclustered (DEP_ID),
   constraint FK_DEPARTME_COORDINAT_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Table: ATTENDANCE                                            */
/*==============================================================*/
create table ATTENDANCE (
   ATT_ID               int       IDENTITY(1,1)        not null,
   DEP_ID               int                  not null,
   DOCTOR_ID            int                  not null,
   DATE                 datetime             null,
   NB_HOURS             int                  null,
   ATTENDED             int                  null,
   PUBLISHED            int                  null,
   COMMENTS             varchar(100)         null,
   constraint PK_ATTENDANCE primary key (ATT_ID),
   constraint FK_ATTENDAN_ATTENDANC_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID),
   constraint FK_ATTENDAN_ATTENDANC_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Table: SECTION                                               */
/*==============================================================*/
create table SECTION (
   SECTIONID            int       IDENTITY(1,1)           not null,
   DOCTOR_ID            int                  null,
   NUMBER               int                  null,
   PHONE_NUMBER         varchar(30)          null,
   LOCATION             varchar(50)          null,
   constraint PK_SECTION primary key nonclustered (SECTIONID),
   constraint FK_SECTION_DIRECTS_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Table: BELONG_TO                                             */
/*==============================================================*/
create table BELONG_TO (
   DEP_ID               int                  not null,
   SECTIONID            int                  not null,
   constraint PK_BELONG_TO primary key (DEP_ID, SECTIONID),
   constraint FK_BELONG_T_BELONG_TO_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID),
   constraint FK_BELONG_T_BELONG_TO_SECTION foreign key (SECTIONID)
      references SECTION (SECTIONID)
)
go

/*==============================================================*/
/* Table: EMPLOYEE                                              */
/*==============================================================*/
create table EMPLOYEE (
   EMP_ID               int       IDENTITY(1,1)           not null,
   DEP_ID               int                  null,
   FIRSTNAME            varchar(20)          null,
   LASTNAME             varchar(20)          null,
   AGE                  int                  null,
   EMAIL                varchar(30)          null,
   CITY                 varchar(30)          null,
   constraint PK_EMPLOYEE primary key nonclustered (EMP_ID),
   constraint FK_EMPLOYEE_WORK_IN_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID)
)
go

/*==============================================================*/
/* Table: FACULTY                                               */
/*==============================================================*/
create table FACULTY (
   FACULTYID            int        IDENTITY(1,1)          not null,
   DOCTOR_ID            int                  null,
   NAME                 varchar(50)          null,
   constraint PK_FACULTY primary key nonclustered (FACULTYID),
   constraint FK_FACULTY_MANAGES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Table: HAS                                                   */
/*==============================================================*/
create table HAS (
   FACULTYID            int                  not null,
   SECTIONID            int                  not null,
   constraint PK_HAS primary key (FACULTYID, SECTIONID),
   constraint FK_HAS_HAS_FACULTY foreign key (FACULTYID)
      references FACULTY (FACULTYID),
   constraint FK_HAS_HAS2_SECTION foreign key (SECTIONID)
      references SECTION (SECTIONID)
)
go

/*==============================================================*/
/* Table: PERMISSIONS                                           */
/*==============================================================*/
create table PERMISSIONS (
   PERMISSIONID         int                  not null,
   DELETE_ATTENDENCE    int                  null,
   UPDATE_ATTENDENCE    int                  null,
   ADD_ATTENDENCE       int                  null,
   constraint PK_PERMISSIONS primary key nonclustered (PERMISSIONID)
)
go

/*==============================================================*/
/* Table: ROLE                                                  */
/*==============================================================*/
create table ROLE (
   ROLE_ID              int       IDENTITY(1,1)           not null,
   PERMISSIONID         int                  null,
   ROLE_NAME            varchar(20)          null,
   constraint PK_ROLE primary key nonclustered (ROLE_ID),
   constraint FK_ROLE_HAS_PERMI_PERMISSI foreign key (PERMISSIONID)
      references PERMISSIONS (PERMISSIONID)
)
go

/*==============================================================*/
/* Table: TEACHES                                               */
/*==============================================================*/
create table TEACHES (
   DOCTOR_ID            int                  not null,
   DEP_ID               int                  not null,
   constraint PK_TEACHES primary key (DOCTOR_ID, DEP_ID),
   constraint FK_TEACHES_TEACHES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID),
   constraint FK_TEACHES_TEACHES2_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID)
)
go

/*==============================================================*/
/* Table: "USERS"                                                */
/*==============================================================*/
create table "USERS" (
   USER_ID              int        IDENTITY(1,1)          not null,
   DOCTOR_ID            int                  null,
   EMP_ID               int                  null,
   ROLE_ID              int                  null,
   USER_USERNAME             varchar(50)          null,
   USER_PASSWORD             varchar(50)          null,
   DATE_CREATED         datetime             null,
   LAST_MODIFIED        datetime             null,
   constraint PK_USER primary key nonclustered (USER_ID),
   constraint FK_USER_USES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID),
   constraint FK_USER_RELATIONS_EMPLOYEE foreign key (EMP_ID)
      references EMPLOYEE (EMP_ID),
   constraint FK_USER_HAS_ROLE_ROLE foreign key (ROLE_ID)
      references ROLE (ROLE_ID)
)
go

use Doctor_Attendance
insert into CATEGORY values ('contract'),('no-contract'),('permanent');
insert into Doctor values (3, 'fff','ttt', 29, 'abc@live.com', 'Beirut');
insert into DEPARTMENT values (1,'Applied Maths',1,10), (1,'Computer Science',2,15), (1,'Physics',3,12),
(1,'Biology',4,8);