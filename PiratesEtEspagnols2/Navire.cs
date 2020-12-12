using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PiratesEtEspagnols
{
    public abstract class Navire
    {
        /// <summary>
        /// ***
        /// </summary>
        public bool EstHorsCombat { get; set; } = false;
        /// <summary>
        /// ***
        /// </summary>
        protected int CanonsCote { get; set; }
        /// <summary>
        /// Les membres initiales (vie) d'un navire.
        /// Utilisé pour calculer la puissance.***
        /// </summary>
        protected int MembresInitial { get; set; }
        /// <summary>
        /// Membres restants d'un navire.
        /// </summary>
        protected int MembresRestant { get; set; }
        /// <summary>
        /// ***
        /// </summary>
        public Canon Canon = null;
        /// <summary>
        /// ***
        /// </summary>
        protected int QuantiteOr { get; set; }
        /// <summary>
        /// ***
        /// </summary>
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

        /// <summary>
        /// ***
        /// </summary>
        /// <param name="pirate"></param>
        public virtual void EtreEvahis(ModelePirate pirate)
        {
            //Cette méthode a des références, elle est importante?
        }

    }
}
