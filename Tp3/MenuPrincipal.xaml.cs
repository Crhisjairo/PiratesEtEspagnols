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
            FenetreCommentJouer fenetreCommentJouer = new FenetreCommentJouer();
            fenetreCommentJouer.Show();

            this.Hide();
        }

        private void OnClickQuitter(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}
