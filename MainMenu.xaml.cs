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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        ImageBrush mainMenuBackground = new ImageBrush();

        public MainMenu()
        {
            InitializeComponent();

            playButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/playOff.png"));
            mainMenuBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Files/mainMenu.png"));
            MyCanvas.Background = mainMenuBackground;
        }

        private void OnClickPlay(object sender, MouseButtonEventArgs e)
        {
            MainWindow newGame = new MainWindow();
            newGame.Show();

            this.Close();
        }

        private void OnMouseEnterPlay(object sender, MouseEventArgs e)
        {
            playButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/playOn.png"));
        }

        private void OnMouseLeavePlay(object sender, MouseEventArgs e)
        {
            playButton.Source = new BitmapImage(new Uri("pack://application:,,,/Files/playOff.png"));
        }
    }
}
