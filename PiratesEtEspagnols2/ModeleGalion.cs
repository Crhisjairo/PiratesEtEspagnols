using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleGalion : Navire
    {
        
        ///***
        internal int CanonsArrier { get; set; }

        /// <summary>
        /// Crée un modèle d'un galion espagnol.
        /// </summary>
        public ModeleGalion()
        {
            CanonsCote = 36;
            CanonsArrier = 2; 
            MembresInitial = 200;
            MembresRestant = MembresInitial;
            Canon = new Canon(0.3, 15, 20);
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
        /// <param name="pirate">Le navire qui attaque.</param>
        public override void EtreEvahis(ModelePirate pirate)
        {
            pirate.VolerOr(QuantiteOr);
            pirate.VolerMembres(MembresRestant);

            QuantiteOr = 0;
            MembresRestant = 0;
        }

        /// <summary>
        /// Donne le attaque par l'arrier que le navire peut faire d'accord avec l'efficience de l'equipage, la cantité de cannons et la puissance du canon.
        /// Si jamais le temps de recharge dès le dernier tir n'est pas respecté la methode retourne un ataque de ZERO.
        /// </summary>
        /// <param name="tick">Moment du jeux dont le navire essaye de tirer avec ses canons d'arrier.</param>
        /// <returns></returns>
        public int TirerArrier(int tick)
        {
            int attaque = 0;
            double efficience = (double)MembresInitial / MembresRestant;
            int tempsRecharche = tick - Canon.DernierTir;

            if (Canon.TempsRecharge <= tempsRecharche)
            {
                Canon.DernierTir = tick;
                attaque = (int)(Canon.Puissance * CanonsArrier * efficience);
            }

            if (EstHorsCombat)
            {
                attaque = 0;
            }

            return attaque;
        }

    }
}
