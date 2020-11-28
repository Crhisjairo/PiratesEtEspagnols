using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VuePirate.xaml
    /// </summary>
    public partial class VuePirate : UserControl
    {
        private static ModelePirate _modelePirate = new ModelePirate();


        private const int Acceleration = 8;
        public double ChangementPositionX { get; set; }
        public double ChangementPositionY { get; set; }

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
        public void MouvementerNavire(Buttons direction)
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

        public void ReplacerNavire(Canvas surface)
        {
            ValiderMouvement(surface);

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
                ChangementPositionY = 0;
            }

            if (nextX < 0)
            {
                ChangementPositionX = 0;
            }
            else if (nextX + ActualWidth > surface.ActualWidth)
            {
                ChangementPositionX = 0;
            }

        }

    }
}
