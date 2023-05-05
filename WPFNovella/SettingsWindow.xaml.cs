using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WPFNovella
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    /// 

    

    public partial class SettingsWindow : Window
    {
        public PropertiesGame PropGameResult { get; set; }

        

        public SettingsWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private MediaPlayer mediaButton = new MediaPlayer();

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            mediaButton.Open(new Uri("Resources\\button-8-88355.mp3", UriKind.Relative));
            mediaButton.Volume = 0.05;
            mediaButton.Play();
            Border border = (Border)sender;
            border.BorderBrush = Brushes.DarkOrange;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.BorderBrush = Brushes.Black;
        }

        private void BorderBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BorderAccept_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PropertiesGame propertiesGame = new PropertiesGame();

            if (LanguageComboBox.SelectedValue is { }) // если не равно null
            {
                propertiesGame.PropLanguage = ((ComboBoxItem)LanguageComboBox.SelectedItem).Content.ToString();        
            }

            propertiesGame.PropMusicVolume = (float)(MusicSlider.Value / 100);
            propertiesGame.PropVoiceVolume = (float)(VoiceSlider.Value / 100);

            if (VoiceActingCheckBox.IsChecked == true)
                propertiesGame.PropIsVoicing = true;
            else
                propertiesGame.PropIsVoicing = false;

            PropGameResult = propertiesGame;

            DialogResult = true;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            MusicSlider.Value = (int)(MainWindow.gameProp.PropMusicVolume * 100);
            VoiceSlider.Value = (int)(MainWindow.gameProp.PropVoiceVolume * 100);
            VoiceActingCheckBox.IsChecked = MainWindow.gameProp.PropIsVoicing;
            int indexToSelect = LanguageComboBox.Items.IndexOf(MainWindow.gameProp.PropLanguage == "Ukrainian (UA)" ? ItemUA : ItemENG);

            // Присваиваем выбранный элемент ComboBox, если он был найден
            if (indexToSelect >= 0)
            {
                LanguageComboBox.SelectedIndex = indexToSelect;
            }
        }
    }
}
