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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Pacu_Man
{
    public partial class Medium : Window
    {
        private int time = 100;
        private DispatcherTimer Timer;

        public string valunenama { get; set; }
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goDown, goUp;
        bool noLeft, noRight, noDown, noUp;

        int speed = 4;
        int winstate = 0;

        int health = 1;

        Rect pacmanHitBox;

        int ghostSpeed = 5;
        int ghostMoveStep = 170;
        int currentGhostStep;
        int score = 0;



        public Medium()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            GameSetUp();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                if (time <= 10)
                {
                    if (time % 2 == 0)
                    {
                        TBCountDown.Foreground = Brushes.Red;
                    }
                    else
                    {
                        TBCountDown.Foreground = Brushes.Red;
                    }
                    time--;
                    TBCountDown.Text = string.Format("{1}", time / 100, time % 100);
                }
                else
                {
                    time--;
                    TBCountDown.Text = string.Format("{1}", time / 100, time % 100);
                }
            }
            else
            {
                Timer.Stop();
                GameOver("LOL HABIS WAKTU AHAHAHHAHA");
            }


        }

        private void coinwin_click(object sender, RoutedEventArgs e)
        {
            score++;
        }

        private void pause_click(object sender, RoutedEventArgs e)
        {

            pause.Visibility = Visibility.Collapsed;
            play.Visibility = Visibility.Visible;
            Timer.Stop();
            gameTimer.Stop();
            ImageBrush paused = new ImageBrush();
            paused.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/PAUSED.png"));
            pausenotice.Fill = paused;

        }
        private void unpause_click(object sender, RoutedEventArgs e)
        {
            play.Visibility = Visibility.Collapsed;
            pause.Visibility = Visibility.Visible;
            Timer.Start();
            gameTimer.Start();
            ImageBrush paused = new ImageBrush();
            paused.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/UNPAUSED.png"));
            pausenotice.Fill = paused;
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left && noLeft == false)
            {

                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;

                goLeft = true;

                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Right && noRight == false)
            {

                noLeft = noUp = noDown = false;
                goLeft = goUp = goDown = false;

                goRight = true;

                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);

            }

            if (e.Key == Key.Up && noUp == false)
            {
                noRight = noDown = noLeft = false;
                goRight = goDown = goLeft = false;

                goUp = true;

                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Down && noDown == false)
            {
                noUp = noLeft = noRight = false;
                goUp = goLeft = goRight = false;

                goDown = true;

                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
        }

        private void GameSetUp()
        {

            MyCanvas.Focus();

            gameTimer.Tick += new EventHandler(GameLoop); ;
            gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;
            NEXT.Visibility = Visibility.Collapsed;
            BACK.Visibility = Visibility.Collapsed;
            RETRY.Visibility = Visibility.Collapsed;
            play.Visibility = Visibility.Collapsed;

            txtScoreend.Visibility = Visibility.Hidden;
            txtScoreref.Visibility = Visibility.Hidden;

            ImageBrush iconImage = new ImageBrush();
            iconImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.gif"));
            icon.Fill = iconImage;

            ImageBrush pauseImage = new ImageBrush();
            pauseImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pause.png"));
            pause.Background = pauseImage;

            ImageBrush playImage = new ImageBrush();
            playImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/play.png"));
            play.Background = playImage;

            ImageBrush pacman1Image = new ImageBrush();
            pacman1Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacopen.png"));
            pacman.Fill = pacman1Image;

            //ImageBrush canvasImage = new ImageBrush();
            //canvasImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bg-01.png"));
            //MyCanvas.Background = canvasImage;

            ImageBrush redGhost = new ImageBrush();
            redGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.png"));
            redGuy.Fill = redGhost;

            ImageBrush orangeGhost = new ImageBrush();
            orangeGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/yellow.png"));
            orangeGuy.Fill = orangeGhost;

            ImageBrush pinkGhost = new ImageBrush();
            pinkGhost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/green.png"));
            pinkGuy.Fill = pinkGhost;



            foreach (var x in MyCanvas.Children.OfType<Ellipse>())

            {
                if ((string)x.Tag == "coin")
                {
                    ImageBrush coins = new ImageBrush();
                    coins.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/coin.png"));
                    x.Fill = coins;
                }
            }
        }
        private void GameLoop(object sender, EventArgs e)
        {

            txtScore.Content = "Score: " + score;


            if (goRight)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (goLeft)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (goUp)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (goDown)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
            }


            if (goDown && Canvas.GetTop(pacman) + 50 > Application.Current.MainWindow.Height)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && Canvas.GetTop(pacman) < 1)
            {

                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(pacman) - 10 < 1)
            {

                noLeft = true;
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
            {

                noRight = true;
                goRight = false;
            }

            pacmanHitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height); // asssign the pac man hit box to the pac man rectangle

            foreach (var x in MyCanvas.Children.OfType<Ellipse>())

            {


                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


                if ((string)x.Tag == "coin")
                {

                    if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                    {

                        x.Visibility = Visibility.Hidden;

                        score++;
                    }
                }

                if ((string)x.Tag == "ghost")
                {

                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        if (health == 0)
                        {
                            Timer.Stop();
                            winstate = 0;
                            GameOver("" + score);
                        }
                        else
                        {
                            health--;
                            pacman.Visibility = Visibility.Hidden;
                            x.Visibility = Visibility.Hidden;
                            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                            x.Visibility = Visibility.Visible;
                            pacman.Visibility = Visibility.Visible;
                            icon.Visibility = Visibility.Hidden;
                        }
                    }


                    if (x.Name.ToString() == "orangeGuy")
                    {

                        Canvas.SetLeft(x, Canvas.GetLeft(x) - ghostSpeed);

                    }
                    else
                    {

                        Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed);
                    }


                    currentGhostStep--;

                    if (currentGhostStep < 1)
                    {

                        currentGhostStep = ghostMoveStep;

                        ghostSpeed = -ghostSpeed;
                    }
                }
            }
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())

            {


                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);


                if ((string)x.Tag == "wall")
                {

                    if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 4);
                        noLeft = true;
                        goLeft = false;
                    }

                    if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 4);
                        noRight = true;
                        goRight = false;
                    }

                    if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 4);
                        noDown = true;
                        goDown = false;
                    }

                    if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 4);
                        noUp = true;
                        goUp = false;
                    }
                }
            }

            if (score == 85)
            {
                Timer.Stop();
                winstate = 1;
                GameOver("You Win, you collected all of the coins");

            }
        }

        private void GameOver(string message)
        {
            if (winstate == 1)
            {
                ImageBrush iconImage = new ImageBrush();
                iconImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/UNPAUSED.png"));
                icon.Fill = iconImage;
                pause.Visibility = Visibility.Collapsed;
                play.Visibility = Visibility.Collapsed;
                Timer.Stop();
                gameTimer.Stop();
                ImageBrush paused = new ImageBrush();
                paused.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/WONV2.png"));
                NEXT.Visibility = Visibility.Visible;
                BACK.Visibility = Visibility.Visible;
                txtScoreend.Visibility = Visibility.Visible;
                txtScoreref.Visibility = Visibility.Visible;
                pausenotice.Fill = paused;

                ImageBrush next = new ImageBrush();
                next.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/UP1.png"));
                NEXT.Background = next;

                ImageBrush back = new ImageBrush();
                back.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/DROP1.png"));
                BACK.Background = back;
                txtScoreend.Content = +score;

            }
            else
            {
                ImageBrush iconImage = new ImageBrush();
                iconImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/UNPAUSED.png"));
                icon.Fill = iconImage;
                pause.Visibility = Visibility.Collapsed;
                play.Visibility = Visibility.Collapsed;

                Timer.Stop();
                gameTimer.Stop();
                ImageBrush paused = new ImageBrush();
                paused.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/LOST.png"));
                BACK.Visibility = Visibility.Visible;
                RETRY.Visibility = Visibility.Visible;
                txtScoreend.Visibility = Visibility.Visible;
                txtScoreref.Visibility = Visibility.Visible;
                pausenotice.Fill = paused;

                ImageBrush next = new ImageBrush();
                next.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/UP1.png"));
                NEXT.Background = next;

                ImageBrush back = new ImageBrush();
                back.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/DROP1.png"));
                BACK.Background = back;

                ImageBrush retry = new ImageBrush();
                retry.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/retry.png"));
                RETRY.Background = retry;

                txtScoreend.Content = +score;
            }

        }
        private void next_click(object sender, RoutedEventArgs e)
        {

            Medium Tomenu = new Medium();
            Tomenu.Show();
            this.Close();



        }
        private void back_click(object sender, RoutedEventArgs e)
        {

            Level_Menu Tomenu = new Level_Menu();
            Tomenu.Show();
            this.Close();

        }
        private void retry_click(object sender, RoutedEventArgs e)
        {

            Easy Tomenu = new Easy();
            Tomenu.Show();
            this.Close();
        }

    }
}