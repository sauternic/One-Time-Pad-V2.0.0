using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace OnTimePad_WPF
{
    public partial class MainWindow
    { 
        private void Auswaehlen()
        {
            //Seeeeeeeeeeeeeeehr wichtig!! :@
            this.strArr_TextBox_PfadWaehlen = null;
            
            //Create OpenFileDialog
            OpenFileDialog OFD = new OpenFileDialog();

            //OpenFileDialog aktueller Pfad als Voreinstellung oder eben vom FileInfoObjekt.
            if (fi1 != null)
            {
                OFD.InitialDirectory = fi1.DirectoryName;
            }
            else
            {

                OFD.InitialDirectory = Text_nur_bis_Umbruch(TextBox_PfadWaehlen.Text);
                
                //OFD.InitialDirectory = Environment.CurrentDirectory;
            }


            OFD.Multiselect = true;
            
            // Geht erst weiter wenn File gewählt.
            OFD.ShowDialog();//Blockiert!!

            //Gaaaaaaaaaaaaaaanz wichtig!! :@
            this.strArr_TextBox_PfadWaehlen = OFD.FileNames;
            
            //Namen der 'FileNames' Liste mal in die Box Schreiben.
            try
            {
                TextBox_PfadWaehlen.Text = Arr_Zu_String(OFD.FileNames);
            }
            catch (Exception) { }

            //Info! :)
            TextBlock_Infos.Text = "Anzahl ausgewählter Files: " + this.strArr_TextBox_PfadWaehlen.Length;
        
        }

        private void AuswaehlenSpeicherOrt()
        {
            FileInfo FI = null;
            FolderBrowserDialog fbd = null;
            
            // Code aus CodeBook Jürgen Bayer Seite 773, etwas abgeändert! ;)
            fbd = new FolderBrowserDialog();
            
            fbd.Description = "Wählen Sie einen Ordner aus:";
            fbd.ShowNewFolderButton = true;

            try
            {
                FI = new FileInfo(Text_nur_bis_Umbruch(TextBox_PfadWaehlen.Text));
 
                //Voreinstellung Speicherort
                fbd.SelectedPath = FI.DirectoryName;
            }
            catch (Exception) { }

            //Geht erst weiter wenn gewählt. 
            fbd.ShowDialog();
            
            TextBox_SpeicherOrt.Text = fbd.SelectedPath; 
           }

        private void AuswaehlenSchluesselPfad()
        {
            FileInfo FI = null;
            FolderBrowserDialog fbd = null;

            // Code aus CodeBook Jürgen Bayer Seite 773, etwas abgeändert! ;)
            fbd = new FolderBrowserDialog();

            fbd.Description = "Wählen Sie einen Ordner aus:";
            fbd.ShowNewFolderButton = true;

            try
            {
                FI = new FileInfo(Text_nur_bis_Umbruch(TextBox_PfadWaehlen.Text));

                //Voreinstellung Speicherort
                fbd.SelectedPath = FI.DirectoryName;
            }
            catch (Exception) { }

            //Geht erst weiter wenn gewählt. 
            fbd.ShowDialog();

            TextBox_Schluessel.Text = fbd.SelectedPath;
        
        }
        
        
        
        //Hilfsfunktionen
        private string Arr_Zu_String(string[] strArr)
        {
            string strErg = "";
   
            try
            { 
                foreach(string str in strArr)
                {
                    strErg += str + "\n";    
                }
                return strErg;
            }
            catch (Exception) { return ""; }
        }

        private string Text_nur_bis_Umbruch(string str)
        {
            string[] strArr = null;

            strArr = str.Split(new char[]{'\n'});
            return strArr[0];
        }
    
    
    }
}
