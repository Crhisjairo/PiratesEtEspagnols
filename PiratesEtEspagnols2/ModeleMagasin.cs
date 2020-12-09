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

    }
}
