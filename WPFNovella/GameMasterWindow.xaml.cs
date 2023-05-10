using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFNovella
{
    /// <summary>
    /// Interaction logic for GameMasterWindow.xaml
    /// </summary>
    public partial class GameMasterWindow : Window
    {
        public GameMasterWindow()
        {
            InitializeComponent();
        }

        private MediaPlayer mediaTheme = new MediaPlayer();

        public void PlaybackMusic(string source)
        {
            string uriSource = source;
            if (mediaTheme != null)
            {
                mediaTheme.Open(new Uri(source, UriKind.Relative));
                mediaTheme.MediaEnded += new EventHandler(Media_Ended);
                mediaTheme.Volume = MainWindow.gameProp.PropMusicVolume;
                mediaTheme.Play();

                return;
            }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            mediaTheme.Position = TimeSpan.Zero;
            mediaTheme.Play();
        }

        private void FadeImage()
        {
            Storyboard fadeInStoryboard = (Storyboard)FindResource("FadeInStoryboard");
            fadeInStoryboard.Begin();
        }
        private void FadeText()
        {
            Storyboard typingEffect = (Storyboard)FindResource("FadeInStoryboardText");
            typingEffect.Begin(InfoTextBox);
        }

        private void FadeButton()
        {
            Storyboard buttonEffect = (Storyboard)FindResource("FadeButton");
            buttonEffect.Begin();
        }

        void Localization()
        {
            string dictionaryPath = "";
            if(MainWindow.gameProp.PropLanguage == "Ukrainian (UA)")
                dictionaryPath = $"Resources\\Dictionary\\UA-Dictionary.xaml";
            else
                dictionaryPath = $"Resources\\Dictionary\\ENG-Dictionary.xaml";

            // загружаем ресурсный словарь из файла
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(dictionaryPath, UriKind.Relative);

            // устанавливаем словарь как главный для приложения
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Localization();
            InfoTextBox.Text = (string)Application.Current.FindResource("startStory0");

            PlaybackMusic("Resources\\1-05 Firelink Shrine.mp3");

            FadeImage(); 
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mediaTheme.Close();
        }

        MediaPlayer mediaButton = new MediaPlayer();

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

        int counter = 0;
        private void BorderSkip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            counter = 18;

            
            FooterBorder_MouseLeftButtonDown(sender, e);
            
        }

        private void FooterBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (counter <= 17)
                StartStory();

            if (counter >= 18)
            {
                BorderSkip.Visibility = Visibility.Collapsed;
                MainStory();
            }         
        }

        string answerText = "";
        private void ButtonAnswer_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            answerText = clickedButton.Content.ToString();
            counter++;
            MainStory();
        }

        bool isHardMode;
        Random random = new Random();

        
        int counterSoulsInHero = 0;
        int counterSoulsWhichHeroSave = 0;
        async void MainStory()
        {
            if (counter == 18)
            {
                counter++;
                PlaybackMusic("Resources\\1-17 Dancer Of The Boreal Valley.mp3");
                ChangeFrame((string)Application.Current.FindResource("mainStory0"), "hero.jpg");
            }
            else if (counter == 19)
            {
                counter++;
                ChangeFrame((string)Application.Current.FindResource("mainStory1"));
            }
            else if(counter == 20)
            {
                counter++;
                ChangeFrame((string)Application.Current.FindResource("mainStory2"), "Rashed_AlAkroka_dark_souls_3_fantasy_art_sword_knight_eclipse_sparks_women-1934187.jpg");
            }
            else if (counter == 21)
            {
                ChangeFrame("");

                ArrowBorder.Visibility = Visibility.Collapsed;
                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                await Task.Delay(2000);
                FadeButton();

                //string btn1Text = ;

                ButtonAnswerOne.Content = (string)Application.Current.FindResource("btnString0");
                ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString1");
                ButtonAnswerThree.Content = "...";

                ButtonAnswerOne.Visibility = Visibility.Visible;
                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;

            }
            else if (counter == 22)
            {

                ButtonAnswerOne.Visibility = Visibility.Hidden;
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if (answerText == ButtonAnswerOne.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory3"));

                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString2");
                    ButtonAnswerThree.Content = "...";
                }
                else if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory4"));
                    
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString2");
                    ButtonAnswerThree.Content = "...";
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory5"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString3");
                    ButtonAnswerThree.Content = "...";
                }

            }
            else if (counter == 23)
            {           
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;
         
                if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory6"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString4");
                    ButtonAnswerThree.Content = "...";
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory7"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString5");
                    ButtonAnswerThree.Content = "...";
                }
            }
            else if (counter == 24)
            {   
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if (answerText == (string)Application.Current.FindResource("btnString4"))
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory8"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString6");
                    ButtonAnswerThree.Content = "...";
                }
                else if (answerText == (string)Application.Current.FindResource("btnString5"))
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory9"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString6");
                    ButtonAnswerThree.Content = "...";
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory8"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString6");
                    ButtonAnswerThree.Content = "...";
                }
            }
            else if (counter == 25)
            {
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory10"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString7");
                    ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString8");
                }          
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory11"));
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString7");
                    ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString8");
                }
            }
            else if (counter == 26)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory12"), "1647197914_1-gamerwall-pro-p-dark-souls-koster-art-krasivie-oboi-2.jpg");

                ArrowBorder.Visibility = Visibility.Visible;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if(answerText == ButtonAnswerTwo.Content)
                    isHardMode = false;
                else 
                    isHardMode = true;

                counter++;

            }
            else if (counter == 27)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory13"), "compas.jpg");
                counter++;
            }
            else if (counter == 28)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory14"), "priroda.jpg");
                counter++;
            }
            else if (counter == 29)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory15"), "katokombi.jpg");
                counter++;
            }
            else if (counter == 30)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory16"));       
                counter++;
            }
            else if (counter == 31)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory17"), "nedryKatak.jpg");
                counter++;
            }
            else if (counter == 32)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory18"), "nitoNear.jpg");            
                PlaybackMusic("Resources\\1-07 Curse-Rotted Greatwood.mp3");
                counter++;
            }
            else if (counter == 33)
            {

                ChangeFrame("");

                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                await Task.Delay(1500);
                FadeButton();
                
                ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString9");
                ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString10");
         
                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;

            }
            else if (counter == 34)
            {              
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                ChangeFrame((string)Application.Current.FindResource("mainStory18"));
                await Task.Delay(5000);

                ChangeFrame((string)Application.Current.FindResource("mainStory19"));
                await Task.Delay(5000);

                ChangeFrame((string)Application.Current.FindResource("mainStory20"));
                await Task.Delay(5000);

                ChangeFrame((string)Application.Current.FindResource("mainStory21"));
                await Task.Delay(5000);

                if (isHardMode)
                {
                    Random random = new Random();
                    if (random.Next(1, 101) <= 15)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory22"));
                        StoryOfTheDeceased();
                        return;
                    }
                }

                ChangeFrame((string)Application.Current.FindResource("mainStory23"));
                await Task.Delay(5000);

                ChangeFrame((string)Application.Current.FindResource("mainStory24"));
                await Task.Delay(5000);

                FadeButton();

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;

                ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString11");
                ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString12");
                
            }
            else if (counter == 35)
            {
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                ArrowBorder.Visibility = Visibility.Visible;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory25"), "nitoDead.jpg");
                    counterSoulsWhichHeroSave++;
                    counter++;
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory26"), "nitoDead.jpg");
                    counterSoulsInHero++;
                    counter++;
                }
            }
            else if (counter == 36)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory27"), "pustoshi.jpg");
                PlaybackMusic("Resources\\1-18 Oceiros, The Consumed King.mp3");
                counter++;
            }
            else if (counter == 37)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory28"), "castleCharodeiki.jpg");
                counter++;
            }
            else if (counter == 38)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory29"), "IzalitaNear.jpg");
                counter++;
            }
            else if (counter == 39)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory30"));
                counter++;
            }
            else if (counter == 40)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory31"), "izolitaFace.jpg");
                counter++;
            }
            else if (counter == 41)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory32"));

                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                await Task.Delay(1500);
                FadeButton();

                ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString13");
                ButtonAnswerThree.Content = "...";

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;
            }
            else if (counter == 42)
            {
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory33"), "izalitaAttack.jpg");
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString14");
                    ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString15");
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory34"), "izalitaAttack.jpg");
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString14");
                    ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString15");
                }
            }
            else if (counter == 43)
            {            
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                Random random = new Random();
                if (answerText == ButtonAnswerTwo.Content)
                {
                    await Task.Delay(1000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory35"));
                    await Task.Delay(5000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory36"));
                    await Task.Delay(5000);    
                   
                    if (isHardMode)
                    {
                        if (random.Next(1, 101) <= 20)
                        {
                            FadeText();
                            InfoTextBox.Text = (string)Application.Current.FindResource("mainStory37");
                            await Task.Delay(6000);
                            StoryOfTheDeceased();
                            return;
                        }                         
                    }

                    if (random.Next(1, 101) <= 90)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory38"));
                        await Task.Delay(7000);

                        ChangeFrame((string)Application.Current.FindResource("mainStory39"), "charodeikaDead.jpg");
                        await Task.Delay(5000);

                        FadeButton();

                        ButtonAnswerTwo.Visibility = Visibility.Visible;
                        ButtonAnswerThree.Visibility = Visibility.Visible;

                        ButtonAnswerTwo.Content = $"{(string)Application.Current.FindResource("mainStory40")} [{counterSoulsWhichHeroSave}/3]";
                        ButtonAnswerThree.Content = $"{(string)Application.Current.FindResource("mainStory41")} [{counterSoulsInHero}/3]";
                    }
                    else
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory37"));
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory42"));
                    await Task.Delay(6000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory43"));
                    await Task.Delay(6000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory44"));
                    await Task.Delay(6000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory45"));
                    await Task.Delay(6000);

                    if (isHardMode)
                    {
                        if (random.Next(1, 101) <= 10)
                        {
                            ChangeFrame((string)Application.Current.FindResource("mainStory37"));
                            await Task.Delay(6000);
                            StoryOfTheDeceased();
                            return;
                        }                           
                    }

                    ChangeFrame((string)Application.Current.FindResource("mainStory46"));
                    await Task.Delay(6000);

                    ChangeFrame((string)Application.Current.FindResource("mainStory39"), "charodeikaDead.jpg");                
                    await Task.Delay(5000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = $"{(string)Application.Current.FindResource("mainStory40")} [{counterSoulsWhichHeroSave}/3]";
                    ButtonAnswerThree.Content = $"{(string)Application.Current.FindResource("mainStory41")} [{counterSoulsInHero}/3]";
                }
            }
            else if (counter == 44)
            {
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                ArrowBorder.Visibility = Visibility.Visible;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory25"), "afterDeadIzolita.jpg");
                    counterSoulsWhichHeroSave++;
                    counter++;
                }
                else
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory26"), "afterDeadIzolita.jpg");
                    counterSoulsInHero++;
                    counter++;
                }
            }
            else if (counter == 45)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory47"), "mountains.jpg");
                PlaybackMusic("Resources\\1-04 Iudex Gundyr.mp3");
                counter++;
            }
            else if (counter == 46)
            {
                Random random = new Random();
            
                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                ChangeFrame((string)Application.Current.FindResource("mainStory48"), "hram.jpg");
                await Task.Delay(8000);

                ChangeFrame((string)Application.Current.FindResource("mainStory49"));
                await Task.Delay(8000);

                if (isHardMode)
                {
                    if (random.Next(1, 101) <= 5)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory50"));
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                ChangeFrame((string)Application.Current.FindResource("mainStory51"), "stormPunch.jpg");
                await Task.Delay(8000);
                        
                if(isHardMode)
                {
                    if (random.Next(1, 101) <= 10)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory50"));
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                ChangeFrame((string)Application.Current.FindResource("mainStory52"), "GvinNear.jpg");
                await Task.Delay(8000);

                ChangeFrame((string)Application.Current.FindResource("mainStory53"));

                ArrowBorder.Visibility = Visibility.Visible;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                counter++;
            }
            else if (counter == 47)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory54"));             
                counter++;
            }
            else if (counter == 48)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory55"), "gvinAttack.jpg");
                counter++;
            }
            else if (counter == 49)
            {
                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;
    
                ChangeFrame((string)Application.Current.FindResource("mainStory56"));
                await Task.Delay(8000);

                if (isHardMode)
                {
                    if (random.Next(1, 101) <= 5)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory50"));
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }
                ChangeFrame((string)Application.Current.FindResource("mainStory57"));
                await Task.Delay(7000);
                ChangeFrame((string)Application.Current.FindResource("mainStory58"));
                await Task.Delay(4000);

                FadeButton();

                ButtonAnswerTwo.Content = (string)Application.Current.FindResource("btnString16");
                ButtonAnswerThree.Content = (string)Application.Current.FindResource("btnString17");

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;
            }
            else if (counter == 50)
            {
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    ChangeFrame((string)Application.Current.FindResource("mainStory59"), "knightAttack.jpg");
                    await Task.Delay(5000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory60"));
                    await Task.Delay(5000);
                    ChangeFrame((string)Application.Current.FindResource("mainStory61"));
                    await Task.Delay(5000);                 
                }
                else
                {
                    if (random.Next(1, 101) > 10)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory62"));
                        await Task.Delay(7000);
                        ChangeFrame((string)Application.Current.FindResource("mainStory63"));
                        await Task.Delay(7000);

                        if (isHardMode)
                        {
                            if (random.Next(1, 101) <= 30)
                            {
                                ChangeFrame((string)Application.Current.FindResource("mainStory50"));
                                await Task.Delay(6000);
                                StoryOfTheDeceased();
                                return;
                            }
                        }

                        ChangeFrame((string)Application.Current.FindResource("mainStory64"), "knightAttack.jpg");
                        await Task.Delay(7000);
                    }
                    else
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory50"));
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                ChangeFrame((string)Application.Current.FindResource("mainStory65"), "angryGvin.jpg");
                await Task.Delay(9000);
                ChangeFrame((string)Application.Current.FindResource("mainStory66"));
                await Task.Delay(8000);

                if (isHardMode)
                {
                    if (random.Next(1, 101) <= 10)
                    {
                        ChangeFrame((string)Application.Current.FindResource("mainStory50"));
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                ChangeFrame((string)Application.Current.FindResource("mainStory67"), "knightAttack.jpg");
                await Task.Delay(8000);
                ChangeFrame((string)Application.Current.FindResource("mainStory68"), "deadGvin.jpg");
                await Task.Delay(4000);

                FadeButton();

                ButtonAnswerTwo.Content = $"{(string)Application.Current.FindResource("mainStory40")} [{counterSoulsWhichHeroSave}/3]";
                ButtonAnswerThree.Content = $"{(string)Application.Current.FindResource("mainStory41")} [{counterSoulsInHero}/3]";

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;
            }

            else if (counter == 51)
            {
                ChangeFrame((string)Application.Current.FindResource("mainStory69"), "heroEnd.jpg");

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                await Task.Delay(12000);

                if (answerText == ButtonAnswerTwo.Content)
                {
                    counterSoulsWhichHeroSave++;                 
                }
                else
                {
                    counterSoulsInHero++;                  
                }
            
                if (counterSoulsInHero == 3)
                    DarkEndStory();
                else
                    GoodEndStory();
            }

        }

        async void GoodEndStory()
        {
            PlaybackMusic("Resources\\1-25 Epilogue.mp3");
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd0"), "hranitelnicaSay.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd1"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd2"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd3"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd4"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd5"), "92361-games-arts-the-keeper-of-fire-fire-keeper-dark.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd6"), "5227654.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd7"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd8"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd9"), "photo_2023-05-03_18-31-50.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyGoodEnd10"), "flowers.jpg");
            await Task.Delay(12000);

            FooterBorder.Visibility = Visibility.Collapsed;
            TheEndLabel.Visibility = Visibility.Visible;
        }

        async void DarkEndStory()
        {
            PlaybackMusic("Resources\\2-10 Sister Friede and Father Ariandel.mp3");
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd0"), "1495318152_ev1ct_fire-keeper.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd1"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd2"), "697790.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd3"), "1495318152_ev1ct_fire-keeper.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd4"), "f75d3239266dbfab17a9c17ffb4a270a.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd5"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd6"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyDarkEnd7"), "c123c6c3f80b0668b8b7a46cdbb28722.jpg");
            await Task.Delay(12000);


            FooterBorder.Visibility = Visibility.Collapsed;
            FadeText();
            TheEndLabel.Visibility = Visibility.Visible;
        }

        async void StoryOfTheDeceased()
        {
            await Task.Delay(6000);
            FooterBorder.IsEnabled = false;
            ArrowBorder.IsEnabled = false;
            ArrowBorder.Visibility = Visibility.Collapsed;

            ChangeFrame("");
            await Task.Delay(3000);
            PlaybackMusic("Resources\\2-07 Unused Track 6.mp3");
            ChangeFrame((string)Application.Current.FindResource("storyOfTheDeceased0"), "dark-souls-ii-humanity-knight.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyOfTheDeceased1"), "nws_dark_souls3_001.jpg");
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyOfTheDeceased2"));
            await Task.Delay(12000);
            ChangeFrame((string)Application.Current.FindResource("storyOfTheDeceased3"), "warriors.jpg");
            await Task.Delay(12000);
            ChangeFrame("", "theEnd.jpg");

            FooterBorder.Visibility = Visibility.Collapsed;           
        }

        void ChangeFrame(string text, string imageName = null)
        {
            if (imageName == null)
                FadeText();
            else
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri($"Resources\\{imageName}", UriKind.Relative));
            }
            InfoTextBox.Text = "";
            foreach(char c in text)
            {
                InfoTextBox.Text += c;
                Task.Delay(20);
            }
        }

        void StartStory()
        {
       
            counter++;        

            if (counter == 1)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory1"), "fotor-ai-202305030380.jpg");
            }
            else if (counter == 2)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory2"), "greaterFlame.jpg");         
            }
            else if (counter == 3)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory3"), "animalsFear.jpg");
            }
            else if (counter == 4)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory4"), "fotor-ai-2023050311139.jpg");
            }
            else if (counter == 5)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory5"), "firstLight.jpg");
            }
            else if (counter == 6)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory6"), "gigants.jpg");
            }
            else if (counter == 7)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory7"), "nito.jpg");
            }
            else if (counter == 8)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory8"), "charodeika.jpg");
            }
            else if (counter == 9)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory9"), "povelitelLight.jpg");
            }
            else if (counter == 10)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory10"), "blackSoul.jpg");
            }
            else if (counter == 11)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory11"), "siluet.jpg");
            }
            else if (counter == 12)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory12"), "pigmeygnom.jpg");
            }
            else if (counter == 13)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory13"), "army.jpg");          
            }
            else if (counter == 14)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory14"), "army2.jpg");
            }
            else if (counter == 15)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory15"), "crystall.jpg");
            }
            else if (counter == 16)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory16"), "nepogashiy.jpg");
            }
            else if (counter == 17)
            {
                ChangeFrame((string)Application.Current.FindResource("startStory17"), "hero.jpg");
            }
        }

    }
}
