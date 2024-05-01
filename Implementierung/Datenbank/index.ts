import { PrismaClient } from "@prisma/client";

const prisma = new PrismaClient();

async function createTestData() {
	try {
		const user1 = await prisma.user.create({
			data: {
				vorname: "Max",
				nachname: "Mustermann",
				geburtstag: new Date(1990, 0, 1),
				geschlecht: "MAENNLICH",
				iban: "DE12345678901234567890",
				rolle: "KUNDE",
			},
		});

		const user2 = await prisma.user.create({
			data: {
				vorname: "Maria",
				nachname: "Musterfrau",
				geburtstag: new Date(1995, 3, 15),
				geschlecht: "WEIBLICH",
				iban: "DE98765432109876543210",
				rolle: "KUNDE",
			},
		});

		const address1 = await prisma.anschrift.create({
			data: {
				land: "Deutschland",
				plz: "12345",
				ort: "Musterstadt",
				strasse: "Hauptstraße",
				hausnummer: "1a",
			},
		});

		const address2 = await prisma.anschrift.create({
			data: {
				land: "Österreich",
				plz: "54321",
				ort: "Teststadt",
				strasse: "Nebenstraße",
				hausnummer: "5",
			},
		});

		await prisma.user.update({
			where: { id: user1.id },
			data: { anschriftId: address1.id },
		});

		await prisma.user.update({
			where: { id: user2.id },
			data: { anschriftId: address2.id },
		});
	} catch (error) {
		console.error(error);
	}
}

async function main() {
	try {
		await createTestData();
		const users = await prisma.user.findMany();
		console.log("All users:", users);
	} catch (error) {
		console.error("Error:", error);
	} finally {
		await prisma.$disconnect();
	}
}

main();
