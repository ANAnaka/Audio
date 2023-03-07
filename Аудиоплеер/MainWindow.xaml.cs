using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;


namespace Аудиоплеер
{
    
    public partial class MainWindow : Window
    {
        public static List<string> list = new List<string>();
        private MediaPlayer player = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();

            List<string> list = new List<string>();

            
        }
        
        public void Papka_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog() { IsFolderPicker = true};
            CommonFileDialogResult result = dlg.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string[] files = Directory.GetFiles(dlg.FileName);
                Regex regex = new Regex(@"\w*\.mp3|\w*\.m4a|\w*\.wav");
                foreach (string item in files)
                {
                    if (regex.IsMatch(item))
                    {
                        MusVbr.Items.Add(regex.Match(item).Value);
                        list.Add(item);

                    }
                }
            }
            Media.Source = new Uri(list[0]);
            Media.Play();
        }

        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            LengthSlider.Maximum = Media.NaturalDuration.TimeSpan.Ticks;
        }

        private void AudioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Media.Position = new TimeSpan(Convert.ToInt64(AudioSlider.Value));
        }

        private void MusVbr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            player = MusVbr.SelectedItem as MediaPlayer;
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            bool player = true;

            if (player == true)
            {
                Media.Pause();
                player = false;
            }
            else
            {
                Media.Play();
                player = true;
            }
            
           
        }



    }
}

