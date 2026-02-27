# Descriere Proiect: Camel Invaders

**Camel Invaders - Joc 2D Space Shooter dezvoltat în Unity**

**Descriere:**
Am proiectat și implementat un joc 2D de tip Space Shooter (inspirat de Space Invaders) folosind motorul grafic Unity și limbajul C#. Proiectul evidențiază capacitatea de a scrie cod modular, scalabil și de a utiliza pattern-uri de design consacrate pentru a rezolva probleme specifice dezvoltării de jocuri.

**Aspecte Tehnice și Realizări:**

*   **Arhitectură Software:**
    *   Implementarea pattern-ului **Singleton** pentru clasa `GameMaster` pentru a centraliza managementul stării jocului (scor, valuri de inamici, condiții de victorie/înfrângere).
    *   Utilizarea **ScriptableObjects** pentru definirea datelor de configurare a valurilor de inamici (`WaveScriptableObject`), permițând designerilor să ajusteze dificultatea și structura nivelurilor direct din editor, fără a modifica codul.

*   **Programare Orientată pe Obiecte (OOP) și Modularitate:**
    *   Folosirea intensivă a **Interfețelor** (`IDamageable`, `IBuffable`) pentru a crea un sistem de interacțiune generic între entități. Aceasta permite ca diverse obiecte (jucător, inamici) să reacționeze la daune sau buff-uri într-un mod polimorfic, reducând dependențele strânse.
    *   Organizarea codului în **Namespace-uri** (`CamelInvaders.GameMaster`, `CamelInvaders.Entity`, etc.) pentru o structură clară și menținerea separării responsabilităților.

*   **Gameplay și Mecanici:**
    *   Dezvoltarea controlerului jucătorului (`Player.cs`) care gestionează input-ul, fizica mișcării și sistemele de luptă.
    *   Implementarea unui sistem de **Scut Regenerabil** și Health Management.
    *   Sistem de arme diversificat (Lasere cu rată de foc rapidă, Rachete).
    *   Logică de **Enemy Spawning** dinamică și comportament de mișcare în grup al inamicilor.

*   **UI și Feedback:**
    *   Integrarea sistemelor de UI (Health Bars, Shield Bars, meniuri) și sincronizarea acestora în timp real cu starea internă a jocului.
    *   Managementul sistemului audio pentru efecte sonore și feedback.

**Tehnologii:** C#, Unity 2022+, Git.
