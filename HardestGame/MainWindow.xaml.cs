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
namespace HardestGame
{
    
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

        bool goLeft, goRight, goUp, goDown;

        int playerSpeed = 3; 
        int obstacle1Speed = 10;
        int obstacle2Speed = 15;
        int obstacle3Speed = 20;
        int obstacle4Speed = 20;
        int obstacle5Speed = 1;
        int obstacle6Speed = 1;
        int obstacle7Speed = 1;
        int obstacle8Speed = 1;
        int coinCount = 1;
        int deathCount;
        int levelCount = 1;
        Rect playerHitBox;



        public MainWindow()
        {
            InitializeComponent();

            GameStart();
        }

        private void GameStart()
        {
            Level1Canvas.Focus();

            gameTimer.Tick += GameWorks;
            gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            gameTimer.Start();


            ImageBrush coinImage = new ImageBrush(); //coinimage
            coinImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/coin.png")); 
            coin.Fill = coinImage;
            coin2.Fill = coinImage;
            coin3.Fill = coinImage;
            coin4.Fill = coinImage;
            coin5.Fill = coinImage;
            coin6.Fill = coinImage;
            coin7.Fill = coinImage;
            coin8.Fill = coinImage;

            ImageBrush obstacleImage = new ImageBrush(); //obstacleimage
            obstacleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obstacle.png"));
            obstacle1.Fill = obstacleImage;
            obstacle2.Fill = obstacleImage;
            obstacle3.Fill = obstacleImage;
            obstacle4.Fill = obstacleImage;

        }

