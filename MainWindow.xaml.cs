using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
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

namespace Bubbles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

        int speed;
        int intervals = 90;
        Random rnd = new Random();

        List<Rectangle> itemRemover = new List<Rectangle>();
        ImageBrush backgroundImage = new ImageBrush();

        int bubbleType;
        int i;
        int missedBubbles;
        bool gameIsOn;
        public static int score;

        MediaPlayer player = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/background.jpg"));
            MyCanvas.Background = backgroundImage;

            RestartGame();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            scoreText.Content = "Score: " + score;
            missedBubblesText.Content = "Missed bubbles: " + missedBubbles;
            intervals -= 10;

            if (intervals < 1)
            {
                ImageBrush bubbleImage = new ImageBrush();

                bubbleType += 1;

                if (bubbleType > 4)
                {
                    bubbleType = 1;
                }

                switch (bubbleType) //changing the bubble image
                {
                    case 1:
                        bubbleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/bubble1.png"));
                        break;
                    case 2:
                        bubbleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/bubble2.png"));
                        break;
                    case 3:
                        bubbleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/bubble3.png"));
                        break;
                    case 4:
                        bubbleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/bubble4.png"));
                        break;
                }

                Rectangle newBubble = new Rectangle     // bubble features
                {
                    Tag = "bubble",
                    Height = 70,
                    Width = 70,
                    Fill = bubbleImage
                };

                Canvas.SetLeft(newBubble, rnd.Next(70, 330));    //spawning bubbles
                Canvas.SetTop(newBubble, 600);

                MyCanvas.Children.Add(newBubble);
                intervals = rnd.Next(60, 150);
            }

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "bubble")      // bubble shivering
                {
                    i = rnd.Next(-2, 3);

                    Canvas.SetTop(x, Canvas.GetTop(x) - speed);
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - (i * -1));
                }

                if (Canvas.GetTop(x) < 5)       // bubble despawn on top
                {
                    itemRemover.Add(x);
                    missedBubbles += 1;
                }
            }

            foreach (Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y);
            }

            if (missedBubbles > 10)     // game over
            {
                gameIsOn = false;
                gameTimer.Stop();

                gameOver gameOver = new gameOver();
                gameOver.Show();

                this.Close();
            }
        }

        private void PopBubble(object sender, MouseButtonEventArgs e)
        {
            if (gameIsOn)
            {
                if (e.OriginalSource is Rectangle)
                {
                    Rectangle activeRec = (Rectangle)e.OriginalSource;

                    var path = Directory.GetCurrentDirectory();

                    player.Open(new Uri(path + "/../../../Files/pop.mp3", UriKind.RelativeOrAbsolute));  // popping sound
                    player.Play();

                    MyCanvas.Children.Remove(activeRec);
                    score += 1;
                }
            }

            if (score % 10 == 0)    // speeding bubbles
            {
                speed += 1;
            }
        }

        private void StartGame()     //after click on PLAY
        {
            MyCanvas.Background = backgroundImage;
            gameTimer.Start();

            missedBubbles = 0;
            score = 0;
            intervals = 90;
            gameIsOn = true;
            speed = 5;
        }

        private void RestartGame()  //after click on NEW GAME
        {
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                itemRemover.Add(x);
            }

            foreach (Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y);
            }

            itemRemover.Clear();

            StartGame();
        }
    }
}
