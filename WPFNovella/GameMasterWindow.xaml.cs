using System;
using System.Collections.Generic;
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

        bool hardMode;
        int counterSoulsInHero = 0;
        int counterSoulsWhichHeroSave = 0;
        async void MainStory()
        {
            if (counter == 18)
            {
                counter++;
                PlaybackMusic("Resources\\1-17 Dancer Of The Boreal Valley.mp3");

                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\hero.jpg", UriKind.Relative));

                InfoTextBox.Text = "*Пробудившись, ти знайшов джерело священного вогню*";
            }
            else if (counter == 19)
            {
                counter++;
                FadeText();

                InfoTextBox.Text = "*Сидячи біля нього ти почув звуки кроків ззаду*";
            }
            else if(counter == 20)
            {
                counter++;
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\Rashed_AlAkroka_dark_souls_3_fantasy_art_sword_knight_eclipse_sparks_women-1934187.jpg", UriKind.Relative));

                InfoTextBox.Text = "*До джерела вогню підійшла якась дівчина*";
            }
            else if (counter == 21)
            {
                FadeText();
                

                InfoTextBox.Text = "";

                ArrowBorder.Visibility = Visibility.Collapsed;
                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                await Task.Delay(2000);
                FadeButton();
                ButtonAnswerOne.Content = "Хто ти?!";
                ButtonAnswerTwo.Content = "Де я?";
                ButtonAnswerThree.Content = "...";

                ButtonAnswerOne.Visibility = Visibility.Visible;
                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;

            }
            else if (counter == 22)
            {
                FadeText();

                ButtonAnswerOne.Visibility = Visibility.Hidden;
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if (answerText == ButtonAnswerOne.Content)
                {    
                    InfoTextBox.Text = "-Я – Емілія, хранителька вогню. Моя роль полягає в тому, щоб підтримувати вогонь, який тримає цей світ у рівновазі.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = "Яке мое призначення?";
                    ButtonAnswerThree.Content = "...";
                }
                else if (answerText == ButtonAnswerTwo.Content)
                {
                    InfoTextBox.Text = "-Ти знаходишся біля багаття священного полум'я, місця, де незгаслі можуть відпочити та відновити сили. Він також служить дороговказом для мандрівників, які блукають світом.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = "-Яке мое призначення?";
                    ButtonAnswerThree.Content = "...";
                }
                else
                {
                    InfoTextBox.Text = "*після деякого часу* -Ти, мабуть, відчуваєш зміни у світі, чи не так? Після того, як володарі відібрали душу Пігмея, світ поринув у пітьму. Але зараз, незгаслі знову прокинулися і світ отримав новий шанс на спасіння. Я вдячна тобі за це.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = "Дякую, яке мое призначення?";
                    ButtonAnswerThree.Content = "...";
                }

            }
            else if (counter == 23)
            {
                FadeText();

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

            
                if (answerText == ButtonAnswerTwo.Content)
                {
                    InfoTextBox.Text = "-Ти з'явився підходящий момент. Тільки Незгаслий може принести світло у цей світ, і ти можеш стати нашим героєм, який врятує його від пітьми.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = "Що мені потрібно робити?";
                    ButtonAnswerThree.Content = "...";
                }
                else
                {
                    InfoTextBox.Text = "-Ти Незгаслий, це означає, що ти є обранцем великого вогню. Твоя роль – знищити володарів, які заволоділи великими темними душами, через яких полум'я, а разом з ним і світ – гаснуть, а темрява стає сильнішою.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = "Де мені шукати їх?";
                    ButtonAnswerThree.Content = "...";
                }
            }
            else if (counter == 24)
            {
                FadeText();
             
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;


                if (answerText == "Що мені потрібно робити?")
                {
                    InfoTextBox.Text = "-Тобі потрібно знайти володарів, та знищити великі темні души, щоб відновити рівновагу у світі та повернути пітьму на своє місце. Ось тобі променевий компас вилкого полум'я. Він проведе тебе до володарів.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = "Дякую.";
                    ButtonAnswerThree.Content = "...";
                }
                else if (answerText == "Де мені шукати їх?")
                {
                    InfoTextBox.Text = "-Ось тобі променевий компас вилкого полум'я. Він проведе тебе до володарів. Я дуже тебе прошу, знищи ці души.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = "Дякую.";
                    ButtonAnswerThree.Content = "...";
                }
                else
                {
                    InfoTextBox.Text = "-Тобі потрібно знайти володарів, та знищити великі темні души, щоб відновити рівновагу у світі та повернути пітьму на своє місце. Ось тобі променевий компас вилкого полум'я. Він проведе тебе до володарів.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = "Дякую.";
                    ButtonAnswerThree.Content = "...";
                }
            }
            else if (counter == 25)
            {
                FadeText();

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;


                if (answerText == ButtonAnswerTwo.Content)
                {
                    InfoTextBox.Text = "-Будь ласка, візьми цей оберіг. Він допоможе тобі у битві проти володарів.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = "*Взяти оберіг* [Easy mode]";
                    ButtonAnswerThree.Content = "*Відмовитись* [Hard mode]";
                }          
                else
                {
                    InfoTextBox.Text = "-Візьми цей оберіг. Він допоможе тобі у битві проти володарів.";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = "*Взяти оберіг* [Easy mode]";
                    ButtonAnswerThree.Content = "... [Hard mode]";
                }
            }
            else if (counter == 26)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\1647197914_1-gamerwall-pro-p-dark-souls-koster-art-krasivie-oboi-2.jpg", UriKind.Relative));

                //FadeText();
                FadeImage();

                ArrowBorder.Visibility = Visibility.Visible;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                if(answerText == ButtonAnswerTwo.Content)
                    hardMode = false;
                else 
                    hardMode = true;

                counter++;

                InfoTextBox.Text = "*Незгаслий відчув сильне бажання допомогти, і зважився вирушити в небезпечну подорож, щоб зруйнувати владу трьох володарів, які утримували великі душі, необхідні для миру.*";
            }
            else if (counter == 27)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\compas.jpg", UriKind.Relative));

                //FadeText();
                FadeImage();

                counter++;

                InfoTextBox.Text = "*Променевий компас засвітився і почав вказувати дорогу, ви вирішили йти слідом.*";
            }
            else if (counter == 28)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\priroda.jpg", UriKind.Relative));

                //FadeText();
                FadeImage();

                counter++;

                InfoTextBox.Text = "*Незгаслий йшов за променем, до першого володаря Ніто, к Першому із Мертвих*";
            }
            else if (counter == 29)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\katokombi.jpg", UriKind.Relative));

                //FadeText();
                FadeImage();

                counter++;

                InfoTextBox.Text = "*Після деякого часу, незгаслий прийшов до входу у величезні катакомби, притулок Ніто*";
            }
            else if (counter == 30)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\katokombi.jpg", UriKind.Relative));

                FadeText();
                //FadeImage();

                counter++;

                InfoTextBox.Text = "*Незгаслий зайшов усередину і почав спускатися в надр катакомби*";
            }
            else if (counter == 31)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\nedryKatak.jpg", UriKind.Relative));

                //FadeText();
                FadeImage();

                counter++;

                InfoTextBox.Text = "*Через деякий час. Ви почули сзаду шум. Незгаслий почав акуратно повертатися у бік звуку*";
            }
            else if (counter == 32)
            {
                GameImage.Source = new BitmapImage(new Uri("Resources\\nitoNear.jpg", UriKind.Relative));

                PlaybackMusic("Resources\\1-07 Curse-Rotted Greatwood.mp3");

                //FadeText();
                FadeImage();

                counter++;

                InfoTextBox.Text = "*Повернувшись Незгаслий завмер.. позаду нього був Ніто, який пильно дивився на незгаслого*";
            }
            else if (counter == 33)
            {
              
                //FadeText();
                //FadeImage();

                InfoTextBox.Text = "";

                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                await Task.Delay(1500);
                FadeButton();
                
                ButtonAnswerTwo.Content = "Бійся мене. Я твоя смерть. [Напасти]";
                ButtonAnswerThree.Content = "*Напасти на Ніто*";

               
                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;

            }
            else if (counter == 34)
            {
                FadeText();
               
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                FadeText();
                InfoTextBox.Text = "*Різко діставши меч, Незгаслий, побіг у бік Ніто, після чого спробував зробити удар*";
                await Task.Delay(5000);

                FadeText();
                InfoTextBox.Text = "*Продзвенів звук металу по всіх тунелях катакомб*";
                await Task.Delay(5000);

                FadeText();
                InfoTextBox.Text = "*Меч Незгаслий та Ніто схерстились у танці бою*";
                await Task.Delay(5000);

                FadeText();
                InfoTextBox.Text = "*Незгаслий спробував нанести ще один удар*";
                await Task.Delay(5000);

                if (hardMode)
                {                    
                    Random random = new Random();
                    if (random.Next(1, 101) <= 15)
                    {
                        FadeText();
                        InfoTextBox.Text = "*Ніто вбив вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                FadeText();
                InfoTextBox.Text = "*Незгаслий зробив вдачиний випад мечем, та пробив наскрізь тіло Ніто*";
                await Task.Delay(5000);

                FadeText();
                InfoTextBox.Text = "*Після цього Ніто впав замертво, а ви отримали першу темну душу. Що ви будете з нею робити?*";
                await Task.Delay(5000);

                FadeButton();

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;

                ButtonAnswerTwo.Content = "Знищити [Після знищення усіх трьох темних душ, ви врятуєте світ від тьми]";
                ButtonAnswerThree.Content = "Поглинути [Після поглиннаня усіх трьох темних душ, ви станете Богом]";
                
            }
            else if (counter == 35)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\nitoDead.jpg", UriKind.Relative));

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                ArrowBorder.Visibility = Visibility.Visible;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    InfoTextBox.Text = "*Після знищення ти відчув, як світ став краще, а тьма стала слабіше*";

                    counterSoulsWhichHeroSave++;

                    counter++;
                }
                else
                {
                    InfoTextBox.Text = "*Незгаслий поглинув душу, та став сильніше*";

                    counterSoulsInHero++;

                    counter++;
                }
            }
            else if (counter == 36)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\pustoshi.jpg", UriKind.Relative));

                PlaybackMusic("Resources\\1-18 Oceiros, The Consumed King.mp3");

                InfoTextBox.Text = "*Після чого Незгаслий пішов к другому володарю*";
                counter++;
            }
            else if (counter == 37)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\castleCharodeiki.jpg", UriKind.Relative));              

                InfoTextBox.Text = "*Через деякій час, Незгаслий дійшов до замку Ізаліти, другої володорки темних душ*";
                counter++;
            }
            else if (counter == 38)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\IzalitaNear.jpg", UriKind.Relative));

                InfoTextBox.Text = "*Пройшовши через ворота замка. Незгаслий побачив вдалині Ізаліту*";
                counter++;
            }
            else if (counter == 39)
            {
                FadeText();
                
                InfoTextBox.Text = "*Голос Ізаліти* - Стій там. Або мені прийдеться вбити тебе!";
                counter++;
            }
            else if (counter == 40)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\izolitaFace.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Ти прийшов до мене, щоб померти?";
                counter++;
            }
            else if (counter == 41)
            {
                FadeText();             

                InfoTextBox.Text = "-Якщо ти підеш, я не переслідуватиму тебе. Але якщо ти залишишся, я буду змушена знищити тебе.";

                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                await Task.Delay(1500);
                FadeButton();

                ButtonAnswerTwo.Content = "Я не збираюся йти. Я прийшов, щоб повернути темні душі.";
                ButtonAnswerThree.Content = "...";

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;
            }
            else if (counter == 42)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\izalitaAttack.jpg", UriKind.Relative));

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;


                if (answerText == ButtonAnswerTwo.Content)
                {
                    InfoTextBox.Text = "-Ти ідіот, в тебе не вийде це, ПОМРИ КОМАХА! *Ізаліта починає чаклувати вогнянну кулю*";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;


                    ButtonAnswerTwo.Content = "[Побігти в атаку]";
                    ButtonAnswerThree.Content = "[Спробувати ухилитися]";
                }
                else
                {
                    InfoTextBox.Text = "-Ти не дуже балакучий, що ж, ПОМРИ! *Ізаліта починає чаклувати вогнянну кулю*";
                    await Task.Delay(2000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = "[Побігти в атаку]";
                    ButtonAnswerThree.Content = "[Спробувати ухилитися]";
                }
            }
            else if (counter == 43)
            {
                //FadeText();

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                Random random = new Random();
                if (answerText == ButtonAnswerTwo.Content)
                {
                    await Task.Delay(1000);
                    FadeText();
                    InfoTextBox.Text = "*Ви з мечем почали бігти на Ізаліту*";
                    await Task.Delay(5000);
                    FadeText();
                    InfoTextBox.Text = "*У бік вас летить вогнянна куля*";
                    await Task.Delay(5000);    
                   
                    if (hardMode)
                    {
                        if (random.Next(1, 101) <= 20)
                        {
                            FadeText();
                            InfoTextBox.Text = "*Ізаліта вбила вас*";
                            await Task.Delay(6000);
                            StoryOfTheDeceased();
                            return;
                        }                         
                    }

                    if (random.Next(1, 101) <= 90)
                    {
                        FadeText();
                        InfoTextBox.Text = "*Ви ухіляетесь від вогнянної кулі, після чого наносите удар*";
                        await Task.Delay(7000);

                        GameImage.Source = new BitmapImage(new Uri("Resources\\charodeikaDead.jpg", UriKind.Relative));
                        FadeImage();
                        InfoTextBox.Text = "*Ваш меч пройшов через плече, розрізавши Ізаліту до живота. Після чого ви отримали другу темну душу. Що ви будете робити з нею?*";
                        await Task.Delay(5000);

                        FadeButton();

                        ButtonAnswerTwo.Visibility = Visibility.Visible;
                        ButtonAnswerThree.Visibility = Visibility.Visible;

                        ButtonAnswerTwo.Content = $"Знищити [{counterSoulsWhichHeroSave}/3]";
                        ButtonAnswerThree.Content = $"Поглинути [{counterSoulsInHero}/3]";
                    }
                    else
                    {
                        FadeText();
                        InfoTextBox.Text = "*Ізаліта вбила вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }
                else
                {
                    FadeText();
                    InfoTextBox.Text = "*Ви почали акуратно йти на Ізаліту ухиляючись від її атак*";
                    await Task.Delay(6000);
                    FadeText();
                    InfoTextBox.Text = "*Після того як ви підійшли до Ізаліти, ви почали наности удари*";
                    await Task.Delay(6000); 
                    FadeText();
                    InfoTextBox.Text = "*Ізаліта завдяки магії відбивала ваші удари*";
                    await Task.Delay(6000);
                    FadeText();
                    InfoTextBox.Text = "*Вона спробувала спалити вас вогнем*";
                    await Task.Delay(6000);

                    if (hardMode)
                    {
                        if (random.Next(1, 101) <= 10)
                        {
                            FadeText();
                            InfoTextBox.Text = "*Ізаліта вбила вас*";
                            await Task.Delay(6000);
                            StoryOfTheDeceased();
                            return;
                        }                           
                    }

                    FadeText();
                    InfoTextBox.Text = "*Ви ухиляєтесь від її магії*";
                    await Task.Delay(6000);

                    GameImage.Source = new BitmapImage(new Uri("Resources\\charodeikaDead.jpg", UriKind.Relative));
                    FadeImage();
                    InfoTextBox.Text = "*Ваш меч пройшов через плече, розрізавши Ізаліту до живота. Після чого ви отримали другу темну душу. Що ви будете робити з нею?*";
                    await Task.Delay(5000);

                    FadeButton();

                    ButtonAnswerTwo.Visibility = Visibility.Visible;
                    ButtonAnswerThree.Visibility = Visibility.Visible;

                    ButtonAnswerTwo.Content = $"Знищити [{counterSoulsWhichHeroSave}/3]";
                    ButtonAnswerThree.Content = $"Поглинути [{counterSoulsInHero}/3]" ;
                }
            }
            else if (counter == 44)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\afterDeadIzolita.jpg", UriKind.Relative));

                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                ArrowBorder.Visibility = Visibility.Visible;

                if (answerText == ButtonAnswerTwo.Content)
                {
                    InfoTextBox.Text = "*Після знищення ти відчув, як світ став краще, а тьма стала слабіше*";

                    counterSoulsWhichHeroSave++;

                    counter++;
                }
                else
                {
                    InfoTextBox.Text = "*Незгаслий поглинув душу, та став сильніше*";

                    counterSoulsInHero++;

                    counter++;
                }
            }
            else if (counter == 45)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\mountains.jpg", UriKind.Relative));

                PlaybackMusic("Resources\\1-04 Iudex Gundyr.mp3");

                InfoTextBox.Text = "*Незгаслий пішов до останнього володаря. Промінь з компаса показував на храм, який знаходився за горами*";
                counter++;
            }
            else if (counter == 46)
            {
                Random random = new Random();

                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\hram.jpg", UriKind.Relative));


                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                InfoTextBox.Text = "*Незгаслий підійшов до храму, як раптом..*";
                await Task.Delay(8000);

                FadeText();
                InfoTextBox.Text = "*З неба в вас летить блискавка...*";
                await Task.Delay(8000);

                if (hardMode)
                {
                    if (random.Next(1, 101) <= 5)
                    {
                        FadeText();
                        InfoTextBox.Text = "*Гвін вбив вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\stormPunch.jpg", UriKind.Relative)); 
                InfoTextBox.Text = "*Ви ухиляєтесь від неї, але одразу летить ще одна...*";
                await Task.Delay(8000);
                        
                if(hardMode)
                {
                    if (random.Next(1, 101) <= 10)
                    {
                        FadeText();
                        InfoTextBox.Text = "*Гвін вбив вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\GvinNear.jpg", UriKind.Relative));
                InfoTextBox.Text = "*Ви змогли ухилитись ще раз, як раптом ви бачите, що з Храму виходить останній володарь душі, Гвін, король Світу*";
                await Task.Delay(8000);

                FadeText();          
                InfoTextBox.Text = "*Голос Гвіна* - Ти зайшов дуже далеко.";

                ArrowBorder.Visibility = Visibility.Visible;

                FooterBorder.IsEnabled = true;
                ArrowBorder.IsEnabled = true;

                counter++;
            }
            else if (counter == 47)
            {        
                FadeText();
                InfoTextBox.Text = "-Твоя мета безглузда. Ніхто не може мене зупинити. Ти помреш!";
               
                counter++;
            }
            else if (counter == 48)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\gvinAttack.jpg", UriKind.Relative));
                InfoTextBox.Text = "-Ти не зможеш перемогти мене, *говорив він, атакуючи Незгаслого.* Я - володарь пітьми, і моя сила безмежна!";

                counter++;
            }
            else if (counter == 49)
            {
                ArrowBorder.Visibility = Visibility.Collapsed;

                FooterBorder.IsEnabled = false;
                ArrowBorder.IsEnabled = false;

                Random random = new Random();

                FadeText();
                InfoTextBox.Text = "-ТИ ПОМРЕШ. І БІЛЬШЕ НІКТО НЕ ЗМОЖЕ ПЕРЕМОГТИ МЕНЕ. *Гвін кинув у Незгаслого ще одну блискавку*";
                await Task.Delay(8000);
                if (hardMode)
                {
                    if (random.Next(1, 101) <= 5)
                    {
                        FadeText();
                        InfoTextBox.Text = "*Гвін вбив вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }
                FadeText();
                InfoTextBox.Text = "*Ви змогли ухилитись*";
                await Task.Delay(7000);
                FadeText();
                InfoTextBox.Text = "*Незгаслий розумів, що йому треба щось вигадати, щоб перемогти*";
                await Task.Delay(4000);

                FadeButton();

                ButtonAnswerTwo.Content = "[Обійти Гвіна і завдати йому удару]";
                ButtonAnswerThree.Content = "Ти жалюгідний. [Зробити атаку спереду]";


                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;
            }
            else if (counter == 50)
            {
                ButtonAnswerTwo.Visibility = Visibility.Hidden;
                ButtonAnswerThree.Visibility = Visibility.Hidden;

                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\knightAttack.jpg", UriKind.Relative));
                Random random = new Random();
                if (answerText == ButtonAnswerTwo.Content)
                {
                    FadeText();
                    InfoTextBox.Text = "*Ви з мечем почали оббігати Гвіна, щоб ударити його*";
                    await Task.Delay(5000);
                    FadeText();
                    InfoTextBox.Text = "*Замахнувшись ви наносите удар та...*";
                    await Task.Delay(5000);
                    FadeText();
                    InfoTextBox.Text = "*Ви успішно наносите йому удар*";
                    await Task.Delay(5000);                 
                }
                else
                {
                    if (random.Next(1, 101) > 10)
                    {
                        FadeText();
                        InfoTextBox.Text = "Незгаслий ухилився від блискавки і зібрався атакувати Гвіна, незважаючи на його погрози.";
                        await Task.Delay(7000);
                        FadeText();
                        InfoTextBox.Text = "*Гвін продовжує кидати блискавки у незгаслого* ПОМРИ!";
                        await Task.Delay(7000);
                        if (hardMode)
                        {
                            if (random.Next(1, 101) <= 30)
                            {
                                FadeText();
                                InfoTextBox.Text = "*Гвін вбив вас*";
                                await Task.Delay(6000);
                                StoryOfTheDeceased();
                                return;
                            }
                        }
                        FadeText();
                        InfoTextBox.Text = "*Ви наблизилися до Гвіна і завдаєте сильного удару*";
                        await Task.Delay(7000);
                    }
                    else
                    {
                        FadeText();
                        InfoTextBox.Text = "*Гвін вбив вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }

                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\angryGvin.jpg", UriKind.Relative));
                InfoTextBox.Text = "*Ви поранили Гвіна* -Ти... Я такого ще не бачив. Ти справді впевнений у своїх силах? Ти думаєш, що можеш змінити перебіг долі цього світу?";
                await Task.Delay(9000);
                FadeText();
                InfoTextBox.Text = "-Ти помиляєшься.. Цей світ нічого не змінить. Він вже мертвий. ПОМРИ *Гвін наносить удар*";
                await Task.Delay(8000);
                if (hardMode)
                {
                    if (random.Next(1, 101) <= 10)
                    {
                        FadeText();
                        InfoTextBox.Text = "*Гвін вбив вас*";
                        await Task.Delay(6000);
                        StoryOfTheDeceased();
                        return;
                    }
                }
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\knightAttack.jpg", UriKind.Relative));
                InfoTextBox.Text = "*Ви ухиляєтесь. Після чого наносете ще один удар по Гвіну*";
                await Task.Delay(8000);
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\deadGvin.jpg", UriKind.Relative));
                InfoTextBox.Text = "*Гвін падає на коліна. Після чого він віддає останню велику душу тобі. Що ти будеш з нею робити?*";
                await Task.Delay(4000);

                FadeButton();

                ButtonAnswerTwo.Content = $"Знищити [{counterSoulsWhichHeroSave}/3]";
                ButtonAnswerThree.Content = $"Поглинути [{counterSoulsInHero}/3]" ;

                ButtonAnswerTwo.Visibility = Visibility.Visible;
                ButtonAnswerThree.Visibility = Visibility.Visible;
            }

            else if (counter == 51)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\heroEnd.jpg", UriKind.Relative));
                InfoTextBox.Text = "*Стоявши серед поля битви, ти побачив як к тобі йде Емілія*";


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
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\hranitelnicaSay.jpg", UriKind.Relative));
            InfoTextBox.Text = "-Ти вбив усіх володарів. Я.. Я дуже дякую тобі, за те що ти врятував наш світ.. Але..";
            await Task.Delay(12000); 
            FadeText();
            InfoTextBox.Text = "-В тобі 4 темна душа.. яку ти успадкував від Пігмея.. І треба також її віддати великому полум'ю..";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "*Незгаслий важко зітхнув*";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "-Не бійся.. Велике полум'я поглине тебе.. І ти здобудеш спокій, Втрачена душа.";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "*Незгаслий повернувся щоб все обміркувати*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\92361-games-arts-the-keeper-of-fire-fire-keeper-dark.jpg", UriKind.Relative));
            InfoTextBox.Text = "-Прошу.. Не бійся.. я допоможу тобі.. *підійшовши ззаду к Незгаслому, та обійняла його*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\5227654.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Незгаслий повернувся до хранительки вогню, після чого кивнув їй*";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "-Дякую тобі.. Прощавай..";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "*Доторкнувшись к Незгаслому. Вона звільнила його душу*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\photo_2023-05-03_18-31-50.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Процес був не найприємнішим*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\flowers.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Після того як хранителька вогню повернула всі душі великому вогню.. У світ повернувся баланс..*";
            await Task.Delay(12000);
            //counter = 100;
            FooterBorder.Visibility = Visibility.Collapsed;
            FadeText();
            TheEndLabel.Visibility = Visibility.Visible;
        }

        async void DarkEndStory()
        {
            PlaybackMusic("Resources\\2-10 Sister Friede and Father Ariandel.mp3");
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\1495318152_ev1ct_fire-keeper.jpg", UriKind.Relative));
            InfoTextBox.Text = "-Я бачу.. що тебе поглинула тьма..";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "-Нащо ти зробив це.. Тепер наш світ помре.. А велике полум'я погасне.";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\697790.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Незгаслий каже* -Тепер я володар цього світу.";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\1495318152_ev1ct_fire-keeper.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Емілія* -Я не дозволю тобі.. ";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\f75d3239266dbfab17a9c17ffb4a270a.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Незгаслий дістав меч, після чого замахнувся і швидким ударом вбив Емілію*";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "-Тепер велике полум'я всередині мене.";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "-Тепер я. Король цього світу.";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\c123c6c3f80b0668b8b7a46cdbb28722.jpg", UriKind.Relative));
            InfoTextBox.Text = "-І ніхто не завадить мені..";
            await Task.Delay(12000);
            //counter = 100;
            FooterBorder.Visibility = Visibility.Collapsed;
            FadeText();
            TheEndLabel.Visibility = Visibility.Visible;
        }

        async void StoryOfTheDeceased()
        {

            FooterBorder.IsEnabled = false;
            ArrowBorder.IsEnabled = false;
            ArrowBorder.Visibility = Visibility.Collapsed;

            InfoTextBox.Text = "";
            await Task.Delay(3000);
            PlaybackMusic("Resources\\2-07 Unused Track 6.mp3");
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\dark-souls-ii-humanity-knight.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Після того як Незгаслий помер. Світ почала поглинати тьма*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\nws_dark_souls3_001.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Світ поринув у морок і хаос, і люди не могли знайти спосіб повернути рівновагу.*";
            await Task.Delay(12000);
            FadeText();
            InfoTextBox.Text = "*Однак не всі надії було втрачено*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\warriors.jpg", UriKind.Relative));
            InfoTextBox.Text = "*Існувала легенда про те, що у світі існують ще незгаслі, які відновлять рівновагу у світі.*";
            await Task.Delay(12000);
            FadeImage();
            GameImage.Source = new BitmapImage(new Uri("Resources\\theEnd.jpg", UriKind.Relative));
            FooterBorder.Visibility = Visibility.Collapsed;           
        }

        

        void StartStory()
        {
            counter++;
            if (counter == 1)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\fotor-ai-202305030380.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Їхні могутні тіла покривала дивовижна луска, завдяки якій вони жили вічно і були невразливими. Також дракони мали могутній артефакт – Початковий кристал, який міг зберігати життя. Так цей темний світ існував невідомий час, і його назвали Епохою Стародавніх.";
            }
            else if (counter == 2)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\greaterFlame.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Але потім з'явилося Полум'я. Полум'я несло у собі чотири Великі Душі. Саме вони стали джерелом конфлікту. Невідомо, звідки прийшли майбутні володарі Душ. В Епоху Стародавніх не існувало чаклунства, яким ми його знаємо, а світ був єдиний.";
            }
            else if (counter == 3)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\animalsFear.jpg", UriKind.Relative));

                InfoTextBox.Text = "-У темряві та тумані існували пригнічені жорстокими володарями істоти різної природи. Вони виживали в суворих умовах, без світла та надії, поки в надрах землі не виникло Полум'я. Можливо, саме воно і закликало нові форми життя з небуття, розділивши все, що існує на жар і холод, життя і смерть, світло і пітьму.";
            }
            else if (counter == 4)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\fotor-ai-2023050311139.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Дехто каже, що дракони не були живими власними силами. Адже життя було слабкістю, яке їм не властиве. Дракони були тією частиною природи, що існує поза життям і смертю.";
            }
            else if (counter == 5)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\firstLight.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Такий порядок речей зберігався до появи вогню, разом із яким прийшли Світло та Темрява. З Темряви з'явилися Вони, що знайшли в першому полум'ї душі Лордів: Перше полум'я - це вогонь, що дарує життя та світло.";
            }
            else if (counter == 6)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\gigants.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Велетні, що вийшли з нього, підкорялися драконам доти, доки чотири гіганти не знайшли в полум'ї 4 великі душі, що перетворили їх на Богів.";
            }
            else if (counter == 7)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\nito.jpg", UriKind.Relative));

                InfoTextBox.Text = "-На першу Душу заявив свої права Ніто, Перший із Мертвих. На зорі Ери Вогню він отримав одну з Великих Душ, після чого став керувати смертями всіх живих істот. У війні проти Драконів Ніто насилав міазми хвороб і смерті на своїх ворогів.";
            }
            else if (counter == 8)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\charodeika.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Інша Душа належала Відьмі Ізаліта, могутній чарівниці. Відьма та її дочки були справжніми майстрами магії вогню.";
            }
            else if (counter == 9)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\povelitelLight.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Третю Душу взяв Гвін, Король Світу. Потужність Полум'я зробила його богом і наділила неймовірними силами. У той час Гвін був відомий як Король Сонячного Світу. У битвах він використовував дива, подібні до Списа блискавки.";
            }
            else if (counter == 10)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\blackSoul.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Остання Душа була особливою. Вона називалася Темною і потрапила до рук безіменного карлика, який став прабатьком усього людства. Вона була невеликою та непомітною, як і її господар. Може здатися, що цьому слабкому карлику дісталися залишки бенкету володарів, жалюгідні крихти.";
            }
            else if (counter == 11)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\siluet.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Але саме ця Душа виявиться сильнішою за інші і стане загрозою всьому світу. Поки володарі збирали сили, щоб розпочати війну, безіменний Пігмей зміг піти непоміченим та загубитися на довгі роки. Довгий час ніхто не підозрював про роль, яку йому судилося зіграти у протистоянні світла та темряви.";
            }
            else if (counter == 12)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\pigmeygnom.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Саме від нього походять люди - він розподілив свої жалюгідні крихти між усіма неживими, причому кожна людина успадкувала частинку його душі. Уламки Темної Душі стали відомі як людяність.";
            }
            else if (counter == 13)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\army.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Але згодом володарі трьох душ почали слабшати, що полум'я подарувало їм силу, почало гаснути. Хтось мав віддати свою душу полум'ю, щоб знову стати Богами. Дізнавшись із пророцтва, що великих темних душ було чотири, всі три володаря підняли все своє військо на пошуки четвертої душі.";
            }
            else if (counter == 14)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\army2.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Гвін відправив своїх вірних лицарів на пошуки Темної Душі, вбиваючи всіх, хто міг її мати. Ніто скористався своєю владою над смертю, щоби створити армію нежиті, яка шукала четверту душу. Ізаліта, використовуючи свою магію, породжувала вогняних істот.";
            }
            else if (counter == 15)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\crystall.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Всі вони хотіли знайти і знищити того, хто має четверту душу. Після довгих років пошуку, вони нарешті дізналися, що четверта душа знаходиться у Пігмея, і вирішили спробувати її відібрати, вбивши його. Після того, як володарі відібрали у Пігмея четверту душу, вона розлетілася на уламки.";
            }
            else if (counter == 16)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\nepogashiy.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Деякі частини вдалося отримати володарям, але інші уламки породили незгаслих - людей з темними душами всередині. Вони були ув'язнені в склепах на багато мільйонів років, але після того, як володарі вкрали душу Пігмея, вони прокинулися і почали свій шлях світом.";
            }
            else if (counter == 17)
            {
                FadeImage();
                GameImage.Source = new BitmapImage(new Uri("Resources\\hero.jpg", UriKind.Relative));

                InfoTextBox.Text = "-Ці люди, що мають темну душу, стали відомі як Незгаслі.";
            }
        }

    }
}
