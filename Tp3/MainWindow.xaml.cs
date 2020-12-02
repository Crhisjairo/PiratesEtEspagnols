using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Jeu _jeu = new Jeu();
        private List<VuePirate> ListePirates { get; set; } = new List<VuePirate>();
        private List<VueEscorte> ListeEscortes { get; set; } = new List<VueEscorte>();
        private List<VueGalion> ListeGalions { get; set; } = new List<VueGalion>();
        
        //private List<UserControl> ListeNavires { get; set; } = new List<UserControl>();

        private DispatcherTimer _horloge = new DispatcherTimer();
        
        public MainWindow()
        {
            //creer et placer les UserControl
            InitializeComponent();
            CreerNavirePirate(350, 890);
            CreerNavireGalion(350,10);
            CreerNavireEscorte(200, 150);
            CreerNavireEscorte(500, 150);

            CreerHorlogeMouvement();

        }

        private void CreerHorlogeMouvement()
        {
            _horloge.Interval = TimeSpan.FromMilliseconds(100);
            _horloge.IsEnabled = true;
            //Méthodes à executer à chaque tick
            _horloge.Tick += HorlogeAvance;

            _horloge.Start();
        }

        private void SetNiveau()
        {

        }

        /// <summary>
        /// Methode pour incluire un navire type Pirate dans le Jeu.
        /// </summary>
        /// <param name="left">l'emplacement "x" du navire</param>
        /// <param name="top">l'emplacement "y"du navire</param>
        private void CreerNavirePirate(int left, int top)
        {
            VuePirate pirate = new VuePirate();
            Surface.Children.Add(pirate);
            Canvas.SetLeft(pirate, left);
            Canvas.SetTop(pirate,top);
            ListePirates.Add(pirate);
            _jeu.AddListeNavires(1);
        }

        /// <summary>
        /// Methode pour incluire un navire type Galion dans le Jeu.
        /// </summary>
        /// <param name="left">l'emplacement "x" du navire</param>
        /// <param name="top">l'emplacement "y"du navire</param>
        private void CreerNavireGalion(int left, int top)
        {
            VueGalion galion = new VueGalion();

            Surface.Children.Add(galion);
            Canvas.SetLeft(galion, left);
            Canvas.SetTop(galion, top);
            ListeGalions.Add(galion);
            _jeu.AddListeNavires(2);
        }

        /// <summary>
        /// Methode pour incluire un navire type Escorte dans le Jeu.
        /// </summary>
        /// <param name="left">l'emplacement "x" du navire</param>
        /// <param name="top">l'emplacement "y"du navire</param>
        private void CreerNavireEscorte(int left, int top)
        {
            VueEscorte escorte = new VueEscorte();

            Surface.Children.Add(escorte);
            Canvas.SetLeft(escorte, left);
            Canvas.SetTop(escorte, top);
            ListeEscortes.Add(escorte);
            _jeu.AddListeNavires(3);
        }

        /// <summary>
        /// Replacer e ataquer a chaque tic de l'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvance(object sender, EventArgs e)
        {
            foreach (VuePirate pirate in ListePirates)
            {
                pirate.ReplacerNavire(Surface);
            }

            foreach (VueEscorte escorte in ListeEscortes)
            {
                escorte.MouvementerNavire(Surface);
            }

            foreach (VueGalion galion in ListeGalions)
            {
                galion.MouvementerNavire(Surface);
            }
        }

        /// <summary>
        /// Recevoir la comande de direction pour le navire pirate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Type de button appuie</param>
       private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            foreach (VuePirate pirate in ListePirates)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        pirate.MouvementerNavire(Buttons.Gauche);
                        break;
                    case Key.Right:
                        pirate.MouvementerNavire(Buttons.Droit);
                        break;
                    case Key.Up:
                        pirate.MouvementerNavire(Buttons.Haut);
                        break;
                    case Key.Down:
                        pirate.MouvementerNavire(Buttons.Bas);
                        break;
                    case Key.Space:
                        break;
                }
            }
            
        }


    }
}
