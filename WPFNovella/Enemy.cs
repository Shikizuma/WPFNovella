using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFNovella
{
    public class Enemy
    {
        double x;
        double y;
        Image image;
        Rect enemyRect;


        public Image GetImage
        {
            get { return image; }
        }


        public Rect GetEnemyRect
        {
            get { return enemyRect; }
            set { enemyRect = value; }
        }


        const int limit = -30;
        Random random = new Random();

        public Enemy(double x, double y, Image image)
        {
            this.x = x;
            this.y = y;
            this.image = image;
        }

        public void Move(double windowWidth, double windowHeight)
        {
         
            if (Canvas.GetTop(image) < -limit || Canvas.GetTop(image) > windowHeight - image.Height + limit)
            {
                y += random.NextDouble() - 0.5;
                y *= -1;
            }
            if (Canvas.GetLeft(image) < -limit || Canvas.GetLeft(image) > windowWidth - image.Width + limit)
            {
                x += random.NextDouble() - 0.5;
                x *= -1;
            }


            Canvas.SetLeft(image, Canvas.GetLeft(image) + x);
            Canvas.SetTop(image, Canvas.GetTop(image) + y);
        }




        public void Smash(Enemy en)
        {
            if (enemyRect.IntersectsWith(en.GetEnemyRect))
            {
                // изменяем направление движения каждого врага на противоположное
                x *= -1;
                en.x *= -1;
            }
        }


    }
}
