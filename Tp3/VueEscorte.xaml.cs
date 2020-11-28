using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VueEscorte.xaml
    /// </summary>
    public partial class VueEscorte : UserControl
    {
        private static ModeleEscorte _escorte = new ModeleEscorte();
        private TypeEscorte typeEscorte;
        
        private const int Acceleration = 4;
        public double ChangementPositionX { get; set; }
        public double ChangementPositionY { get; set; }

        public VueEscorte()
        {
            InitializeComponent();
            GenererImageAleatoire();
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
            }else if (typeEscorte == TypeEscorte.Croix)
            {
                ChangerEtatTypeCroix(etat);
            }else if (typeEscorte == TypeEscorte.Epee)
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

        /// <summary>
        /// Donne une image d'escorte aléatoirement.
        /// Il existe 3 types de navires escorte: Cheval, Croix et Epee.
        /// </summary>
        private void GenererImageAleatoire()
        {
            Random ran = new Random();

            switch (ran.Next(0, 3))
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
        /// Fait le choix vers quelle direction le navire doit aller.
        /// </summary>
        /// <param name="surface">La surface danns laquele le navire est placé</param>
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

        /// <summary>
        /// Verifie se le navire se mantien dedans le Canvas s'il fait le mouvement.
        /// </summary>
        /// <param name="surface">L'espace où le navire peut se mouvementer</param>
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

        /// <summary>
        /// Sert à placer le navire dans le canvas.
        /// </summary>
        public void ReplacerNavire()
        {

            Canvas.SetTop(this, Canvas.GetTop(this) + ChangementPositionY);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + ChangementPositionX);
            
        }

    }
}
