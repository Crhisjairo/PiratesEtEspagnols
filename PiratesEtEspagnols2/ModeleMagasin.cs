using System.Security.Cryptography.X509Certificates;
using PiratesEtEspagnols;

namespace PiratesEtEspagnols2
{
    public class ModeleMagasin
    {
        /// <summary>
        /// Pirate qui contiendra l'or.
        /// On lui enlèvera son or lors d'un achat.
        /// Il est s'est crée dans la classe Jeu.
        /// </summary>
        private ModelePirate _pirate;

        /// <summary>
        /// Crée un modèle du magasin.
        /// </summary>
        /// <param name="pirate">Pirate qui va effectuer l'achat.</param>
        public ModeleMagasin(ModelePirate pirate)
        {
            _pirate = pirate;
        }

        /// <summary>
        /// Enlève l'or du pirate pour la propriété ajouté à ce même.
        /// </summary>
        /// <param name="p">Propriété à améliorer (article à acheter)</param>
        public void Acheter(ProprietesPirate p, int prix)
        {
            switch (p)
            {
                case ProprietesPirate.Membres:
                    _pirate.EnleverPropriete(ProprietesPirate.Or, prix); //Code adaptable pour le futur.
                    _pirate.AjouterPropriete(ProprietesPirate.Membres, 10); //la valeurAAjouter peut être changer. Code adaptable pour le futur.
                    break;
                case ProprietesPirate.Degats:
                    _pirate.EnleverPropriete(ProprietesPirate.Or, prix);
                    _pirate.AjouterPropriete(ProprietesPirate.Degats, 10);
                    break;
                case ProprietesPirate.Cannons:
                    _pirate.EnleverPropriete(ProprietesPirate.Or, prix);
                    _pirate.AjouterPropriete(ProprietesPirate.Cannons, 10);
                    break;
            }
        }

        /// <summary>
        /// Permet de récuperer l'or du pirate pour l'afficher dans le magasin.
        /// </summary>
        /// <returns>L'or du pirate.</returns>
        public int GetOrPirate()
        {
            return _pirate.GetProprietesPirate(ProprietesPirate.Or);
        }
    }

    
}
