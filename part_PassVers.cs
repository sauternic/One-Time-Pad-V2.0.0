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
using System.Windows.Threading;

namespace OnTimePad_WPF
{
    public partial class MainWindow : Window
    {
        private void Pass_Vers()
        {
            FileStream ein = null;
            FileStream aus = null;

            Random Rn = new Random();

            string FileTemp = "";

            try
            {  
                #region TextBox_PfadWaehlen.Text
                FileInfo FI1 = this.fi1;
                FileInfo FI2 = new FileInfo(str_TextBox_SpeicherOrt);

                //Endung darf nicht: "_K_V_Pass"  haben!
                if (FI1.Name.IndexOf("_K_V_Pass") != -1)
                {
                    AbbruchFlag = true;
                    MessageBox.Show("Falsche Datei! :(");
                    return;
                }
                
                FileTemp = FI2.FullName + "\\_" + Convert.ToString(Rn.Next(100000000, 999999999)) + FI1.Extension;

                string File_K_V_Pass = FI2.FullName + "\\" + FI1.Name.Replace(FI1.Extension, "") + "_K_V_Pass" + FI1.Extension;
                #endregion
             
            
           
                #region %%%%%%%%%%%%%%%%%%%%% Komprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Komprimieren :/");
                
                ein = new FileStream(FI1.FullName, FileMode.Open);
                aus = new FileStream(FileTemp, FileMode.Create);
                   
                byte[] byArr = new byte[ein.Length];
                ein.Read(byArr, 0, byArr.Length);

                //Zwischenschritt sonst alles gleich(einmal durch den DeflateStream Konstruktor lassen! :/ )
                DeflateStream ausDef = new DeflateStream(aus, System.IO.Compression.CompressionLevel.Optimal);
                ausDef.Write(byArr, 0, byArr.Length);

                ausDef.Close();
                ein.Close();
                aus.Close();
                #endregion %%%%%%%%%%%%%%%%%%%%% Ende Komprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            
                #region %%%%%%%%%%%%%%%%%%%%% Verschlüsseln Password %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% 
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Verschlüsseln :/");
                
                Char[] schlusselArray = null;
                byte by;
                int index = 0;
                ein = null;
                aus = null;
                
                String schluessel = Password1.Password; ;
                
                if (schluessel.Length < 1 || Password1.Password != Password2.Password)
                {
                    MessageBox.Show("Fehler, mit Password stimmt was nicht! :(");
                    if (FileTemp != "")
                        new FileInfo(FileTemp).Delete();
                    
                    this.AbbruchFlag = true;
                    return;
                }
            
                ein = new FileStream(FileTemp, FileMode.Open);
                aus = new FileStream(File_K_V_Pass, FileMode.Create);

                schlusselArray = schluessel.ToCharArray();
                
                //Länge für for
                long lan = (long)ein.Length;
                for (long x = 0; x < lan; ++x)
                {
                    //Einzelne Bytes
                    by = (byte)ein.ReadByte();


                    //index für Schlüssel immer wieder zurückstellen
                    if (index >= (schlusselArray.Length))
                        index = 0;
                    ////////Hier wird das Byte verändert/////////
                    by = (byte)((int)by ^ (int)schlusselArray[index]);
                    /////////////////////////////////////////////
                    index++;

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
                
                // Ojb Erzeugen um zu löschen! :/
                if (FileTemp != "")
                    new FileInfo(FileTemp).Delete();
            }
        }
    
    }
}
