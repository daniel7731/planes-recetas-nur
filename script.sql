use appservice;
go
CREATE TABLE TipoAlimento (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL
);
CREATE TABLE UnidadMedida (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Simbolo NVARCHAR(10) NOT NULL
);
-- Insert basic units
INSERT INTO UnidadMedida (Nombre, Simbolo)
VALUES
('Gramos', 'g'),
('Kilogramos', 'kg'),
('Mililitro', 'Ml'),
('Litro', 'L');

INSERT INTO TipoAlimento (Nombre)
VALUES
('Verdura'),
('Fruta'),
('FrutoSeco'),
('CarneRoja'),
('CarneBlanca'),
('Grano'),
('Carbohidrato');
CREATE TABLE Tiempo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL
);

INSERT INTO Tiempo (Nombre)
VALUES
('Breaskfast'),
('HalfMorning'),
('Lunch'),
('HalfAfternoon'),
('Dinner');
CREATE TABLE Paciente (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Email NVARCHAR(150) NULL,
    Telefono NVARCHAR(50) NULL,
    Peso DECIMAL(10,2) NULL,
    Altura DECIMAL(10,2) NULL
);
CREATE TABLE Nutricionista (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1
);
CREATE TABLE Categoria (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    TipoAlimentoId INT NOT NULL,
    FOREIGN KEY (TipoAlimentoId) REFERENCES TipoAlimento(Id)
);
CREATE TABLE Ingrediente (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Calorias DECIMAL(10,2) NOT NULL,
    CategoriaId UNIQUEIDENTIFIER NOT NULL,
    UnidadId INT NOT NULL,
    FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id),
    FOREIGN KEY (UnidadId) REFERENCES UnidadMedida(Id)
);

CREATE TABLE Receta (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Instrucciones NVARCHAR(MAX) NOT NULL,
    TiempoId INT NOT NULL,
    CONSTRAINT FK_Receta_Tiempo
        FOREIGN KEY (TiempoId) REFERENCES Tiempo(Id)
);

CREATE TABLE RecetaIngrediente (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    RecetaId UNIQUEIDENTIFIER NOT NULL,
    IngredienteId UNIQUEIDENTIFIER NOT NULL,
    CantidadValor DECIMAL(10,2) NULL,
    CONSTRAINT FK_RecetaIngrediente_Receta
        FOREIGN KEY (RecetaId) REFERENCES Receta(Id) ON DELETE CASCADE,
    CONSTRAINT FK_RecetaIngrediente_Ingrediente
        FOREIGN KEY (IngredienteId) REFERENCES Ingrediente(Id)
);

CREATE TABLE PlanAlimentacion (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

    PacienteId      UNIQUEIDENTIFIER NOT NULL,
    NutricionistaId UNIQUEIDENTIFIER NOT NULL,

    FechaInicio DATE NOT NULL,
    FechaFin    DATE NOT NULL,
    DuracionDias AS DATEDIFF(DAY, FechaInicio, FechaFin) PERSISTED,
    CONSTRAINT FK_PlanAlimentacion_Paciente
        FOREIGN KEY (PacienteId) REFERENCES Paciente(Id),

    CONSTRAINT FK_PlanAlimentacion_Nutricionista
        FOREIGN KEY (NutricionistaId) REFERENCES Nutricionista(Id),

    CONSTRAINT CK_PlanAlimentacion_Duracion
        CHECK (FechaFin > FechaInicio AND DATEDIFF(DAY, FechaInicio, FechaFin) IN (15, 30))
);
CREATE TABLE Dieta (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    FechaConsumo DATETIME NOT NULL,
    PlanAlimentacionId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Dieta_PlanAlimentacion
        FOREIGN KEY (PlanAlimentacionId) REFERENCES PlanAlimentacion(Id) ON DELETE CASCADE
);

CREATE TABLE DietaReceta (
    ID INT IDENTITY(1,2) PRIMARY KEY,
    DietaId   UNIQUEIDENTIFIER NOT NULL,
    RecetaId  UNIQUEIDENTIFIER NOT NULL,
    TiempoId  INT NOT NULL, 
    Orden INT NULL,
    CONSTRAINT FK_DietaReceta_Dieta
        FOREIGN KEY (DietaId) REFERENCES Dieta(Id) ON DELETE CASCADE,
    CONSTRAINT FK_DietaReceta_Receta
        FOREIGN KEY (RecetaId) REFERENCES Receta(Id),   
    CONSTRAINT FK_DietaReceta_Tiempo REFERENCES Tiempo(Id)

);
INSERT INTO Categoria (Id, Nombre, TipoAlimentoId)
VALUES
-- 1. Verdura
(NEWID(), 'Verdura fresca', 1),
(NEWID(), 'Verdura congelada', 1),
(NEWID(), 'Verdura orgánica', 1),
(NEWID(), 'Verdura de hoja verde', 1),
(NEWID(), 'Verdura de raíz', 1),

