
/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     8/4/2023 11:59:21 PM                         */
/*==============================================================*/
/* Creating the database */

USE master

GO

CREATE DATABASE Doctor_Attendance_Test

go

Use Doctor_Attendance_Test
go


/*==============================================================*/
/* Table: ATTENDANCE                                            */
/*==============================================================*/
create table ATTENDANCE (
   ATT_ID               int        IDENTITY(1,1)          not null,
   DEP_ID               int                  not null,
   DOCTOR_ID            int                  not null,
   DATE                 datetime             null,
   NB_HOURS             int                  null,
   ATTENDED             int                  null,
   PUBLISHED            int                  null,
   COMMENTS             varchar(100)         null
)
go
create unique index ATT_PK on ATTENDANCE (
ATT_ID ASC
)
go

alter table ATTENDANCE
   add constraint PK_ATTENDANCE primary key (DEP_ID, DOCTOR_ID)
go

/*==============================================================*/
/* Table: BELONG_TO                                             */
/*==============================================================*/
create table BELONG_TO (
   DEP_ID               int                  not null,
   SECTIONID            int                  not null
)
go

alter table BELONG_TO
   add constraint PK_BELONG_TO primary key (DEP_ID, SECTIONID)
go

/*==============================================================*/
/* Table: CATEGORY                                              */
/*==============================================================*/
create table CATEGORY (
   CATEGORY_ID          int      IDENTITY(1,1)            not null,
   TYPE                 varchar(20)          null
)
go

/*==============================================================*/
/* Index: CATEGORY_PK                                           */
/*==============================================================*/
create unique index CATEGORY_PK on CATEGORY (
CATEGORY_ID ASC
)
go

alter table CATEGORY
   add constraint PK_CATEGORY primary key nonclustered (CATEGORY_ID)
go

/*==============================================================*/
/* Table: DEPARTMENT                                            */
/*==============================================================*/
create table DEPARTMENT (
   DEP_ID               int      IDENTITY(1,1)            not null,
   DOCTOR_ID            int                  null,
   DEP_NAME             varchar(30)          null,
   NBDOCTORS            int                  null
)
go

/*==============================================================*/
/* Index: DEPARTMENT_PK                                         */
/*==============================================================*/
create unique index DEPARTMENT_PK on DEPARTMENT (
DEP_ID ASC
)
go

alter table DEPARTMENT
   add constraint PK_DEPARTMENT primary key nonclustered (DEP_ID)
go

/*==============================================================*/
/* Table: DOCTOR                                                */
/*==============================================================*/
create table DOCTOR (
   DOCTOR_ID            int      IDENTITY(1,1)            not null,
   DEP_ID               int                  null,
   CATEGORY_ID          int                  null,
   FILE_NUMBER          varchar(30)          null,
   FIRSTNAME            varchar(20)          null,
   LASTNAME             varchar(20)          null,
   AGE                  int                  null,
   EMAIL                varchar(30)          null,
   CITY                 varchar(30)          null
)
go

/*==============================================================*/
/* Index: DOCTOR_PK                                             */
/*==============================================================*/
create unique index DOCTOR_PK on DOCTOR (
DOCTOR_ID ASC
)
go

alter table DOCTOR
   add constraint PK_DOCTOR primary key nonclustered (DOCTOR_ID)
go


/*==============================================================*/
/* Table: EMPLOYEE                                              */
/*==============================================================*/
create table EMPLOYEE (
   EMP_ID               int        IDENTITY(1,1)          not null,
   DEP_ID               int                  null,
   FIRSTNAME            varchar(20)          null,
   LASTNAME             varchar(20)          null,
   AGE                  int                  null,
   EMAIL                varchar(30)          null,
   CITY                 varchar(30)          null
)
go

/*==============================================================*/
/* Index: EMPLOYEE_PK                                           */
/*==============================================================*/
create unique index EMPLOYEE_PK on EMPLOYEE (
EMP_ID ASC
)
go

alter table EMPLOYEE
   add constraint PK_EMPLOYEE primary key nonclustered (EMP_ID)
go

/*==============================================================*/
/* Table: FACULTY                                               */
/*==============================================================*/
create table FACULTY (
   FACULTYID            int      IDENTITY(1,1)            not null,
   DOCTOR_ID            int                  null,
   NAME                 varchar(50)          null
)
go

/*==============================================================*/
/* Index: FACULTY_PK                                            */
/*==============================================================*/
create unique index FACULTY_PK on FACULTY (
FACULTYID ASC
)
go

alter table FACULTY
   add constraint PK_FACULTY primary key nonclustered (FACULTYID)
go

/*==============================================================*/
/* Table: HAS                                                   */
/*==============================================================*/
create table HAS (
   FACULTYID            int                  not null,
   SECTIONID            int                  not null
)
go

alter table HAS
   add constraint PK_HAS primary key (FACULTYID, SECTIONID)
go

/*==============================================================*/
/* Table: PERMISSIONS                                           */
/*==============================================================*/
create table PERMISSIONS (
   PERMISSIONID         int       IDENTITY(1,1)           not null,
   DELETE_ATTENDENCE    int                  null,
   UPDATE_ATTENDENCE    int                  null,
   ADD_ATTENDENCE       int                  null
)
go

