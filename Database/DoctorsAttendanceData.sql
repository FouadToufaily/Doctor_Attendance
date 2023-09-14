use Doctor_Attendance_Test10

insert into CATEGORY values ('contract'),('no-contract'),('permanent');

/* Departments for faculty of sciences */
insert into DEPARTMENT values (null,'Mathematics',10), (null,'Computer Science & Statistics',15), (null,'Physics & Electronics',12),
(null,'Life & Earth Sciences',8), (null,'Chemistry & Biochemistry',10);

/* Departments for faculty of Public Health */
insert into DEPARTMENT values (null,'Sciences Infirmières',10), (null,'Laboratory Sciences',15), (null,'Physiothérapie',12),
(null,'Midwifery',8);

/* Departments for faculty of Law*/
insert into DEPARTMENT values (null,'Law',10), (null,'Political Sciences',10)

/* Doctors in dep 1, Mathematis */
insert into Doctor values (1, 1, 'D0001', 'Hassan','Abbas', 30, 'habbas@ul.edu.lb', 'Beirut'),
                          (1, 1, 'D0002', 'Ibtissam','Zeaiter', 30, 'zibtissambbas@ul.edu.lb', 'Beirut'),
                          (1, 1, 'D0003', 'Hassan','Ibrahim', 30, 'Ihassan@ul.edu.lb', 'Beirut'),
                          (1, 2, 'D0004', 'Zeinab','Salloum', 30, 'szeinab@ul.edu.lb', 'Beirut'),
                          (1, 1, 'D0005', 'Wael','Youssef', 30, 'ywael@ul.edu.lb', 'Beirut'),
                          (1, 1, 'D0006', 'Youssef','Ayyad', 30, 'ayoussef@ul.edu.lb', 'Beirut'),

/* Doctors in dep 2, Computer Science and Statistics */
                          (2, 1, 'D0007', 'Zeinab','Assaghir', 30, 'azeinab@ul.edu.lb', 'Beirut'),
                          (2, 1, 'D0008', 'Ahmad','Faour', 30, 'fahmad@ul.edu.lb', 'Beirut'),
                          (2, 2, 'D0009', 'Iqbal','Hussein', 30, 'hiqbal@ul.edu.lb', 'Beirut'),
                          (2, 1, 'D0010', 'Antoun','Yaakoub', 30, 'yantoun@ul.edu.lb', 'Beirut'),
                          (2, 1, 'D0011', 'Zein','Ibrahim', 30, 'izein@ul.edu.lb', 'Beirut'),
                          (2, 3, 'D0012', 'Kamal','Beydoun', 30, 'bkamal@ul.edu.lb', 'Beirut'),
                          (2, 1, 'D0013', 'Bassem','Haidar', 30, 'hbassem@ul.edu.lb', 'Beirut');

/* Doctors in dep 3, Physics and Electronics */
insert into Doctor values (3, 1, 'D0014', 'Yasser','Mhanna', 30, 'yamoha@ul.edu.lb', 'Beirut');
insert into Doctor values (3, 1, 'D0015', 'Oussama','Bazzi', 30, 'boussama@ul.edu.lb', 'Beirut');
insert into Doctor values (3, 2, 'D0016', 'Hassan','Ghamlouch', 30, 'ghassan@ul.edu.lb', 'Beirut');
insert into Doctor values (3, 1, 'D0017', 'Rafik','Kattan', 30, 'krafik@ul.edu.lb', 'Beirut');
insert into Doctor values (3, 3, 'D0018', 'Salah','Hamieh', 30, 'hsalah@ul.edu.lb', 'Beirut');
insert into Doctor values (3, 1, 'D0019', 'Mouenes','Fadlallah', 30, 'fmouenes@ul.edu.lb', 'Beirut');
insert into Doctor values (3, 1, 'D0020', 'Wissam','Mouzannar', 30, 'mwissam@ul.edu.lb', 'Beirut');