-- 2. Fruta
(NEWID(), 'Fruta tropical', 2),
(NEWID(), 'Fruta cítrica', 2),
(NEWID(), 'Fruta de estación', 2),
(NEWID(), 'Fruta seca', 2),
(NEWID(), 'Fruta congelada', 2),

-- 3. FrutoSeco
(NEWID(), 'Nueces', 3),
(NEWID(), 'Almendras', 3),
(NEWID(), 'Castañas', 3),
(NEWID(), 'Pistachos', 3),
(NEWID(), 'Maní', 3),

-- 4. CarneRoja
(NEWID(), 'Res', 4),
(NEWID(), 'Cordero', 4),
(NEWID(), 'Cerdo', 4),
(NEWID(), 'Carne molida', 4),
(NEWID(), 'Carne curada', 4),

-- 5. CarneBlanca
(NEWID(), 'Pollo', 5),
(NEWID(), 'Pavo', 5),
(NEWID(), 'Conejo', 5),
(NEWID(), 'Pescado blanco', 5),
(NEWID(), 'Carne de ave', 5),

-- 6. Grano
(NEWID(), 'Arroz', 6),
(NEWID(), 'Trigo', 6),
(NEWID(), 'Avena', 6),
(NEWID(), 'Cebada', 6),
(NEWID(), 'Maíz', 6),

-- 7. Carbohidrato
(NEWID(), 'Pan', 7),
(NEWID(), 'Pasta', 7),
(NEWID(), 'Cereal', 7),
(NEWID(), 'Papa', 7),
(NEWID(), 'Yuca', 7);

ALTER TABLE Ingrediente
ADD CantidadValor decimal not null default 1;

-- #################################################################################
-- # INSERTS DE INGREDIENTES
-- # La Calorias y UnidadId son valores de ejemplo
-- #################################################################################

-- Categoría: Verdura de raíz (Id: 63F881A9-B533-410B-BC4C-0093990F9DA1)
DECLARE @VerduraRaizId UNIQUEIDENTIFIER = '63F881A9-B533-410B-BC4C-0093990F9DA1';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Zanahoria fresca', 41.00, @VerduraRaizId, 1),
(NEWID(), 'Remolacha cruda', 43.00, @VerduraRaizId, 1),
(NEWID(), 'Rábano picante', 48.00, @VerduraRaizId, 1),
(NEWID(), 'Jengibre (raíz)', 80.00, @VerduraRaizId, 1),
(NEWID(), 'Nabo cocido', 28.00, @VerduraRaizId, 1),
(NEWID(), 'Yuca (mandioca)', 160.00, @VerduraRaizId, 1),
(NEWID(), 'Boniato (camote)', 86.00, @VerduraRaizId, 1),
(NEWID(), 'Cebolla blanca', 40.00, @VerduraRaizId, 3),
(NEWID(), 'Puerro', 61.00, @VerduraRaizId, 3),
(NEWID(), 'Ajo (cabeza)', 149.00, @VerduraRaizId, 3);


-- Categoría: Cebada (Id: 47C51B84-BB85-49F6-96A1-0701D7144A0E)
DECLARE @CebadaId UNIQUEIDENTIFIER = '47C51B84-BB85-49F6-96A1-0701D7144A0E';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Cebada perlada', 354.00, @CebadaId, 1),
(NEWID(), 'Harina de cebada', 345.00, @CebadaId, 1),
(NEWID(), 'Malta de cebada', 370.00, @CebadaId, 1),
(NEWID(), 'Cebada en copos', 320.00, @CebadaId, 1),
(NEWID(), 'Cerveza de cebada (1 lata)', 150.00, @CebadaId, 2),
(NEWID(), 'Sopa de cebada (ración)', 200.00, @CebadaId, 2),
(NEWID(), 'Cebada tostada', 380.00, @CebadaId, 1),
(NEWID(), 'Extracto de malta', 300.00, @CebadaId, 4),
(NEWID(), 'Granos de cebada integral', 360.00, @CebadaId, 1),
(NEWID(), 'Pan de cebada (rebanada)', 90.00, @CebadaId, 3);


