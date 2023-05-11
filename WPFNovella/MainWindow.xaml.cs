using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace WPFNovella
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       static public PropertiesGame gameProp = new PropertiesGame();

        public MainWindow()
        {
            InitializeComponent();
            //StartMainWindow.Cursor = cursor;
        }

        private MediaPlayer mediaMainTheme = new MediaPlayer();
        private MediaPlayer mediaButton = new MediaPlayer();

        public void PlaybackMusic()
        {
            if (mediaMainTheme != null)
            {
                mediaMainTheme.Open(new Uri("Resources\\1-02 Prologue.mp3", UriKind.Relative));
                mediaMainTheme.Volume = gameProp.PropMusicVolume;
                mediaMainTheme.MediaEnded += new EventHandler(Media_Ended);
                mediaMainTheme.Play();

                return;
            }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            mediaMainTheme.Position = TimeSpan.Zero;
            mediaMainTheme.Play();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {         
            PlaybackMusic();
            
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            mediaButton.Open(new Uri("Resources\\button-8-88355.mp3", UriKind.Relative));
            mediaButton.Volume = 0.01;
            mediaButton.Play();
            Border border = (Border)sender;
            border.BorderBrush = Brushes.DarkOrange;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.BorderBrush = Brushes.Black;
        }


        private void BorderStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaMainTheme.Stop();
            Hide();

            GameMasterWindow gameMasterWindow = new GameMasterWindow();
            gameMasterWindow.Owner = this;
            gameMasterWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;


            if (gameMasterWindow.ShowDialog() == true)
            {
                Show();
            }
            else
            {
                Show();
                mediaMainTheme.Play();
            }

        }

        private void BorderSettings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Owner = this;
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (settings.ShowDialog() == true)
            {
                gameProp = settings.PropGameResult;
                mediaMainTheme.Volume = gameProp.PropMusicVolume;
            }
        }

        private void BorderExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {          
            this.Close();
        }

    }
}
