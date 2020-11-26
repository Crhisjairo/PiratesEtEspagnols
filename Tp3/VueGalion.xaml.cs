using System.Windows.Controls;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VueGalion.xaml
    /// </summary>
    public partial class VueGalion : UserControl
    {
        private ModeleGalion _galion = new ModeleGalion();
        public VueGalion()
        {
            InitializeComponent();
        }
    }
}