-- Categoría: Cereal (Id: A185F37C-EC77-4037-BF7F-0DF4555E58B9)
DECLARE @CerealId UNIQUEIDENTIFIER = 'A185F37C-EC77-4037-BF7F-0DF4555E58B9';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Trigo integral', 339.00, @CerealId, 1),
(NEWID(), 'Maíz molido', 370.00, @CerealId, 1),
(NEWID(), 'Centeno en grano', 338.00, @CerealId, 1),
(NEWID(), 'Avena instantánea (sobre)', 100.00, @CerealId, 3),
(NEWID(), 'Mijo', 378.00, @CerealId, 1),
(NEWID(), 'Quinoa cruda', 368.00, @CerealId, 1),
(NEWID(), 'Bulgur', 342.00, @CerealId, 1),
(NEWID(), 'Espelta', 338.00, @CerealId, 1),
(NEWID(), 'Hojuelas de maíz (ración)', 150.00, @CerealId, 1),
(NEWID(), 'Amaranto (semilla)', 371.00, @CerealId, 1);


-- Categoría: Arroz (Id: 5F46DCC2-156D-4EC4-8854-0E16FB78308B)
DECLARE @ArrozId UNIQUEIDENTIFIER = '5F46DCC2-156D-4EC4-8854-0E16FB78308B';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Arroz blanco cocido', 130.00, @ArrozId, 1),
(NEWID(), 'Arroz integral cocido', 111.00, @ArrozId, 1),
(NEWID(), 'Harina de arroz', 366.00, @ArrozId, 1),
(NEWID(), 'Arroz basmati crudo', 350.00, @ArrozId, 1),
(NEWID(), 'Arroz jazmín crudo', 340.00, @ArrozId, 1),
(NEWID(), 'Arroz salvaje cocido', 166.00, @ArrozId, 1),
(NEWID(), 'Arroz para sushi (cocido)', 150.00, @ArrozId, 1),
(NEWID(), 'Leche de arroz (vaso)', 120.00, @ArrozId, 2),
(NEWID(), 'Arroz frito (porción)', 250.00, @ArrozId, 1),
(NEWID(), 'Galletas de arroz inflado', 35.00, @ArrozId, 3);


-- Categoría: Cordero (Id: 313DE149-AA99-48FF-8D2A-1F6A778469AD)
DECLARE @CorderoId UNIQUEIDENTIFIER = '313DE149-AA99-48FF-8D2A-1F6A778469AD';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Chuleta de cordero (cocida)', 350.00, @CorderoId, 3),
(NEWID(), 'Pierna de cordero asada', 280.00, @CorderoId, 1),
(NEWID(), 'Costillar de cordero', 380.00, @CorderoId, 1),
(NEWID(), 'Paletilla de cordero (cruda)', 250.00, @CorderoId, 1),
(NEWID(), 'Carne picada de cordero', 270.00, @CorderoId, 1),
(NEWID(), 'Riñones de cordero', 130.00, @CorderoId, 1),
(NEWID(), 'Hígado de cordero', 135.00, @CorderoId, 1),
(NEWID(), 'Estofado de cordero (porción)', 400.00, @CorderoId, 2),
(NEWID(), 'Lomo de cordero', 290.00, @CorderoId, 1),
(NEWID(), 'Brocheta de cordero', 220.00, @CorderoId, 3);


-- Categoría: Papa (Id: 3B96F79B-7488-479F-8A43-263EBA3C9077)
DECLARE @PapaId UNIQUEIDENTIFIER = '3B96F79B-7488-479F-8A43-263EBA3C9077';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Papa cocida (con piel)', 87.00, @PapaId, 1),
(NEWID(), 'Papa frita (patatas fritas)', 312.00, @PapaId, 1),
(NEWID(), 'Puré de papa', 83.00, @PapaId, 1),
(NEWID(), 'Papa asada', 93.00, @PapaId, 1),
(NEWID(), 'Papa dulce (batata) asada', 90.00, @PapaId, 1),
(NEWID(), 'Papa al vapor', 75.00, @PapaId, 1),
(NEWID(), 'Harina de papa', 330.00, @PapaId, 1),
(NEWID(), 'Gajo de papa especiado', 180.00, @PapaId, 1),
(NEWID(), 'Papa pre-frita congelada', 250.00, @PapaId, 1),
(NEWID(), 'Papa deshidratada (copos)', 350.00, @PapaId, 1);


