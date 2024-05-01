# Datenbank

## Team

- **Marcel** [@AdmiralCM](https://github.com/AdmiralCM)
- Steven [@StevoMc](https://github.com/StevoMc)
- Maxi
- Nady [@Iskuzmi](https://github.com/Iskuzmi)

## Aufgaben

Ist für die Konzeptionierung, Implementierung und Administrierung der Datenbank zuständig ist.
Dazu gehört neben der Erstellung der Datenbasis gemäß der Anforderungen der anderen Gruppen auch eine Benutzerverwaltung, die Bereitstellung von Testdaten sowie eventuell die Formulierung möglichen Auswertungen auf den Daten.
Diese Gruppe ist auch zentraler Ansprechpartner für die Koordination des Gesamtprojekts.

## PostgreSQL Datenbank

Die Datenbank wird mit PostgreSQL realisiert. Die Datenbank wird auf einem eigenen Server als Docker Container gehostet.

### Datenbankmodell

Das Datenbankmodell wird mit Prisma realisiert. Hierbei handelt es sich um ein ORM (Object-Relational Mapping) Tool, das die Datenbankstruktur in einer `schema.prisma` Datei definiert. Aus dieser Datei ([[prisma/schema.prisma]]) wird dann das Datenbankmodell generiert.

#### Prisma Editor

Um das Datenbankmodell zu visualisieren, kann das Prisma Editor Tool verwendet werden:

- [Prisma Editor](https://prisma-editor.vercel.app/)

![Datenbankmodell](Datenbankmodell.png)

### Datenbankzugriff

Für Prisma wird eine Umgebungsvariable `DATABASE_URL` benötigt, die auf die Datenbank zeigt. Diese wird in der `.env` Datei definiert.

Struktur der `.env` Datei:

```bash
DATABASE_URL="postgresql://user:password@host:port/database"
````

#### Prisma Zugang

**User:** postgres
**Passwort:** Auf Nachfrage
**Host:** fs223.de
**Port:** 5432
**Datenbank:** fitnessstudio
