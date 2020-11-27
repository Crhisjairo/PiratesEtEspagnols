using System;
using System.Collections.Generic;

namespace PiratesEtEspagnols
{
    public abstract class Navire
    {
        public static List<Navire> ListeNavires = new List<Navire>();
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
       
        public Canon canon = new Canon();
        protected bool EstEnemiePirate { get; set; }
        protected double Efficacite { get; set; }
        protected Dictionary<string, double> PositionNavire { get; set; } = new Dictionary<string,double>();
        private double TailleNavire { get; set; }
        private double LongueurNavire { get; set; }


        public Navire(Canvas canvas)
        {
            PlacerNavireCanvas(canvas);
            TailleNavire = ActualHeight;
            LongueurNavire = ActualWidth;

            ListeNavires.Add(this);
        }


        public void PlacerNavireCanvas(Canvas canvas) 
        {
            double positionX = Canvas.GetLeft(this);
            double positionY = Canvas.GetTop(this);
           
            PositionNavire.Add("Haut", positionX);
            PositionNavire.Add("Bas", positionX + TailleNavire); //où est le canon
            PositionNavire.Add("Gauche", positionY);
            PositionNavire.Add("Droite", positionY + LongueurNavire);

        } 


        public virtual void Deplacer(Buttons button, Canvas canvas) //? pirate
        {
            double nouvellePositionX = PositionNavire["Gauche"];
            double nouvellePositionY = PositionNavire["Haut"];

            switch (button)
            {
                case Buttons.Bas:
                    nouvellePositionY += VitesseDeplacement;
                    if (!InterdireMouvement(canvas, nouvellePositionX, nouvellePositionY))
                    {
                        PositionNavire["Haut"] += VitesseDeplacement;
                        PositionNavire["Bas"] += VitesseDeplacement;
                    }
                    break;
                case Buttons.Haut:
                    nouvellePositionY -= VitesseDeplacement;
                    if (!InterdireMouvement(canvas, nouvellePositionX, nouvellePositionY))
                    {
                        PositionNavire["Haut"] -= VitesseDeplacement;
                        PositionNavire["Bas"] -= VitesseDeplacement;
                    }
                    break;
                case Buttons.Droit:
                    nouvellePositionX += VitesseDeplacement;
                    if (!InterdireMouvement(canvas, nouvellePositionX, nouvellePositionY))
                    {
                        PositionNavire["Droite"] += VitesseDeplacement;
                        PositionNavire["Gauche"] += VitesseDeplacement;
                    }
                    break;
                case Buttons.Gauche:
                    nouvellePositionX -= VitesseDeplacement;
                    if (!InterdireMouvement(canvas, nouvellePositionX, nouvellePositionY))
                    {
                        PositionNavire["Droite"] -= VitesseDeplacement;
                        PositionNavire["Gauche"] -= VitesseDeplacement;
                    }
                    break;
                default:
                    break;
            }

            Canvas.SetLeft(this, PositionNavire["Droite"]);
            Canvas.SetTop(this, PositionNavire["Haut"]);

        } 

        public bool InterdireMouvement(Canvas canvas, double positionX, double positionY)
        {
            bool mouvementInterdit = false;

            if (positionX < 0)
            {
                mouvementInterdit = true;
            }
            else if (positionX + LongueurNavire > canvas.ActualWidth)
            {
                mouvementInterdit = true;
            }

            if (positionY < 0)
            {
                mouvementInterdit = true;
            }
            else if (positionY + TailleNavire > canvas.ActualHeight)
            {
                mouvementInterdit = true;
            }

            if (verifierColisionAvecAutreNavire(positionX, positionY))
            {
                mouvementInterdit = true;
            }

            return mouvementInterdit;
        }


        //TODO
        public bool verifierColisionAvecAutreNavire(double positionX, double positionY)
        {
            bool colide = false;

            foreach (Navire autreNavire in ListeNavires)
            {
                if (!autreNavire.Equals(this))
                {
                    if (positionY > autreNavire.PositionNavire["Bas"])
                    {
                        if(positionY <0)
                    } 
                    else if (positionY + TailleNavire> autreNavire.PositionNavire["Haut"])
                    {

                    }
                }

               
            }

            return colide;
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

        public void ChangerEtat()
        {
            //Change l'état
        }
    }
}
