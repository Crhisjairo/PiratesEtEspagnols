using System;
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
        private Navire _modelePirate = new ModelePirate();


        private const int Acceleration = 4;
        internal int TickHorloge { set; get; } = 0;
        private double ChangementPositionX { get; set; }
        private double ChangementPositionY { get; set; }



        // images/Navires/Pirate/PirateEtat1.png
        public VuePirate()
        {
            InitializeComponent();
        }

        public void ChangerEtat(EtatNavire etat)
        {
           //_modelePirate.ChangerEtat();

            if (etat == EtatNavire.Neuf)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat3.png"));
            else if (etat == EtatNavire.Mort)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat4.png"));
        }

        /// <summary>
        /// Defire vers quelle direction le navire doit se deplacer.
        /// </summary>
        /// <param name="direction">Reçoit le blutton clique par l'utilisateur</param>
        public void ReplacerNavirePirate(Buttons direction)
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
            }
        }

        /// <summary>
        /// Fait le choix vers quelle direction le navire doit aller.
        /// </summary>
        /// <param name="surface">La surface danns laquele le navire est placé</param>
        public void MouvementerNavire()
        {

            Canvas.SetTop(this, Canvas.GetTop(this) + ChangementPositionY);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + ChangementPositionX);
            ChangementPositionY = 0;
            ChangementPositionX = 0;
        }

        /// <summary>
        /// Valide si le mouvement est valide(le navire se mantien dans le canvas et ne surpasse autre navire)
        /// </summary>
        /// <param name="surface"> C'est le canvas où tous les élements sont placées</param>
        public void ValiderMouvement(Canvas surface)
        {
            double nextY = Canvas.GetTop(this) + ChangementPositionY;
            double nextX = Canvas.GetLeft(this) + ChangementPositionX;

            if (nextY < 0)
            {
                ChangementPositionY = 0;
            }
            else if (nextY + ActualHeight > surface.ActualHeight)
            {
                ChangementPositionY = surface.ActualHeight - (nextY + ActualHeight);
            }

            if (nextX < 0)
            {
                ChangementPositionX = 0;
            }
            else if (nextX + ActualWidth > surface.ActualWidth)
            {
                ChangementPositionX = surface.ActualWidth - (nextX + ActualWidth);
            }

        }

        public List<double> PositionNavire()
        {
            List<double> ListePositionNavire = new List<double>();
            
            double gauche = Canvas.GetLeft(this) + ChangementPositionX;
            ListePositionNavire.Add(gauche);
            double droit = gauche + ActualWidth;
            ListePositionNavire.Add(droit);

            double haut = Canvas.GetTop(this) + ChangementPositionY;
            ListePositionNavire.Add(haut);
            double bas = haut + ActualHeight;
            ListePositionNavire.Add(bas);
            
            return ListePositionNavire;
        }

        
        public List<double> PositionTireNavire()
        {
            List<double> ListePositionNavire = new List<double>();

            double gauche = Canvas.GetLeft(this); 
            ListePositionNavire.Add(gauche - _modelePirate._canon.ChamDeTire);
            double droit = gauche + ActualWidth;
            ListePositionNavire.Add(droit + _modelePirate._canon.ChamDeTire);

            double haut = Canvas.GetTop(this);
            ListePositionNavire.Add(haut);
            double bas = haut + ActualHeight;
            ListePositionNavire.Add(bas);

            return ListePositionNavire;
        }


        public void BloquerMouvement()
        {
            ChangementPositionY = 0;
            ChangementPositionX = 0;
        }

        public int Tirer()
        {
            double attaque = _modelePirate.Tirer(TickHorloge);
            return (int) attaque;
        }

        public string GetVie()
        {
            string textVie = "Vie Pirate : ";
            int vie = (_modelePirate).DonnerQuantiteMembresRestants();
            return textVie + vie.ToString();
        }

        public Navire GetTypeNavire()
        {
            return _modelePirate;
        }

        public void SubirAttaque(int forceAttaque, bool estEnemiePirate)
        {
            if (estEnemiePirate != _modelePirate.EstEnemiePirate)
            {
                _modelePirate.EtreAttaque(forceAttaque);
            }
        }
    }
}
