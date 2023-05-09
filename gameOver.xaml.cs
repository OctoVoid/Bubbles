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
using System.Windows.Shapes;

namespace Bubbles
{
    /// <summary>
    /// Interaction logic for gameOver.xaml
    /// </summary>
    public partial class gameOver : Window
    {
        ImageBrush backgroundImage = new ImageBrush();


        public gameOver()
        {
            InitializeComponent();

            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/background.jpg"));
            MyCanvas.Background = backgroundImage;
            newGameButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/newgameOff.png"));
            quitButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/quitOff.png"));

            uScoredText.Content = "Your score was: " + MainWindow.score;
        }

        private void OnMouseEnterNew(object sender, MouseEventArgs e)
        {
            newGameButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/newgameOn.png"));
        }

        private void OnMouseLeaveNew(object sender, MouseEventArgs e)
        {
            newGameButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/newgameOff.png"));
        }

        private void OnClickNew(object sender, MouseButtonEventArgs e)
        {
            MainWindow newGame = new MainWindow();
            newGame.Show();

            this.Close();
        }

        private void OnMouseEnterQuit(object sender, MouseEventArgs e)
        {
            quitButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/quitOn.png"));
        }

        private void OnMouseLeaveQuit(object sender, MouseEventArgs e)
        {
            quitButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/quitOff.png"));
        }

        private void OnClickQuit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
