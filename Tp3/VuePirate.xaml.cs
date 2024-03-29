﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VuePirate.xaml
    /// </summary>
    public partial class VuePirate : UserControl, IVueNavire
    {
        private Navire _modelePirate = null;

        public static double PosInitX { get; } = 320;
        public static double PosInitY { get; } = 800;

        /// <summary>
        /// Quel est l'acceletation de cette vue à chaque tick d'horloge.
        /// </summary>
        private const int Acceleration = 4;
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

        public VuePirate(Navire modelePirate)
        {
            InitializeComponent();
            _modelePirate = (ModelePirate) modelePirate;
        }

        /// <summary>
        /// Defire vers quelle direction le navire doit se deplacer.
        /// </summary>
        /// <param name="direction">Reçoit le button clique par l'utilisateur</param>
        public void ChoisirMouvementNavirePirate(Buttons direction)
        {
            switch (direction)
            {
                case Buttons.Bas:
                    ChangementPositionY += Acceleration;
                    break;
                case Buttons.Haut:
                    ChangementPositionY -= Acceleration;
                    break;
                case Buttons.Gauche:
                    ChangementPositionX -= Acceleration;
                    break;
                case Buttons.Droit:
                    ChangementPositionX += Acceleration;
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Calcule le dommage que le navire va aporter à ses enimies.
        /// </summary>
        /// <returns>Dommage causé par le navire</returns>
        public int Tirer()
        {
            double attaque = _modelePirate.Tirer(TickHorloge);
            return (int)attaque;
        }

        /// <summary>
        /// Valide si le mouvement est valide(le navire se mantien dans le canvas et ne surpasse autre navire)
        /// </summary>
        /// <param name="surface"> C'est le canvas où tous les élements sont placées</param>
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
            BloquerMouvement();
        }

        /// <summary>
        /// Ajoute le champ de tir à la position du navire pour savoir jusqu'à quel position le tir va affecter. 
        /// </summary>
        /// <returns>dictionaire avec tout le champ de tir</returns>
        public Dictionary<string, double> GetChampDeTir()
        {
            Dictionary<string, double> dictPositionTir = new Dictionary<string, double>();
            int champDeTir = _modelePirate.Canon.ChamDeTire;

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
        /// Methode que retire de la vie du navire.
        /// </summary>
        /// <param name="forceAttaque">Le dommage que le navire va subir</param>
        public void SubirAttaque(int forceAttaque)
        {
            _modelePirate.EtreAttaque(forceAttaque);
        }

        /// <summary>
        /// Cherche la quantité de membres restants du navire.
        /// </summary>
        /// <returns>string avec la quantité de vie (membres)</returns>
        public string GetVie()
        {
            string textVie = "Vie : ";
            int vie = (_modelePirate).DonnerQuantiteMembresRestants();
            return textVie + vie.ToString();
        }

        /// <summary>
        /// Sert a recuperer la quantité de vie du navire
        /// </summary>
        /// <returns>Quantité de vie du navire</returns>
        public int GetVieEntier()
        {
            return _modelePirate.DonnerQuantiteMembresRestants();
        }

        /// <summary>
        /// Returne la quantité d'or et de armes que le pirate a dans son navire.
        /// </summary>
        /// <returns> string avec les informations de combien d'or et la quantite d'armes que le pirate a</returns>
        public string GetBiens()
        {
            string textBiens = GetVie();
            textBiens += ((ModelePirate)_modelePirate).GetBiens();
            return textBiens;
        }

        /// <summary>
        /// Retourne si le navire est encore dans le jeu
        /// </summary>
        /// <returns></returns>
        public bool EstMort()
        {
            return _modelePirate.EstHorsCombat;
        }

        /// <summary>
        /// Change l'image du navir d'acord avec la quantité de vie qu'il a encore.
        /// </summary>
        /// <param name="etat">Definisse dans quel etat de degat le navire est</param>
        public void ChangerEtat(EtatNavire etat)
        {
            //_modelePirate.ChangerEtat();

            if (etat == EtatNavire.Neuf)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat1.png", UriKind.Relative));
            else if (etat == EtatNavire.peuDommage)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat2.png", UriKind.Relative));
            else if (etat == EtatNavire.TresDommage)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat3.png", UriKind.Relative));
            else if (etat == EtatNavire.Mort)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat4.png", UriKind.Relative));
        }
    }
}
