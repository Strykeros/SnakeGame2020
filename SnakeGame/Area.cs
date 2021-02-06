using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    class Area : PictureBox
    {
        Random randFood = new Random();
        Food food = new Food();
        Snake snake = new Snake();
        public Area()
        {
            InitializeArea();

        }

        private void InitializeArea()
        {
            this.BackColor = Color.LightSkyBlue;
            this.Height = 400;
            this.Width = 400;

        }

        public void RandomFoodLoc()
        {
            food.Left = randFood.Next(0, this.Width - food.Width);
            food.Top = randFood.Next(0, this.Height - food.Height);
        }

    }
}
