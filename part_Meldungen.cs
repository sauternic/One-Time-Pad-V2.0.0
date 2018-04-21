using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTimePad_WPF
{
    public partial class MainWindow
    {
        private delegate void MeinDele(string str);
        
        private void Meldungen(string str)
        {
            TextBlock_Meldungen.Text = str;   
        }
    }
}
