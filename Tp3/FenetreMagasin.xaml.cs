using System.Windows;
using System.Windows.Controls;
using PiratesEtEspagnols;
using PiratesEtEspagnols2;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour FenetreMagasin.xaml
    /// </summary>
    public partial class FenetreMagasin : Window
    {
        private ModeleMagasin _magasin;


        public FenetreMagasin(ModelePirate pirate)
        {
            InitializeComponent();
            _magasin = new ModeleMagasin(pirate);

            SetAffichageOrDisponible();

            Closing += Window_Closing;//Changement de l'action lors de la fermeture de la fênetre.
        }

        private void SetAffichageOrDisponible()
        {
            OrDisponible.Text = _magasin.GetOrPirate().ToString();
        }

        public void VerifierPossibiliteAchat()
        {
            if (_magasin.GetOrPirate() < int.Parse(TextBlockPrixVie.Text)) //Si l'or est plus petit que prix affiché (XAML)
            {
                BtAdquerirVie.IsEnabled = false; //Active le boutton pour acheter la vie
            }

            if (_magasin.GetOrPirate() < int.Parse(TextBlockPrixDegats.Text))
            {
                BtAdquerirDegats.IsEnabled = false;
            }

            if (_magasin.GetOrPirate() < int.Parse(TextBlockPrixCannons.Text))
            {
                BtAdquerirCannons.IsEnabled = false;
            }
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

        private void Acheter_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;


            switch (button.Name)
            {
                case "BtAdquerirVie":

                    FoisAchetesVie.Text = (int.Parse(FoisAchetesVie.Text) + 1).ToString();
                    _magasin.Acheter(ProprietesPirate.Membres, int.Parse(TextBlockPrixVie.Text));
                    break;
                case "BtAdquerirDegats":
                    FoisAchetesDegats.Text = (int.Parse(FoisAchetesDegats.Text) + 1).ToString();
                    _magasin.Acheter(ProprietesPirate.Degats, int.Parse(TextBlockPrixDegats.Text));
                    break;
                case "BtAdquerirCannons":
                    FoisAchetesCannons.Text = (int.Parse(FoisAchetesCannons.Text) + 1).ToString();
                    _magasin.Acheter(ProprietesPirate.Cannons, int.Parse(TextBlockPrixCannons.Text));
                    break;
            }

            SetAffichageOrDisponible();
            VerifierPossibiliteAchat();
        }
    }
}
