using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PiratesEtEspagnols
{
    public abstract class Navire
    {
        public static List<Navire> ListeNavires = new List<Navire>();
        
        protected int VitesseDeplacement { get; set; } = 1;
        protected double Efficacite { get; set; }

        public Canon canon = new Canon();
        protected int CanonsCote { get; set; }
        protected int CanonsAvant { get; set; } = 0;
        
        protected int MembresInitial { get; set; }
        protected int MembresRestant { get; set; }
        protected bool EstHorsCombat { get; set; } = false;

        
        protected bool EstEnemiePirate { get; set; }
        
        private double TailleNavire { get; set; }
        private double LongueurNavire { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public Navire()
        {
        }


    }
}
