# Témalaboratórium - A Unity keretrendszer megismerése

## Dokumentáció

Játékunk, az Entropy Wars, egy shooter arcade mini game, ahol a játékos űrhajójával egy űrbéli háborút vív ellenséges űrlények hajói ellen. Képes az ellenséges egységeket lőni, az ellenséges egységek eliminálásával és az életbenmaradással gyűjt pontot magának a játékos. Ezen pontok gyűjtése a fő cél. A játékmenet nehézsége az által nő, hogy hány pontot gyűjtött már a játékmenet során, így lépkedve szintről szintre. A játékosnak 3 élete van. Ellenséges egységbe való ütközéskor csökken az életpontja a játékosnak. Egy ilyen ütközéskor az űrhajó körül egy robbanás történik, ami eliminál a körzetben minden egységet - de az ilyenkor eliminált egységekért nem jár pont -, illetve egy rövid timeout erejéig sérthetetlenné válik az űrhajó, azután újra hatással vannak rá ütközések. A játékmenet az életpontok elvesztésével zárul, azonban lehetősége van a játékosnak a gyűjtött pontok felével újrakezdeni a játékot.

### Technikai jellemzők

- 2D játéktér parallax háttérrel.
- Központban a játékos űrhajója, minimálisan késleltett képernyőkövetéssel
- Konstans sebességű mozgás WASD billentyűkkel, a hajó az egér irányában néz
- Lövés bal egérkattintásra, a lövés automatizálható a kattintás tartásával
- A boostok 2 elemű FIFO-ba töltődnek be
- Az eredmények XML formátumban tárolódnak le, a Unity crossplatform választja ki a megfelelő perzisztens tárolási módszert.

### Specifikáció

A pontos specifikáció megtalálható itt: [https://github.com/triszt4n/unity-project/blob/master/Docs/specification.md](https://github.com/triszt4n/unity-project/blob/master/Docs/specification.md)

## Projekt beszámoló

### Projektmenedzsmentünk alapjai

Csapatunk (Bucsy Benjámin, Kovács Bertalan, Piller Trisztán, Törő Anna Nina) a GitHub lehetőségeinek felhasználásával fejlesztette a játékot: az oldalon van a [git repository-nk](https://github.com/triszt4n/unity-project/), illetve ugyanitt vezettük fel a ticketeket issue-k formájában, készítettünk project board-ot, valamint a GitHub Actions segítségével állítottunk fel build workflow-t, amelyet a GitHub Pages segítségével [szolgálunk ki egy weboldalon](https://triszt4n.github.io/unity-project/).

Október elején közösen megterveztük a játék alapjait (játéktér, célok és első ticketek). Majd utána november és december során leginkább úgy haladtunk tovább a ticketekkel, hogy szerveztünk közös kódolásos napokat, amikor összeültünk, és legtöbbször a pair programming módszerével fejlesztettünk (egyik fél kódol, másik felügyel és kutat megoldások iránt, esetleg tesztel).

Konkrét felelősség szétosztása nem volt, leginkább agilitásra törekedtünk, aki ötlettel rendelkezett, hibát vélt felfedezni, akkor pusholt egy issue-t a repository-ba, és legtöbbször vagy a közös alkalmakon, vagy önállóan kiválasztotta magának egy-egy személy, hogy azt meg is oldja (önmagát assign-olta az issue-ra), pull requestet csinált rá, ami a többie általi code review-t követően került be. A masterbe pusholást hanyagoltuk. Így történtek meg a bugfixek, refaktorok és a finomhangolások.

### Közös alkalmak

- **Közös alkalom #1:** Tervezés: alapok, prefabok megállapítása, 2D tér és elemeinek működésének felvázolása papíron magunknak
- **Közös alkalom #2:** Quality of life és utolsó funkcionalitások: kamerarezgés implementálása a robbanásoknál, kilépési gomb in-game, meghalás logika, timeout a boostok létezésére, hangeffektek
- **Közös alkalom #3:** Simítások: egyes elemek felnagyítása, UI elemek fixálása jobb helyekre, hangeffektusok finomhangolása, colliderek igazítása, zoom-olás, egyedi kurzorikon
- **Közös alkalom #4:** Utolsó simítások: tesztelés, balanszolás, apró effektek, assetek (sprite-ok) cseréje

### Az én részem a fejlesztésben

- Autómatikus projektfordítás megoldása Github Actions-el. Erre a találtam a GameCI-t, és egy cikket, amiben megoldják, hogy rögtön Github Pages-re ki legyen rakva a lefordított webgl projekt.
- A játékos mozgatásának és kamera követésének első verziója, itt még erőhatásokkal mozgattuk a hajót és csak billentyűzetről lehetett vezérelni. Később módosítva lett.
- Életcsík felrakása a UI-ra és élet adó powerup prefabjának elkészítése.
- Powerup-ok implementálása a közösen kidolgozott OO modell alapján.
- Pajzs ellenfél hozzáadása.
- Kiválasztott hangeffektek lejátszása a megfelelő eseményeknél.
- Ellenfelek felrobbanásánál megoldottam, hogy egy törmelék prefabot Instantiate- eljenek, így azt a hatást keltik, mintha darabokra esnének.
- Kisebb javítások és refactorálások:
  - Játékos mozgás darabosságának kijavítása
  - Játék mentés funkciók osztályba szervezése
  - Powerup controller átalakítása, hogy vizuális effektekteket is adjon a játékosnak
