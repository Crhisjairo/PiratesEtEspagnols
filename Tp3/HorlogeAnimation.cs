using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tp3
{
    public abstract class HorlogeAnimation
    {
        private static DispatcherTimer _horlogeAnimation = new DispatcherTimer();

        private static double CompteurBackground { get; set; } = 0;
        private static double CompteurAffichageNiveau { get; set; } = 0;

        public static void DemarrerHorloge(int IntervalSecondes)
        {
            _horlogeAnimation.Interval = TimeSpan.FromSeconds(IntervalSecondes);
            _horlogeAnimation.IsEnabled = true;
            //Méthodes à executer à chaque tick
            _horlogeAnimation.Tick += HorlogeAnimationAvance;
        }

        private static void HorlogeAnimationAvance(object sender, EventArgs e)
        {
            CompteurBackground++;
            CompteurAffichageNiveau++;
        }

        public static bool EstCompteurBackgroundFinit()
        {
            if (CompteurBackground == 5)
            {
                return true;
            }

            return false;
        }
    }
}
