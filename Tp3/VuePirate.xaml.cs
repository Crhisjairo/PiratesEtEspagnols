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
       
        // images/Navires/Pirate/PirateEtat1.png
        public VuePirate()
        {
            InitializeComponent();
        }

        public void ChangerEtat(EtatNavire etat)
        {
            _modelePirate.ChangerEtat();

            if (etat == EtatNavire.Neuf)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat3.png"));
            else if (etat == EtatNavire.Mort)
                ImagePirate.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat4.png"));
        }
    }
}
