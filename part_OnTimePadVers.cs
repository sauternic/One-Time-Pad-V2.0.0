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
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace OnTimePad_WPF
{
    public partial class MainWindow : Window
    {
        private void On_Time_Pad_Vers()
        {
           FileStream ein = null;
           FileStream aus = null;
           FileStream ausSchluessel = null;
           Random Rn = new Random();
           string TempFile = ""; 

           try
           { 
               #region TextBox_PfadWaehlen.Text
               FileInfo FI1 = this.fi1;
               FileInfo FI2 = new FileInfo(str_TextBox_SpeicherOrt);

               //Endung darf nicht: "K_V_Pad_"  haben!
               if (FI1.Name.IndexOf("K_V_Pad_") != -1)
               {
                   AbbruchFlag = true;
                   MessageBox.Show("Falsche Datei! :(");
                   return;
               }

               TempFile = FI2.FullName + "\\_" + Convert.ToString(Rn.Next(100000000, 999999999)) + FI1.Extension;

               string File_K_V_Pad = FI2.FullName + "\\" + "K_V_Pad_" + FI1.Name;
               string File_Schl_Pad = FI2.FullName + "\\" + "Schl_Pad_" + FI1.Name; ;
               #endregion




               #region %%%%%%%%%%%%%%%%%%%%% Komprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
               this.Dispatcher.Invoke(new MeinDele(Meldungen),DispatcherPriority.Normal, "Komprimieren :/");
             
               ein = new FileStream(FI1.FullName, FileMode.Open);
               aus = new FileStream(TempFile, FileMode.Create);   


               byte[] byArr = new byte[ein.Length];
               ein.Read(byArr, 0, byArr.Length);

               //Zwischenschritt sonst alles gleich(einmal durch den DeflateStream Konstruktor lassen! :/ )
               DeflateStream ausDef = new DeflateStream(aus, System.IO.Compression.CompressionLevel.Optimal);
               ausDef.Write(byArr, 0, byArr.Length);

               ausDef.Close();
               ein.Close();
               aus.Close();
               #endregion %%%%%%%%%%%%%%%%%%%%% Ende Komprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


               
               
               #region %%%%%%%%%%%%%%%%%%%%% Verschlüsseln mit Pad %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
               this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Schlüssel Erzeugen :/");
               
               ein = null;
               aus = null;
               
               //Byte Array Bestellen! :)
               Byte[] schlusselArray = CNSAlgorythmus.NSAlgorithmus(new FileInfo(TempFile).Length);
               byte by;

               ein = new FileStream(TempFile, FileMode.Open);
               aus = new FileStream(File_K_V_Pad, FileMode.Create);
               ausSchluessel = new FileStream(File_Schl_Pad, FileMode.Create);
               
               //Schlüssel gleich Rausschreiben ins File
               ausSchluessel.Write(schlusselArray, 0, schlusselArray.Length);
               
               this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Verschlüsseln :/");

               //Länge für for
               long lan = (long)ein.Length;

               for (long x = 0; x < lan; ++x)
               {
                   //Einzelne Bytes
                   by = (byte)ein.ReadByte();// Jedes Byte der Komprimierten Datei Einlesen.

                   ////////Hier wird das Byte verändert/////////
                   by = (byte)((int)by ^ (int)schlusselArray[x]);
                   /////////////////////////////////////////////

                   aus.WriteByte(by);
               }
               this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Fertig! :)");
               #endregion %%%%%%%%%%%%%%%%%%%%% Ende Verschlüsseln Password %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
           
           }
           catch (Exception e)
           {
               AbbruchFlag = true;
               
               this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "");
               
               MessageBox.Show(e.Message);
           }
           finally// Wird immer Ausgeführt mit oder ohne Exception 
           {
                if (ein != null)
                    ein.Close();
                if (aus != null)
                    aus.Close();
                if (ausSchluessel != null)
                    ausSchluessel.Close();
                
                // Ojb Erzeugen um zu löschen! :/
                if (TempFile != "")
                    new FileInfo(TempFile).Delete();  
           }         
        }
    }
}
