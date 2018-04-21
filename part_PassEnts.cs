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
        private void Pass_Ents()
        {
            FileInfo FI1 = null;
            FileInfo FI2 = null;

            Random Rn = new Random();
            
            FileStream ein = null;
            FileStream aus = null;

            string FileTemp = "";
            string Fiel__K_V_Pass = "";

            try
            {
                #region TextBox_PfadWaehlen.Text
                FI1 = this.fi1;
                FI2 = new FileInfo(str_TextBox_SpeicherOrt);

                //Endung auf: "_K_V_Pass"  Überprüfen!
                if (FI1.Name.IndexOf("_K_V_Pass") == -1)
                {
                    AbbruchFlag = true;
                    MessageBox.Show("Falsche Datei! :(");
                    return;
                }

                FileTemp = FI2.FullName + "\\_" + Convert.ToString(Rn.Next(100000000, 999999999)) + FI1.Extension;

                Fiel__K_V_Pass = FI2.FullName + "\\" + FI1.Name.Replace("_K_V_Pass", "");
                #endregion



                #region %%%%%%%%%%%%%%%%%%%%% Entschlüsseln Password %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Entschlüsseln :/");
                
                ein = new FileStream(FI1.FullName, FileMode.Open);
                aus = new FileStream(FileTemp, FileMode.Create);
                   
                String schluessel = Password1.Password; ;
                if (schluessel.Length < 1 || Password1.Password != Password2.Password)
                {
                    MessageBox.Show("Fehler, mit Password stimmt was nicht! :(");
                    ein.Close();
                    aus.Close();
                    if (FileTemp != "")
                        new FileInfo(FileTemp).Delete();
                    
                    this.AbbruchFlag = true;
                    return;
                }

                Char[] schlusselArray = null;
                int by;
                int index = 0;

                //Länge für for
                long lan = (long)ein.Length;

                schlusselArray = schluessel.ToCharArray();

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

                    aus.WriteByte((byte)by);
                }


                ein.Close();
                aus.Close();
                #endregion %%%%%%%%%%%%%%%%%%%%% Ende Entschlüsseln Password %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


                
                #region %%%%%%%%%%%%%%%%%%%%% Dekomprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Dekomprimieren :/");
                
                ein = null;
                aus = null;

                ein = new FileStream(FileTemp, FileMode.Open);
                aus = new FileStream(Fiel__K_V_Pass, FileMode.Create);

                //Zwischenschritt sonst alles gleich(einmal durch den DeflateStream Konstrukto lassen! :/ )
                DeflateStream eingehendDef = new DeflateStream(ein, CompressionMode.Decompress);

                while ((by = eingehendDef.ReadByte()) != -1)
                {
                    aus.WriteByte((byte)by);
                }
                eingehendDef.Close();

                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Fertig! :)");
                #endregion %%%%%%%%%%%%%%%%%%%%% Ende Dekomprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%     
            
            }
            catch (Exception e) 
            {
                AbbruchFlag = true;
                
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "");
                
                MessageBox.Show(e.Message);
                if (aus != null)
                    aus.Close();
                if (Fiel__K_V_Pass != "")
                    new FileInfo(Fiel__K_V_Pass).Delete();
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
