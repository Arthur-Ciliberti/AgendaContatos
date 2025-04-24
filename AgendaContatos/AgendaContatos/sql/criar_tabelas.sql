CREATE TABLE contato (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    apelido VARCHAR(50),
    cpf CHAR(11),
    telefone VARCHAR(20),
    email VARCHAR(100),
    data_cadastro TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    data_ultima_alteracao TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE contato_backup (
    id INT,
    nome VARCHAR(100),
    apelido VARCHAR(50),
    cpf CHAR(11),
    telefone VARCHAR(20),
    email VARCHAR(100),
    data_cadastro TIMESTAMP WITHOUT TIME ZONE,
    data_ultima_alteracao TIMESTAMP WITHOUT TIME ZONE,
    data_exclusao TIMESTAMP WITHOUT TIME ZONE
);