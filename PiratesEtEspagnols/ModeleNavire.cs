using System;

namespace PiratesEtEspagnols
{
    class ModeleNavire
    {
        /// <summary>
        /// Vitesse de deplacement du navire.
        /// </summary>
        protected int VitesseDeplacement { get; set; } = 1;
        /// <summary>
        /// Nombre des cannon droits
        /// </summary>
        protected int CannonsDroit { get; set; }
        /// <summary>
        /// Nombre des cannon gauches
        /// </summary>
        protected int CannonsGauche { get; set; }
        /// <summary>
        /// Nombre des cannon arrières
        /// </summary>
        protected int CannonsArrieres { get; set; } = 0;

        /// <summary>
        /// Membres initial pour calculer l'éfficacité.
        /// </summary>
        protected int MembresInitial { get; set; }

        /// <summary>
        /// Membres restant pour calculer la vie du navire.
        /// </summary>
        protected int MembresRestant { get; set; }
        /// <summary>
        /// État du navire.
        /// </summary>
        protected bool EstHorsCombat { get; set; } = false;
       
        protected Cannon cannon = new Cannon();
        protected bool EstEnemiePirate { get; set; }

        public ModeleNavire()
        {
        }

        public void Deplacer(Buttons button)
        {

        }


        public void Tirer()
        {

        }

        public void VerifierColition(double positionX, double positionY)
        {

        }

        public void PerdreMembres()
        {

        }

        private void CalculerEfficacite()
        {

        }
         
         
        private void VerifierHorsCombat()
        {
            if ((this.MembresInitial) / 3 >= this.MembresRestant)
            {
                EstHorsCombat = true;
            }
        }
        

    }
}
