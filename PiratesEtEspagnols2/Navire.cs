using System;
using System.Collections.Generic;

namespace PiratesEtEspagnols
{
    public class Navire
    {
        /// <summary>
        /// Vitesse de deplacement du navire.
        /// </summary>
        protected int VitesseDeplacement { get; set; } = 1;
        /// <summary>
        /// Nombre des canon droits
        /// </summary>
        protected int CanonsDroit { get; set; }
        /// <summary>
        /// Nombre des canon gauches
        /// </summary>
        protected int CanonsGauche { get; set; }
        /// <summary>
        /// Nombre des canon arrières
        /// </summary>
        protected int CanonsArrier { get; set; } = 0;
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
       
        protected Canon canon = new Canon();
        protected bool EstEnemiePirate { get; set; }
        protected double Efficacite { get; set; }

        protected Dictionary<string, double> PositionNavire { get; set; } = new Dictionary<string,double>();
        
        //TODO
        private double TailleNavire = 10.0;
        private double LongueurNavire = 2.0;
        double positionX = 0.0;
        double positionY = 0.0;

        public Navire()
        {
            PlacerNavireCanvas();
        }

        public virtual void PlacerNavireCanvas() //changer pour le NavirePirate - public override void PlacerNavireCanvas()
        {
            PositionNavire.Add("Arrier", positionX);
            PositionNavire.TryGetValue("Arrier", out positionX);
            PositionNavire.Add("Front", positionX + TailleNavire);
            PositionNavire.Add("Gauche", positionY);
            PositionNavire.TryGetValue("Gauche", out positionY);
            PositionNavire.Add("Droite", positionY + LongueurNavire);
        }


        //TODO
        public void Deplacer(Buttons button)
        {
            switch (button)
            {
                case Buttons.Bas:
                    PositionNavire["Arrier"] = 40.2;
                    break;

            }    
        }

        // TODO
        public void Tirer()
        {
            VerifierHorsCombat();
            
            if (!EstHorsCombat)
            {
                
                //CalculerEfficaciteAttaque();
            }
        }

        public void VerifierProximite(double positionX, double positionY)
        {

        }

        
        /// <summary>
        /// Calcule quelle est la puissance de l'attaque d'accord avec la quantité de canons, la puissance des canons et la quantité d'équipage vive.
        /// </summary>
        /// <param name="cote">L'attaque est fait de quelle cotê nu navire</param>
        /// <returns>La puissance de l'attaque du navire verns son enimie</returns>
        private double CalculerEfficaciteAttaque (CoteCanon cote)
        {
            double puissanceAttaque = 0;
            switch (cote)
            {
                case CoteCanon.Arrier:
                    puissanceAttaque= this.CanonsArrier * this.canon.Puissance;
                    break;
                case CoteCanon.Droite:
                    puissanceAttaque= this.CanonsDroit * this.canon.Puissance;
                    break;
                case CoteCanon.Gauche:
                    puissanceAttaque = this.CanonsGauche * this.canon.Puissance;
                    break;
            }

            puissanceAttaque *= VerifierEfficacite();

            return puissanceAttaque;
        }

        /// <summary>
        /// Calculer l'efficacité du navire d'accord avec l'équipage restant.
        /// </summary>
        /// <returns>L'efficacité d'opération du navire. Elle doit être entre 0 et 1</returns>
        private double VerifierEfficacite()
        {
            this.Efficacite = (double)MembresRestant/(double)MembresInitial;
            return Efficacite;
        }

        /// <summary>
        /// Vérifie si le bateau est encore capable de jouer.
        /// Quand la quantité de membres restant est égal ou plus petit que 1/3 de l'équipage initial le navire doit sortir du combat.
        /// </summary>
        private void VerifierHorsCombat()
        {
            if ((this.MembresInitial) / 3 >= this.MembresRestant)
            {
                EstHorsCombat = true;
            }
        }

        public void PerdreMembres()
        {

        }
    }
}
