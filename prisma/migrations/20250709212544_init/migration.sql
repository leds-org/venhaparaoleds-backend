/*
  Warnings:

  - You are about to alter the column `createdAt` on the `candidatos` table. The data in that column could be lost. The data in that column will be cast from `Unsupported("timestamp(3)")` to `DateTime`.
  - You are about to alter the column `dataDeNascimento` on the `candidatos` table. The data in that column could be lost. The data in that column will be cast from `Unsupported("timestamp(3)")` to `DateTime`.
  - You are about to alter the column `updatedAt` on the `candidatos` table. The data in that column could be lost. The data in that column will be cast from `Unsupported("timestamp(3)")` to `DateTime`.
  - You are about to alter the column `createdAt` on the `concursos` table. The data in that column could be lost. The data in that column will be cast from `Unsupported("timestamp(3)")` to `DateTime`.
  - You are about to alter the column `updatedAt` on the `concursos` table. The data in that column could be lost. The data in that column will be cast from `Unsupported("timestamp(3)")` to `DateTime`.
  - Made the column `profissoes` on table `candidatos` required. This step will fail if there are existing NULL values in that column.
  - Made the column `vagas` on table `concursos` required. This step will fail if there are existing NULL values in that column.

*/
-- RedefineTables
PRAGMA defer_foreign_keys=ON;
PRAGMA foreign_keys=OFF;
CREATE TABLE "new_candidatos" (
    "id" TEXT NOT NULL PRIMARY KEY,
    "nome" TEXT NOT NULL,
    "dataDeNascimento" DATETIME NOT NULL,
    "cpf" TEXT NOT NULL,
    "profissoes" TEXT NOT NULL,
    "createdAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" DATETIME NOT NULL
);
INSERT INTO "new_candidatos" ("cpf", "createdAt", "dataDeNascimento", "id", "nome", "profissoes", "updatedAt") SELECT "cpf", "createdAt", "dataDeNascimento", "id", "nome", "profissoes", "updatedAt" FROM "candidatos";
DROP TABLE "candidatos";
ALTER TABLE "new_candidatos" RENAME TO "candidatos";
CREATE UNIQUE INDEX "candidatos_cpf_key" ON "candidatos"("cpf");
CREATE TABLE "new_concursos" (
    "id" TEXT NOT NULL PRIMARY KEY,
    "orgao" TEXT NOT NULL,
    "edital" TEXT NOT NULL,
    "codigo" TEXT NOT NULL,
    "vagas" TEXT NOT NULL,
    "createdAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" DATETIME NOT NULL
);
INSERT INTO "new_concursos" ("codigo", "createdAt", "edital", "id", "orgao", "updatedAt", "vagas") SELECT "codigo", "createdAt", "edital", "id", "orgao", "updatedAt", "vagas" FROM "concursos";
DROP TABLE "concursos";
ALTER TABLE "new_concursos" RENAME TO "concursos";
CREATE UNIQUE INDEX "concursos_codigo_key" ON "concursos"("codigo");
PRAGMA foreign_keys=ON;
PRAGMA defer_foreign_keys=OFF;
