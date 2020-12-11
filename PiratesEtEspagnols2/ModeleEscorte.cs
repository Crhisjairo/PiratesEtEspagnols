using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleEscorte : Navire
    {
        
        /*private TypeEscorte type = TypeEscorte.Cheval;
        */

        public ModeleEscorte()
        {
            CanonsCote = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            Canon = new Canon(0.3, 10, 15);
            DeterminerQuantiteArmes();
        }

        /// <summary>
        /// Determiner au hasard combien d'armes l'escorte possede.
        /// </summary>
        private void DeterminerQuantiteArmes()
        {
            Random random = new Random();
            QuantiteArmes = random.Next(0,36);
        }

        /// <summary>
        /// Subir l'attaque final des pirates.
        /// </summary>
        /// <param name="pirate">Le navire qui attaque</param>
        public override void EtreEvahis(ModelePirate pirate)
        {
            pirate.VolerArmes(QuantiteArmes);
            pirate.VolerMembres(MembresRestant);

            QuantiteArmes = 0;
            MembresRestant = 0;
        }
    }





    public enum TypeEscorte
    {
        Epee,
        Cheval,
        Croix
    }
}
