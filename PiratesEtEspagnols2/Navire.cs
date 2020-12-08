using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PiratesEtEspagnols
{
    public abstract class Navire
    {
        public static List<Navire> ListeNavires = new List<Navire>();
        
        public Canon _canon = new Canon(10, 20, 20); //*JUSTE POUR COMPILER
        protected int CanonsCote { get; set; }
        protected int MembresInitial { get; set; }
        protected int MembresRestant { get; set; }
        protected bool EstHorsCombat { get; set; } = false;
        public bool EstEnemiePirate { get; set; }
        
        public Navire()
        {
        }

        public double Tirer(int tick)
        {
            double attaque = 0;
            double efficience = (double)MembresInitial / MembresRestant;
            int tempsRecharche = tick - _canon.DernierTir;

            if (_canon.TempsRecharge <= tempsRecharche)
            {
                _canon.DernierTir = tick;
                attaque = _canon.Puissance * CanonsCote * efficience;
            }

            return attaque;
        }

        public void EtreAttaque(int attaque)
        {

            if (MembresRestant - attaque <= MembresInitial/3)
            {
                MembresRestant -= attaque;
                EstHorsCombat = true;
            }
            else
            {
                MembresRestant -= attaque;
            }
        }

        public int DonnerQuantiteMembresRestants()
        {
            return MembresRestant;
        }
    }
}