/* Doctors in dep 4, Life and Earth Sciences */
insert into Doctor values (4, 1, 'D0021', 'Mohamad','Mortada', 30, 'mortadamh@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0022', 'Ziad','Abdelrazzak', 30, 'aziad@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 2, 'D0023', 'Ali','Chaker', 30, 'chali@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 3, 'D0024', 'Kassem','Hamze', 30, 'khamze@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 2, 'D0025', 'Mahmoud','Homsi', 30, 'hmahmoud@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0026', 'Nabil','Amacha', 30, 'anabil@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0027', 'Haidar','Akl', 30, 'ahaidar@ul.edu.lb', 'Beirut');

/* Doctors in dep 5, Chemistry and Biochemistry */
insert into Doctor values (4, 2, 'D0028', 'ghassan','ibrahim', 30, 'gibrahim@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0029', 'Rabih','Hussein', 30, 'hrabih@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0030', 'Kamal','Hariri', 30, 'hkamal@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 2, 'D0031', 'Leila','Ghannam', 30, 'gleila@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0032', 'Mazen','Kurdi', 30, 'kmazen@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 3, 'D0033', 'Mostapha','Hamieh', 30, 'hmostapha@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0034', 'Rami','Alakoum', 30, 'arami@ul.edu.lb', 'Beirut');
insert into Doctor values (4, 1, 'D0035', 'Ali','Kanj', 30, 'kali@ul.edu.lb', 'Beirut'),

/* Doctors in dep 6 => 9,in public health faculty */
                         
                         (6, 1, 'D0036', 'Tris', 'Limpricht', 45, 'tlimpricht1@gnu.org', 'Ladysmith'),
                         (6, 1, 'D0037', 'Hermione', 'Dyster', 33, 'hdyster2@berkeley.edu', 'Boundiali'),
						 (6, 2, 'D0038', 'Kesley', 'Leighfield', 34, 'kleighfielda@pen.io', 'Mujur Satu'),
                         (6, 2, 'D0039', 'Dukie', 'Kainz', 37, 'dkainzb@domainmarket.com', 'Baota'),

                         (7, 2, 'D0040', 'Sal', 'Upsale', 38, 'supsale3@mapy.cz', 'Pawak'),
                         (7, 1, 'D0041', 'Geoff', 'Hatter', 41, 'ghatter4@ehow.com', 'Fornelos'),
                         (7, 2, 'D0042', 'Alejandra', 'Shouler', 44, 'ashouler5@cbc.ca', 'Shuigou'),
						 (7, 1, 'D0043', 'Nealy', 'MacKintosh', 46, 'nmackintoshc@ucoz.com', 'Eksjö'),

                         (8, 1, 'D0044', 'Dmitri', 'Agastina', 32, 'dagastina6@businessinsider.com', 'Quitilipi'),
                         (8, 1, 'D0045', 'Finlay', 'Lipsett', 47, 'flipsett7@who.int', 'Kanuma'),
                         (8, 1, 'D0046', 'Sibby', 'Dendle', 47, 'sdendle8@tumblr.com', 'Cassanayan'),
						 (8, 2, 'D0047', 'Meghann', 'Conti', 35, 'mcontid@t.co', 'Ljungsbro'),

                         (9, 2, 'D0048', 'Randell', 'Eary', 49, 'reary9@blogger.com', 'Daciyao'),
						 (9, 1, 'D0049', 'Reube', 'Tesimon', 30, 'rtesimon0@cnbc.com', 'Rzeczenica'),
						 (9, 1, 'D0050', 'Lelia', 'Gillani', 39, 'lgillanie@naver.com', 'Krajan'),
                         (9, 1, 'D0051', 'Dominga', 'Paddington', 48, 'dpaddingtonf@sourceforge.net', 'Moscow'),

/* Doctors in dep 6 => 9,in Law faculty */
                        (10, 1, 'D0052', 'Verina', 'Keywood', 41, 'vkeywoodg@harvard.edu', 'Saraza'),
                        (10, 2, 'D0053', 'Tiebout', 'Stairs', 38, 'tstairsh@newsvine.com', 'Serh'),
                        (10, 1, 'D0054', 'Nanice', 'Berens', 46, 'nberensi@cocolog-nifty.com', 'Russkaya Polyana'),
                        (10, 1, 'D0055', 'Erminia', 'Giacubo', 39, 'egiacuboj@goo.gl', 'Taokeng'),

                        (11, 1, 'D0056', 'Kirby', 'Weepers', 44, 'kweepersk@ibm.com', 'Jocotán'),
                        (11, 2, 'D0057', 'Gilberta', 'Vaux', 44, 'gvauxl@usatoday.com', 'Charleroi'),
                        (11, 2, 'D0058', 'Raquela', 'Littlewood', 49, 'rlittlewoodm@seattletimes.com', 'Hujiaba'),
                        (11, 2, 'D0059', 'Butch', 'Layman', 37, 'blaymann@csmonitor.com', 'Xibing');

/*HOS Doctor for Public Health Faculty */

/*Updating HOF, faculty of sciences*/
update DEPARTMENT set DOCTOR_ID = 1 where DEP_NAME = 'Mathematics'
update DEPARTMENT set DOCTOR_ID = 7 where DEP_NAME = 'Computer Science & Statistics' 
update DEPARTMENT set DOCTOR_ID = 14 where DEP_NAME = 'Physics & Electronics' 
update DEPARTMENT set DOCTOR_ID = 21 where DEP_NAME = 'Life & Earth Sciences'
update DEPARTMENT set DOCTOR_ID = 28 where DEP_NAME = 'Chemistry & Biochemistry' 

/*Updating HOF, faculty of public health*/
update DEPARTMENT set DOCTOR_ID = 36 where DEP_NAME = 'Sciences Infirmières' 
update DEPARTMENT set DOCTOR_ID = 40 where DEP_NAME = 'Laboratory Sciences'
update DEPARTMENT set DOCTOR_ID = 44 where DEP_NAME = 'Physiothérapie' 
update DEPARTMENT set DOCTOR_ID = 48 where DEP_NAME = 'Midwifery' 

/*Updating HOF, faculty of Law*/
update DEPARTMENT set DOCTOR_ID = 53 where DEP_NAME = 'Law' 
update DEPARTMENT set DOCTOR_ID = 56 where DEP_NAME = 'Political Sciences'


insert into FACULTY values (8, 'Sciences'), (36, 'Public Health'), (52,'Law');

insert into SECTION values (32, 35,'05460584','Hadath'), (23, 3,'01680248', 'Fanar'), (29, 26,'06386364', 'Tripoli')
                         , (35, 17,'08812553', 'Bikaa'), (11, 11,'07761980', 'Nabatieh');

/* HOD as Users */
insert into Users values (1,null,2,'habbas@ul.edu.lb','pass123',null,null);
insert into Users values (7,null,2,'azeinab@ul.edu.lb','pass123',null,null);
insert into Users values (14,null,2,'yamoha@ul.edu.lb','pass123',null,null);
insert into Users values (21,null,2,'mortadamh@ul.edu.lb','pass123',null,null);
insert into Users values (28,null,2,'gibrahim@ul.edu.lb','pass123',null,null);

insert into Users values (36,null,2,'tlimpricht1@gnu.org','pass123',null,null);
insert into Users values (40,null,2,'supsale3@mapy.cz', 'Pass123',null,null);
insert into Users values (44,null,2,'dagastina6@businessinsider.com','pass123',null,null);
insert into Users values (48,null,2,'reary9@blogger.com','pass123',null,null);

insert into Users values (53,null,2,'tstairsh@newsvine.com','pass123',null,null);
insert into Users values (56,null,2,'kweepersk@ibm.com','pass123',null,null);

/* HOF as users */
insert into Users values (8,null,2,'fahmad@ul.edu.lb','pass123',null,null);
insert into Users values (36,null,2,'tlimpricht1@gnu.org','pass123',null,null);
insert into Users values (52,null,2,'vkeywoodg@harvard.edu','pass123',null,null);

/* HOS as users */
insert into Users values (32,null,2,'kmazen@ul.edu.lb','pass123',null,null);
insert into Users values (23,null,2,'chali@ul.edu.lb','pass123',null,null);
insert into Users values (29,null,2,'hrabih@ul.edu.lb','pass123',null,null);
insert into Users values (35,null,2,'kali@ul.edu.lb','pass123',null,null);
insert into Users values (11,null,2,'izein@ul.edu.lb','pass123',null,null);

/* Employees as Employees */
insert into Employee values (1,'Tala','Ali',30,'atala@gmail.com','Beirut');
insert into Employee values (2,'Hala','Najem',30,'nhala@gmail.com','Beirut');

insert into Employee values (1,'Admin1','Test',30,'admin1@gmail.com','Beirut');

/* Employee as User*/
insert into Users values (null,1, 1,'atala@gmail.com','pass123',null,null);
insert into Users values (null,2, 1,'nhala@gmail.com','pass123',null,null);

/* Admin */
insert into Users values (null,3, 5,'admin1@gmail.com','pass123',null,null);


insert into HAS values (1,1), (1,2) , (1,3), (1,4), (1,5); /* faculty of sciences has 5 sections */
insert into HAS values (2,1), (2,2) , (2,3), (2,4), (2,5); /* faculty of public health has 5 sections */
insert into HAS values (3,1), (3,2) , (3,3), (3,4), (3,5); /* faculty of law has 5 sections */

insert into BELONG_TO values (1,1), (2,1) , (3,1), (4,1), (5,1);
insert into BELONG_TO values (6,2), (7,2) , (8,2), (9,2);
insert into BELONG_TO values (10,3), (11,3);

insert into Role values (null, 'Secretary'), (null, 'HOD'),(null, 'HOF'),(null, 'HOS'),(null, 'Admin');
