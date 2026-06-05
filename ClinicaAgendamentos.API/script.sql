CREATE TABLE IF NOT EXISTS Usuarios (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Email TEXT NOT NULL,
                                        Senha TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Pacientes (
                                         Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                         Nome TEXT NOT NULL,
                                         Cpf TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Profissionais (
                                             Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                             Nome TEXT NOT NULL,
                                             Especialidade TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Consultas (
                                         Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                         PacienteId INTEGER NOT NULL,
                                         ProfissionalId INTEGER NOT NULL,
                                         DataHoraInicio TEXT NOT NULL,
                                         DataHoraFim TEXT NOT NULL,
                                         FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id),
    FOREIGN KEY (ProfissionalId) REFERENCES Profissionais(Id)
    );

-- Seed: usuário de teste (senha: 123456)
INSERT OR IGNORE INTO Usuarios (Email, Senha) 
VALUES ('admin@clinica.com', '$2a$11$SynxYXAXfTAgHwvL8rH03OuwBJZ9uvbsxBpImO3Iq2hPqpsAhyK8K');