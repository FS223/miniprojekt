-- CreateEnum
CREATE TYPE "Geschlecht" AS ENUM ('MAENNLICH', 'WEIBLICH', 'DIVERS');

-- CreateEnum
CREATE TYPE "Rolle" AS ENUM ('ADMIN', 'PERSONAL', 'KUNDE');

-- CreateEnum
CREATE TYPE "Mitgliedstatus" AS ENUM ('BRONZE', 'SILBER', 'GOLD', 'PLATINUM');

-- CreateTable
CREATE TABLE "Account" (
    "id" TEXT NOT NULL,
    "userId" TEXT,
    "benutzername" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "passwort" TEXT NOT NULL,

    CONSTRAINT "Account_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "User" (
    "id" TEXT NOT NULL,
    "vorname" TEXT NOT NULL,
    "nachname" TEXT,
    "geburtstag" DATE,
    "geschlecht" "Geschlecht",
    "anschriftId" TEXT NOT NULL,
    "iban" VARCHAR(34) NOT NULL,
    "bild" TEXT,
    "rolle" "Rolle",
    "mitgliedstatus" "Mitgliedstatus",

    CONSTRAINT "User_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Anschrift" (
    "id" TEXT NOT NULL,
    "land" TEXT NOT NULL,
    "plz" TEXT NOT NULL,
    "ort" TEXT NOT NULL,
    "strasse" TEXT NOT NULL,
    "hausnummer" TEXT NOT NULL,
    "zusatz" TEXT,

    CONSTRAINT "Anschrift_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Kurs" (
    "id" TEXT NOT NULL,
    "bezeichnung" TEXT NOT NULL,
    "beschreibung" TEXT,
    "kursLeiter" TEXT NOT NULL,
    "kursLeiterId" TEXT NOT NULL,
    "minTeilnehmer" INTEGER NOT NULL DEFAULT 0,
    "maxTeilnehmer" INTEGER NOT NULL DEFAULT 16,
    "preis" DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    "dauer" INTEGER NOT NULL DEFAULT 45,

    CONSTRAINT "Kurs_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Termin" (
    "id" TEXT NOT NULL,
    "startZeit" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "dauer" INTEGER NOT NULL DEFAULT 45,
    "bezeichnung" TEXT,
    "kursId" TEXT,

    CONSTRAINT "Termin_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "_KursToUser" (
    "A" TEXT NOT NULL,
    "B" TEXT NOT NULL
);

-- CreateIndex
CREATE UNIQUE INDEX "Account_userId_key" ON "Account"("userId");

-- CreateIndex
CREATE UNIQUE INDEX "Account_benutzername_key" ON "Account"("benutzername");

-- CreateIndex
CREATE UNIQUE INDEX "Account_email_key" ON "Account"("email");

-- CreateIndex
CREATE UNIQUE INDEX "User_anschriftId_key" ON "User"("anschriftId");

-- CreateIndex
CREATE UNIQUE INDEX "Kurs_id_key" ON "Kurs"("id");

-- CreateIndex
CREATE UNIQUE INDEX "Termin_id_key" ON "Termin"("id");

-- CreateIndex
CREATE UNIQUE INDEX "_KursToUser_AB_unique" ON "_KursToUser"("A", "B");

-- CreateIndex
CREATE INDEX "_KursToUser_B_index" ON "_KursToUser"("B");

-- AddForeignKey
ALTER TABLE "Account" ADD CONSTRAINT "Account_userId_fkey" FOREIGN KEY ("userId") REFERENCES "User"("id") ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "User" ADD CONSTRAINT "User_anschriftId_fkey" FOREIGN KEY ("anschriftId") REFERENCES "Anschrift"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Termin" ADD CONSTRAINT "Termin_kursId_fkey" FOREIGN KEY ("kursId") REFERENCES "Kurs"("id") ON DELETE SET NULL ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "_KursToUser" ADD CONSTRAINT "_KursToUser_A_fkey" FOREIGN KEY ("A") REFERENCES "Kurs"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "_KursToUser" ADD CONSTRAINT "_KursToUser_B_fkey" FOREIGN KEY ("B") REFERENCES "User"("id") ON DELETE CASCADE ON UPDATE CASCADE;
