/*
  Warnings:

  - You are about to alter the column `contestCode` on the `Contest` table. The data in that column could be lost. The data in that column will be cast from `Int` to `BigInt`.

*/
-- RedefineTables
PRAGMA defer_foreign_keys=ON;
PRAGMA foreign_keys=OFF;
CREATE TABLE "new_Contest" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "agency" TEXT NOT NULL,
    "publicNotice" TEXT NOT NULL,
    "contestCode" BIGINT NOT NULL,
    "jobTitles" TEXT NOT NULL
);
INSERT INTO "new_Contest" ("agency", "contestCode", "id", "jobTitles", "publicNotice") SELECT "agency", "contestCode", "id", "jobTitles", "publicNotice" FROM "Contest";
DROP TABLE "Contest";
ALTER TABLE "new_Contest" RENAME TO "Contest";
CREATE UNIQUE INDEX "Contest_contestCode_key" ON "Contest"("contestCode");
PRAGMA foreign_keys=ON;
PRAGMA defer_foreign_keys=OFF;
