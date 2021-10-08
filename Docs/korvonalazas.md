# Main concept

*Ez a fájl markdownban írodott, legkönnyebb és leggyorsabb, ha VSCode-ban nyitod meg és szerkeszted, esetleg Notepad++-ban.*

- 2D
- Geometry Wars 2-höz hasonló játék
- Már az elején könnyen ki lehet alakítani egy kész koncepciót és könnyen bővíthetővé válik, tudunk további extrakat beleraktni.

## Leszögezendők

- Mi a cél?
  - Pontgyűjtés (meghalt enemykből) és túlélés
  - Vanank segítségek, ellenfelek
- Enemy unitok
  - Követők (minionok)
  - Random mozgók (walkers)
  - Egyenesen menők (DVD ikon, dodgers)
  - Amit ha sikerül elkapnod, akkor extra pont (cikkesz == snitch)
  - Pajzsos (csak egyik oldalról lehet eltalálni)
  - Kígyó -> testétől is sebződsz, de a fejét eltalálva pusztul csak el.
- Powerups (random felbukkanó)
  - Kapuk, amin ha átmegy, felrobban a körülütte lévő enemykkel - alkalmi
  - Egy random cucc, amit ha begyűjtesz, akkor mögötted 1s késlekedéssel követ és utánoz - temporary
  - Pajzs (sokáig immortal) - temporary
  - +1 élet - alkalmi
  - Super mariós csillag (amihez hozzáérsz, meghal, és shielded van, és gyors vagy) - temporary
  - Kell ezeket majd csoportosítani!
  - FIFO a hatásokra (2 elemű FIFO)
- Lesz score board
- Ha érinted az ellenséget (tűréshatár lesz)
  - 3-szor tudsz felrobbanni, ekkor minden felrobban körülötted, és ezért nem kapsz pontot, addig kapsz egy kis egérutat
- Level design nem kell, hanem pont alapján nehezedik a játék.
- Ha meghalsz, újrakezdhetsz de fele annyi ponttal.
- **Játszhatóság kedvéért úgyis változtatunk a dolgokon.**
- Nagy plane, a kamera mozog veled egy kisebb teret belátva.
