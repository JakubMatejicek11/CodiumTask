
# zadanieCodium

### Popis
Tento READ ME slúži ako návod na spustenie tejto aplikácie.
---

## Požiadavky:
1. **.NET SDK** vo verzii **8.0**
2. **Visual Studio 2022**
3. **SQL Server**
---

## Inštalácia a spustenie:

### Krok 1: Klonovanie repozitára
1. Otvorte **Visual Studio** a klonujte repozitár z GitHub:
   ```bash
   git clone https://github.com/JakubMatejicek11/CodiumTask.git
   ```
   
### Krok 2: Obnova NuGet balíčkov
1. Po otvorení projektu vo **Visual Studio** kliknite pravým tlačidlom myši na riešenie (**Solution**) v **Solution Explorer**.
2. Vyberte možnosť **Restore NuGet Packages**.
3. Týmto krokom sa stiahnu a nainštalujú všetky potrebné NuGet balíčky.

### Krok 3: Konfigurácia databázy
1. V projekte nájdete súbor **appsettings.json** v zložke **zadanieCodium**.
2. Zmeňte reťazec **ConnectionStrings:DefaultConnection** podľa vášho nastavenia databázy SQL Server.
   Príklad:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
3. Spustite migráciu databázy na vytvorenie potrebných tabuliek nasledovne:
   - Otvorte NuGet Package Manager Console cez Tools > NuGet Package Manager > Package Manager Console vo Visual Studiu.
   - Potom spustite tento príkaz na aplikovanie migrácií:
       ```bash
         Update-Database
       ```
   
## Dodatočné informácie: 

### Logovanie chýb:
Všetky chyby sa zapisujú do súborov v zložke **Logs**. Pri výskyte výnimky (na strane servera alebo klienta) sa chyba zaloguje do denníkov.
