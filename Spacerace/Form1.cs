using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Spacerace
{
    public partial class Spacerace : Form
    {
        SoundPlayer Explosion = new SoundPlayer(Properties.Resources.Explosion);
        SoundPlayer Victory = new SoundPlayer(Properties.Resources.VictorySound);

        Rectangle player1 = new Rectangle(200, 550, 40, 10);
        int player1Speed = 10;
        Rectangle player2 = new Rectangle(400, 550, 40, 10);
        int player2Speed = 10;

        List<Rectangle> balls = new List<Rectangle>();
        List<Rectangle> ballsRight = new List<Rectangle>();
        List<int> ballSpeeds = new List<int>();
        List<string> ballColours = new List<string>();
        int ballSize = 10;

        int P1Score = 0;
        int P2Score = 0;


        bool downDown = false;
        bool upDown = false;
        bool keyUp = false;
        bool keyDown = false;


        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;
        

        


        public Spacerace()
        {
             
            InitializeComponent();

            winLabel.Text = "Welcome To Space Race \n Click Play To Begin";
            playButton.Visible = true;
            playButton.Enabled = true;
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            playButton.Visible = false;
            playButton.Enabled = false;
            yesButton.Visible = false;
            yesButton.Enabled = false;
            noButton.Visible = false;
            noButton.Enabled = false;
            winLabel.Text = "";
          

            if (upDown == true && player1.Y < 550 - player1.Height)
            {
                player1.Y += player1Speed;
                
            }
            if (downDown == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
               
            }

            if (keyUp == true && player2.Y < 550 - player2.Height)
            {
                player2.Y += player2Speed;
               
            }
            if (keyDown == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
                
            }

            for (int i = 0; i < balls.Count(); i++)
            {
                //find the new postion of y based on speed 
                int x = balls[i].X + ballSpeeds[i];

                //replace the rectangle in the list with updated one using new y 
                balls[i] = new Rectangle(x, balls[i].Y, ballSize, ballSize);
            }
            for (int i = 0; i < ballsRight.Count(); i++)
            {
                //find the new postion of y based on speed 
                int x = ballsRight[i].X - ballSpeeds[i];

                //replace the rectangle in the list with updated one using new y 
                ballsRight[i] = new Rectangle(x, ballsRight[i].Y, ballSize, ballSize);
            }


            randValue = randGen.Next(0, 101);

            if (randValue < 11)
            {
                int y = randGen.Next(10, this.Height - ballSize * 6);
                balls.Add(new Rectangle(10, y, ballSize, ballSize));
                ballSpeeds.Add(randGen.Next(2, 10));

                ballColours.Add("white");
            }
            if (randValue < 11)
            {
                int y = randGen.Next(10, this.Height - ballSize * 6);
                ballsRight.Add(new Rectangle(600, y, ballSize, ballSize));
                ballSpeeds.Add(randGen.Next(2, 10));

                ballColours.Add("white");
            }

            //check if ball is below play area and remove it if it is 
            for (int i = 0; i < balls.Count(); i++)
            {


                if (balls[i].X > this.Width - ballSize)
                {
                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);

                }
            }
            for (int i = 0; i < ballsRight.Count(); i++)
            {


                if (ballsRight[i].X < 0)
                {
                    ballsRight.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);

                }
            }

            for (int i = 0; i < balls.Count(); i++)
            {
                if (player1.IntersectsWith(balls[i]))
                {
                    player1.Y = 550;
                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                    Explosion.Play();

                }
                if (player2.IntersectsWith(balls[i]))
                {
                    Explosion.Play();
                    player2.Y = 550;
                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                   

                }
            }
            for (int i = 0; i < ballsRight.Count(); i++)
            {
                if (player1.IntersectsWith(ballsRight[i]))
                {
                    player1.Y = 550;
                    ballsRight.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);

                }
                if (player2.IntersectsWith(ballsRight[i]))
                {
                    player2.Y = 550;
                    ballsRight.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);

                }
            }

            if (player1.Y == 0)
            {
                P1Score++;
                player1.Y = 550;
            }
            if (player2.Y == 0)
            {
                P2Score++;
                player2.Y = 550;
            }

            if (P1Score == 3)
            {
                Victory.Play();
                gameLoop.Stop();
                winLabel.Text = "Player 1 Wins    Play Again?";
                yesButton.Visible = true;
                yesButton.Enabled = true;
                noButton.Visible = true;
                noButton.Enabled = true;
            }
            if(P2Score == 3)
            {
                Victory.Play();
                
                gameLoop.Stop();
                winLabel.Text = "Player 2 Wins    Play Again?";
                yesButton.Visible = true;
                yesButton.Enabled = true;
                noButton.Visible = true;
                noButton.Enabled = true;
                
                
            }


            Refresh();
        }

        private void Spacerace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    upDown = true;
                    break;
                case Keys.W:
                    downDown = true;
                    break;
                case Keys.Down:
                    keyUp = true;
                    break;
                case Keys.Up:
                    keyDown = true;
                    break;
            }
        }

        private void Spacerace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    upDown = false;
                    break;
                case Keys.W:
                    downDown = false;
                    break;
                case Keys.Down:
                    keyUp = false;
                    break;
                case Keys.Up:
                    keyDown = false;
                    break;
            }
        }

        private void Spacerace_Paint(object sender, PaintEventArgs e)
        {

            P1ScoreLabel.Text = $"P1 Score: {P1Score}";
            P2ScoreLabel.Text = $"P2 Score: {P2Score}";



            //draw hero 
            e.Graphics.FillRectangle(whiteBrush, player1);
            e.Graphics.FillRectangle(whiteBrush, player2);

            //draw balls 
            for (int i = 0; i < balls.Count(); i++)
            {
                if (ballColours[i] == "white")
                {
                    e.Graphics.FillEllipse(whiteBrush, balls[i]);
                }

            }
            for (int i = 0; i < ballsRight.Count(); i++)
            {
                if (ballColours[i] == "white")
                {
                    e.Graphics.FillEllipse(whiteBrush, ballsRight[i]);
                }

            }
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
         
            player1.Y = 550;
            player2.Y = 550;
            yesButton.Visible = false;
            yesButton.Enabled = false;
            noButton.Visible = false;
            noButton.Enabled = false;
            winLabel.Text = "";
            gameLoop.Start();
            P1Score = 0;
            P2Score = 0;
            P1ScoreLabel.Text = $"P1 Score: {P1Score}";
            P2ScoreLabel.Text = $"P2 Score: {P2Score}";
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            player1.Y = 0;
            player2.Y = 0;
            P1Score--;
            P2Score--;
            gameLoop.Start();
           
        }
    }

}



