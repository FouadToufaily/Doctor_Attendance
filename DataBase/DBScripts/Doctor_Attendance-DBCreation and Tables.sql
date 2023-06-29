/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2021                    */
/* Created on:     19/6/2023  5:28:17 PM                        */
/*==============================================================*/


/* Creating the database */

USE master

GO

CREATE DATABASE Doctor_Attendance
/*ON 
( NAME = univdbase_dat,
  FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Doctor_Attendance.mdf')
LOG ON
( NAME = 'Doctor_Attendance',
  FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Doctor_Attendance.ldf')
  */
go




/* Creating tables and indices */

Use Doctor_Attendance
go

/*==============================================================*/
/* Table: CATEGORY                                              */
/*==============================================================*/
create table CATEGORY (
   CATEGORY_ID          int                  not null,
   TYPE                 varchar(20)          null,
   constraint PK_CATEGORY primary key nonclustered (CATEGORY_ID)
)
go

/*==============================================================*/
/* Table: DOCTOR                                                */
/*==============================================================*/
create table DOCTOR (
   DOCTOR_ID            int                  not null,
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
   DEP_ID               int                  not null,
   DOCTOR_ID            int                  null,
   NUMBER               int                  null,
   NBDOCTORS            int                  null,
   constraint PK_DEPARTMENT primary key nonclustered (DEP_ID),
   constraint FK_DEPARTME_COORDINAT_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Table: ATTENDENCE                                            */
/*==============================================================*/
create table ATTENDENCE (
   DEP_ID               int                  not null,
   DOCTOR_ID            int                  not null,
   ATT_ID               int                  null,
   DATE                 datetime             null,
   NB_HOURS             int                  null,
   COMMENTS             varchar(100)         null,
   constraint PK_ATTENDENCE primary key (DEP_ID, DOCTOR_ID),
   constraint FK_ATTENDEN_ATTENDENC_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID),
   constraint FK_ATTENDEN_ATTENDENC_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Table: SECTION                                               */
/*==============================================================*/
create table SECTION (
   SECTIONID            int                  not null,
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
/* Index: COORDINATES_FK                                        */
/*==============================================================*/
create index COORDINATES_FK on DEPARTMENT (
DOCTOR_ID ASC
)
go

/*==============================================================*/
/* Table: EMPLOYEE                                              */
/*==============================================================*/
create table EMPLOYEE (
   EMP_ID               int                  not null,
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
/* Index: WORK_IN_FK                                            */
/*==============================================================*/
create index WORK_IN_FK on EMPLOYEE (
DEP_ID ASC
)
go

/*==============================================================*/
/* Table: FACULTY                                               */
/*==============================================================*/
create table FACULTY (
   FACULTYID            int                  not null,
   DOCTOR_ID            int                  null,
   NAME                 varchar(50)          null,
   constraint PK_FACULTY primary key nonclustered (FACULTYID),
   constraint FK_FACULTY_MANAGES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
)
go

/*==============================================================*/
/* Index: MANAGES_FK                                            */
/*==============================================================*/
create index MANAGES_FK on FACULTY (
DOCTOR_ID ASC
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
/* Index: DIRECTS_FK                                            */
/*==============================================================*/
create index DIRECTS_FK on SECTION (
DOCTOR_ID ASC
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
/* Table: "USER"                                                */
/*==============================================================*/
create table "USER" (
   EMP_ID2              int                  not null,
   DOCTOR_ID            int                  null,
   EMP_ID               int                  null,
   PERMISSIONID         int                  null,
   FIRSTNAME            varchar(20)          null,
   LASTNAME             varchar(20)          null,
   AGE                  int                  null,
   EMAIL                varchar(30)          null,
   CITY                 varchar(30)          null,
   DATECREATED          datetime             null,
   LASTMODIFIED         datetime             null,
   constraint PK_USER primary key nonclustered (EMP_ID2),
   constraint FK_USER_USES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID),
   constraint FK_USER_RELATIONS_EMPLOYEE foreign key (EMP_ID)
      references EMPLOYEE (EMP_ID),
   constraint FK_USER_HAS_PERMI_PERMISSI foreign key (PERMISSIONID)
      references PERMISSIONS (PERMISSIONID)
)
go

/*==============================================================*/
/* Index: USES_FK                                               */
/*==============================================================*/
create index USES_FK on "USER" (
DOCTOR_ID ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_9_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_9_FK on "USER" (
EMP_ID ASC
)
go

/*==============================================================*/
/* Index: HAS_PERMISISON_FK                                     */
/*==============================================================*/
create index HAS_PERMISISON_FK on "USER" (
PERMISSIONID ASC
)
go
