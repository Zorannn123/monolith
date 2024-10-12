# monolith
Nakon preuzimanja koda, pokrenuti DeliveryApp u Visual Studiu 2022, prethodno kreirati bazu (DeliveryDatabase) u SQL Server Management Studiu (localhost), odabrati Windows Authentication prilikom povezivanja. 
Otvoriti package manager console i pokrenuti komandu Update-Database, ukoliko postoji problem sa postojecim migracijama, obrisati folder i ponovo napraviti migracije Add-Migration "ime migracije".
Zatim pokrenuti deliveryapp-client u Visual Studio Code-u, preuzeti potrebne pakete (npm install) preko terminala i pokrenuti komandom npm start.
