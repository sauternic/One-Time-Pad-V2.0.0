using System;

namespace OnTimePad_WPF
{
    class CHP
    {
        public double startwert { get; set; }

        public double hpMethode()
        {
            this.startwert += Math.PI;
            this.startwert = Math.Pow(startwert, 8);
            this.startwert -= Math.Floor(startwert);
            return startwert;
        }
    }
}
