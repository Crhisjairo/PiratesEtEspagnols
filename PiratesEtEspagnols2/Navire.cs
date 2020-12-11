using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PiratesEtEspagnols
{
    public abstract class Navire
    {
        public bool EstHorsCombat { get; set; } = false;

        protected int CanonsCote { get; set; }
        protected int MembresInitial { get; set; }
        protected int MembresRestant { get; set; }
        public Canon Canon = null;
        protected int QuantiteOr { get; set; }
        protected int QuantiteArmes { get; set; }

        public Navire()
        {
        }

        /// <summary>
        /// Donne le attaque que le navire peut faire d'accord avec l'efficience de l'equipage, la cantité de cannons et la puissance du canon.
        /// Si jamais le temps de recharge dès le dernier tir n'est pas respecté la methode retourne uun ataque de ZERO.
        /// </summary>
        /// <param name="tick">Moment du jeux dont le navire essaye de tirer</param>
        /// <returns></returns>
        public double Tirer(int tick)
        {
            double attaque = 0;
            double efficience = (double)MembresInitial / MembresRestant;
            int tempsRecharche = tick - Canon.DernierTir;

            if (Canon.TempsRecharge <= tempsRecharche)
            {
                Canon.DernierTir = tick;
                attaque = Canon.Puissance * CanonsCote * efficience;
            }

            if (EstHorsCombat)
            {
                attaque = 0;
            }

            return attaque;
        }

        /// <summary>
        /// Verifier la perde après un attaque enimie.
        /// Si l'equipage restant est plus petit que 1/3 de la quantité de membres initial le navire est hors combat.
        /// </summary>
        /// <param name="attaque">La force du attaque subit</param>
        public void EtreAttaque(int attaque)
        {
            if (MembresRestant - attaque <= MembresInitial/3)
            {
                if (MembresRestant - attaque < 0)
                {
                    MembresRestant = 0;
                }
                else
                {
                    MembresRestant -= attaque;
                }
                EstHorsCombat = true;
            }
            else
            {
                MembresRestant -= attaque;
            }
        }

        /// <summary>
        /// Verifie combien de membres sont encore en vie
        /// </summary>
        /// <returns>La vie actuel du navire</returns>
        public int DonnerQuantiteMembresRestants()
        {
            return MembresRestant;
        }

        public virtual void EtreEvahis(ModelePirate pirate)
        {
        }

    }
}
