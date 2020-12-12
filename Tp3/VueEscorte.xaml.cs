﻿using System;
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
        /// <summary>
        /// Modele Escorte du tipe Navire, c'est avec cette navire que l'user control se comunique avec l'objet navire.
        /// </summary>
        private Navire _modeleEscorte = null;
        
        public static double PosInitX { get; } = 100;
        public static double PosInitY { get; } = 200;

        /// <summary>
        /// Quel est l'acceletation de cette vue à chaque tick d'horloge.
        /// </summary>
        private const int Acceleration = 2;
        /// <summary>
        /// Temps de jeu de cette navire.
        /// Utilisé pour les tests de tir (recharge de canon)
        /// </summary>
        public int TickHorloge { set; get; }
        /// <summary>
        /// Le deplacement desiré vers la droit/gauche dans le tour.
        /// </summary>
        public double ChangementPositionX { get; set; }
        /// <summary>
        /// Le deplacement desiré vers l'haut/bas dans le tour.
        /// </summary>
        public double ChangementPositionY { get; set; }
        /// <summary>
        /// Position top estimé dans l'axe Y apres le deplacement desiré.
        /// </summary>
        private double NextY { get; set; }
        /// <summary>
        /// Position left estimé dans l'axe X apres le deplacement desiré.
        /// </summary>
        private double NextX { get; set; }
        /// <summary>
        /// Vaiable pour faire les choix randomiques comme le mouvment, si le navire tire, etc.
        /// </summary>
        private static Random _random = new Random();
        /// <summary>
        /// La force de l'attaque fait dans le tour. 
        /// </summary>
        private int DommageAttaque { get; set; }

        /// <summary>
        /// definisse le type d'escorte utilisé.
        /// </summary>
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
        /// Sert a recuperer la quantité de vie du navire
        /// </summary>
        /// <returns>Quantité de vie du navire</returns>
        public int GetVieEntier()
        {
            return _modeleEscorte.DonnerQuantiteMembresRestants();
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
                    ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat1.png", UriKind.Relative));
                    break;
                case 1:
                    ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat1.png", UriKind.Relative));
                    break;
                case 2:
                    ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat1.png", UriKind.Relative));
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
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat1.png", UriKind.Relative));
            else if (etat == EtatNavire.peuDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat2.png", UriKind.Relative));
            else if (etat == EtatNavire.TresDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat3.png", UriKind.Relative));
            else if (etat == EtatNavire.Mort)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte1Etat4.png", UriKind.Relative));
        }

        /// <summary>
        /// Change l'image d'état d'un escorte de type croix.
        /// </summary>
        /// <param name="etat">État du navire</param>
        private void ChangerEtatTypeCroix(EtatNavire etat)
        {
            if (etat == EtatNavire.Neuf)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat1.png", UriKind.Relative));
            else if (etat == EtatNavire.peuDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat2.png", UriKind.Relative));
            else if (etat == EtatNavire.TresDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat3.png", UriKind.Relative));
            else if (etat == EtatNavire.Mort)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte2Etat4.png", UriKind.Relative));
        }

        /// <summary>
        /// Change l'image d'état d'un escorte de type Epee.
        /// </summary>
        /// <param name="etat">État du navire</param>
        private void ChangerEtatTypeEpee(EtatNavire etat)
        {
            if (etat == EtatNavire.Neuf)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat1.png", UriKind.Relative));
            else if (etat == EtatNavire.peuDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat2.png", UriKind.Relative));
            else if (etat == EtatNavire.TresDommage)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat3.png", UriKind.Relative));
            else if (etat == EtatNavire.Mort)
                ImageEscorte.Source = new BitmapImage(new Uri("images/Navires/Escortes/Escorte3Etat4.png", UriKind.Relative));
        }

    }

}
