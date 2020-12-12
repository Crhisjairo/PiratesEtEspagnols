using System.Collections.Generic;

namespace PiratesEtEspagnols
{
    public class Jeu
    {
        /// <summary>
        /// Nombre par défaut des navires. 
        /// </summary>
        private static Dictionary<string, int> NavireParDefaut = new Dictionary<string, int>()
        {
            {"galions" , 1},
            {"escortes", 3},
            {"pirates", 1}
        };

        /// <summary>
        /// Galions correspondants au niveau de jeu.
        /// </summary>
        private int _galionsDePartie = NavireParDefaut["galions"];
        /// <summary>
        /// Escortes correspondants au niveau de jeu.
        /// </summary>
        private int _escortesDePartie = NavireParDefaut["escortes"];
        /// <summary>
        /// Pirates correspondants au niveau de jeu.
        /// </summary>
        private int _piratesDePartie = NavireParDefaut["pirates"];
        /// <summary>
        /// Niveau du jeu.
        /// </summary>
        private int _niveau = 1;
        /// <summary>
        /// Logique des navires associés à une clé pour les rélier au vues.
        /// </summary>
        private Dictionary<int, Navire> _dicModeleNavires;
        /// <summary>
        /// Logique du pirate. Il est strictement lié à une vue pirate.
        /// </summary>
        private ModelePirate _pirate;

        static void Main(string[] args)
        {
        }

        /// <summary>
        /// Création d'un jeu.
        /// </summary>
        public Jeu()
        {
            
        }

        /// <summary>
        /// Prépare le jeu.
        /// Il initialise le pirate et les navires. 
        /// </summary>
        public void PreparerJeu()
        {
            _pirate = new ModelePirate();
            _dicModeleNavires = new Dictionary<int, Navire>();

            CreerModelesDeNavires();
        }

        /// <summary>
        /// Créer des modeles de navires (Galions, escortes et pirate) avec un clé pour l'associer à
        /// sa propre vue.
        /// </summary>
        private void CreerModelesDeNavires()
        {
            int cle = 0;

            //Créer galions avec clé correspondante
            for (int i = 0; i < _galionsDePartie; i++, cle++)
            {
                _dicModeleNavires.Add(cle, new ModeleGalion());
            }

            //Créer escortes à partir de la fin de la clé precedente
            for (int i = 0; i < _escortesDePartie; i++, cle++)
            {
                _dicModeleNavires.Add(cle, new ModeleEscorte());
            }

            //Le pirate est déjà crée.
            
        }

        /// <summary>
        /// Utilisé returner la quantité d'ennemis crées.
        /// </summary>
        /// <returns>Le nombre de navires dans le jeu.</returns>
        public int GetNombreNavires()
        {
            return _dicModeleNavires.Count;
        }

        /// <summary>
        /// Permet de recupérer le pirate pour utiliser ses propriétés.
        /// Ex: l'or, la vitesse, la puissance, etc.
        /// </summary>
        /// <returns>Le pirate (joueur).</returns>
        public ModelePirate GetPirate()
        {
            return _pirate;
        }

        /// <summary>
        /// Permet de recupérer une navire du diccionaire des navires selon sa clé.
        /// </summary>
        /// <param name="cle">Clé du navire.</param>
        /// <returns>Le navire associé à la clé.</returns>
        public Navire GetNavire(int cle)
        {
            return _dicModeleNavires[cle];
        }

        /// <summary>
        /// Donne le niveau au le jeu doit commencer.
        /// </summary>
        /// <param name="nouveauNiveau">Niveau ou le jeu commencera.</param>
        public void SetNiveau(int nouveauNiveau)
        {
            _niveau = nouveauNiveau;

            switch (_niveau)
            {
                case 2: //Niveau 2
                    SetCantiteEnemis(1, 5);
                    PreparerJeu();
                    break;
                case 3: //Niveau 3
                    SetCantiteEnemis(1, 7);
                    PreparerJeu();
                    break;
                case 4: //Niveau 4
                    SetCantiteEnemis(1, 9);
                    PreparerJeu();
                    break;
            }
        }

        /// <summary>
        /// Permet de recupérer le niveau actuel avec le préfix "Niveau: ".
        /// </summary>
        /// <returns>Le niveau actuel avec préfix.</returns>
        public string ToStringNiveauActuel()
        {
            return "Niveau: " + _niveau;
        }

        /// <summary>
        /// Permet de recupérer le niveau actuel sous forme d'entier.
        /// </summary>
        /// <returns>Le niveau actuel.</returns>
        public int GetNiveauActuel()
        {
            return _niveau;
        }

        /// <summary>
        /// Permet d'assigner le nombre de galions et escortes.
        /// Cette méthode est utiliser lors du changement de niveau.
        /// </summary>
        /// <param name="nombreGalions">Nouveau nombre de galions.</param>
        /// <param name="nombreEscortes">Nouveau nombre d'escortes.</param>
        private void SetCantiteEnemis(int nombreGalions, int nombreEscortes)
        {
            _galionsDePartie = nombreGalions;
            _escortesDePartie = nombreEscortes;
        }
    }
}