/*==============================================================*/
/* Index: PERMISSIONS_PK                                        */
/*==============================================================*/
create unique index PERMISSIONS_PK on PERMISSIONS (
PERMISSIONID ASC
)
go

alter table PERMISSIONS
   add constraint PK_PERMISSIONS primary key nonclustered (PERMISSIONID)
go

/*==============================================================*/
/* Table: ROLE                                                  */
/*==============================================================*/
create table ROLE (
   ROLE_ID              int       IDENTITY(1,1)           not null,
   PERMISSIONID         int                  null,
   ROLE_NAME            varchar(20)          null
)
go

/*==============================================================*/
/* Index: ROLE_PK                                               */
/*==============================================================*/
create unique index ROLE_PK on ROLE (
ROLE_ID ASC
)
go

alter table ROLE
   add constraint PK_ROLE primary key nonclustered (ROLE_ID)
go

/*==============================================================*/
/* Table: SECTION                                               */
/*==============================================================*/
create table SECTION (
   SECTIONID            int       IDENTITY(1,1)           not null,
   DOCTOR_ID            int                  null,
   NUMBER               int                  null,
   PHONE_NUMBER         varchar(30)          null,
   LOCATION             varchar(50)          null
)
go

/*==============================================================*/
/* Index: SECTION_PK                                            */
/*==============================================================*/
create unique index SECTION_PK on SECTION (
SECTIONID ASC
)
go

alter table SECTION
   add constraint PK_SECTION primary key nonclustered (SECTIONID)
go

/*==============================================================*/
/* Table: USERS                                                 */
/*==============================================================*/
create table USERS (
   USER_ID              int     IDENTITY(1,1)             not null,
   DOCTOR_ID            int                  null,
   EMP_ID               int                  null,
   ROLE_ID              int                  null,
   USERNAME             varchar(50)          null,
   PASSWORD             varchar(50)          null,
   DATE_CREATED         datetime             null,
   LAST_MODIFIED        datetime             null
)
go

/*==============================================================*/
/* Index: USERS_PK                                              */
/*==============================================================*/
create unique index USERS_PK on USERS (
USER_ID ASC
)
go

alter table USERS
   add constraint PK_USERS primary key nonclustered (USER_ID)
go

alter table ATTENDANCE
   add constraint FK_ATTENDAN_ATTENDANC_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID)
go

alter table ATTENDANCE
   add constraint FK_ATTENDAN_ATTENDANC_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
go

alter table BELONG_TO
   add constraint FK_BELONG_T_BELONG_TO_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID)
go

alter table BELONG_TO
   add constraint FK_BELONG_T_BELONG_TO_SECTION foreign key (SECTIONID)
      references SECTION (SECTIONID)
go

alter table DEPARTMENT
   add constraint FK_DEPARTME_COORDINAT_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
go

alter table DOCTOR
   add constraint FK_DOCTOR_IS_OF_TYP_CATEGORY foreign key (CATEGORY_ID)
      references CATEGORY (CATEGORY_ID)
go

alter table DOCTOR
   add constraint FK_DOCTOR_TEACHES_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID)
go

alter table EMPLOYEE
   add constraint FK_EMPLOYEE_WORK_IN_DEPARTME foreign key (DEP_ID)
      references DEPARTMENT (DEP_ID)
go

alter table FACULTY
   add constraint FK_FACULTY_MANAGES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
go

alter table HAS
   add constraint FK_HAS_HAS_FACULTY foreign key (FACULTYID)
      references FACULTY (FACULTYID)
go

alter table HAS
   add constraint FK_HAS_HAS2_SECTION foreign key (SECTIONID)
      references SECTION (SECTIONID)
go

alter table ROLE
   add constraint FK_ROLE_HAS_PERMI_PERMISSI foreign key (PERMISSIONID)
      references PERMISSIONS (PERMISSIONID)
go

alter table SECTION
   add constraint FK_SECTION_DIRECTS_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
go

alter table USERS
   add constraint FK_USERS_HAS_ROLE_ROLE foreign key (ROLE_ID)
      references ROLE (ROLE_ID)
go

alter table USERS
   add constraint FK_USERS_RELATIONS_EMPLOYEE foreign key (EMP_ID)
      references EMPLOYEE (EMP_ID)
go

alter table USERS
   add constraint FK_USERS_USES_DOCTOR foreign key (DOCTOR_ID)
      references DOCTOR (DOCTOR_ID)
go


use Doctor_Attendance_Test
insert into CATEGORY values ('contract'),('no-contract'),('permanent');
insert into DEPARTMENT values (null,'Applied Maths',10), (null,'Computer Science',15), (null,'Physics',12),
(null,'Biology',8);
insert into Doctor values (1, 1, 'f0001', 'fff','ttt', 29, 'abc@live.com', 'Beirut');
update DEPARTMENT set DOCTOR_ID = 1 where DEP_NAME = 'Computer Science'
update DEPARTMENT set DOCTOR_ID = 1 where DEP_NAME = 'Applied Maths'