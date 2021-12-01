# 2021-22. ősz Témalaboratórium (Szoftverfejlesztés AUT) - A Unity keretrendszer megismerése

*Résztvevők:* [Bucsy Benjámin](https://github.com/poiuztrewq1), [Kovács Bertalan](https://github.com/vercibar), [Piller Trisztán](https://github.com/triszt4n), [Törő Anna Nina](https://github.com/tetsuchii)

*Git repository:* [github.com/triszt4n/unity-project](https://github.com/triszt4n/unity-project/)

*Project board-ok:* [github.com/triszt4n/unity-project/projects](https://github.com/triszt4n/unity-project/projects)

## Specifikáció 2D-s játékra: Entropy Wars

Játékunkat a [Geometry Wars 2](https://www.youtube.com/watch?v=1E5b_wbspaQ) című játék inspirálta. A lényeg a játékmenet működéséről:

- A játék során a játékos egy **űrhajót** irányít, amelyet egy nagyobb 2D-s térben képes irányítani.
- Az űrhajót támadják **ellenséges egységek (űrlények)**, amik különbözőféleképp tudnak mozogni és veszélyt jelenteni.
- Azonban a játékmenetet különöző **boostok** is segítik.
- A játékos az űrhajójával képes az ellenséges egységeket **lőni**, az ellenséges egységek eliminálásával és az életbenmaradással gyűjt pontot magának a játékos.
- A pontok gyűjtése a fő cél. A játékmenet nehézsége az által nő (ellenséges egységek generálásának megsűrűsödése), hogy hány pontot gyűjtött már a játékmenet során, így lépkedve szintről szintre.
- A játékosnak 3 élete van. Ellenséges egységbe való ütközéskor csökken az életpontja a játékosnak. Egy ilyen ütközéskor az űrhajó körül egy robbanás történik, ami eliminál a körzetben minden egységet (de az ilyenkor eliminált egységekért nem jár pont), illetve egy rövid timeout erejéig sérthetetlenné válik az űrhajó, azután újra hatással vannak rá ütközések.
- A játékmenet az életpontok elvesztésével zárul, azonban lehetősége van a játékosnak a gyűjtött pontok felével újrakezdeni a játékot (hogy ne a legelejéről kelljen újrakezdeni).
- Scoreboardon lehet megtekinteni a játékmenetek eredményeit.
- Nagy a játéktér, a kamera plánja mozog veled egy kisebb teret belátva, zoom igazítható.

### Ellenséges egységek

Képernyőn kívül jönnek létre a játéktérben. Eliminálásukkor űrroncsdarabokat hagynak maguk után.

- Minion: egy kis csoportban generálódnak, követnek gyorsulva feléd (van max sebességük)
- Walker: egy kis csoportban generálódnak, random mozognak
- Bumper: egyenes vonalakban mozognak, fallal való ütközéskor irányt változtat
- Dodger: követnek állandó sebességgel, és próbálják kikerülni a lövedékeid
- Snitch: amit ha sikerül elkapnod, akkor extra pont, ekkor nem sérülsz (lelövése csupán standard pontot ér)
- Shielded: pajzsa van, csak egy bizonyos irányból lehet eltalálni, viszont nem sért a pajzsa, csupán tol vele.

### Boostok

Az időhöz kötött boostok egy 2 elemű FIFO-ban helyezkednek el, ami azt jelenti hogy a hatások egymásra halmozhatóak. A boostok nem mozognak a térben. A boostok idővel eltűnnek, ez a nehézségi szint növekedésével felgyorsul. Képernyőn kívül jönnek létre a játéktérben.

#### Alkalmi boostok

- Gate: Kapu, amin ha átmegy az űrhajód, felrobbannak a körülütte lévő ellenségek (pontot nem kapsz értük)
- Health: +1 élet (3 fölé nem mehetsz)

#### Átmeneti boostok

- Hologram: generálódik mögötted egy holografikus másolat az űrhajódról, ami követ és utánozza, amit csinálsz, csupán fél másodperc késleltetéssel mindezt.
- Shield: pajzsot kapsz, bizonyos ideig sérthetetlen maradsz
- SuperMode: gyorsabbá válsz
