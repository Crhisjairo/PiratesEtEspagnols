using System.Windows;
using System.Windows.Controls;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Jeu jeu = new Jeu();

        public MainWindow()
        {
            InitializeComponent();

            SetPersonnagesAuCanvas();
        }

        private void SetPersonnagesAuCanvas()
        {
            //Canvas.SetLeft(Pirate, jeu.GetPositionPirateX());
            //Canvas.SetTop(Pirate, jeu.GetPositionPirateY());
        }
    }
}
