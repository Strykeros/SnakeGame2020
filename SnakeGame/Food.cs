using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    class Food : PictureBox
    {
        Random randFood = new Random();
        Snake snake = new Snake();
       public Food()
       {
            InitializeFood();
       }

        private void InitializeFood()
        {
            this.Width = 20;
            this.Height = 20;

            this.BackColor = Color.Green;

        }
    }
}