-- Categoría: Pescado blanco (Id: D9128E81-1D35-4418-824D-34432C572DFF)
DECLARE @PescadoBlancoId UNIQUEIDENTIFIER = 'D9128E81-1D35-4418-824D-34432C572DFF';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Merluza (cocida)', 86.00, @PescadoBlancoId, 1),
(NEWID(), 'Bacalao fresco (cocido)', 105.00, @PescadoBlancoId, 1),
(NEWID(), 'Lenguado a la plancha', 90.00, @PescadoBlancoId, 1),
(NEWID(), 'Panga (cocida)', 80.00, @PescadoBlancoId, 1),
(NEWID(), 'Dorada (a la sal)', 120.00, @PescadoBlancoId, 1),
(NEWID(), 'Fletán (halibut)', 110.00, @PescadoBlancoId, 1),
(NEWID(), 'Abadejo (cocido)', 90.00, @PescadoBlancoId, 1),
(NEWID(), 'Tilapia (filete)', 96.00, @PescadoBlancoId, 1),
(NEWID(), 'Rape (cola)', 95.00, @PescadoBlancoId, 1),
(NEWID(), 'Caballa (en conserva)', 205.00, @PescadoBlancoId, 1);


-- Categoría: Pistachos (Id: 399A37BB-4703-40E1-941F-369B2D51D952)
DECLARE @PistachosId UNIQUEIDENTIFIER = '399A37BB-4703-40E1-941F-369B2D51D952';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Pistachos crudos (sin cáscara)', 562.00, @PistachosId, 1),
(NEWID(), 'Pistachos tostados y salados', 575.00, @PistachosId, 1),
(NEWID(), 'Mantequilla de pistacho (cda)', 90.00, @PistachosId, 4),
(NEWID(), 'Helado de pistacho (taza)', 280.00, @PistachosId, 2),
(NEWID(), 'Aceite de pistacho (cda)', 120.00, @PistachosId, 4),
(NEWID(), 'Pistachos molidos (harina)', 580.00, @PistachosId, 1),
(NEWID(), 'Leche de pistacho (vaso)', 90.00, @PistachosId, 2),
(NEWID(), 'Barra energética con pistachos', 180.00, @PistachosId, 3),
(NEWID(), 'Pistachos verdes (pelados)', 570.00, @PistachosId, 1),
(NEWID(), 'Turrón de pistacho (porción)', 150.00, @PistachosId, 1);


-- Categoría: Avena (Id: C8EE09E0-8EEC-4F25-95AF-3AB6235578DA)
DECLARE @AvenaId UNIQUEIDENTIFIER = 'C8EE09E0-8EEC-4F25-95AF-3AB6235578DA';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Hojuelas de avena tradicional', 389.00, @AvenaId, 1),
(NEWID(), 'Harina de avena', 400.00, @AvenaId, 1),
(NEWID(), 'Avena instantánea', 370.00, @AvenaId, 1),
(NEWID(), 'Leche de avena (vaso)', 130.00, @AvenaId, 2),
(NEWID(), 'Salvado de avena', 246.00, @AvenaId, 1),
(NEWID(), 'Galletas de avena (unidad)', 50.00, @AvenaId, 3),
(NEWID(), 'Muesli con avena', 410.00, @AvenaId, 1),
(NEWID(), 'Granola de avena', 450.00, @AvenaId, 1),
(NEWID(), 'Avena cortada (steel-cut)', 370.00, @AvenaId, 1),
(NEWID(), 'Barra de cereal de avena', 120.00, @AvenaId, 3);


-- Categoría: Nueces (Id: 0AC3EC26-2F75-46DC-A13C-3DB9A4CA6AB6)
DECLARE @NuecesId UNIQUEIDENTIFIER = '0AC3EC26-2F75-46DC-A13C-3DB9A4CA6AB6';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Nueces de Castilla (enteras)', 654.00, @NuecesId, 1),
(NEWID(), 'Nuez pecana', 690.00, @NuecesId, 1),
(NEWID(), 'Nuez de macadamia', 718.00, @NuecesId, 1),
(NEWID(), 'Nuez de Brasil', 659.00, @NuecesId, 1),
(NEWID(), 'Mantequilla de nuez (cda)', 100.00, @NuecesId, 4),
(NEWID(), 'Nuez moscada (molida)', 525.00, @NuecesId, 5), -- UnidadId 5 puede ser gramos pequeños/pizca
(NEWID(), 'Aceite de nuez (cda)', 120.00, @NuecesId, 4),
(NEWID(), 'Nueces caramelizadas', 550.00, @NuecesId, 1),
(NEWID(), 'Tarta de nuez (porción)', 400.00, @NuecesId, 3),
(NEWID(), 'Nueces troceadas', 654.00, @NuecesId, 1);


