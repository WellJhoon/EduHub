Create database EduAsyncHub

use EduAsyncHub

-- Tabla de Roles
CREATE TABLE Roles (
    RolID INT PRIMARY KEY,
    NombreRol VARCHAR(50) NOT NULL
);

-- Inserción de roles
INSERT INTO Roles (RolID, NombreRol) VALUES
    (1, 'Estudiante'),
    (2, 'Profesor'),
    (3, 'Administrador');

	select * from Roles

CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    CorreoElectronico VARCHAR(100) UNIQUE NOT NULL,
    Contraseña VARCHAR(255) NOT NULL,
    FotoPerfil VARCHAR(255),
    DescripcionBreve VARCHAR(255),
    Intereses VARCHAR(255),
    Habilidades VARCHAR(255),
    ConfiguracionPrivacidad BIT,
    ConfiguracionNotificaciones BIT,
    RolID INT,
    Permisos BIT,
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

select * from Usuarios

-- Tabla de Carreras
CREATE TABLE Carreras (
    CarreraID INT PRIMARY KEY,
    NombreCarrera VARCHAR(100) NOT NULL
);

-- Inserción de carreras 
INSERT INTO Carreras (CarreraID, NombreCarrera) VALUES
    (1, 'Ingeniería Informática'),
    (2, 'Ciencias de la Computación'),
    (3, 'Desarrollo de Software'),
    (4, 'Redes y Comunicaciones'),
    (5, 'Seguridad Informática');

    INSERT INTO Carreras  (CarreraID, NombreCarrera) VALUES (6, 'No matriculado');

	select * from Carreras

-- Tabla de Estudiantes
CREATE TABLE Estudiantes (
    EstudianteID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT UNIQUE,
    CarreraID INT,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (CarreraID) REFERENCES Carreras(CarreraID)
);

	select * from Estudiantes


-- Tabla de Profesores
CREATE TABLE Profesores (
    ProfesorID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT UNIQUE,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

select * from Profesores

-- Tabla de Materias
CREATE TABLE Materias (
    MateriaID INT PRIMARY KEY IDENTITY(1,1),
    NombreMateria VARCHAR(100) NOT NULL
);

-- Inserción de materias
INSERT INTO Materias (NombreMateria) VALUES
    ('Programación Avanzada'),
    ('Bases de Datos'),
    ('Redes de Computadoras'),
    ('Seguridad en Sistemas'),
    ('Desarrollo Web'),
    ('Inteligencia Artificial'),
    ('Sistemas Operativos'),
    ('Diseño de Algoritmos'),
    ('Ciberseguridad'),
    ('Desarrollo de Aplicaciones Móviles'),
    ('Cloud Computing'),
    ('Análisis de Datos'),
    ('Ingeniería de Software'),
    ('Realidad Virtual'),
    ('Cómputo Cuántico'),
    ('Desarrollo Ágil'),
    ('Machine Learning'),
    ('Blockchain'),
    ('Diseño de Redes'),
    ('Programación Funcional'),
    ('Arquitectura de Software'),
    ('Computación en la Nube'),
    ('Big Data'),
    ('Robótica'),
    ('Ciberdefensa'),
    ('Interacción Humano-Computadora'),
    ('Computación Gráfica'),
    ('IoT (Internet of Things)');

-- Tabla de CarrerasMaterias 
CREATE TABLE CarrerasMaterias (
    MateriaCarreraID INT PRIMARY KEY IDENTITY(1,1),
    CarreraID INT,
    MateriaID INT,
    FOREIGN KEY (CarreraID) REFERENCES Carreras(CarreraID),
    FOREIGN KEY (MateriaID) REFERENCES Materias(MateriaID)
);

-- Asignación de materias a carreras
INSERT INTO CarrerasMaterias (CarreraID, MateriaID) VALUES
(1,2), (1,3), (1,4), (1,7), (1,8), (1,12), (1,19), (1,20), (1,21), (1,24), (1,27), (1,28), --Ingeniería Informática
(2,2), (2,3), (2,4), (2,7), (2,8), (2,12), (2,14), (2,15), (2,17), (2,18), (2,20), (2,21), (2,23), (2,24), (2,25), (2,26), (2,27), (2,28), --Ciencias de la computacion
(3,1), (3,2), (3,4), (3,5), (3,6), (3,8), (3,10), (3,11), (3,12), (3,13), (3,14), (3,16), (3,17), (3,18), (3,20), (3,21), (3,22), (3,28), --Desarrollo de software
(4,2), (4,3), (4,4), (4,7), (4,8), (4,9), (4,11), (4,15), (4,19), (4,22), (4,24), (4,25), (4,26), (4,27), (4,28), -- Redes y comunicaciones
(5,2), (5,3), (5,4), (5,7), (5,8), (5,9), (5,11), (5,15), (5,18), (5,19), (5,25), (5,28); -- Seguridad informatica


-- Modificación Tabla de ProfesorMateria
CREATE TABLE ProfesorMateria (
    AsignacionMateriaID INT PRIMARY KEY IDENTITY(1,1),
    ProfesorID INT,
    MateriaID INT,
    FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID),
    FOREIGN KEY (MateriaID) REFERENCES Materias(MateriaID)
);

