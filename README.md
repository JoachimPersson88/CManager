# Checklista för CManager-applikation

## Konsolapplikation / Solution / Projekt ✅

- Applikationen är byggd som en konsolapplikation. ✔

- Solution heter CManager. ✔

- Konsol-projektet heter CManager.Presentation.ConsoleApp. ✔


## Repository (filhantering / JSON) ✅

- Det finns en Repository-klass för kunder. ✔

- Det går att spara en lista av kunder till en fil. ✔

- Filen är i JSON-format. ✔

- Det går att läsa in kunder från filen. ✔

- De inlästa kunderna läggs in i en lista i programmet. ✔

## Service (logik för kunder)

- Det finns en Service-klass för kunder. ✔

- Det går att skapa en ny kund via Service. ✔

- När kund skapas får den ett unikt Id (GUID). ✔

- Det går att hämta alla kunder från listan via Service. ✔

- Det går att hämta en specifik kund från listan via Service. ✔

- Det går att ta bort en specifik kund från listan via Service.


## Controller / Meny (konsol)

- Det finns en Controller som hanterar konsol-menyn. ✔

- Menyn loopar (man kommer alltid tillbaka till menyn efter ett val).

#### Menyn har följande alternativ:

- Skapa kund ✔

- Visar dialog som låter användaren ange: ✔
	- Förnamn ✔
	- Efternamn ✔
	- E-postadress ✔
	- Telefonnummer ✔
	- Gatuadress ✔
	- Postnummer ✔
	- Ort ✔
	- Visa kunder ✔
	
- Visar alla kunder med: ✔
	- Fullständigt namn ✔
	- E-postadress ✔
	- Visa en specifik kund ✔

 - Visar en (vald) kunds: ✔
	- Fullständigt namn ✔
	- Id ✔
	- Telefonnummer ✔
	- E-postadress ✔
	- Gatuadress ✔ 
	- Postnummer ✔
	- Ort ✔

- Ta bort en specifik kund.

- Man kan ta bort kund baserat på e-postadress.

- Ett meddelande visar att kunden har blivit borttagen.


## SOLID / Interface / Tester / Git

- Minst en SOLID-princip är tillämpad. ✔

- Service använder ett interface. ✔

- Repository använder ett interface. ✔

- Det finns minst ett enhetstest för en metod i Service. ✔

- Enhetstestet använder mock för att simulera Repository. ✔

- Flera pushar till GitHub har gjorts under projektets gång.

- Commit-meddelanden är tydliga och visar progression.