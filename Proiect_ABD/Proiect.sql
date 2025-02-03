CREATE DATABASE Proiect_ABD;
GO

USE Proiect_ABD;
GO

CREATE TABLE Roles(
	_role_id INT PRIMARY KEY,
	_role_name NVARCHAR(50)
);


CREATE TABLE Users (
    _id INT PRIMARY KEY IDENTITY,
    _name NVARCHAR(50) NOT NULL,
    _password NVARCHAR(100) NOT NULL,
	_email NVARCHAR(100) NOT NULL UNIQUE,
    _role_id INT NOT NULL,
	FOREIGN KEY (_role_id) REFERENCES Roles(_role_id)
	ON UPDATE CASCADE
);



CREATE TABLE Equipments (
	_id INT PRIMARY KEY IDENTITY,
    _name NVARCHAR(100) NOT NULL,
    _status NVARCHAR(20) NOT NULL,
    _last_update DATETIME NOT NULL
);


CREATE TABLE Reports (
    _report_id INT PRIMARY KEY IDENTITY,
    _title NVARCHAR(100) NOT NULL,
    _description NVARCHAR(MAX) NULL,
    _created_by INT NOT NULL, -- User who created the report
    _created_at DATETIME NOT NULL DEFAULT GETDATE(),
    _equipment_id INT NOT NULL, -- Optional: Link to specific equipment
    FOREIGN KEY (_created_by) REFERENCES Users(_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (_equipment_id) REFERENCES Equipments(_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE Maintenance (
    _maintenance_id INT PRIMARY KEY IDENTITY,
    _equipment_id INT NOT NULL, -- Equipment being maintained
    _performed_by INT NOT NULL, -- User who performed the maintenance
    _description NVARCHAR(MAX), -- Details of the maintenance activity
    _performed_at DATETIME NOT NULL DEFAULT GETDATE(),
    _status NVARCHAR(20) -- Status after maintenance (e.g., "Completed", "Pending")
    FOREIGN KEY (_equipment_id) REFERENCES Equipments(_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (_performed_by) REFERENCES Users(_id)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);




INSERT INTO Equipments
Values('Imprimanta', 'disponibil', GETDATE());



INSERT INTO Equipments
Values('Camera foto', 'disponibil', GETDATE()),
	  ('Mouse', 'disponibil', GETDATE()),
	  ('Tastatura', 'disponibil', GETDATE()),
	  ('Scanner', 'disponibil', GETDATE()),
	  ('Monitor', 'disponibil', GETDATE())

INSERT INTO Users
VALUES('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918','admin@outlook.com', 0), -- parola = admin
		('marian', 'a80b568a237f50391d2f1f97beaf99564e33d2e1c8a2e5cac21ceda701570312','marianaionesei7@gmail.com', 0), --parola = parola
		('catalin', '65ea5c55a9c3c88d87c704e8d8f94f3f3971e894fd2d4a29760a0aa261b2f94b','stircatalin@gmail.com', 1) -- parola = stir

delete from Users;

INSERT INTO Roles
VALUES(0, 'Administrator'),
		(1, 'Tehnician'), 
		(2, 'Mentenanta')

INSERT INTO Roles
VALUES(3, 'Manager')



