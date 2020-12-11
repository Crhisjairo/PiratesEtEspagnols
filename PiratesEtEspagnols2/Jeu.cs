using System.Collections.Generic;

namespace PiratesEtEspagnols
{
    public class Jeu
    {
        private const int Max_galions = 1;
        private const int Max_escortes = 5;
        private const int Max_pirate = 1;

        private Dictionary<int, Navire> _dicModeleNavires = new Dictionary<int, Navire>();
        private ModelePirate _pirate = new ModelePirate();

        static void Main(string[] args)
        {

        }

        public Jeu()
        {
            
        }

        /// <summary>
        /// Crée les modelesNavires, 
        /// </summary>
        public void PreparerJeu()
        {
            CreerModelesDeNavires();
        }

        /// <summary>
        /// Créer des modeles de navires (Galions, escortes et pirate) avec un clé pour associer à
        /// sa propre vue.
        /// </summary>
        private void CreerModelesDeNavires()
        {
            int cle = 0;

            //Créer galions avec clé correspondante
            for (int i = 0; i < Max_galions; i++, cle++)
            {
                _dicModeleNavires.Add(cle, new ModeleGalion());
            }

            //Créer escortes à partir de la fin de la clé precedente
            for (int i = 0; i < Max_escortes; i++, cle++)
            {
                _dicModeleNavires.Add(cle, new ModeleEscorte());
            }

            //Le pirate est déjà crée.
            
        }

        /// <summary>
        /// Utilisé returner la quantité d'enimeis crées.
        /// </summary>
        /// <returns>Nombre de navires créés</returns>
        public int GetNombreNavires()
        {
            return _dicModeleNavires.Count;
        }

        /// <summary>
        /// Permet de recuperer le pirate pour utiliser ses propriétés.
        /// Ex: l'or, la vitesse, la puissance, etc.
        /// </summary>
        /// <returns></returns>
        public ModelePirate GetPirate()
        {
            return _pirate;
        }

        public Navire GetNavire(int cle)
        {
            return _dicModeleNavires[cle];
        }
    }
}