select * from ProfesorMateria


-- Modificación de la tabla de EstudianteMateria
CREATE TABLE EstudianteMateria (
    InscripcionMateriaID INT PRIMARY KEY IDENTITY(1,1),
    EstudianteID INT,
    MateriaID INT,
    FOREIGN KEY (EstudianteID) REFERENCES Estudiantes(EstudianteID),
    FOREIGN KEY (MateriaID) REFERENCES Materias(MateriaID)
);

select * from EstudianteMateria

-- Tabla de Asignaciones
CREATE TABLE Asignaciones (
    AsignacionID INT PRIMARY KEY IDENTITY(1,1),
    MateriaID INT,
    ProfesorID INT,
    Titulo VARCHAR(100) NOT NULL,
    Descripcion TEXT,
    FechaPublicacion DATETIME DEFAULT GETDATE(),
    FechaVencimiento DATE,
    FOREIGN KEY (MateriaID) REFERENCES Materias(MateriaID),
    FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID)
);

select * from Asignaciones

-- Tabla de RespuestasEstudiantes
CREATE TABLE RespuestasEstudiantes (
    RespuestaID INT PRIMARY KEY IDENTITY(1,1),
    EstudianteID INT,
    AsignacionID INT,
    Respuesta TEXT,
    Calificacion FLOAT,
    ComentariosProfesor TEXT,
    FOREIGN KEY (EstudianteID) REFERENCES Estudiantes(EstudianteID),
    FOREIGN KEY (AsignacionID) REFERENCES Asignaciones(AsignacionID)
);

-- Tabla de Asistencia
CREATE TABLE Asistencia (
    AsistenciaID INT PRIMARY KEY IDENTITY(1,1),
    EstudianteID INT,
    MateriaID INT,
    ProfesorID INT,
    FechaAsistencia DATE NOT NULL,
    Asistio BIT,
    FOREIGN KEY (EstudianteID) REFERENCES Estudiantes(EstudianteID),
    FOREIGN KEY (MateriaID) REFERENCES Materias(MateriaID),
    FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID)
);


-- Tabla de Calificaciones
CREATE TABLE Calificaciones (
    CalificacionID INT PRIMARY KEY IDENTITY(1,1),
    EstudianteID INT,
    MateriaID INT,
    ProfesorID INT,
    Calificacion FLOAT NOT NULL,
    FechaPublicacion DATE NOT NULL,
    FOREIGN KEY (EstudianteID) REFERENCES Estudiantes(EstudianteID),
    FOREIGN KEY (MateriaID) REFERENCES Materias(MateriaID),
    FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID)
);


-- Tabla de Auditoria
CREATE TABLE Auditoria (
    AuditoriaID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT,
    Accion VARCHAR(255) NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);

