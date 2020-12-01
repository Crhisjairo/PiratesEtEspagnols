using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VueGalion.xaml
    /// </summary>
    public partial class VueGalion : UserControl, IVueNavire
    {
        private static ModeleGalion _galion = new ModeleGalion();

        private const int Acceleration = 2;
        public double ChangementPositionX { get; set; }
        public double ChangementPositionY { get; set; }
        private double NextY { get; set; }
        private double NextX { get; set; }

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


        public void ReplacerNavire()
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
            
        }

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

            MouvementerNavire();
        }

        public void MouvementerNavire()
        {
            Canvas.SetTop(this, Canvas.GetTop(this) + ChangementPositionY);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + ChangementPositionX);
        }


        public List<double> PositionNavire()
        {
            List<double> ListePositionNavire = new List<double>();

            double gauche = NextX;//Canvas.GetLeft(this);
            ListePositionNavire.Add(gauche);
            double droit = gauche + ActualWidth;
            ListePositionNavire.Add(droit);

            double haut = NextY;//Canvas.GetTop(this);
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

    }
}
