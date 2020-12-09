using System.Windows;
using PiratesEtEspagnols;
using PiratesEtEspagnols2;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour FenetreMagasin.xaml
    /// </summary>
    public partial class FenetreMagasin : Window
    {
        private ModeleMagasin magasin;

        public FenetreMagasin(ModelePirate pirate)
        {
            InitializeComponent();
            magasin = new ModeleMagasin(pirate);

            Closing += Window_Closing;//Changement de l'action lors de la fermeture de la fênetre.
            
        }

        /// <summary>
        /// Cache la fênetre à la place de la fermer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

            MainWindow.DemarrerHorlogePrincipal(); //Demarre le horloge principal pour continuer le jeu.
        }
    }
}
