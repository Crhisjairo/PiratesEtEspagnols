using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Jeu _jeu = new Jeu();

        private static FenetreMagasin fenetreMagasin = new FenetreMagasin(_jeu.GetPirate());

        private List<IVueNavire> ListeNavire { get; set; } = new List<IVueNavire>();
        private List<int> ListAttaques { get; set; } = new List<int>();
        
        private static DispatcherTimer _horloge = new DispatcherTimer();

        public MainWindow()
        {
            //creer et placer les UserControl
            InitializeComponent();

            //**À CHANGER** Faut initialiser dans _jeu. _jeu va tout donner//
            CreerNavire(350, 980, "pirate");
            CreerNavire(350,10, "galion");
            CreerNavire(200, 150, "escorte");
            CreerNavire(500, 150, "escorte");

            CreerHorlogeMouvement();
        }

        private void CreerHorlogeMouvement()
        {
            _horloge.Interval = TimeSpan.FromMilliseconds(300);
            _horloge.IsEnabled = true;
            //Méthodes à executer à chaque tick
            _horloge.Tick += HorlogeAvance;

            DemarrerHorlogePrincipal();
        }


        /// <summary>
        /// Permet d'arreter l'horloge principal.
        /// </summary>
        public static void ArreterHorlogePrincipal()
        {
            _horloge.Stop();
        }

        /// <summary>
        /// Permet de démarrer l'horloge principal.
        /// </summary>
        public static void DemarrerHorlogePrincipal()
        {
            _horloge.Start();
        }

        private void SetNiveau()
        {

        }

        /// <summary>
        /// Methode pour incluire un navire dans le Jeu.
        /// </summary>
        /// <param name="left">l'emplacement "x" du navire</param>
        /// <param name="top">l'emplacement "y"du navire</param>
        /// <param name="type">si le navire est un Galion, un pirate ou un escorte</param>
        private void CreerNavire(int left, int top, string type)
        {
            IVueNavire navire = null;
            if(type == "pirate")
            {
                navire = new VuePirate();
                _jeu.AddListeNavires(1);
            } else if (type == "galion")
            {
                navire = new VueGalion();
                _jeu.AddListeNavires(2);
            } else if (type == "escorte")
            {
                navire = new VueEscorte();
                _jeu.AddListeNavires(3);
            }

            Surface.Children.Add((UIElement)navire);
            Canvas.SetLeft((UIElement)navire, left);
            Canvas.SetTop((UIElement)navire, top);
            ListeNavire.Add(navire);
            ListAttaques.Add(0);
            
        }

        /// <summary>
        /// Replacer e ataquer a chaque tic de l'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvance(object sender, EventArgs e)
        {

            for (int i = 0; i < ListeNavire.Count; i++)
            {
                if (ListeNavire[i] is VueGalion)
                {
                    ((VueGalion) ListeNavire[i]).ReplacerNavire();
                    ((VueGalion) ListeNavire[i]).TickHorloge++;

                }
                else if (ListeNavire[i] is VueEscorte)
                {
                    ((VueEscorte) ListeNavire[i]).ReplacerNavire();
                    ((VueEscorte) ListeNavire[i]).TickHorloge++;
                }
                else if (ListeNavire[i] is VuePirate)
                {
                    ((VuePirate) ListeNavire[i]).TickHorloge++;
                }

                ListeNavire[i].ValiderMouvement(Surface);
                EviterColision(ListeNavire[i]);
                ListeNavire[i].MouvementerNavire();
                ListAttaques[i] = ListeNavire[i].Tirer();
            }

            for (int i = 0; i < ListeNavire.Count; i++)
            {
                VerifierSubitAttaque(i);

            }

            AfficherVie();
        }

        /// <summary>
        /// Recevoir la comande de direction pour le navire pirate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Type de button appuie</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (IVueNavire navire in ListeNavire)
            {
                if (navire is VuePirate)
                {
                    switch (e.Key)
                    {
                        case Key.Left:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Gauche);
                            break;
                        case Key.Right:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Droit);
                            break;
                        case Key.Up:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Haut);
                            break;
                        case Key.Down:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Bas);
                            break;
                        case Key.Space:
                            break;
                    }
                }
            }

        }

        public void EviterColision(IVueNavire navireReference)
        {
            List<double> positionNavireReference = navireReference.PositionNavire();
            List<double> positionNavireEvalue = null;

            foreach (IVueNavire navireEvalue in ListeNavire)
            {
                if (!Object.ReferenceEquals(navireEvalue, navireReference))
                {
                    positionNavireEvalue = navireEvalue.PositionNavire();

                    if (VerifierColision(positionNavireReference, positionNavireEvalue))
                    {
                        navireReference.BloquerMouvement();
                    }
                }
            }
        }

        private bool VerifierColision(List<double> positionReference, List<double> positionEvalue)
        {
            bool estColision = false;

            if ((positionReference[2] >= positionEvalue[2] && positionReference[2] <= positionEvalue[3]) ||
                (positionReference[3] <= positionEvalue[3] && positionReference[3] >= positionEvalue[2]))
            {
                if ((positionReference[0] >= positionEvalue[0] && positionReference[0] <= positionEvalue[1]) ||
                    (positionReference[1] <= positionEvalue[1] && positionReference[1] >= positionEvalue[0]))
                {
                    estColision = true;
                }
            }
            return estColision;
        }

        public void AfficherVie() /*CHANGER METHODE POUR AFFICER TOUS LES VIES*/ //TODO
        {
            //ViePirate.Text = ((VuePirate)ListeNavire[0]).GetVie();

        }

        public void VerifierSubitAttaque(int index)
        {
            List<double> positionNavireReference = ListeNavire[index].PositionTireNavire();

            List<double> positionNavireEvalue = null;

            foreach (IVueNavire navireEvalue in ListeNavire)
            {
                if (!Object.ReferenceEquals(navireEvalue, ListeNavire[index]))
                {
                    positionNavireEvalue = navireEvalue.PositionNavire();

                    if (VerifierColision(positionNavireReference, positionNavireEvalue))
                    {
                        navireEvalue.SubirAttaque(ListAttaques[index], ListeNavire[index].GetTypeNavire().EstEnemiePirate); 
                    }
                }
            }
        }

        private void ButtonMagasin_OnClick(object sender, RoutedEventArgs e)
        {
            ArreterHorlogePrincipal();
            fenetreMagasin.Show();

        }

        /// <summary>
        /// Fermeture de la fenêtre.
        /// Changement de cette méthode qui est appelé chaque fois qu'on appui sur le X.
        /// Fermeture du programme explicitement. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
