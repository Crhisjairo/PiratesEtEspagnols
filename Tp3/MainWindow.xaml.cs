using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        /// <summary>
        /// Jeu principal.
        /// </summary>
        private static Jeu _jeu = new Jeu();
        /// <summary>
        /// Fenêtre du magasin qui va être initialisé après.
        /// </summary>
        private static FenetreMagasin fenetreMagasin;

        /// <summary>
        /// Dictionnaire contenant les vues des navires.
        /// </summary>
        private Dictionary<int, IVueNavire> _dicVueNavires;
        /// <summary>
        /// Vue du pirate.
        /// </summary>
        private VuePirate _vuePirate;

        /// <summary>
        /// ****
        /// </summary>
        private int AttaquePirate { get; set; }

        /// <summary>
        /// Horloge principal.
        /// </summary>
        private static DispatcherTimer _horloge;

        /// <summary>
        /// Compteur de l'animation de présentation (Mouvement du background plus vite).
        /// </summary>
        private int _compteurAnimationPresentation = 0;
        /// <summary>
        /// Compteur de l'animation du background.
        /// </summary>
        private int _compteurAnimationBackground = 0;

        public MainWindow()
        {
            //Creer et placer les UserControl
            InitializeComponent();
            //Initialiser le jeu.
            InitialisationDuJeu();
        }

        private void InitialisationDuJeu()
        {
            //Préparation de logique (modèle).
            _jeu.PreparerJeu();

            //Préparation de la vue du jeu.
            PreparerVueJeu();

            //Préparation des Navires (vues).
            CreerVueDesNavires();

            //Horloge*
            CreerHorlogeMouvement();

            //Méthodes Debug//
            //VerifierCorrespondanceDeCles();
        }

        /// <summary>
        /// Prepare l'interface d'utilisateur, la vue.
        /// </summary>
        private void PreparerVueJeu()
        {
            fenetreMagasin = new FenetreMagasin(_jeu.GetPirate());
            _dicVueNavires = new Dictionary<int, IVueNavire>();

            TextBlockNiveauActuel.Text = _jeu.ToStringNiveauActuel(); //affichage du niveau.
        }

        /// <summary>
        /// ***
        /// </summary>
        private void CreerHorlogeMouvement()
        {
            _horloge = new DispatcherTimer();

            _horloge.Interval = TimeSpan.FromMilliseconds(50);
            _horloge.IsEnabled = true;

            //Méthodes à executer à chaque tick
            _horloge.Tick += HorlogeAvanceAnimationPresentation; //d'abord, on ajoute l'animation pour la présentation
            _horloge.Tick += VerifierAnimationFinit; //Verification si l'animation est finit.

            DemarrerHorlogePrincipal();
        }

        /// <summary>
        /// Déplace le background au rythme de l'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvanceAnimationPresentation(object sender, EventArgs e)
        {
            //Changement de margin de l'images pour avoir l'impression qu'elle bouge.
            NiveauBackground.Margin = new Thickness(0, -1214, 0, -18 - _compteurAnimationPresentation * 30);
            _compteurAnimationPresentation ++;
        }

        /// <summary>
        /// Verifie si l'animation initial (Déplacement rapide du background) est finit.
        /// Quand cette animation est finit, différentes méthodes s'enlèvent et s'ajoutent d'un tick d'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifierAnimationFinit(object sender, EventArgs e)
        {
            if (_compteurAnimationPresentation > 20) //si le compteur est arrivé à un point
            {
                _horloge.Tick -= HorlogeAvanceAnimationPresentation; //supression de la méthode animation.
                _horloge.Tick -= VerifierAnimationFinit; //suppression de la méthode de verification parce qu'il y a rien à vérifier

                _horloge.Tick += HorlogeAvanceJeu; //Ajout de la méthode qui roule le jeu.
                _horloge.Tick += HorlogeAvanceAnimationBackground; //Ajout de la méthode qui déplace le background.
                _horloge.Tick += VerifierEtatNavires;
            }
        }

        /// <summary>
        /// Verifie l'état d'un navire à chaque tick pour ensuite lui donner une apparence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifierEtatNavires(object sender, EventArgs e)
        {
            foreach (IVueNavire navire in _dicVueNavires.Values)
            {
                    if (navire is VueGalion)
                    {
                        if (navire.GetVieEntier() == 0)
                        {
                            ((VueGalion)navire).ChangerEtat(EtatNavire.Mort);
                        } else if (navire.GetVieEntier() < 45)
                        {
                            ((VueGalion)navire).ChangerEtat(EtatNavire.TresDommage);
                        }
                        else if (navire.GetVieEntier() < 75)
                        {
                            ((VueGalion)navire).ChangerEtat(EtatNavire.peuDommage);
                        }

                    }else if (navire is VueEscorte)
                    {
                        if (navire.GetVieEntier() == 0)
                        {
                            ((VueEscorte)navire).ChangerEtat(EtatNavire.Mort);
                        }else if (navire.GetVieEntier() < 45)
                        {
                            ((VueEscorte)navire).ChangerEtat(EtatNavire.TresDommage);
                        }
                        else if (navire.GetVieEntier() < 75)
                        {
                            ((VueEscorte)navire).ChangerEtat(EtatNavire.peuDommage);
                        }
                        
                    }
            }
            //Pour le pirate
            if (_vuePirate.GetVieEntier() == 0)
            {
                _vuePirate.ChangerEtat(EtatNavire.Mort);
            }
            else if (_vuePirate.GetVieEntier() < 45)
            {
                _vuePirate.ChangerEtat(EtatNavire.TresDommage);
            }
            else if (_vuePirate.GetVieEntier() < 75)
            {
                _vuePirate.ChangerEtat(EtatNavire.peuDommage);
            }

        }

        /// <summary>
        /// Déplace le background lentement au rythme de l'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvanceAnimationBackground(object sender, EventArgs e)
        {
            if (_compteurAnimationBackground % 5 == 0)
            {
                NiveauBackground.Margin = new Thickness(0, -1214, 0, 
                    (-18 - _compteurAnimationPresentation * 30) - _compteurAnimationBackground * 1.2);
            }

            if (_compteurAnimationBackground > 390)
            {
                _horloge.Tick -= HorlogeAvanceAnimationBackground;
            }

            //txtListeVueNavires.Text = _compteurAnimationBackground.ToString(); //Affiche le compteur d'animation dans le HUD.

            _compteurAnimationBackground++;
        }

        /// <summary>
        /// Création des vues des navires selon son modèle.
        /// </summary>
        private void CreerVueDesNavires()
        {
            for (int i = 0; i < _jeu.GetNombreNavires(); i++)
            {
                
                if(_jeu.GetNavire(i) is ModeleGalion)
                {
                    //Ajout au dicVue le pirate avec sa clé correspondant du modèle
                    _dicVueNavires.Add(i, new VueGalion(_jeu.GetNavire(i)));

                } else if (_jeu.GetNavire(i) is ModeleEscorte)
                {
                    //Ajout au dicVue le galion avec sa clé correspondant du modèle
                    _dicVueNavires.Add(i, new VueEscorte(_jeu.GetNavire(i)));

                } 

            }

            //Création de la VuePirate - Il est forcement lié au modelePirate, car il est le seul.
            _vuePirate = new VuePirate(_jeu.GetPirate());

            AjouterNaviresAuCanvas();
        }

        /// <summary>
        /// Placer des navires dans le canvas de façon logique. Pirate au bas de l'écran et enmi au haut avec un distance entre eux.
        /// </summary>
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
            if (EstJeuFinit())
            {
                ReinitialiserVue();
                return; //Arrête le jeu.
            }

            AfficherVie(); //***
            _vuePirate.TickHorloge++; //***
            MouvementerNavires(); //***
            VerifierAttaques(); //***
        }

        /// <summary>
        /// Arrête l'horloge, efface tous les éléments du canvas, repositionne le background et réinitialise les compteurs d'animations.
        /// </summary>
        private void ReinitialiserVue()
        {
            _horloge.Stop();

            Surface.Children.Clear(); //Netoyage du canvas 

            NiveauBackground.Margin = new Thickness(0, -1214, 0, -18); //position par défaut du background.

            _compteurAnimationBackground = 0;
            _compteurAnimationPresentation = 0;

            InitialisationDuJeu();
        }

        /// <summary>
        /// Verifie s'il y a un gagnant.
        /// </summary>
        /// <returns></returns>
        private bool EstJeuFinit()
        {
            //Si le pirate est mort, le jeu s'arrête.
            if (_vuePirate.EstMort())
            {
                //activer button RECOMENCER
                MessageBox.Show("Vous avez perdu :(\nLe jeu va recommencer complètement.", "Pirate detruit");
                ReinitialiserVue();
                return true;
            }

            //Si au moins un navire est en vie, le jeu continue.
            foreach (IVueNavire navire in _dicVueNavires.Values)
            {
                if (!navire.EstMort())
                {
                    return false;
                }
            }

            //Si les navires sont tous mort et le pirate vivant, le pirate gagne
            MessageBox.Show("On avance. Arrr!!!", "Espagnols detruits");

            AvancerNiveau();
            return true;
            //activer button nextLevel
        }

        /// <summary>
        /// Avance un niveau du jeu.
        /// </summary>
        private void AvancerNiveau()
        {
            int niveauActuel = _jeu.GetNiveauActuel();

            if (niveauActuel == 4)
            {
                MessageBox.Show("Wow, vous avez vaincu les espagnols!\n" +
                                "Merci d'avoir joué! Le jeu va se fermer.", "Espagnols detruits");
                Close();
                return;
            }

            ReinitialiserVue();

            _jeu.SetNiveau(niveauActuel + 1);
        }

        /// <summary>
        /// Recevoir la comande de direction pour le navire pirate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Type de button appuie</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            AttaquePirate = 0;
            
            switch (e.Key)
            {
                case Key.Left:
                    _vuePirate.ChoisirMouvementNavirePirate(Buttons.Gauche);
                    break;
                case Key.Right:
                    _vuePirate.ChoisirMouvementNavirePirate(Buttons.Droit);
                    break;
                case Key.Up:
                    _vuePirate.ChoisirMouvementNavirePirate(Buttons.Haut);
                    break;
                case Key.Down:
                    _vuePirate.ChoisirMouvementNavirePirate(Buttons.Bas);
                    break;
                case Key.Space:
                    AttaquePirate = _vuePirate.Tirer();
                    break;
            }
        }

        /// <summary>
        /// Mouvementer chaque objet dans le canvas;
        /// </summary>
        private void MouvementerNavires()
        {
            //Pour mouvementer le Pirate
            if (!_vuePirate.EstMort())
            {
                _vuePirate.ValiderMouvement(Surface);
                EviterColision(_vuePirate);
                _vuePirate.MouvementerNavire();
            }

            //Pour mouvementer les enimies
            foreach (IVueNavire navire in _dicVueNavires.Values)
            {
                if (navire.EstMort())
                {
                    navire.BloquerMouvement();
                }
                else
                {
                    if (navire is VueGalion)
                    {
                        ((VueGalion)navire).ChoisirMouvementNavire();
                    }
                    else if (navire is VueEscorte)
                    {
                        ((VueEscorte)navire).ChoisirMouvementNavire();
                    }

                    navire.ValiderMouvement(Surface);
                    EviterColision(navire);
                    navire.MouvementerNavire();
                }
            }
        }

        /// <summary>
        /// La methode va bloquer des mouvements d'un navire s'il se colide avec autre des navires du canvas.
        /// </summary>
        /// <param name="navireReference">Le navire qu'essaye de se mouvementer</param>
        public void EviterColision(IVueNavire navireReference)
        {
            Dictionary<string, double> positionNavireReference = navireReference.GetPositionPrevueNavire();
            Dictionary<string, double> positionNavireEvalue = null;

            for (int i = 0; i < _jeu.GetNombreNavires(); i++)
            {
                IVueNavire navireEvalue = _dicVueNavires[i];
                if (!Object.ReferenceEquals(navireEvalue, navireReference))
                {
                    positionNavireEvalue = navireEvalue.GetPositionReelNavire();

                    if (VerifierEstEnColision(positionNavireReference, positionNavireEvalue))
                    {
                        navireReference.BloquerMouvement();
                        if (navireEvalue.EstMort() && navireReference is VuePirate)
                        {
                            _jeu.GetNavire(i).EtreEvahis(_jeu.GetPirate());
                        }
                    }
                }

            }

            if (!(navireReference is VuePirate))
            {
                positionNavireEvalue = _vuePirate.GetPositionReelNavire();

                if (VerifierEstEnColision(positionNavireReference, positionNavireEvalue))
                {
                    navireReference.BloquerMouvement();
                }
            }
        }

        /// <summary>
        /// Verifier si le deux objets sont en train de se colidir dans le canvas.
        /// </summary>
        /// <param name="positionReference">La position du objet que se mouvement</param>
        /// <param name="positionEvalue">La position du objet que la methode va verifier si l'objet en mouvement se colide avec</param>
        /// <returns>boolean que di se le mouvement cause un colision ou il ne cause pas.</returns>
        private bool VerifierEstEnColision(Dictionary<string, double> positionReference, Dictionary<string, double> positionEvalue)
        {
            bool estColision = false;


            if ((positionReference["haut"] >= positionEvalue["haut"] && positionReference["haut"] <= positionEvalue["bas"]) ||
                (positionReference["bas"] <= positionEvalue["bas"] && positionReference["bas"] >= positionEvalue["haut"]))
            {
                if ((positionReference["gauche"] >= positionEvalue["gauche"] && positionReference["gauche"] <= positionEvalue["droit"]) ||
                    (positionReference["droit"] <= positionEvalue["droit"] && positionReference["droit"] >= positionEvalue["gauche"]))
                {
                    estColision = true;
                }
            }
            return estColision;
        }

        /// <summary>
        /// Verifier si les navires ont subit des attaques
        /// </summary>
        private void VerifierAttaques()
        {
            foreach (IVueNavire navireEnime in _dicVueNavires.Values)
            {
                if (!navireEnime.EstMort() && !_vuePirate.EstMort())
                {
                    
                    Attaquer(navireEnime, _vuePirate);
                    Attaquer(_vuePirate, navireEnime);
                    
                    //si Galeon, il a un attaque extra (arrier)
                    if (navireEnime is VueGalion)
                    {
                        AttaquerArrier(navireEnime, _vuePirate);
                    }
                }
            }
        }

        /// <summary>
        /// Verifie se l'ataque du navire qui attaque fait quelque dommage dans un navire enimie.
        /// </summary>
        /// <param name="navireAttaque">Navire que est en train d'attaquer</param>
        /// <param name="navireDefense">Navire enimie attaquée</param>
        private void Attaquer(IVueNavire navireAttaque, IVueNavire navireDefense)
        {
            Dictionary<string, double> positionTir = navireAttaque.GetChampDeTir();
            Dictionary<string, double> positionNavireDefense = navireDefense.GetPositionReelNavire();
            int dommage = 0;

            if (navireAttaque is VueEscorte)
            {
                dommage = ((VueEscorte)navireAttaque).GetForceAttaque();
            }
            else if (navireAttaque is VueGalion)
            {
                dommage = ((VueGalion)navireAttaque).GetForceAttaque();
            }


            if (VerifierEstEnColision(positionTir, positionNavireDefense))
            {
                if (navireAttaque is VuePirate)
                {
                    navireDefense.SubirAttaque(AttaquePirate);
                }
                else
                {
                    navireDefense.SubirAttaque(dommage);
                }
            }
        }


        /// <summary>
        /// Verifie se l'ataque du navire qui attaque fait quelque dommage dans un navire enimie.
        /// </summary>
        /// <param name="navireAttaque">Navire que est en train d'attaquer</param>
        /// <param name="navireDefense">Navire enimie attaquée</param>
        private void AttaquerArrier(IVueNavire navireAttaque, IVueNavire navireDefense)
        {
            Dictionary<string, double> positionTir = ((VueGalion)navireAttaque).GetChampDeTirArrier();
            Dictionary<string, double> positionNavireDefense = navireDefense.GetPositionReelNavire();

            if (VerifierEstEnColision(positionTir, positionNavireDefense))
            {
                navireDefense.SubirAttaque(((VueGalion)navireAttaque).GetForceAttaqueArrier());
            }
        }

        /// <summary>
        /// Afficher Vie de chaque navire dans le pier
        /// </summary>
        private void AfficherVie()
        {
            string textEnnemie = "Vie Ennemies: \n";

            for (int i = 0; i < _jeu.GetNombreNavires(); i++)
            {
                textEnnemie += _dicVueNavires[i].GetVie() + "\n";
            }

            VieEnnemie.Text = textEnnemie;

            ViePirate.Text = "Pirate: \n" + _vuePirate.GetBiens();
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
            fenetreMagasin.VerifierPossibiliteAchat();

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
                //txtListeVueNavires.Text += "\n" + _dicVueNavires[i].GetType().ToString();
               //txtListeModelesNavires.Text += "\n" + _jeu.GetNavire(i).ToString();

            }
        }

        private void ButtonTuer_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _dicVueNavires.Count; i++)
            {
                _dicVueNavires[i].SubirAttaque(1000);
            }
        }
    }
}
