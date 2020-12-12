using System.Windows;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour FenetreCommentJouer.xaml
    /// </summary>
    public partial class FenetreCommentJouer : Window
    {
        public FenetreCommentJouer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bouton pour commencer le jeu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonJouer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow fenetre = new MainWindow();
            fenetre.Show();

            this.Hide();
        }
    }
}