        private void GameWorks(object sender, EventArgs e)
        {

            leftCoin.Content = "LeftCoin : " + coinCount;
            if (coinCount == 0)
            {
                leftCoin.Content = "go to nextround!";
            }

            death.Content = "Death : " + deathCount;
            level.Content = "Level : " + levelCount;
            
            if (goLeft)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed); //canvas에서 playerspeed 값을 뺌
            }
            if (goRight)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
            if (goUp)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
            }
            if (goDown)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            }

            if (goLeft && Canvas.GetLeft(player) < 1)
            {
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(player) + 50 > Application.Current.MainWindow.Width)
            {
                goRight = false;
            }
            if (goDown && Canvas.GetTop(player) + 70 > Application.Current.MainWindow.Height)
            {
                goDown = false;
            }
            if (goUp && Canvas.GetTop(player) < 85)
            {
                goUp = false;
            }


            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            foreach (var x in Level1Canvas.Children.OfType<Rectangle>())
            {
                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                if ((string)x.Tag == "coin") 
                {
                    if (playerHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible) //coin 먹었을때에 coin 투명도 없애고 leftcoin --
                    {
                        x.Visibility = Visibility.Hidden;
                        coinCount--;
                    }
                }

                if (coinCount == 0 && (string)x.Tag == "greenplace" && x.Visibility == Visibility.Visible) //level1 통과시 
                {
                    if (playerHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(player, 330); 
                        Canvas.SetLeft(player, 9);
                        coinCount = 3;
                        levelCount++;
                        obstacle1.Visibility = Visibility.Hidden;
                        obstacle2.Visibility = Visibility.Hidden;
                        greenplace.Visibility = Visibility.Hidden;

                        greenplace2.Visibility = Visibility.Visible;
                        coin2.Visibility = Visibility.Visible;
                        coin3.Visibility = Visibility.Visible;
                        coin4.Visibility = Visibility.Visible;

                        wall.Visibility = Visibility.Visible;
                        wall_Copy.Visibility = Visibility.Visible;
                        wall_Copy1.Visibility = Visibility.Visible;

                        obstacle3.Visibility = Visibility.Visible;
                        obstacle4.Visibility = Visibility.Visible;
                    }

                }

                if (coinCount == 0 && (string)x.Tag == "greenplace2" && x.Visibility == Visibility.Visible) //level2 통과시
                {
                    if (playerHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(player, 580);
                        Canvas.SetLeft(player, 35);
                        coinCount = 4;
                        levelCount++;

                        greenplace2.Visibility = Visibility.Hidden;

                        wall.Visibility = Visibility.Hidden;
                        wall_Copy.Visibility = Visibility.Hidden;
                        wall_Copy1.Visibility = Visibility.Hidden;

                        obstacle3.Visibility = Visibility.Hidden;
                        obstacle4.Visibility = Visibility.Hidden;
                        greenplace3.Visibility = Visibility.Visible;

                        startplace.Visibility = Visibility.Hidden;
                        startplace2.Visibility = Visibility.Visible;

                        coin5.Visibility = Visibility.Visible;
                        coin6.Visibility = Visibility.Visible;
                        coin7.Visibility = Visibility.Visible;
                        coin8.Visibility = Visibility.Visible;

                        obstacle5.Visibility = Visibility.Visible;
                        obstacle6.Visibility = Visibility.Visible;
                        obstacle7.Visibility = Visibility.Visible;
                        obstacle8.Visibility = Visibility.Visible;
                    }
                }

                if (coinCount == 0 && (string)x.Tag == "greenplace3" && x.Visibility == Visibility.Visible) //level 3통과시
                {
                    if (playerHitBox.IntersectsWith(hitBox))
                    {
                        gameTimer.Stop();
                        MessageBox.Show("Total Death : " + deathCount, "Game Over");
                        leftCoin.Content = "Game Over";
                    }
                        
                }

                    if ((string)x.Tag == "obstacle") //세로로 이동하는 obstacle
                {
                    if (playerHitBox.IntersectsWith(hitBox))
                    {
                        if (x.Visibility == Visibility.Visible)
                        {
                            Canvas.SetTop(player, 330);
                            Canvas.SetLeft(player, 9);
                            deathCount++;
                            
                            if (levelCount == 1)
                            {
                                coinCount = 1;
                                coin.Visibility = Visibility.Visible;
                            }
                            if (levelCount == 2)
                            {
                                coinCount = 3;
                                coin2.Visibility = Visibility.Visible;
                                coin3.Visibility = Visibility.Visible;
                                coin4.Visibility = Visibility.Visible;
                            }
                        }                    
                    }
                    if (x.Name.ToString() == "obstacle1")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) - obstacle1Speed);
                        if (Canvas.GetTop(x) <90 || Canvas.GetTop(x) >630)
                        {
                            obstacle1Speed = -obstacle1Speed;
                        }
                    }
                    if (x.Name.ToString() == "obstacle2")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + obstacle2Speed);
                        if (Canvas.GetTop(x) < 90 || Canvas.GetTop(x) > 630)
                        {
                            obstacle2Speed = -obstacle2Speed;
                        }
                    }
                }

                if ((string)x.Tag == "obstacle2") //가로로 이동하는 obstacle2
                { 
                    if(playerHitBox.IntersectsWith(hitBox))
                    {
                        if (x.Visibility == Visibility.Visible)
                        {
                            Canvas.SetTop(player, 330);
                            Canvas.SetLeft(player, 9);
                            deathCount++;
                            if (levelCount == 1)
                            {
                                coinCount = 1;
                                coin.Visibility = Visibility.Visible;
                            }
                            if (levelCount == 2)
                            {
                                coinCount = 3;
                                coin2.Visibility = Visibility.Visible;
                                coin3.Visibility = Visibility.Visible;
                                coin4.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    if (x.Name.ToString() == "obstacle3")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + obstacle3Speed);
                        if (Canvas.GetLeft(x) < 1 || Canvas.GetLeft(x) > 650)
                        {
                            obstacle3Speed = -obstacle3Speed;
                        }
                    }
                    if (x.Name.ToString() == "obstacle4")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + obstacle4Speed);
                        if (Canvas.GetLeft(x) < 1 || Canvas.GetLeft(x) > 650)
                        {
                            obstacle4Speed = -obstacle4Speed;
                        }
                    }

                }

                if ((string)x.Tag == "obstacle3")
                {
                    if (playerHitBox.IntersectsWith(hitBox))
                    {
                        if (x.Visibility == Visibility.Visible)
                        {
                            Canvas.SetTop(player, 580);
                            Canvas.SetLeft(player, 35);
                            deathCount++;
                            if (levelCount == 1)
                            {
                                coinCount = 1;
                                coin.Visibility = Visibility.Visible;
                            }
                            if (levelCount == 2)
                            {
                                coinCount = 3;
                                coin2.Visibility = Visibility.Visible;
                                coin3.Visibility = Visibility.Visible;
                                coin4.Visibility = Visibility.Visible;
                            }
                            if (levelCount == 3)
                            {
                                coinCount = 4;
                                coin5.Visibility = Visibility.Visible;
                                coin6.Visibility = Visibility.Visible;
                                coin7.Visibility = Visibility.Visible;
                                coin8.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    if (x.Name.ToString() == "obstacle5")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + obstacle5Speed);
                        if (Canvas.GetTop(x) < 84 || Canvas.GetTop(x) > 204)
                        {
                            obstacle5Speed = -obstacle5Speed;
                        }
                    }
                    if (x.Name.ToString() == "obstacle6")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + obstacle6Speed);
                        if (Canvas.GetTop(x) < 250 || Canvas.GetTop(x) > 370)
                        {
                            obstacle6Speed = -obstacle6Speed;
                        }
                    }
                    if (x.Name.ToString() == "obstacle7")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + obstacle7Speed);
                        if (Canvas.GetTop(x) < 332 || Canvas.GetTop(x) > 452)
                        {
                            obstacle7Speed = -obstacle7Speed;
                        }
                    }
                    if (x.Name.ToString() == "obstacle8")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + obstacle8Speed);
                        if (Canvas.GetTop(x) < 497 || Canvas.GetTop(x) > 617)
                        {
                            obstacle8Speed = -obstacle8Speed;
                        }
                    }
                }
            }                    
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left )
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            if (e.Key == Key.Up)
            {
                goUp = true;
            }
            if (e.Key == Key.Down)
            {
                goDown = true;
            }

        }

        private void CanvasKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            if (e.Key == Key.Up)
            {
                goUp = false;
            }
            if (e.Key == Key.Down)
            {
                goDown = false;
            }
        }

        

       
        
    }
}
