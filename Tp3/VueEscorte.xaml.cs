using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VueEscorte.xaml
    /// </summary>
    public partial class VueEscorte : UserControl, IVueNavire
    {
        private Navire _modeleEscorte = null;
        
        public static double PosInitX { get; } = 100; //100
        public static double PosInitY { get; } = 200;

        private const int Acceleration = 2;
        public int TickHorloge { set; get; }
        public double ChangementPositionX { get; set; }
        public double ChangementPositionY { get; set; }
        private double NextY { get; set; }
        private double NextX { get; set; }
        private static Random _random = new Random();
        private int DommageAttaque { get; set; }

        //Christian, Est-ce que tu utilise ça pour quelque chose?
        private TypeEscorte typeEscorte;
        
        public VueEscorte(Navire modeleEscorte)
        {
            InitializeComponent();
            GenererImageAleatoire();
            _modeleEscorte = (ModeleEscorte) modeleEscorte;
        }

        /// <summary>
        /// Methode pour l'ordi definir quel serait le mouvement que le navire doit prendre dans le tour.
        /// </summary>
        public void ChoisirMouvementNavire()
        {
            //toujour reinitializer le choix à 0.
            int choixDirection = 0;
            TickHorloge++;
            DommageAttaque = 0;

            if (this.EstMort())
            {
                ChangementPositionY = 0;
                ChangementPositionX = 0;
            }
            else
            {
                choixDirection = _random.Next(5);

                if (choixDirection < 1)
                {
                    ChangementPositionY += Acceleration; //Aller vers bas
                }
                else if (choixDirection < 2)
                {
                    ChangementPositionY -= Acceleration; // Aller vers haut
                }
                else if (choixDirection < 3)
                {
                    ChangementPositionX -= Acceleration; // Aller vers gauche
                }
                else if (choixDirection < 4)
                {
                    ChangementPositionX += Acceleration; // Aller vers droit
                }
                else
                {
                    DommageAttaque = Tirer();
                }
            }

        }

        /// <summary>
        /// Calcule le dommage que le navire va aporter à ses enimies.
        /// </summary>
        /// <returns>Dommage causé par le navire</returns>
        public int Tirer()
        {
            double attaque = _modeleEscorte.Tirer(TickHorloge);
            return (int)attaque;
        }

        /// <summary>
        /// Verifie se le navire se mantien dedans le Canvas s'il fait le mouvement.
        /// </summary>
        /// <param name="surface">L'espace où le navire peut se mouvementer</param>
        public void ValiderMouvement(Canvas surface)
        {
            NextY = Canvas.GetTop(this) + ChangementPositionY;
            NextX = Canvas.GetLeft(this) + ChangementPositionX;

            if (NextY < 0)
            {
                ChangementPositionY = 0;
            }
            else if (NextY + ActualHeight > surface.ActualHeight)
            {
                ChangementPositionY = surface.ActualHeight - (NextY + ActualHeight);
            }

            if (NextX < 0)
            {
                ChangementPositionX = 0;
            }
            else if (NextX + ActualWidth > surface.ActualWidth)
            {
                ChangementPositionX = surface.ActualWidth - (NextX + ActualWidth);
            }
        }

        /// <summary>
        /// Permet savoir où le navire se trouve dans le canvas après le mouvement est fait.
        /// </summary>
        /// <returns>dictionaire avec les positions extremes ATTENDUES du navire : point plus à droite, point plus à gauche, point plus au haut et point plus au bas</returns>
        public Dictionary<string, double> GetPositionPrevueNavire()
        {
            Dictionary<string, double> dictPositionNavire = new Dictionary<string, double>();

            double gauche = Canvas.GetLeft(this) + ChangementPositionX;
            dictPositionNavire.Add("gauche", gauche);
            double droit = gauche + ActualWidth;
            dictPositionNavire.Add("droit", droit);

            double haut = Canvas.GetTop(this) + ChangementPositionY;
            dictPositionNavire.Add("haut", haut);
            double bas = haut + ActualHeight;
            dictPositionNavire.Add("bas", bas);

            return dictPositionNavire;
        }

        /// <summary>
        /// Permet savoir où le navire se trouve dans le canvas dans l'exact moment.
        /// </summary>
        /// <returns>dictionaire avec les positions extremes RÉELS du navire : point plus à droite, point plus à gauche, point plus au haut et point plus au bas</returns>
        public Dictionary<string, double> GetPositionReelNavire()
        {
            Dictionary<string, double> dictPositionNavire = new Dictionary<string, double>();

            double gauche = Canvas.GetLeft(this);
            dictPositionNavire.Add("gauche", gauche);
            double droit = gauche + ActualWidth;
            dictPositionNavire.Add("droit", droit);

            double haut = Canvas.GetTop(this);
            dictPositionNavire.Add("haut", haut);
            double bas = haut + ActualHeight;
            dictPositionNavire.Add("bas", bas);

            return dictPositionNavire;
        }

        /// <summary>
        /// Va faire de sorte que navire ne bouge pas parce qu'il change le deplacement vertical et horizontal à ZERO. 
        /// </summary>
        public void BloquerMouvement()
        {
            ChangementPositionY = 0;
            ChangementPositionX = 0;
        }

        /// <summary>
        /// Sert à placer le navire dans le canvas.
        /// </summary>
        public void MouvementerNavire()
        {
            Canvas.SetTop(this, Canvas.GetTop(this) + ChangementPositionY);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + ChangementPositionX);
        }
        
        /// <summary>
        /// Ajoute le champ de tir à la position du navire pour savoir jusqu'à quel position le tir va affecter. 
        /// </summary>
        /// <returns>dictionaire avec tout le champ de tir</returns>
        public Dictionary<string, double> GetChampDeTir()
        {
            Dictionary<string, double> dictPositionTir = new Dictionary<string, double>();
            int champDeTir = _modeleEscorte.Canon.ChamDeTire;

            double gauche = Canvas.GetLeft(this);
            dictPositionTir.Add("gauche", (gauche - (double)champDeTir));
            double droit = gauche + ActualWidth;
            dictPositionTir.Add("droit", (droit + (double)champDeTir));

            double haut = Canvas.GetTop(this);
            dictPositionTir.Add("haut", haut);
            double bas = haut + ActualHeight;
            dictPositionTir.Add("bas", bas);

            return dictPositionTir;
        }

        /// <summary>
        /// Methode pour recuperer la force du attaque.
        /// </summary>
        /// <returns>La force du attaque</returns>
        internal int GetForceAttaque()
        {
            return DommageAttaque;
        }

        /// <summary>
        /// Methode que retire de la vie du navire.
        /// </summary>
        /// <param name="forceAttaque">Le dommage que le navire va subir</param>
        public void SubirAttaque(int forceAttaque)
        {
            _modeleEscorte.EtreAttaque(forceAttaque);
        }

        /// <summary>
        /// Cherche la quantité de membres restants du navire.
        /// </summary>
        /// <returns>string avec la quantité de vie (membres)</returns>
        public string GetVie()
        {
            string textVie = "Escorte : ";
            int vie = (_modeleEscorte).DonnerQuantiteMembresRestants();
            return textVie + vie.ToString();
        }

        /// <summary>
        /// Retourne si le navire est encore dans le jeu
        /// </summary>
        /// <returns></returns>
        public bool EstMort()
        {
            return _modeleEscorte.EstHorsCombat;
        }

        /// <summary>
        /// Donne une image d'escorte aléatoirement.
        /// Il existe 3 types de navires escorte: Cheval, Croix et Epee.
        /// </summary>
        private void GenererImageAleatoire()
        {
            switch (_random.Next(0,3))
            {
                case 0:
                    typeEscorte = TypeEscorte.Cheval;
                    break;
                case 1:
                    typeEscorte = TypeEscorte.Croix;
                    break;
                case 2:
                    typeEscorte = TypeEscorte.Epee;
                    break;
            }
        }

        /// <summary>
        /// Change l'état du navire Escorte selon son type.
        /// Il existe 3 types de navires escorte: Cheval, Croix et Epee.
        /// </summary>
        /// <param name="etat">État du navire</param>
        public void ChangerEtat(EtatNavire etat)
        {
            //_escorte.ChangerEtat();

            if (typeEscorte == TypeEscorte.Cheval)
            {
                ChangerEtatTypeCheval(etat);
            }
            else if (typeEscorte == TypeEscorte.Croix)
            {
                ChangerEtatTypeCroix(etat);
            }
            else if (typeEscorte == TypeEscorte.Epee)
            {
                ChangerEtatTypeEpee(etat);
            }
        }

        /// <summary>
        /// Change l'image d'état d'un escorte de type cheval.
        /// </summary>
        /// <param name="etat">État du navire</param>
        private void ChangerEtatTypeCheval(EtatNavire etat)
        {
            if (etat == EtatNavire.Neuf)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat3.png"));
            else if (etat == EtatNavire.Mort)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat4.png"));
        }

        /// <summary>
        /// Change l'image d'état d'un escorte de type croix.
        /// </summary>
        /// <param name="etat">État du navire</param>
        private void ChangerEtatTypeCroix(EtatNavire etat)
        {
            if (etat == EtatNavire.Neuf)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat3.png"));
            else if (etat == EtatNavire.Mort)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat4.png"));
        }

        /// <summary>
        /// Change l'image d'état d'un escorte de type Epee.
        /// </summary>
        /// <param name="etat">État du navire</param>
        private void ChangerEtatTypeEpee(EtatNavire etat)
        {
            if (etat == EtatNavire.Neuf)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat3.png"));
            else if (etat == EtatNavire.Mort)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat4.png"));
        }

    }

}
