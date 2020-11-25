using System.Windows.Controls;
using PiratesEtEspagnols;

namespace Tp3
{
    /// <summary>
    /// Logique d'interaction pour VueEscorte.xaml
    /// </summary>
    public partial class VueEscorte : UserControl
    {
        private ModeleEscorte _escorte = new ModeleEscorte();

        public VueEscorte()
        {
            InitializeComponent();
        }
    }
}
