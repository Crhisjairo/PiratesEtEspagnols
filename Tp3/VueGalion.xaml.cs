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
        public VueGalion()
        {
            InitializeComponent();
        }
        public void ChangerEtat(EtatNavire etat)
        {
            _galion.ChangerEtat();

            if (etat == EtatNavire.Neuf)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat1.png"));
            else if (etat == EtatNavire.peuDommage)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat2.png"));
            else if (etat == EtatNavire.TresDommage)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat3.png"));
            else if (etat == EtatNavire.Mort)
                ImageGalion.Source = new BitmapImage(new Uri("images/Navires/Pirate/PirateEtat4.png"));
        }
    }
}
