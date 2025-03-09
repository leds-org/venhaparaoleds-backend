CREATE SCHEMA IF NOT EXISTS desafioledstest;

CREATE TABLE IF NOT EXISTS candidatos (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome VARCHAR(255) NOT NULL,
    data_nascimento DATE NOT NULL,
    cpf VARCHAR(11) UNIQUE NOT NULL,
	profissoes TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS concursos (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    orgao VARCHAR(100) NOT NULL,
    edital VARCHAR(10) NOT NULL,
    codigo_concurso VARCHAR(20) NOT NULL,
	vagas TEXT NOT NULL
);


INSERT INTO candidatos (nome, data_nascimento, cpf, profissoes)
VALUES
    ('Lindsey Craft', '1976-05-19', '18284508434', '[carpinteiro]'),
    ('Jackie Dawson', '1970-08-14', '31166797347', '[marceneiro, assistente administrativo]'),
    ('Cory Mendoza', '1957-02-11', '56551235392', '[carpinteiro, marceneiro]')
ON CONFLICT (cpf) DO NOTHING


INSERT INTO concursos (orgao, edital, codigo_concurso, vagas)
VALUES
    ('SEDU', '9/2016', '61828450843', '[analista de sistemas, marceneiro]'),
    ('SEJUS', '15/2017', '61828450843', '[carpinteiro, professor de matemática, assistente administrativo]'),
    ('SEJUS', '17/2017', '95655123539', '[professor de matemática]');

