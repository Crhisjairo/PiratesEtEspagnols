using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class Jeu
    {
        private ModeleGalion _galion = new ModeleGalion();
        private List<ModeleEscorte> _escortes = new List<ModeleEscorte>();
        private ModelePirate _pirate = new ModelePirate();

        static void Main(string[] args)
        {
            
        }

        public Jeu()
        {

        }

        public double GetPositionPirateX()
        {
            return 0;
        }

        public double GetPositionPirateY()
        {
            return 0;
        }

    }
}
