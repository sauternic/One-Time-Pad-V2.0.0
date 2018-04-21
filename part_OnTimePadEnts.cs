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
        private void On_Time_Pad_Ents()
        {
            FileInfo FI1 = null;
            FileInfo FI2 = null;
            FileInfo FI3 = null;

            Random Rn = new Random();
            
            FileStream ein = null;
            FileStream ein_Schluessel = null;
            FileStream aus = null;

            string FileTemp = "";
            string File_K_V_Pad = "";
            string temp = "";
   
            try
            {  
                #region TextBox_PfadWaehlen.Text
                FI1 = this.fi1;
                FI2 = new FileInfo(str_TextBox_SpeicherOrt);
                FI3 = new FileInfo(str_TextBox_Schluessel);
                              
                //Endung auf: "K_V_Pad_"  Überprüfen!
                if (FI1.Name.IndexOf("K_V_Pad_") == -1)
                {
                    AbbruchFlag = true;
                    MessageBox.Show("Falsche Datei! :(");
                    return;
                }
             
                FileTemp = FI2.FullName + "\\_" + Convert.ToString(Rn.Next(100000000, 999999999)) + FI1.Extension;

                File_K_V_Pad = FI2.FullName + "\\" + FI1.Name.Replace("K_V_Pad_", "");
                #endregion


                
                
                #region %%%%%%%%%%%%%%%%%%%%% Entschlüsseln durch Zusammenführen %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Entschlüsseln :/");
                
                
                ein = new FileStream(FI1.FullName, FileMode.Open);
                try
                {
                    //Muli Schlüssel Fähigkeit mit Exception, wenn Schlüssel nicht findet! ;)
                    ein_Schluessel = new FileStream(str_TextBox_Schluessel + "\\" + FI1.Name, FileMode.Open); 
                }
                catch (Exception)
                {
                    try
                    {
                        temp = FI1.Name.Replace("K_V_Pad_", "Schl_Pad_");
                        ein_Schluessel = new FileStream(str_TextBox_Schluessel + "\\" + temp, FileMode.Open);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Schlüssel nicht Gefunden");
                    }
                }
                aus = new FileStream(FileTemp, FileMode.Create);


                  
                byte[] schlusselArray = new byte[ein_Schluessel.Length];
                ein_Schluessel.Read(schlusselArray, 0, (int)ein_Schluessel.Length);
                int by;

                //Länge für for
                long lan = (long)ein.Length;

                for (long x = 0; x < lan; ++x)
                {
                    //Einzelne Bytes
                    by = (byte)ein.ReadByte();

                    
                    ////////Hier wird das Byte verändert/////////
                    by = (byte)((int)by ^ (int)schlusselArray[x]);
                    /////////////////////////////////////////////

                    aus.WriteByte((byte)by);
                }


                ein.Close();
                ein_Schluessel.Close();
                aus.Close();
                #endregion %%%%%%%%%%%%%%%%%%%%% Ende Entschlüsseln durch Zusammenführen %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


                
                
                #region %%%%%%%%%%%%%%%%%%%%% Dekomprimieren %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                this.Dispatcher.Invoke(new MeinDele(Meldungen), DispatcherPriority.Normal, "Dekomprimieren! :/");
                
                ein = null;
                aus = null;

                ein = new FileStream(FileTemp, FileMode.Open);
                aus = new FileStream(File_K_V_Pad, FileMode.Create);


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
                if (File_K_V_Pad != "")
                    new FileInfo(File_K_V_Pad).Delete();
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
        }//Ende Methode
    }//Ende Class
}
