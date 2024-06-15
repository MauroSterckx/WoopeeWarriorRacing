# WoopeeWarriorRacing

## Inleiding
Stap in de wereld van Woopée Warrior Racing, de baanbrekende VR-racegame die je meeneemt naar adembenemende circuits vol actie en snelheid. In deze futuristische racegame zit je als speler in de cockpit van een hypermoderne raceauto, waar je het opneemt tegen geavanceerde machine learning agents die zijn ontworpen om je rijvaardigheden tot het uiterste te testen.
### Samenvatting
Elke race is een intense strijd om de overwinning, waarbij je reflexen en strategisch inzicht op de proef worden gesteld. Met verschillende uitdagende tracks die variëren van snelle stadscircuits tot verraderlijke bergwegen, biedt Woopée Warrior Racing een meeslepende ervaring die je keer op keer doet terugkeren voor meer. Bereid je voor op de race van je leven en word de ultieme Woopée Warrior!

## Methoden

### Installatie
    - Installatie: enkel vermelden van de versies meegeven, geen uitleg over hoe je moet installeren, tenzij het over zaken gaat die niet in de cursus staan
### Verloop van spel
Wanneer je Woopée Warrior Racing start, kom je op een prachtige UI terecht die je direct onderdompelt in de race-ervaring. Hier kun je verschillende keuzes maken om je race voor te bereiden:

1) Kies je auto:
    - Selecteer je favoriete auto.
    - Personaliseer de kleur van je auto.

2) Kies je track:
    - Voorlopig heb je keuze uit twee uitdagende tracks.

3) Pas je settings aan:
    - Stel je audio-instellingen naar wens in.
    - Kies je gebruikersnaam.

Als je tevreden bent met je keuzes, druk je op Continue. Je wordt dan naar de geselecteerde track gebracht in de gekozen auto. Je hebt zelf de volledige controle over de auto in VR alsof je een echte racer bent. Je zal moeten sturen, gas geven en remmen om de finish te bereiken.

Daarna begint de spanning te stijgen met een countdown. Zodra de countdown eindigt, ben je vrij om te racen tegen de verschillende ML Agents. Let op: als je de muren raakt, ga je 'dood' en verschijnt er een game over-scherm met een DNF (Did Not Finish).

Wanneer je de finish bereikt, krijg je je positie in de race te zien en is het spel afgelopen. Als je wint of finisht, zie je in de achtergrond feestvierende toeschouwers op de track, wat je overwinning extra speciaal maakt.

Bereid je voor op de ultieme race-ervaring en laat zien dat jij de beste Woopée Warrior bent!

    - Duidelijk overzicht van de observaties, mogelijke acties en beloningen
    - Beschrijving van de objecten
    - Beschrijvingen van de gedragingen van de objecten
    - Alle informatie van de one-pager
    - Indien van toepassing: waar jullie afwijken van de one-pager en waarom

## Resultaten
In het begin van ons ontwikkeltraject hadden onze ML-Agents moeite om goed te presteren. We hebben behoorlijk wat gesukkeld met de initiële aanpak. Ons oorspronkelijke idee was om waypoints op de tracks te plaatsen en de agents een array te geven met de locaties van de transforms van die waypoints. Het doel was simpel: de agents moesten zo snel mogelijk naar de locatie van de transforms rijden. Helaas bleek deze aanpak niet effectief en stuitten we op diverse problemen.

We ondervonden vooral problemen bij de bochten te nemen, de agent kon namelijk de bocht (de muren rond de bocht) niet zien en stootte hier telkens tegen aan, wat op een endEpisode leidde:<br>
<!-- ![gif1](./scr/VR-vid1.gif) -->
![gif1](./scr/VR-Track-1.gif)

Daarna besloten we over te stappen op een andere benadering door gebruik te maken van RaycastPerception3D. We gaven de agents zowel de transform locations als de raycastperceptions om hen te helpen bij het navigeren. Deze combinatie bleek een stuk beter te werken, en onze agents werden merkbaar competenter in het volgen van de juiste route:
![gif2](./scr/VR-Track-2.gif)

Om de prestaties verder te verbeteren, wilden we de agents sneller laten rijden. Dit bereikten we door een strafsysteem te implementeren: als een agent trager was dan zijn vorige keer, werd hij bestraft. Deze aanpak stimuleerde de agents om steeds efficiënter en sneller te worden, wat uiteindelijk leidde tot indrukwekkende resultaten op de verschillende tracks.

    - Resultaten van de training met Tensorboard afbeeldingen
    - Beschrijving van de Tensorboard grafieken
    - Opvallende waarnemingen tijdens het trainen 
## Conclusie
    - Eén zin die nog eens samenvat wat jullie hebben gedaan
    - Kort overzicht resultaten (2 á 3 zinnen zonder cijfers te vernoemen)
    - Een 'persoonlijke' visie op de resultaten, wat betekenen de resultaten nu eigenlijk
    - Verbeteringen naar de toekomst toe

## Bronvermelding
    Je mag eventueel gebruik maken van de instructievideo's van deze cursus of naar de Obelix tutorial. Voor alle ander materiaal waar je gebruik van maakt en dat bestaat uit meer dan één regel aan code, verwijs je naar de oorspronkelijke bron op een gepaste wijze (APA-stijl). Een ontbrekende of incorrecte verwijzing wordt als plagiaat beschouwd.
ZET BRONVERMELDING VOOR CHATGPT VOOR TE HELPEN MET DEZE README!!