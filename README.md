# Inlämninguppgift 2
Philip Waern\
JENSEN YH, Programmering med C# och .NET, fortsättning

## Uppgiftsbeskrivning
Uppgiften handlar om att skapa backend-delen för en bankapplikation som skall
köras via webben. Eftersom banken planerar att bygga både en
mobilapplikation och en webapplikation skall lösningen innehålla ett web api
som arbetar mot en färdig databas som ni får tillgång till. Den skall skrivas med
ASP.NET Web api och innehålla inloggning och hantering av behörighet.

## Syfte med uppgiften
- Att visa att ni kan designa ett fungerande ASP.NET web api
enligt de riktlinjer vi gått igenom
- Att visa att ni kan arbeta med säkerhet i ett web api.
- Visa att ni kan hantera en mer komplex uppgift och med större
datamängder i en databas.

## Kravspecifikation
- Det skall finnas två typer av användare. Kunder och administratörer. Det skall
finnas en inloggning och hantering av användare. Applikationen skall hantera
dessa två typer av användare och både authentication och authorization. Man
måste logga in som admin för att få tillgång till admin funktionalitet. En kund
skall inte kunna komma åt denna funktionalitet och en admin skall inte få
tillgång till funktionalitet som hör till kunder.

- En Admin användare skall kunna lägga upp nya användare (kunder) och
skapa ett användarkonto som gör att kunden kan logga in. En ny användare
skall få ett bankkonto samtidigt som den skapas. Ett bankkonto skall kunna 
vara av olika typer tex sparkonto, personkonto mm. Det räcker med ett admin
konto dvs en admin behöver inte kunna lägga upp andra admins.
- När en kund är inlagd skall admin också kunna lägga upp lån för kunden. När
kunden tar ett lån skall pengarna sättas in på ett av kunden konton.
- En kund skall kunna logga in och få en översikt på alla sina konton. Där skall
man se typen av konto och det aktuella saldot. Man skall sedan kunna gå in på
varje konto och se de transaktioner som är gjorda.
- En kund skall kunna skapa fler bankkonton av olika typ till sig själv. Den skall
kunna göra överföringar mellan sina egna konton och även till andra kunder i
banken om man har deras kontonummer. I det fallet dras pengar från det egna
kontot och läggs till på den andra kundens konto.
- Ni skall utgå från den databas som ni får med exempeldata. Men det är tillåtet
att göra ändringar i den för att anpassa den till er lösning. Databasen består av
fejkade uppgifter men har likheter med en riktig struktur.
- Det skall finnas hantering av användare och vilken roll (behörighet) den har.
Ni skall använda JWT tokens för att verifiera användare.
- Lösningen måste göras objektorienterat och följa de riktlinjer som vi gått
igenom på lektionerna. Koden skall fungera och web api:et skall gå att köra
utan fel dvs alla metoder skall kunna anropas via POSTMAN och de skall
returnera lämpliga koder och data.
