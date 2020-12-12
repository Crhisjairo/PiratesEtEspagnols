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
        /// <summary>
        /// Initialise le jeu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void OnClickCommencer(object sender, RoutedEventArgs routedEventArgs)
        {
            FenetreCommentJouer fenetreCommentJouer = new FenetreCommentJouer();
            fenetreCommentJouer.Show();

            this.Hide();
        }

        /// <summary>
        /// Ferme la fenetre du jeu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void OnClickQuitter(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}
