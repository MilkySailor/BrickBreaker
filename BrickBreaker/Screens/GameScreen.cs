﻿/*  Created by: 
 *  Project: Brick Breaker
 *  Date: 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        //player1 button control keys - DO NOT CHANGE
        public static Boolean leftArrowDown, rightArrowDown, spaceDown;
        bool shotReady = false;

        // Game values
        public static int lives;
        public static int saveLevel = 1;
        public static int gameLevel = 1;
        int totalLevels = 5;
        int timerDrawLocation = 0;
        int crosshairX, crosshairY;
        int gravity = 1;
        int shotgunShots = 0;
        int clickCounter = 0;

        public bool awaitingLaunch = true;
        public static bool edgeProtector = false;
        public static bool stickyPaddle = false;
        public static bool moveBall = false;
        public static int ballDamage = 1;
        public static string powerupMessage = "";
        public static bool isSaveLevelSelcted = false;
        public static bool shotgunPowerUp = false;

        // timers
        public static int paddleSizeTimer = 0;
        public static int paddleSpeedTimer = 0;
        public static int speedBallTimer = 0;
        public static int damageTimer = 0;
        public static int fireBallTimer = 0;
        public static int explosiveHitTimer = 0;
        public static int magnetTimer = 0;
        public static int messageTimer = 0;

        //pictures for blocks
        Bitmap[] ducks = {Properties.Resources.Duck01, Properties.Resources.Duck02, Properties.Resources.Duck03, Properties.Resources.Duck11
        , Properties.Resources.Duck12, Properties.Resources.Duck13, Properties.Resources.Duck21, Properties.Resources.Duck22
        , Properties.Resources.Duck23, Properties.Resources.Duck31, Properties.Resources.Duck32, Properties.Resources.Duck33
        , Properties.Resources.Duck41, Properties.Resources.Duck42, Properties.Resources.Duck43};
        int theoTracker = 0;

        Random random = new Random();
        int score;

        // Paddle and Ball objects
        public static Paddle paddle;
        public static List<Ball> balls = new List<Ball>();

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();
        List<Block> deadBlocks = new List<Block>();

        // list of power ups
        List<PowerUp> powerups = new List<PowerUp>();


        // fire ball power up
        List<Fire> flames = new List<Fire>();

        // explosive hit power up
        List<Point> explosions = new List<Point>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.Transparent);

        SolidBrush ballBrush = new SolidBrush(Color.Red);

        SolidBrush red = new SolidBrush(Color.Red);
        SolidBrush orange = new SolidBrush(Color.Orange);
        SolidBrush yellow = new SolidBrush(Color.Yellow);
        SolidBrush purple = new SolidBrush(Color.Purple);
        SolidBrush darkBlue = new SolidBrush(Color.FromArgb(0, 0, 200));
        SolidBrush brush = new SolidBrush(Color.White);
        Color colour = Color.White;

        Pen shotgunPen = new Pen(Color.Black);
        Pen moveBallPen = new Pen(Color.Lime, 3);

        Font font;

        // List of all colours for good power ups
        public static List<Color> powerupColours = new List<Color> { Color.Green, Color.Cyan, Color.Red, Color.Pink, Color.Purple, Color.Yellow, Color.Magenta, Color.Beige, Color.Orange, Color.Maroon, Color.Lime };
        Stopwatch stopwatch = new Stopwatch();


        Bitmap paddleImage = new Bitmap(Properties.Resources.Paddle);

        // Font
        Font powerupMessageFont = new Font("Forte", 50);

        Rectangle CrosshairRectangle = new Rectangle();

        #endregion

        public GameScreen()
        {
            InitializeComponent();

            crosshairX = this.Width / 2;
            crosshairY = this.Height / 2;

            font = new Font("Arial", 24, FontStyle.Bold);

            OnStart();
        }

        public void OnStart()
        {
            //set life counter
            lives = 3;


            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.Transparent);

            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = this.Height - paddle.height - 80;


            /**
             * Set the ball speed to 0 so it doesnt move upon start so it can be launched
             */
            int xSpeed = 4;
            int ySpeed = 4;
            int ballSize = 20;
            balls.Clear();
            balls.Add(new Ball(ballX, ballY, xSpeed, ySpeed, ballSize));

            #region Creates blocks for generic level. Need to replace with code that loads levels.

            //TODO - replace all the code in this region eventually with code that loads levels from xml files

            LevelBuild();

            #endregion

            // Just make sure the ball doesnt move on start
        }


        public void LevelBuild()
        {
            //balls.Clear();
            //balls.Add(new Ball(ballX, ballY, xSpeed, ySpeed, ballSize));
            int x, y, hp;
            string color, level;

            blocks.Clear();
            XmlReader reader;

            if (isSaveLevelSelcted && saveLevel <= totalLevels)
            {
                string levelFile = "Resources/UserLevel" + saveLevel + ".xml";
                reader = XmlReader.Create(levelFile);
                level = "Save Level" + saveLevel.ToString();

            }
            else
            {
                string levelFile = "Resources/GameLevel" + gameLevel + ".xml";
                reader = XmlReader.Create(levelFile);
                level = "Level" + gameLevel.ToString();
            }

            //XmlReader reader = XmlReader.Create("Resources/LevelEditorXML.xml");

            reader.ReadToFollowing("Level");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    x = Convert.ToInt32(reader.ReadString());

                    reader.ReadToNextSibling("y");
                    y = Convert.ToInt32(reader.ReadString());

                    reader.ReadToNextSibling("hp");
                    hp = Convert.ToInt32(reader.ReadString());

                    reader.ReadToNextSibling("color");
                    color = reader.ReadString();

                    Color newColor = Color.FromName(color);

                    Block newBlock = new Block(x, y, hp, newColor);
                    blocks.Add(newBlock);
                }
            }

            reader.Close();

            this.Focus();
            gameTimer.Enabled = true;
            this.Focus();


            for (int i = 1; i < balls.Count; i++)
            {
                balls.RemoveAt(i);
            }

            WritePowerupMessage(level);
            balls[0].canMove = false;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    stickyPaddle = false;
                    spaceDown = true;
                    break;
                case Keys.P:
                    if (pauseLabel.Visible == false)
                    {
                        Pause();
                    }
                    else
                    {
                        Resume();
                    }
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            CrosshairRectangle = new Rectangle(crosshairX - 10, crosshairY - 10, 20, 20);

            // Call Ball Launch
            BallLaunch();

            livesLabel.Text = $"Lives: {lives}";
            scoreLabel.Text = $"Score: {score}";

            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }

            foreach (Ball b in balls)
            {
                b.PaddleCollision(paddle);
                if (!moveBall)
                {
                    b.Move();
                }
                b.WallCollision(this);
                if (b.BottomCollision(this))
                {
                    if (b == balls[0] && !edgeProtector)
                    {
                        lives--;

                        // Moves the ball back to origin
                        b.x = ((paddle.x - (b.size / 2)) + (paddle.width / 2));
                        b.y = (this.Height - paddle.height) - 85;

                        // Prevent ball from moving to allow for launch
                        balls[0].canMove = false;

                    }
                    else if (!edgeProtector)
                    {
                        balls.Remove(b);
                        break;
                    }
                    else
                    {
                        b.ySpeed *= -1;
                        edgeProtector = false;
                    }
                }
            }

            // Check if ball has collided with any blocks
            foreach (Ball ball in balls)
            {
                foreach (Block b in blocks)
                {
                    if (ball.BlockCollision(b, ball))
                    {
                        if (explosiveHitTimer > 0)
                        {
                            explosions.Add(new Point(ball.x, ball.y));
                        }

                        b.hp -= ballDamage;

                        if (b.hp <= 0)
                        {
                            JustinCode();

                            if (random.Next(1, 2) == 1)
                            {
                                if (random.Next(1, 4) == 1)
                                {
                                    powerups.Add(new PowerUp(random.Next(PowerUp.badPowerups.Length * -1, 0), b.x, b.y));
                                }
                                else
                                {
                                    powerups.Add(new PowerUp(random.Next(1, PowerUp.goodPowerups.Length + 1), b.x, b.y));
                                }
                            }

                            deadBlocks.Add(b);
                            blocks.Remove(b);
                        }

                        break;
                    }
                }
            }


            if (blocks.Count == 0)
            {
                gameTimer.Enabled = false;
                OnEnd();
            }

            foreach (PowerUp p in powerups)
            {
                p.Move();
                if (p.type == 0)
                {
                    powerups.Remove(p);
                    break;
                }
            }

            CountTimers();

            if (lives == 0)
            {
                CooperCode();
                gameTimer.Enabled = false;
                Form form = this.FindForm();
                MenuScreen ps = new MenuScreen();

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

                form.Controls.Add(ps);
                form.Controls.Remove(this);
                //OnEnd();
            }
            TheodoropoulosCode();

            foreach (Block deadBlock in deadBlocks)
            {
                int speed = 4;

                if (deadBlock.y < this.Height)
                {
                    deadBlock.y += speed + gravity;
                    gravity += 1;
                }

                if (deadBlock.y > this.Height)
                {
                    deadBlocks.Remove(deadBlock);
                    gravity = 1;
                    //up = true;
                    break;
                }
            }

            if (shotgunPowerUp && shotReady == true)
            {
                //if (stopwatch.ElapsedMilliseconds > 1500)
                //{
                var reloadSound = new System.Windows.Media.MediaPlayer();
                reloadSound.Open(new Uri(Application.StartupPath + "/Resources/shotguCopyEdited.Wav"));
                reloadSound.Play();

                shotReady = false;
                //}
            }

            foreach (Block b in blocks)
            {
                foreach (Point p in explosions)
                {
                    if (p.X - (b.x + b.width / 2) < 160 && p.Y - (b.y + b.height / 2) < 80)
                    {
                        b.hp -= ballDamage;


                    }
                }
                if (b.hp <= 0)
                {
                    JustinCode();

                    if (random.Next(1, 9) == 1)
                    {
                        if (random.Next(1, 4) == 1)
                        {
                            powerups.Add(new PowerUp(random.Next(PowerUp.badPowerups.Length * -1, 0), b.x, b.y));
                        }
                        else
                        {
                            powerups.Add(new PowerUp(random.Next(1, PowerUp.goodPowerups.Length + 1), b.x, b.y));
                        }
                    }

                    deadBlocks.Add(b);
                    blocks.Remove(b);

                    break;
                }
            }

            explosions.Clear();

            //redraw the screen
            Refresh();
        }

        public void BallLaunch()
        {
            if (balls[0].canMove == false)
            {
                balls[0].x = paddle.x + (paddle.width / 2) - (balls[0].size / 2);
                balls[0].y = paddle.y - (paddle.height);
            }

            if (spaceDown)
            {
                balls[0].canMove = true;
            }
        }

        public void JustinCode()
        {
            score++;
        }

        private void GameScreen_MouseMove(object sender, MouseEventArgs e)
        {
            crosshairX = e.X;
            crosshairY = e.Y;
        }

        private void GameScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (shotgunPowerUp && !moveBall)
            {
                if (shotgunPowerUp && shotgunShots < 3)
                {
                    string file = Application.StartupPath + "/Resources/Shot.wav";

                    var shotSound = new System.Windows.Media.MediaPlayer();
                    shotSound.Open(new Uri(Application.StartupPath + "/Resources/shotgunShotEdited.wav"));
                    shotSound.Play();

                    stopwatch.Start();
                    shotReady = true;

                    //var reloadSound = new System.Windows.Media.MediaPlayer();
                    //reloadSound.Open(new Uri(Application.StartupPath + "/Resources/shotguCopyEdited.Wav"));
                    //reloadSound.Play();

                    for (int i = blocks.Count - 1; i >= 0; i--)
                    {
                        if (blocks[i].CrossHairCollision(blocks[i], CrosshairRectangle))
                        {
                            deadBlocks.Add(blocks[i]);
                            blocks.Remove(blocks[i]);
                            score++;
                        }
                    }

                }
                else
                {
                    shotgunPowerUp = false;
                    shotgunShots = 0;
                }

            }

            if (moveBall && Math.Abs(crosshairX - balls[0].x) < 150 && Math.Abs(crosshairY - balls[0].y) < 150)
            {
                balls[0].x = crosshairX;
                balls[0].y = crosshairY;
                moveBall = false;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            Resume();
        }

        public void Pause()
        {
            gameTimer.Enabled = false;
            pauseLabel.Visible = true;
            pauseLivesLabel.Visible = true;
            pauseScoreLabel.Visible = true;
            exitButton.Visible = true;
            resumeButton.Visible = true;

            pauseLabel.BringToFront();
            pauseLivesLabel.BringToFront();
            pauseScoreLabel.BringToFront();
            exitButton.BringToFront();
            resumeButton.BringToFront();

            pauseLivesLabel.Text = $"Lives: {lives}";
            pauseScoreLabel.Text = $"Score: {score}";
        }

        public void Resume()
        {
            gameTimer.Enabled = true;
            pauseLabel.Visible = false;
            pauseLivesLabel.Visible = false;
            pauseScoreLabel.Visible = false;
            exitButton.Visible = false;
            resumeButton.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            clickCounter++;

            if (clickCounter % 2 == 0)
            {
                Resume();
            }
            else
            {
                Pause();

            }
        }

        public void OnEnd()
        {
            CooperCode();

            if (gameLevel < totalLevels)
            {
                JustinCode2();

            }
            else if (gameLevel >= totalLevels || saveLevel >= totalLevels)
            {
                // Goes to the game over screen
                Form form = this.FindForm();
                MenuScreen ps = new MenuScreen();

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

                form.Controls.Add(ps);
                form.Controls.Remove(this);
            }
        }

        public void JustinCode2()
        {
            if (isSaveLevelSelcted)
            {
                saveLevel++;
                LevelBuild();
            }
            else
            {
                gameLevel++;
                LevelBuild();
            }
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            if (shotgunPowerUp && !moveBall)
            {
                e.Graphics.DrawEllipse(shotgunPen, crosshairX - 25, crosshairY - 25, 50, 50);
                e.Graphics.DrawLine(shotgunPen, crosshairX - 25, crosshairY, crosshairX + 25, crosshairY);
                e.Graphics.DrawLine(shotgunPen, crosshairX, crosshairY - 25, crosshairX, crosshairY + 25);

            }
            if (moveBall)
            {
                if (Math.Abs(crosshairX - balls[0].x) < 150 && Math.Abs(crosshairY - balls[0].y) < 150)
                {
                    e.Graphics.DrawLine(moveBallPen, crosshairX - 15, crosshairY - 15, crosshairX + 15, crosshairY + 15);
                    e.Graphics.DrawLine(moveBallPen, crosshairX + 15, crosshairY - 15, crosshairX - 15, crosshairY + 15);
                }
                else
                {
                    e.Graphics.DrawLine(shotgunPen, crosshairX - 15, crosshairY - 15, crosshairX + 15, crosshairY + 15);
                    e.Graphics.DrawLine(shotgunPen, crosshairX + 15, crosshairY - 15, crosshairX - 15, crosshairY + 15);
                }

                e.Graphics.DrawRectangle(moveBallPen, balls[0].x - 150, balls[0].y - 150, 300, 300);
            }

            // Draws Fire for fireball powerup
            foreach (Fire f in flames)
            {
                e.Graphics.FillEllipse(f.fireColour, f.x, f.y, f.size + random.Next(-2, 3), f.size + random.Next(-2, 3));
            }

            // Draws paddle
            e.Graphics.DrawImage(paddleImage, paddle.x, paddle.y, paddle.width, paddle.width * 1.1f);


            // Draws blocks
            foreach (Block b in blocks)
            {
                //e.Graphics.FillRectangle(new SolidBrush(b.colour), b.x, b.y, b.width, b.height);
                e.Graphics.DrawImage(ducks[(b.hp - 1) * 3 + b.frame - 1], b.x, b.y, b.width, b.width);
            }

            foreach (Block b in deadBlocks)
            {
                //e.Graphics.FillRectangle(new SolidBrush(b.colour), b.x, b.y, b.width, b.height);
                //e.Graphics.DrawImage(ducks[(b.hp - 1) * 3 + b.frame - 1], b.x, b.y, b.width, b.width);
                e.Graphics.DrawImage(ducks[1], b.x, b.y, b.width, b.width);
            }

            // Draws balls
            foreach (Ball b in balls)
            {
                if (b == balls[0])
                {
                    e.Graphics.FillEllipse(darkBlue, b.x, b.y, b.size, b.size);
                }
                else
                {
                    e.Graphics.FillEllipse(ballBrush, b.x, b.y, b.size, b.size);
                }
            }

            // Draws powerups
            foreach (PowerUp p in powerups)
            {
                e.Graphics.FillRectangle(p.powerupBrush, p.x, p.y, p.size, p.size);
            }

            if (edgeProtector)
            {
                e.Graphics.FillRectangle(purple, 0, Height - 5, Width, 5);
            }

            //brush.Color = powerupColours[8];
            //foreach (Point p in explosions)
            //{
            //    e.Graphics.FillEllipse(brush, p.X - 80, p.Y - 40, 160, 80);
            //}

            // Draws powerup message
            if (messageTimer > 0)
            {
                e.Graphics.DrawString(powerupMessage, powerupMessageFont, darkBlue, 100, this.Height / 2);
            }

            // Draw timers

            timerDrawLocation = 0;

            if (random.Next(1, 5) == 1) // Bad power up flash
            {
                if (colour == Color.FromArgb(255, 200, 0))
                {
                    colour = Color.FromArgb(255, 100, 0);
                }
                else
                {
                    colour = Color.FromArgb(255, 200, 0);
                }
            }

            if (paddle.width == 160)
            {
                DrawTimer(paddleSizeTimer, 1000, powerupColours[1], e);
            }
            else
            {
                DrawTimer(paddleSizeTimer, 1000, colour, e);
            }
            DrawTimer(paddleSpeedTimer, 750, colour, e);
            DrawTimer(speedBallTimer, 500, colour, e);
            DrawTimer(damageTimer, 750, powerupColours[6], e);
            DrawTimer(fireBallTimer, 600, powerupColours[2], e);
            DrawTimer(explosiveHitTimer, 1000, powerupColours[8], e);
            DrawTimer(magnetTimer, 2000, powerupColours[9], e);

        }

        public void DrawTimer(int timer, int max, Color colour, PaintEventArgs e)
        {
            double pie;

            if (timer > 0)
            {
                timerDrawLocation += 40;
                brush.Color = colour;
                pie = Math.Round(Convert.ToDouble(timer * 360 / max));
                e.Graphics.FillPie(brush, Width - 50, timerDrawLocation, 40, 40, -90, Convert.ToInt16(pie));
            }
        }

        public void TheodoropoulosCode()
        {
            theoTracker++;

            if (theoTracker == 15)
            {
                theoTracker = 0;
                foreach (Block b in blocks)
                {
                    if (b.frame == 1) { b.flow = 1; }
                    else if (b.frame == 3) { b.flow = -1; }
                    b.frame += b.flow;
                }
            }
        }

        public void CooperCode()
        {
            string name;
            name = SelectScreen.username;
            string HS = score.ToString();
            int intScore = Convert.ToInt32(HS);

            Scores newScore = new Scores(name, intScore);

            MenuScreen.scores.Add(newScore);

            XmlWriter writer = XmlWriter.Create("HighScoreXML.xml", null);
            writer.WriteStartElement("HighScores");

            foreach (Scores s in MenuScreen.scores)
            {
                writer.WriteElementString("Name", s.name);
                writer.WriteElementString("Score", s.score.ToString());
            }

            writer.WriteEndElement();
            writer.Close();

        }

        public static void WritePowerupMessage(string message)
        {
            messageTimer = 100;
            powerupMessage = message;
        }

        public void CountTimers()
        {
            if (speedBallTimer == 0 && balls[0].speed == 2)
            {
                foreach (Ball b in balls)
                {
                    b.speed = 1;
                }
            }
            else
            {
                speedBallTimer--;
            }

            if (fireBallTimer != 0)
            {
                fireBallTimer--;
                flames.Add(new Fire(balls[0].x, balls[0].y));
            }
            foreach (Fire f in flames)
            {
                f.Move();
            }
            foreach (Fire f in flames)
            {
                if (f.i > 275)
                {
                    flames.Remove(f);
                    break;
                }
            }

            if (paddleSizeTimer == 0)
            {
                paddle.width = 80;
            }
            else
            {
                paddleSizeTimer--;
            }

            if (paddleSpeedTimer == 0)
            {
                paddle.speed = 8;
            }
            else
            {
                paddleSpeedTimer--;
            }

            if (damageTimer == 0)
            {
                ballDamage = 1;
            }
            else
            {
                damageTimer--;
            }

            if (explosiveHitTimer > 0)
            {
                explosiveHitTimer--;
            }

            if (magnetTimer > 0)
            {
                magnetTimer--;
            }

            if (messageTimer > 0)
            {
                messageTimer--;
            }
        }
    }
}
