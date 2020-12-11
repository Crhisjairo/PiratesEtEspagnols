using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PiratesEtEspagnols
{
    public class ModelePirate : Navire
    {
        

        public ModelePirate()
        {
            CanonsCote = 5;
            MembresInitial = 80;
            MembresRestant = MembresInitial;
            _canon = new Canon(1.5, 2, 10);
            QuantiteOr = 0; 
        }

        /// <summary>
        /// Ajoute à l'equipage des pirates des marins que desirent se convert
        /// </summary>
        /// <param name="equipageEnmie">le nombre de personnes à bord au moment de l'attaque</param>
        internal void VolerMembres(int equipageEnmie)//TODO
        {
            int  equipageConquis = 0;
            
            equipageConquis = Dispute(equipageEnmie) / 2;

            MembresRestant += equipageConquis;
        }

        /// <summary>
        /// Calcule combien de membres d'équipage du navire ennemi sont pris selon la règle (1 pirate vaut 5 marins royalles)
        /// </summary>
        /// <param name="equipageEnmie">le nombre de personnes à bord au moment de l'attaque</param>
        /// <returns>Le nombre d'ennemis capturés</returns>
        private int Dispute(int equipageEnmie)
        {
            // chaque 1 pirate est capable de capturer 5 membres de la marine espagnole
            int equipageCapture = MembresRestant * 5;

            if (equipageEnmie > equipageCapture)
            {
                return equipageCapture;
            }

            return equipageEnmie;
        }

        /// <summary>
        /// L'or obtenu des invasions des navires enemies et des vendes de armes est ajoute aux biens des pirates
        /// </summary>
        /// <param name="valeurRecupere">La quantite de or volé ou reçu par la vend d'un arme</param>
        internal void VolerOr(int valeurRecupere)//TODO
        {
            QuantiteOr += valeurRecupere;
        }

        /// <summary>
        /// Des armes volées pendent des invasions sont mis dans les biens des pirates.
        /// </summary>
        /// <param name="quantiteVole">La quantité d'armes volés</param>
        internal void VolerArmes(int quantiteVole)//TODO
        {
            QuantiteArmes += quantiteVole;
        }


    }
}
