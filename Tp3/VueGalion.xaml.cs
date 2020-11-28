using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VueGalion.xaml
    /// </summary>
    public partial class VueGalion : UserControl
    {
        private static ModeleGalion _galion = new ModeleGalion();

        private const int Acceleration = 2;
        public double ChangementPositionX { get; set; }
        public double ChangementPositionY { get; set; }

        public VueGalion()
        {
            InitializeComponent();
        }
        public void ChangerEtat(EtatNavire etat)
        {
           // _galion.ChangerEtat();

            if (etat == EtatNavire.Neuf)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat3.png"));
            else if (etat == EtatNavire.Mort)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat4.png"));
        }




        public void MouvementerNavire(Canvas surface)
        {
            Random random = new Random();
            int choixDirection = random.Next(45);

            if (choixDirection < 10)
            {
                ChangementPositionY += Acceleration;
            }
            else if (choixDirection < 20)
            {
                ChangementPositionY -= Acceleration;
            }
            else if (choixDirection < 30)
            {
                ChangementPositionX -= Acceleration;
            }
            else if (choixDirection < 40)
            {
                ChangementPositionX += Acceleration;
            }
            else
            {
                ChangementPositionX += 0;
            }

            ValiderMouvement(surface);

            ReplacerNavire();

        }

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

        public void ReplacerNavire()
        {

            Canvas.SetTop(this, Canvas.GetTop(this) + ChangementPositionY);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + ChangementPositionX);

        }

    }
}
