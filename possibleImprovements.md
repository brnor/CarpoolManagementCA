### Dosta toga je moglo bolje:

#### Front

- "Dovršiti frontend.
  - Error handling ne radi ispravno.
  - Rađen nabrzaka te je kvaliteta koda dosta patila radi toga.
  - Neka imena atributa/varijabli je teško popratiti.
  - Razmotriti mogućnost razdvajanja komponenti u još više manjih komponenti uz obraćanje pažnje na mogućnost code-reuse-a.
  
#### Back
- Backend je korišten kao proba "clean architecture" strukture te je tu potrošen veći dio vremena na učenje nekih noviteta.
  Naime, ovakva struktura daje na uvid kako bi neki veći projekt potencijalno mogao izgledati te na dobar način pokušava prisiliti odvajanje zasebnih cjelina projekta.
- Poboljšati generiranje validacijskih grešaka
  - Za jednostavne validacije se koristi FluentValidation koji na detaljan generira puni izvještaj o grešci u response-u.
  - Za kompliciranije validacije obavljaju se "ručne" provjere i koriste custom iznimke koji nažalost nemaju izvještaj niti strukturu kao FluentValidation greške.
  
