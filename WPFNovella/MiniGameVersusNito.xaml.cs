using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFNovella
{
    /// <summary>
    /// Interaction logic for MiniGameVersusNito.xaml
    /// </summary>
    /// 
    public partial class MiniGameVersusNito : Window
    {
        public bool isHardMode;
        private DispatcherTimer timer;

        private double playerX, playerY;
        private double playerSpeed = 350;
        Rect playerRect;

        Enemy[] enemies = new Enemy[4];

        public MiniGameVersusNito()
        {
            InitializeComponent();

            playerX = Canvas.GetLeft(player);
            playerY = Canvas.GetTop(player);

            int speedX = 10;
            int speedY = 10;

            enemies[0] = new Enemy(speedX, speedY, enemy);
            enemies[1] = new Enemy(speedX, speedY, enemy1);
            enemies[2] = new Enemy(speedX, speedY, enemy2);
            enemies[3] = new Enemy(speedX, speedY, enemy3);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16); // 60 FPS
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        void PlayerMove()
        {
            double dx = 0, dy = 0;
            if (IsKeyDown(Key.Left) || IsKeyDown(Key.A))
                dx -= playerSpeed;
            if (IsKeyDown(Key.Right) || IsKeyDown(Key.D))
                dx += playerSpeed;
            if (IsKeyDown(Key.Up) || IsKeyDown(Key.W))
                dy -= playerSpeed;
            if (IsKeyDown(Key.Down) || IsKeyDown(Key.S))
                dy += playerSpeed;

            double deltaTime = timer.Interval.TotalSeconds;
            playerX += dx * deltaTime;
            playerY += dy * deltaTime;

            // ограничиваем координаты персонажа, чтобы он не выходил за пределы канваса
            double maxX = canvas.ActualWidth - player.ActualWidth;
            double maxY = canvas.ActualHeight - player.ActualHeight;
            playerX = Math.Max(0, Math.Min(playerX, maxX));
            playerY = Math.Max(0, Math.Min(playerY, maxY));

        
            Canvas.SetLeft(player, playerX);
            Canvas.SetTop(player, playerY);
        }

        DateTime startTime;
        private void Timer_Tick(object sender, EventArgs e)
        {    
            TimeSpan elapsed = DateTime.Now - startTime; 
            TimerLabel.Content = elapsed.ToString(@"mm\:ss");


  
            if (elapsed >= TimeSpan.FromSeconds(isHardMode ? 60 : 30))
            {
             
                timer.Stop();
      
                EndGame();
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Move(ActualWidth, ActualHeight);

                for (int j = 0; j < enemies.Length; j++)
                {
                    if (i != j) 
                    {
                        enemies[i].Smash(enemies[j]);
                    }
                }
            }

            PlayerMove();

            enemies[0].GetEnemyRect = new Rect(Canvas.GetLeft(enemies[0].GetImage), Canvas.GetTop(enemies[0].GetImage), 30, 30);
            enemies[1].GetEnemyRect = new Rect(Canvas.GetLeft(enemies[1].GetImage), Canvas.GetTop(enemies[1].GetImage), 30, 30);
            enemies[2].GetEnemyRect = new Rect(Canvas.GetLeft(enemies[2].GetImage), Canvas.GetTop(enemies[2].GetImage), 30, 30);
            enemies[3].GetEnemyRect = new Rect(Canvas.GetLeft(enemies[3].GetImage), Canvas.GetTop(enemies[3].GetImage), 30, 30);

            playerRect = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), 50, 50);

            if (enemies[0].GetEnemyRect.IntersectsWith(playerRect) || enemies[1].GetEnemyRect.IntersectsWith(playerRect)
                || enemies[2].GetEnemyRect.IntersectsWith(playerRect) || enemies[3].GetEnemyRect.IntersectsWith(playerRect))
            {
                
                timer.Stop();
                LooseGame();
            }
        }


        async void LooseGame()
        {
            BorderStart.Visibility = Visibility.Visible;
            InfoTextBlock.FontSize = 80;
            InfoTextBlock.Padding = new Thickness(10, 60, 10, 20);
            InfoTextBlock.Text = $"YOU'RE DEAD";
            await Task.Delay(5000);
            DialogResult = false;
        }
        
        private void EndGame()
        {
            timer.Stop();
            DialogResult = true;
        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {
            BorderStart.Visibility = Visibility.Visible;
            string time = (isHardMode ? 60 : 30).ToString() + " секунд";
            InfoTextBlock.Text = $"Приготуйтися!\nВам потрібно вижити протягом {time} від Ніто, ухиляйтесь від його черепів!\n\n*Нажміть на пробіл, щоб почати гру*";
            timer.Stop();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                BorderStart.Visibility = Visibility.Hidden;
                startTime = DateTime.Now;
                timer.Start();
            }
        }

        private bool IsKeyDown(Key key)
        {
            return Keyboard.IsKeyDown(key);
        }

    }
}
