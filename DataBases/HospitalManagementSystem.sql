create database HospitalManagementSystem;

create table Patient(
patientID int identity(1,1) primary key,
firstname varchar(50) not null,
lastname varchar(40) ,
dob datetime not null,
gender varchar(30) check(gender in ('Male','Female','Others')),
contactnumber varchar(10) not null unique,
[address] varchar(20) not null
);


create table Doctors(
doctorId int identity(1,1) primary key,
firstname varchar(50) not null,
lastname varchar(40),
specialization varchar(30) not null,
contactnumber varchar(10) not null unique,
);

create table Appointments(
appointmentID int identity(1,1) primary key,
patientID int not null,
doctorID int not null,
appointmentdate datetime default getdate(),
[description] varchar(30)
foreign key (patientID) references Patient(patientId),
foreign key (doctorID) references Doctors(doctorId)
);