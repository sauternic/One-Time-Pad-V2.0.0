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
using Microsoft.Win32;

namespace OnTimePad_WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        //Konstruktor
        public MainWindow()
        {
            InitializeComponent();
        }

        //Ereignis-Methoden
        private void Button_Auswaehlen(object sender, RoutedEventArgs e)
        {
             
            
            Auswaehlen();
            
            if (fi1 != null)
            {
                //TextBox_PfadWaehlen.Text = fi1.FullName;
                TextBox_SpeicherOrt.Text = fi1.DirectoryName;
                TextBox_Schluessel.Text = fi1.DirectoryName;
            
            }
            
        }
        private void Button_SpeicherOrt(object sender, RoutedEventArgs e)
        {
            AuswaehlenSpeicherOrt();
        }
        private void Button_Schluessel(object sender, RoutedEventArgs e)
        {
            AuswaehlenSchluesselPfad();
        }

        
        private void Button_Speichern(object sender, RoutedEventArgs e)
        {
            FensterSchliessen(null, null);
            MessageBox.Show("Fenster-Daten Abgespeichert.\n\n(Wird beim Schliessen des Fensters\nautomatisch gemacht!) :)");
        }
        private void Button_Klein(object sender, RoutedEventArgs e)
        {
            // Diese Fields sind im Xaml Definiert.
            this.Height = 400;
            this.Width = 574;
        }
        private void Button_Gross(object sender, RoutedEventArgs e)
        {
            this.Height = 800;
            this.Width = 1190;
        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox1.Content = "On Time Pad:";
            Button_Schlüssel.Visibility = Visibility.Visible;
            TextBox_Schluessel.Visibility = Visibility.Visible;

            Password1.Visibility = Visibility.Hidden;
            Password2.Visibility = Visibility.Hidden;
            TextblockPassword1.Visibility = Visibility.Hidden;
            TextblockPassword2.Visibility = Visibility.Hidden;


        }
        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox1.Content = "Password:";
            Button_Schlüssel.Visibility = Visibility.Hidden;
            TextBox_Schluessel.Visibility = Visibility.Hidden;

            Password1.Visibility = Visibility.Visible;
            Password2.Visibility = Visibility.Visible;
            TextblockPassword1.Visibility = Visibility.Visible;
            TextblockPassword2.Visibility = Visibility.Visible;
        }
        
        private void FensterLaden(object sender, EventArgs e)
        {
            try
            {
                string[] strArr = System.IO.File.ReadAllLines("DatenFenster.config");
                
                this.Height = Convert.ToDouble(strArr[0]);
                this.Width = Convert.ToDouble(strArr[1]);
                this.TextBox_PfadWaehlen.Text = strArr[2];
                this.TextBox_SpeicherOrt.Text = strArr[3];
                this.CheckBox1.IsChecked = Convert.ToBoolean(strArr[4]);
            
            }
            catch (Exception) { }   
        
            // Wichtig beim Initalisieren wenn Checkbox nicht True ist! :/
            if (!(bool)CheckBox1.IsChecked)
            {
               Button_Schlüssel.Visibility = Visibility.Hidden;
               TextBox_Schluessel.Visibility = Visibility.Hidden;
               CheckBox1.Content = "Password:";
            }
        }
        private void FensterSchliessen(object sender, EventArgs e)
        {
            string strHeight = Convert.ToString(this.Height);
            string strWidth = Convert.ToString(this.Width);
            string strPfadWaehlen = TextBox_PfadWaehlen.Text;
            string strSpeicherOrt = TextBox_SpeicherOrt.Text;
            string strBool =  Convert.ToString(CheckBox1.IsChecked);
            //TextConkatenieren! :)
            string strConcat = strHeight + "\n" + strWidth + "\n" + strPfadWaehlen + "\n" + strSpeicherOrt + "\n" + strBool;
            
            try
            {
                System.IO.File.WriteAllText("DatenFenster.config", strConcat);
            
            }
            catch (Exception) {}   
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            
            this.Background = Brushes.Yellow;
        }
        
        private void Form1_DragLeave_1(object sender, DragEventArgs e)
        {
            this.Background = Brushes.Aqua;
        } 
        
        private void Form1_Drop_1(object sender, DragEventArgs e)
        {
            //Seeeeeeeeeeeeeeehr wichtig!! :@
            this.strArr_TextBox_PfadWaehlen = null;
            
            string[] strArr = (string[])e.Data.GetData(DataFormats.FileDrop,false);

            //FileInfo Objekt wegen DirectoryName (siehe Unten)
            fi1 = new FileInfo(strArr[0]);

            TextBox_PfadWaehlen.Text = Arr_Zu_String(strArr);
            
            //String Array Field wieder Füllen!!!
            this.strArr_TextBox_PfadWaehlen = strArr;
            
            TextBox_SpeicherOrt.Text = fi1.DirectoryName;

            //Das ist neu! Das Schlüssel Pfad nicht mehr gewählt werden muss! :))))
            TextBox_Schluessel.Text = fi1.DirectoryName;

            this.Background = Brushes.Aqua;

            //Info! :)
            TextBlock_Infos.Text = "Anzahl ausgewählter Files: " + this.strArr_TextBox_PfadWaehlen.Length;
        }
    }
}
 