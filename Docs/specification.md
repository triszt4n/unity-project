# 2021-22. ősz Témalaboratórium (Szoftverfejlesztés AUT) - A Unity keretrendszer megismerése

*Résztvevők:* [Bucsy Benjámin](https://github.com/poiuztrewq1), [Kovács Bertalan](https://github.com/vercibar), [Piller Trisztán](https://github.com/triszt4n), [Törő Anna Nina](https://github.com/tetsuchii)

*Git repository:* [github.com/triszt4n/unity-project](https://github.com/triszt4n/unity-project/)

*Project board-ok:* [github.com/triszt4n/unity-project/projects](https://github.com/triszt4n/unity-project/projects)

## Specifikáció 2D-s játékra: Entropy Wars

Játékunk a [Geometry Wars 2](https://www.youtube.com/watch?v=1E5b_wbspaQ) című játék klónja lesz - bő egyszerűsítésekkel. A lényeg a játékmenet működéséről:

- A játék során a játékos egy **űrhajót** irányít, amelyet egy nagyobb 2D-s térben képes irányítani.
- Az űrhajót támadják **ellenséges egységek (űrlények)**, amik különbözőféleképp tudnak mozogni és veszélyt jelenteni.
- Azonban a játékmenetet különöző **boostok** is segítik.
- A játékos az űrhajójával képes az ellenséges egységeket **lőni**, az ellenséges egységek eliminálásával gyűjt pontot magának a játékos.
- A pontok gyűjtése a fő cél. A játékmenet nehézsége az által nő (ellenséges egységek generálásának megsűrűsödése, új egységtípus megjelenése stb.), hogy hány pontot gyűjtött már a játékmenet során, így lépkedve szintről szintre.
- A játékosnak 3 élete van. Ellenséges egységbe való ütközéskor csökken az életpontja a játékosnak. Egy ilyen ütközéskor az űrhajó körül egy robbanás történik, ami eliminál a körzetben minden egységet (de az ilyenkor eliminált egységekért nem jár pont), illetve egy rövid timeout erejéig sérthetetlenné válik az űrhajó, azután újra hatással vannak rá ütközések.
- A játékmenet az életpontok elvesztésével zárul, azonban lehetősége van a játékosnak a gyűjtött pontok felével újrakezdeni a játékot (hogy ne a legelejéről kelljen újrakezdeni).
- Scoreboardon lehet megtekinteni a játékmenetek eredményeit.
- Nagy a játéktér, a kamera plánja mozog veled egy kisebb teret belátva.

*DISCLAIMER*: **Játszhatóság kedvéért még változtathatunk a dolgokon.**

### Ellenséges egységek

- Minion: egy kis csoportban generálódnak, követnek X tempóban (ahogy nehezedik a játék, annyival gyorsabbak lesznek)
- Walker: egy kis csoportban generálódnak, random mozognak
- Dodger: egyenes vonalakban mozognak, fallal való ütközéskor irányt változtat
- Snitch: amit ha sikerül elkapnod, akkor extra pont (cikkesz)
- Shielded: pajzsa van, csak egy bizonyos irányból lehet eltalálni
- Snake: kígyó, aminek testétől is sebződsz, de a fejét eltalálva pusztul csak el.

### Boostok

Az időhöz kötött boostok egy 2 elemű FIFO-ban helyezkednek el, ami azt jelenti hogy a hatások egymásra halmozhatóak. A boostok nem mozognak a térben.

#### Alkalmi boostok

- OneTimeBoost #0: Kapu, amin ha átmegy az űrhajód, felrobbannak a körülütte lévő ellenségek (pontot kapsz minden eliminált ellenségért)
- OneTimeBoost #1: +1 élet

#### Átmeneti boostok

- TemporaryBoost #0: generálódik mögötted egy holografikus másolat az űrhajódról, ami követ és utánozza, amit csinálsz, csupán 1s késleltetéssel mindezt.
- TemporaryBoost #1: pajzsot kapsz, bizonyos ideig sérthetetlen maradsz
- TemporaryBoost #2: amihez hozzáérsz, meghal, de egyben pajzsot is kapsz, és gyorsabbá válsz
