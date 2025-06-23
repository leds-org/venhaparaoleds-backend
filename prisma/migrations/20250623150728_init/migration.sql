-- CreateTable
CREATE TABLE "Candidate" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "CPF" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "birthdate" DATETIME NOT NULL,
    "professions" TEXT NOT NULL
);

-- CreateTable
CREATE TABLE "Contest" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "agency" TEXT NOT NULL,
    "publicNotice" TEXT NOT NULL,
    "contestCode" INTEGER NOT NULL,
    "jobTitles" TEXT NOT NULL
);

-- CreateIndex
CREATE UNIQUE INDEX "Candidate_CPF_key" ON "Candidate"("CPF");

-- CreateIndex
CREATE UNIQUE INDEX "Contest_contestCode_key" ON "Contest"("contestCode");
