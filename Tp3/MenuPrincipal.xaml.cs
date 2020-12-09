using System.Windows;

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
