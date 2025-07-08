/*
  Warnings:

  - You are about to drop the column `CPF` on the `Candidate` table. All the data in the column will be lost.
  - Added the required column `cpf` to the `Candidate` table without a default value. This is not possible if the table is not empty.

*/
-- RedefineTables
PRAGMA defer_foreign_keys=ON;
PRAGMA foreign_keys=OFF;
CREATE TABLE "new_Candidate" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "cpf" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "birthdate" DATETIME NOT NULL,
    "professions" TEXT NOT NULL
);
INSERT INTO "new_Candidate" ("birthdate", "id", "name", "professions") SELECT "birthdate", "id", "name", "professions" FROM "Candidate";
DROP TABLE "Candidate";
ALTER TABLE "new_Candidate" RENAME TO "Candidate";
CREATE UNIQUE INDEX "Candidate_cpf_key" ON "Candidate"("cpf");
PRAGMA foreign_keys=ON;
PRAGMA defer_foreign_keys=OFF;
