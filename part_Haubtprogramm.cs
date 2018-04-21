using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OnTimePad_WPF
{
    
    
    public partial class MainWindow
    {
        //Fields zur Kommunikation aus den Hintergrund-Threads! :)
        private string[] strArr_TextBox_PfadWaehlen = null;
        private string str_TextBox_SpeicherOrt = "";
        private string str_TextBox_Schluessel = "";
        //Fields
        private FileInfo fi1;
        private bool AbbruchFlag = false;
        
        private async void Button_Verschluesseln(object sender, RoutedEventArgs e)
        {
            this.AbbruchFlag = false;

            //Wenn nichts ausgewählt wurde oder nur von Hand ein Pfad in 
            //'TextBox_PfadWaehlen' Eingetragen wurde!! :/
            if (strArr_TextBox_PfadWaehlen == null)
            {
                this.strArr_TextBox_PfadWaehlen = new string[1];
                this.strArr_TextBox_PfadWaehlen[0] = TextBox_PfadWaehlen.Text;
            }
            
            //Schleife um alle Dateien die ausgewählt wurden durchzugehen. :)
            for (int x = 0; x < strArr_TextBox_PfadWaehlen.Length; ++x)
            {
                if (this.AbbruchFlag)
                    break;

                //Fields initalisieren
                fi1 = null;
                fi1 = new FileInfo(strArr_TextBox_PfadWaehlen[x]);
                
                //Infos! :)
                TextBlock_Infos.Text = "Files Nr: " + (x + 1) + "  " + fi1.Name;
                
               
                //Felder Aktualisieren. :/
                this.str_TextBox_SpeicherOrt = TextBox_SpeicherOrt.Text;
                this.str_TextBox_Schluessel = TextBox_Schluessel.Text;

                if ((bool)CheckBox1.IsChecked)
                {
                    Button_Ver.IsEnabled = false;
                    await Task.Factory.StartNew(() => On_Time_Pad_Vers());
                    Button_Ver.IsEnabled = true;
                }
                else
                {
                    Button_Ver.IsEnabled = false;
                    await Task.Factory.StartNew(() => Pass_Vers());
                    Button_Ver.IsEnabled = true;
                }
            }
            
         }
        private async void Button_Entschluesseln(object sender, RoutedEventArgs e)
        {
            this.AbbruchFlag = false;

            //Wenn nichts ausgewählt wurde oder nur von Hand ein Pfad in 
            //'TextBox_PfadWaehlen' Eingetragen wurde!! :/
            if (strArr_TextBox_PfadWaehlen == null)
            {
                this.strArr_TextBox_PfadWaehlen = new string[1];
                this.strArr_TextBox_PfadWaehlen[0] = TextBox_PfadWaehlen.Text;
            }


            //Schleife um alle Dateien die ausgewählt wurden durchzugehen. :)
            for (int x = 0; x < strArr_TextBox_PfadWaehlen.Length; ++x)
            {
                if (this.AbbruchFlag)
                    break;
                
                //Fields initalisieren
                fi1 = null;
                fi1 = new FileInfo(strArr_TextBox_PfadWaehlen[x]);

                //Infos! :)
                TextBlock_Infos.Text = "Files Nr: " + (x + 1) + "  " + fi1.Name;
                
                
              
                //Felder Aktualisieren. :/
                this.str_TextBox_SpeicherOrt = TextBox_SpeicherOrt.Text;
                this.str_TextBox_Schluessel = TextBox_Schluessel.Text;

                if ((bool)CheckBox1.IsChecked)
                {
                    Button_Ent.IsEnabled = false;
                    await Task.Factory.StartNew(() => On_Time_Pad_Ents());
                    Button_Ent.IsEnabled = true;
                }
                else
                {
                    Button_Ent.IsEnabled = false;
                    await Task.Factory.StartNew(() => Pass_Ents());
                    Button_Ent.IsEnabled = true;
                }
            }
        }
    }
}
