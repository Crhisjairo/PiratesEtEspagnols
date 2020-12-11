namespace PiratesEtEspagnols
{
    public class ModelePirate : Navire
    {
        public ModelePirate()
        {
            CanonsCote = 5;
            MembresInitial = 80;
            MembresRestant = MembresInitial;
            Canon = new Canon(1.5, 5, 10);
            QuantiteOr = 500; 
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

        public string GetBiens()
        {
            string text = "\n- Armes : ";
            text += QuantiteArmes.ToString();
            text += "\n- Or : ";
            text += QuantiteOr.ToString();

            return text;
        }

        /// <summary>
        /// Modifie les propriétés du pirate.
        /// Principalement, le magasin utilisera cette fonction.
        /// Ces propriétés sont: Sa vie, son dégât et ses cannons.
        /// </summary>
        /// <param name="p">Propriété à modifier.</param>
        /// <param name="nouvelleValeur">Nouvelle valeur de la propriété.</param>
        public void SetPropriete(ProprietesPirate p, int nouvelleValeur)
        {
            switch (p)
            {
                case ProprietesPirate.Membres:
                    MembresRestant = nouvelleValeur;
                    break;
                case ProprietesPirate.Degats:
                    QuantiteArmes = nouvelleValeur;
                    break;
                case ProprietesPirate.Cannons:
                    CanonsCote = nouvelleValeur;
                    break;
                case ProprietesPirate.Or:
                    QuantiteOr = nouvelleValeur;
                    break;
            }
        }

        /// <summary>
        /// Ajoute une valeur à la propriété choisit.
        /// </summary>
        /// <param name="p">Propriété à modifier.</param>
        /// <param name="valeurAAjouter">Nouvelle valeur de la propriété.</param>
        public void AjouterPropriete(ProprietesPirate p, int valeurAAjouter)
        {
            switch (p)
            {
                case ProprietesPirate.Membres:
                    MembresRestant += valeurAAjouter;
                    break;
                case ProprietesPirate.Degats:
                    QuantiteArmes += valeurAAjouter;
                    break;
                case ProprietesPirate.Cannons:
                    CanonsCote += valeurAAjouter;
                    break;
                case ProprietesPirate.Or:
                    QuantiteOr += valeurAAjouter;
                    break;
            }
        }

        public void EnleverPropriete(ProprietesPirate p, int valeurAEnlever)
        {
            switch (p)
            {
                case ProprietesPirate.Membres:
                    MembresRestant -= valeurAEnlever;
                    break;
                case ProprietesPirate.Degats:
                    QuantiteArmes -= valeurAEnlever;
                    break;
                case ProprietesPirate.Cannons:
                    CanonsCote -= valeurAEnlever;
                    break;
                case ProprietesPirate.Or:
                    QuantiteOr -= valeurAEnlever;
                    break;
            }
        }

        public int GetProprietesPirate(ProprietesPirate p)
        {
            switch (p)
            {
                case ProprietesPirate.Membres:
                    return MembresRestant;
                case ProprietesPirate.Degats:
                    return QuantiteArmes;
                case ProprietesPirate.Cannons:
                    return CanonsCote;
                case ProprietesPirate.Or:
                    return QuantiteOr;
                default:
                    return -1;
            }
        }
    }


    public enum ProprietesPirate
    {
        Membres,
        Degats,
        Cannons,
        Or
    }
}
