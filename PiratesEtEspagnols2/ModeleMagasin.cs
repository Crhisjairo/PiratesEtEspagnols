using System.Security.Cryptography.X509Certificates;
using PiratesEtEspagnols;

namespace PiratesEtEspagnols2
{
    public class ModeleMagasin
    {
        /// <summary>
        /// Pirate qui contiendra l'or. Il est se crée dans la classe Jeu.
        /// </summary>
        private ModelePirate _pirate;

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

        public int GetOrPirate()
        {
            return _pirate.GetProprietesPirate(ProprietesPirate.Or);
        }
    }

    
}
