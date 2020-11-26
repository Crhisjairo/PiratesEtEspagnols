using System.Windows.Controls;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VuePirate.xaml
    /// </summary>
    public partial class VuePirate : UserControl
    {
        private Pirate _pirate = new Pirate();
        public VuePirate()
        {
            InitializeComponent();
        }
    }
}
