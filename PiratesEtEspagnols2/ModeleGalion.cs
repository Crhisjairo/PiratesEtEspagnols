using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleGalion : Navire
    {
        

        internal int CanonsArrier { get; set; }

        public ModeleGalion()
        {
            CanonsCote = 36;
            CanonsArrier = 2; 
            MembresInitial = 200;
            MembresRestant = MembresInitial;
            _canon = new Canon(0.3, 10, 20);
            DeterminerQuantiteOr();
        }

        /// <summary>
        /// Determiner au hasard combien d'or le galion possede.
        /// </summary>
        private void DeterminerQuantiteOr()
        {
            Random random = new Random();
            QuantiteOr = random.Next(10, 500);
        }

        /// <summary>
        /// Subir l'attaque final des pirates.
        /// </summary>
        /// <param name="pirate">Le navire qui attaque</param>
        public override void EtreEvahis(ModelePirate pirate)
        {
            pirate.VolerOr(QuantiteOr);
            pirate.VolerMembres(MembresRestant);

            QuantiteOr = 0;
            MembresRestant = 0;
        }

    }
}
