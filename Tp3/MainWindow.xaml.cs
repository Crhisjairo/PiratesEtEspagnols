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

        //private List<IVueNavire> ListeVueNavire { get; set; } = new List<IVueNavire>();

        private Dictionary<int, IVueNavire> _dicVueNavires = new Dictionary<int, IVueNavire>();
        private VuePirate _vuePirate;

        private List<int> ListAttaques { get; set; } = new List<int>();
        
        private static DispatcherTimer _horloge = new DispatcherTimer();

        /// <summary>
        /// Compteur de l'animation de présentation (Mouvement du background et message du niveau).
        /// </summary>
        private int compteurAnimation = 0;

        public MainWindow()
        {
            //Creer et placer les UserControl
            InitializeComponent();
            
            //Préparation de logique (modèle).
            _jeu.PreparerJeu();

            //Préparation des vues (vues).
            CreerVueDesNavires();

            //Horloge*
            CreerHorlogeMouvement();


            //Méthodes Debug//
            VerifierCorrespondanceDeCles();
        }

        private void CreerHorlogeMouvement()
        {
            _horloge.Interval = TimeSpan.FromMilliseconds(100);
            _horloge.IsEnabled = true;
            //Méthodes à executer à chaque tick
            _horloge.Tick += HorlogeAvanceAnimation; //d'abord, on ajoute l'animation pour la présentation
            _horloge.Tick += VerifierAnimationFinit; //Verification si l'animation est finit.

            DemarrerHorlogePrincipal();
        }

        private void HorlogeAvanceAnimation(object sender, EventArgs e)
        {
            //Changement de margin de l'images pour avoir l'impression qu'elle bouge.
            NiveauBackground.Margin = new Thickness(0, -1214, 0, -18 - compteurAnimation * 30);
            compteurAnimation ++;
        }

        private void VerifierAnimationFinit(object sender, EventArgs e)
        {
            if (compteurAnimation > 20) //si le
            {
                _horloge.Tick -= HorlogeAvanceAnimation; //supression de la méthode qu'animation.
                _horloge.Tick -= VerifierAnimationFinit;
                _horloge.Tick += HorlogeAvanceJeu; //Ajout de la méthode qui roule le jeu.
            }
        }


        /// <summary>
        /// Methode pour incluire un navire dans le Jeu.
        /// </summary>
        /// <param name="left">l'emplacement "x" du navire</param>
        /// <param name="top">l'emplacement "y"du navire</param>
        /// <param name="type">si le navire est un Galion, un pirate ou un escorte</param>
        private void CreerVueDesNavires()
        {
            for (int i = 0; i < _jeu.GetNombreNavires(); i++)
            {
                
                if(_jeu.GetNavire(i) is ModeleGalion)
                {
                    //Ajout au dicVue le pirate avec sa clé correspondant du modèle
                    _dicVueNavires.Add(i, new VueGalion());

                } else if (_jeu.GetNavire(i) is ModeleEscorte)
                {
                    //Ajout au dicVue le galion avec sa clé correspondant du modèle
                    _dicVueNavires.Add(i, new VueEscorte());

                } 
                //else if (_jeu.GetNavire(i) is ModelePirate)
                //{
                //    //Ajout au dicVue le escorte avec sa clé correspondant du modèle
                //    _dicVueNavires.Add(i, new VuePirate());
                //}
            }

            //Création de la VuePirate - Il est forcement lié au modelePirate, car il est le seul.
            _vuePirate = new VuePirate();

            AjouterNaviresAuCanvas();

        }
        private void AjouterNaviresAuCanvas()
        {
            //Car les position des escortes varient en fonction de la position initial
            double tempPosXEscorte = VueEscorte.PosInitX;
            double tempPosYEscorte = VueEscorte.PosInitY;

            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                if (_dicVueNavires[i] is VueGalion)
                {
                    VueGalion galionTemp = (VueGalion) _dicVueNavires[i];

                    Surface.Children.Add((UIElement) galionTemp);
                    Canvas.SetLeft((UIElement)galionTemp, VueGalion.PosInitX);
                    Canvas.SetTop((UIElement)galionTemp, VueGalion.PosInitY);
                }

                if (_dicVueNavires[i] is VueEscorte)
                {
                    VueEscorte escorteTemp = (VueEscorte)_dicVueNavires[i];

                    Surface.Children.Add((UIElement)escorteTemp);
                    Canvas.SetLeft((UIElement)escorteTemp, tempPosXEscorte);
                    Canvas.SetTop((UIElement)escorteTemp, tempPosYEscorte);

                    //Separer les escortes, si sa dépase le canvas, on "saute à l'autre ligne"
                    if (tempPosXEscorte >= 500) //500 c'est à peu près la largeur* du canvas pour assurer une distance avec la bordure
                    {
                        tempPosXEscorte = VueEscorte.PosInitX; //réinitialisation de la pos x.
                        tempPosYEscorte += 150;
                    }
                    else
                    {
                        tempPosXEscorte += 150;
                    }
                }
            }

            //Ajout du pirate, car il est indépendant des liste de vuesNavires .
            Surface.Children.Add((UIElement)_vuePirate);
            Canvas.SetLeft((UIElement)_vuePirate, VuePirate.PosInitX);
            Canvas.SetTop((UIElement)_vuePirate, VuePirate.PosInitY);

        }

        /// <summary>
        /// Replacer et ataquer à chaque tick d'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvanceJeu(object sender, EventArgs e)
        {

            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                if (_dicVueNavires[i] is VueGalion)
                {
                    ((VueGalion) _dicVueNavires[i]).ReplacerNavire();
                    ((VueGalion)_dicVueNavires[i]).TickHorloge++;

                }
                else if (_dicVueNavires[i] is VueEscorte)
                {
                    ((VueEscorte) _dicVueNavires[i]).ReplacerNavire();
                    ((VueEscorte) _dicVueNavires[i]).TickHorloge++;
                }
                    //***À REVISER***///
                    _vuePirate.TickHorloge++;

                    _vuePirate.ValiderMouvement(Surface);
                    EviterColision(_vuePirate);
                    _vuePirate.MouvementerNavire();

                    //***À VERIFIER***//
                    //ListAttaques[i] = _vuePirate.Tirer();

                    _dicVueNavires[i].ValiderMouvement(Surface);
                    EviterColision(_dicVueNavires[i]);
                    _dicVueNavires[i].MouvementerNavire();

                    //***À VERIFIER***//
                    //ListAttaques[i] = _dicVueNavires[i].Tirer();
            }



            //***À VERIFIER***//
            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                //VerifierSubitAttaque(i);
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
                switch (e.Key)
                    {
                        case Key.Left:
                            _vuePirate.ReplacerNavirePirate(Buttons.Gauche);
                            break;
                        case Key.Right:
                            _vuePirate.ReplacerNavirePirate(Buttons.Droit);
                            break;
                        case Key.Up:
                            _vuePirate.ReplacerNavirePirate(Buttons.Haut);
                            break;
                        case Key.Down:
                            _vuePirate.ReplacerNavirePirate(Buttons.Bas);
                            break;
                        case Key.Space:
                            break;
                    }
            
        }

        public void EviterColision(IVueNavire navireReference)
        {
            List<double> positionNavireReference = navireReference.PositionNavire();
            List<double> positionNavireEvalue = null;

            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                IVueNavire navireEvalue = _dicVueNavires[i];

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
            //ViePirate.Text = ((VuePirate)ListeVueNavire[0]).GetVie();

        }

        //***À VERIFIER***///
        public void VerifierSubitAttaque(int index)
        {
            List<double> positionNavireReference = _dicVueNavires[index].PositionTireNavire();

            List<double> positionNavireEvalue = null;

            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                IVueNavire navireEvalue = _dicVueNavires[i];

                if (!Object.ReferenceEquals(navireEvalue, _dicVueNavires[index]))
                {
                    positionNavireEvalue = navireEvalue.PositionNavire();

                    if (VerifierColision(positionNavireReference, positionNavireEvalue))
                    {
                        navireEvalue.SubirAttaque(ListAttaques[index], _dicVueNavires[index].GetTypeNavire().EstEnemiePirate);
                    }
                }
            }
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

        //***MÉTHODES DEBUG***//
        /// <summary>
        /// Affiche dans des TextBlocks les différents contenus des dictionnaires.
        /// 1er TextBlock -> Contenu du dictionnaire des vues _dicVueNavires.
        /// 2ème TextBlock -> Contenu du dictionnaire des modeles (Jeu) _dicModeleNavires par la méthode _jeu.GetNavire(i).
        /// </summary>
        private void VerifierCorrespondanceDeCles()
        {

            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                txtListeVueNavires.Text += "\n" + _dicVueNavires[i].GetType().ToString();
                txtListeModelesNavires.Text += "\n" + _jeu.GetNavire(i).ToString();

            }
        }
    }
}
