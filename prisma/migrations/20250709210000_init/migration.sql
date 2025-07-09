CREATE TABLE "candidatos" (
    "id" TEXT NOT NULL,
    "nome" TEXT NOT NULL,
    "dataDeNascimento" TIMESTAMP(3) NOT NULL,
    "cpf" TEXT NOT NULL,
    "profissoes" TEXT[],
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "candidatos_pkey" PRIMARY KEY ("id")
);

CREATE TABLE "concursos" (
    "id" TEXT NOT NULL,
    "orgao" TEXT NOT NULL,
    "edital" TEXT NOT NULL,
    "codigo" TEXT NOT NULL,
    "vagas" TEXT[],
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,

    CONSTRAINT "concursos_pkey" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "candidatos_cpf_key" ON "candidatos"("cpf");

CREATE UNIQUE INDEX "concursos_codigo_key" ON "concursos"("codigo");
