using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Game : Form
    {
        Area area = new Area();
        Snake snake = new Snake();
        Timer mainTimer = new Timer();
        Timer foodTimer = new Timer();
        Random rand = new Random();
        Food food = new Food();
        int score = 0;
        public Game()
        {
            InitializeComponent();
            InitializeGame();
            InitializeMainTimer();
            InitializeFoodTimer();
        }

        private void InitializeGame()
        {
            this.Height = 600;
            this.Width = 600;
            this.Controls.Add(area);
            area.Top = 100;
            area.Left = 100;

            // adding snake body on a form

            snake.Render(this);
            this.KeyDown += new KeyEventHandler(Game_Keydown);


            // adding food

            this.Controls.Add(food);
            food.BringToFront();
            SetFoodLocation();
        }

        private void RandomizeFoodLocation()
        {
            food.Top = 100 + rand.Next(0, 20) * 20;
            food.Left = 100 + rand.Next(0, 20) * 20;
        }

        private void SetFoodLocation()
        {
            bool touch;
            do
            {
                RandomizeFoodLocation();
                touch = false;
                foreach (var sp in snake.snakePixels)
                {
                    if (sp.Location == food.Location)
                    {
                        touch = true;
                        break;
                    }

                }
            }
            while (touch);
        }

        private void SnakeFoodCollision()
        {
            if (snake.snakePixels[0].Bounds.IntersectsWith(food.Bounds))
            {
                //increase score
                score += 10;
                //regenerate food
                SetFoodLocation();
                //add new pixel to the snake
                int left = snake.snakePixels[snake.snakePixels.Count - 1].Left;
                int top = snake.snakePixels[snake.snakePixels.Count - 1].Top;
                snake.AddPixel(left, top);
                snake.Render(this);
                if(mainTimer.Interval >= 20)
                {
                    mainTimer.Interval -= 20;
                }
            }
        }

        private void SnakeBorderCollision()
        {
            if (!snake.snakePixels[0].Bounds.IntersectsWith(area.Bounds))
            {
                GameOver();
            }
        }

        private void SnakeSelfCollision()
        {
            for(int i = 1; i < snake.snakePixels.Count; i++)
            {
                if (snake.snakePixels[0].Bounds.IntersectsWith(snake.snakePixels[i].Bounds))
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            mainTimer.Stop();
            MessageBox.Show("Game over! Your score: " + score);

        }

        private void InitializeMainTimer()
        {
            mainTimer.Interval = 500;
            mainTimer.Tick += new EventHandler(MainTimer_Tick);
            mainTimer.Start();
        }

        private void InitializeFoodTimer()
        {
            foodTimer.Interval = 100;
            foodTimer.Tick += new EventHandler(FoodTimer_Tick);
            foodTimer.Start();
        }


        private void MainTimer_Tick(object sender, EventArgs e)
        {
            snake.Move();
            SnakeFoodCollision();
            SnakeBorderCollision();
            SnakeSelfCollision();
        }

        private void FoodTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Game_Keydown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    if (snake.HorizontalVelocity != -1)
                    {
                        snake.HorizontalVelocity = 1;
                    }
                    snake.VerticalVelocity = 0;
                    break;
                case Keys.A:
                    if (snake.HorizontalVelocity != 1)
                    {
                        snake.HorizontalVelocity = -1;
                    }
                    snake.VerticalVelocity = 0;
                    break;
                case Keys.S:
                    snake.HorizontalVelocity = 0;
                    if (snake.VerticalVelocity != -1)
                    {
                        snake.VerticalVelocity = 1;
                    }
                    break;
                case Keys.W:
                    snake.HorizontalVelocity = 0;
                    if (snake.VerticalVelocity != 1)
                    {
                        snake.VerticalVelocity = -1;
                    }
                    break;
            }


        }

    }
}
