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
        private List<IVueNavire> ListeNavire { get; set; } = new List<IVueNavire>();

        //private List<UserControl> ListeNavires { get; set; } = new List<UserControl>();

        private DispatcherTimer _horloge = new DispatcherTimer();

        public MainWindow()
        {
            //creer et placer les UserControl
            InitializeComponent();
            CreerNavire(350, 980, "pirate");
            CreerNavire(350,10, "galion");
            CreerNavire(200, 150, "escorte");
            CreerNavire(500, 150, "escorte");


            //creer horloge
            _horloge.Interval = TimeSpan.FromMilliseconds(100);
            _horloge.IsEnabled = true;
            _horloge.Tick += HorlogeAvance;
            _horloge.Start();
        }

        /// <summary>
        /// Methode pour incluire un navire dans le Jeu.
        /// </summary>
        /// <param name="left">l'emplacement "x" du navire</param>
        /// <param name="top">l'emplacement "y"du navire</param>
        /// <param name="type">si le navire est un Galion, un pirate ou un escorte</param>
        private void CreerNavire(int left, int top, string type)
        {
            IVueNavire navire = null;
            if(type == "pirate")
            {
                navire = new VuePirate();
                _jeu.AddListeNavires(1);
            } else if (type == "galion")
            {
                navire = new VueGalion();
                _jeu.AddListeNavires(2);
            } else if (type == "escorte")
            {
                navire = new VueEscorte();
                _jeu.AddListeNavires(3);
            }

            Surface.Children.Add((UIElement)navire);
            Canvas.SetLeft((UIElement)navire, left);
            Canvas.SetTop((UIElement)navire, top);
            ListeNavire.Add(navire);
            
        }
        
        /// <summary>
        /// Replacer e ataquer a chaque tic de l'horloge.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HorlogeAvance(object sender, EventArgs e)
        {

            for (int i = 0; i < ListeNavire.Count; i++)
            {
                if (ListeNavire[i] is VueGalion)
                {
                    ((VueGalion)ListeNavire[i]).ReplacerNavire();
                } 
                else if (ListeNavire[i] is VueEscorte)
                {
                    ((VueEscorte)ListeNavire[i]).ReplacerNavire();
                }

                ListeNavire[i].ValiderMouvement(Surface);
                EviterColision(ListeNavire[i]);
                ListeNavire[i].MouvementerNavire();
            }

        }

        /// <summary>
        /// Recevoir la comande de direction pour le navire pirate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Type de button appuie</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (IVueNavire navire in ListeNavire)
            {
                if (navire is VuePirate)
                {
                    switch (e.Key)
                    {
                        case Key.Left:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Gauche);
                            break;
                        case Key.Right:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Droit);
                            break;
                        case Key.Up:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Haut);
                            break;
                        case Key.Down:
                            ((VuePirate)navire).ReplacerNavirePirate(Buttons.Bas);
                            break;
                        case Key.Space:
                            break;
                    }
                }
            }

        }


        public void EviterColision(IVueNavire navireReference)
        {
            List<double> positionNavireReference = navireReference.PositionNavire();
            List<double> positionNavireEvalue = null;

            foreach (IVueNavire navireEvalue in ListeNavire)
            {
                if (!Object.ReferenceEquals(navireEvalue, navireReference))
                {
                    positionNavireEvalue = navireEvalue.PositionNavire();

                    if ((positionNavireReference[2] >= positionNavireEvalue[2] && positionNavireReference[2] <= positionNavireEvalue[3]) ||
                        (positionNavireReference[3] <= positionNavireEvalue[3] && positionNavireReference[3] >= positionNavireEvalue[2]))
                    {
                        if ((positionNavireReference[0] >= positionNavireEvalue[0] && positionNavireReference[0] <= positionNavireEvalue[1]) ||
                            (positionNavireReference[1] <= positionNavireEvalue[1] && positionNavireReference[1] >= positionNavireEvalue[0]))
                        {
                            navireReference.BloquerMouvement();
                        }
                    }
                }
            }
        }


    }
}
