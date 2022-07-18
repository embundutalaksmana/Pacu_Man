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

namespace Pacu_Man
{
    /// <summary>
    /// Interaction logic for Level_Menu.xaml
    /// </summary>
    public partial class Level_Menu : Window
    {
        
        public Level_Menu()
        {
           
            InitializeComponent();
        }
        
        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            Easy Tomenu = new Easy();
            Tomenu.Show();
            this.Close();
        }

        private void Med_Click(object sender, RoutedEventArgs e)
        {
            
            Medium Tomenu = new Medium();
            Tomenu.Show();
            this.Close();
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
         
            Hard Tomenu = new Hard();
            Tomenu.Show();
            this.Close();
        }
    }
}