-- Categoría: Maní (Id: F6FE0472-FDE5-439F-9921-3F7DDBCA926E)
DECLARE @ManiId UNIQUEIDENTIFIER = 'F6FE0472-FDE5-439F-9921-3F7DDBCA926E';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Maní tostado y salado', 587.00, @ManiId, 1),
(NEWID(), 'Mantequilla de maní (2 cdas)', 190.00, @ManiId, 4),
(NEWID(), 'Maní crudo', 567.00, @ManiId, 1),
(NEWID(), 'Aceite de maní (cda)', 120.00, @ManiId, 4),
(NEWID(), 'Maní japonés (con cáscara)', 500.00, @ManiId, 1),
(NEWID(), 'Snack de maní con miel', 520.00, @ManiId, 1),
(NEWID(), 'Harina de maní desgrasada', 380.00, @ManiId, 1),
(NEWID(), 'Salsa de maní (porción)', 250.00, @ManiId, 2),
(NEWID(), 'Bombón de maní y chocolate', 110.00, @ManiId, 3),
(NEWID(), 'Maní confitado', 480.00, @ManiId, 1);


-- Categoría: Carne de ave (Id: CB120002-33C9-45EB-B475-437E96E8DA49)
DECLARE @CarneAveId UNIQUEIDENTIFIER = 'CB120002-33C9-45EB-B475-437E96E8DA49';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Pechuga de pollo (sin piel, cocida)', 165.00, @CarneAveId, 1),
(NEWID(), 'Muslo de pollo (con piel, asado)', 200.00, @CarneAveId, 1),
(NEWID(), 'Pavo (pechuga, cocida)', 135.00, @CarneAveId, 1),
(NEWID(), 'Alas de pollo (fritas, unidad)', 150.00, @CarneAveId, 3),
(NEWID(), 'Carne de pato (asada)', 337.00, @CarneAveId, 1),
(NEWID(), 'Hígado de pollo', 167.00, @CarneAveId, 1),
(NEWID(), 'Pavo molido', 180.00, @CarneAveId, 1),
(NEWID(), 'Sopa de pollo (taza)', 120.00, @CarneAveId, 2),
(NEWID(), 'Gallina cocida', 230.00, @CarneAveId, 1),
(NEWID(), 'Embutido de pavo (rebanada)', 30.00, @CarneAveId, 3);


-- Categoría: Pavo (Id: A22302FA-057B-4603-A68D-49A7816CE1FE)
DECLARE @PavoId UNIQUEIDENTIFIER = 'A22302FA-057B-4603-A68D-49A7816CE1FE';
-- Nota: Dado que 'Pavo' ya se cubrió parcialmente en 'Carne de ave', estos se centran en productos específicos.
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Filete de pavo (cocido)', 135.00, @PavoId, 1),
(NEWID(), 'Jamón de pavo (rebanada)', 30.00, @PavoId, 3),
(NEWID(), 'Carne de pavo ahumada', 150.00, @PavoId, 1),
(NEWID(), 'Salchicha de pavo (unidad)', 180.00, @PavoId, 3),
(NEWID(), 'Pavo mechado (carne oscura)', 200.00, @PavoId, 1),
(NEWID(), 'Taco de pavo (unidad)', 250.00, @PavoId, 3),
(NEWID(), 'Pavo molido magro', 150.00, @PavoId, 1),
(NEWID(), 'Pavo deshuesado (crudo)', 140.00, @PavoId, 1),
(NEWID(), 'Consomé de pavo (taza)', 50.00, @PavoId, 2),
(NEWID(), 'Albóndiga de pavo (unidad)', 70.00, @PavoId, 3);


-- Categoría: Almendras (Id: 08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96)
DECLARE @AlmendrasId UNIQUEIDENTIFIER = '08082E0E-3DB0-4FC5-82AD-6BE43D1D8B96';
INSERT INTO Ingrediente (Id, Nombre, Calorias, CategoriaId, UnidadId) VALUES
(NEWID(), 'Almendras crudas (enteras)', 579.00, @AlmendrasId, 1),
(NEWID(), 'Leche de almendra (sin azúcar, vaso)', 40.00, @AlmendrasId, 2),
(NEWID(), 'Harina de almendra', 600.00, @AlmendrasId, 1),
(NEWID(), 'Mantequilla de almendra (cda)', 100.00, @AlmendrasId, 4),
(NEWID(), 'Almendras laminadas', 590.00, @AlmendrasId, 1),
(NEWID(), 'Almendras tostadas con sal', 585.00, @AlmendrasId, 1),
(NEWID(), 'Aceite de almendra (cda)', 120.00, @AlmendrasId, 4),
(NEWID(), 'Mazapán (porción)', 450.00, @AlmendrasId, 1),
(NEWID(), 'Granos de almendra (pelados)', 580.00, @AlmendrasId, 1),
(NEWID(), 'Chocolate con almendras (barra)', 550.00, @AlmendrasId, 1);