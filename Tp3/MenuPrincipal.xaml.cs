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

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void OnClickCommencer(object sender, RoutedEventArgs routedEventArgs)
        {
            MainWindow fenetre = new MainWindow();
            fenetre.Show();

            this.Close();
        }

        private void OnClickQuitter(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}
