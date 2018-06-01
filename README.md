# On-Time-Pad Version 2.0.0

## Verschlüsselungs Programm

### C# WPF

#### Mit dem C Sharp RNGCryptoServiceProvider

Verschlüsselungs - Programm  => On-Time-Pad


Einfach per Drag and Drop Dateien ins Fenster Ziehen.

### Wahlweise:

- Password Verschlüsselung periodisch (nicht sehr sicher)

- On-Time-Pad Verschlüsselung,  sehr sicher weil Schlüssel genau so lange wie die Datei.


Der zufalls Generator ist die `RNGCryptoServiceProvider Klasse von C Sharp` die gemäss Beschreibung  
**starke Zufallszahlen** für Kryptographische Anwendungen Erzeugt!  

Einzige Chance ist wenn der Zufallsgenerator der Schlüsselerstellung nachgeahmt werden kann.  
Aber das kann nur ein professioneller Kryptograph Beurteilen wie sicher der Zufallsschlüssel ist.  


**Und Achtung beim Entschlüsseln müssen die Namen der Dateien Stimmen, so wie sie erstellt werden.**


# Anleitung: 

**Eigentlich Selbsterklärend. ;)**

Verschlüsseln:
![Verschlüsseln](https://github.com/sauternic/On-Time-Pad-V2.0.0/blob/master/Verschl%C3%BCsseln.gif)

Entschlüsseln:
![Entschlüsseln](https://github.com/sauternic/On-Time-Pad-V2.0.0/blob/master/Entschl%C3%BCsseln.gif)


1. Haken links Mitte muss gesetzt sein, so das `On Time Pad:` steht links.
2. Nur mit Drag and Drop die Datei(en) ins Fenster ziehen bis es Gelb wird dann loslassen
3. Verschlüsseln Button Drücken, Fertig :)))
4. Aus jeder Datei werden 2 Dateien Erzeugt, ein Schlüssel und der Verschlüsselte Klartext.
5. Die Namen auf Keinen Fall Ändern!
6. Zum Entschlüsseln einfach die Datei die mit  `K_V_PAD_`  anfängt ins Fenster Ziehen.
7. Dann Entschlüsseln Button Drücken, Fertig!! :)))


Ps: Es können auch mehrere Dateien gleichzeitig Verschlüsselt werden! :))))  
    Die Dateien werden vor dem Verschlüsseln Komprimiert mit dem Deflate Algorithmus.
