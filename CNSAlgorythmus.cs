using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTimePad_WPF
{
    class CNSAlgorythmus
    {
        //Statisch Aufrufbare-Methode mit Rückgabe!! :)
        //Siehe unten zusammengeklappt die zugrundeliegende Main Methode! :/
        public static byte[] NSAlgorithmus(long groesse)
        {
            long i = 20;
            int vergleich1 = 0;
            byte[] by = new byte[groesse];

            //Die ersten x Iterationen Verwerfen! ;) (siehe Unten)
            Random rnd1 = new Random();
            //Nur Hilfszufallgenerator für Startwert und Iterationanzahl wechseln beim Aendern.
            Random rnd2 = new Random();
            //Instanz Zufallsgenerator
            CHP CHP1 = new CHP();

            // Erste Werte Verwerfen (Unregelmässig)
            vergleich1 = rnd1.Next(4, 21);
            for (int x = 0; x < vergleich1; ++x)
            {
                CHP1.startwert = rnd2.NextDouble();
            }

            for (long x = 0; x < groesse; ++x)
            {

                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@2
                //Startwert alle x (Ungregelmässig) Wiederholungen neu setzen! ;)
                if (x > i)
                {
                    i = rnd2.Next(20, 300) + x;
                    CHP1.startwert = rnd1.NextDouble();
                }
                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@2

                //Hier wird die Haubtarbeit getan!!!!!!!! :'@
                by[x] = Convert.ToByte(CHP1.hpMethode() * 255);

            }//Ende for
            return by;
        }//Ende Methode NSAlgorithmus
    }
}
