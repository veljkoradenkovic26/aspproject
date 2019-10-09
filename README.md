ASP - NEWS Project
Tema projekta
*Tema ovog rada su novinski clanci. Svi ce moci da citaju novinske clanke i anonimni korisnici ce moci da ostavljaju komentare ispod clanka. Svaki clanak pripada odredjenoj kategoriji i moci ce da se pretrazuje po kategoriji ili naslovu clanka. Svaki clanak moze da ima jednu ili vise slika.

Opis funkcionalnosti
Projekat je podeljen u zasebne celine

Celine :
Domenski sloj
Poslovne logike
Sloj implementacije poslovne logike
Sloj za skladistenje podataka
API ( News, Comment, Category, User )
Web app ( News i Comments Controller )
Domenski sloj

Domenski sloj sadrzi sve modele koji su upotrebljeni za pravljenje baze 'projekti'. Sloj za skladistenje podataka sadrzi konfiguracje kako bi smo napisali pravila pod kojim bi se podaci kasnije unosili u bazu.

Kreiranje clanaka

Imamo mogucnost da kreiramo novinski clanak koji mora da pripada odredjenoj kategoriji. To znaci, da prvo kreiramo kategorije po kojima cemo sortirati novinske clanke. Kada imamo jedan clanak , na njega mozemo da postavimo komentar. Da bi se postavio, citalac mora da unese svoje ime, email i tekst komentara. Po kreiranju komentara, salje se meil citaocu o uspesnosti postavljanja komentara na clanak.

Logovanje

Logovanje je omoguceno na stranici Home -> Login. Prilikom uspesnog logovanja, bicete redirektovani na Home(Index), ali nisu postavljeni linkovi za redirekciju na News.
