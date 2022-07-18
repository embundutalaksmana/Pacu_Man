using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace Pacu_Man
{
    /// <summary>
    /// Interaction logic for LoginGame.xaml
    /// </summary>
    public partial class LoginGame : Window
    {
        public string valunenama { get; set; }

        public LoginGame()
        {
            InitializeComponent();
            LoginSetUp();
            BlinkingImage(lab1, 700, 9000);

        }
        private void LoginSetUp()
        {
            MyCanvas.Focus();
        }

       private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Level_Menu Tomenu = new Level_Menu();
            Tomenu.DataContext = this;
            Tomenu.Show();
            this.Close();
        }
        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space )
            {
                Level_Menu Tomenu = new Level_Menu();
                Tomenu.DataContext = this;
                Tomenu.Show();
                this.Close();
            }

        }
        public void BlinkingImage(Image lab1, int length, double repetition)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.2,
                Duration = new Duration(TimeSpan.FromMilliseconds(length)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(repetition)
            };
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            Storyboard.SetTarget(opacityAnimation, lab1);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            storyboard.Begin(lab1);
        }
    }
}
