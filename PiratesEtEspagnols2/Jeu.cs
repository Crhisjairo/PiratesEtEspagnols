using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PiratesEtEspagnols2;

namespace PiratesEtEspagnols
{
    public class Jeu
    {
        private const int Max_galions = 1;
        private const int Max_escortes = 6;
        private const int Max_pirate = 1;

        //public List<Navire> ListeNavires { get; set; } = new List<Navire>();
        private Dictionary<int, Navire> _dicModeleNavires = new Dictionary<int, Navire>();

        //EST-CE QUE TU A BESOIN DE CES TROIS ITEMS?
        private ModelePirate _pirate;
        


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

            //Créer pirate.
            _pirate = new ModelePirate();

            ////Créer pirates*** à partir de la fin de la clé precedente.
            ////***Il doit avoir seulement un pirate pour ce jeu.
            //for (int i = 0; i < Max_pirate; i++)
            //{
            //    _dicModeleNavires.Add(cle, new ModelePirate());
            //}


        }

        /// <summary>
        /// Utilisé pour créer les vues des navires.
        /// </summary>
        /// <returns>Nombre de navires créées</returns>
        public int GetNombreNavires()
        {
            return _dicModeleNavires.Count;
        }

        //public void AddListeNavires(int typeNavire)
        //{
        //    if (typeNavire == 1)
        //    {
        //        Navire pirate = new ModelePirate();
        //        ListeNavires.Add(pirate);
        //    }
        //    else if (typeNavire == 2)
        //    {
        //        Navire galion = new ModeleGalion();
        //        ListeNavires.Add(galion);
        //    }
        //    else if (typeNavire == 3)
        //    {
        //        Navire escorte = new ModeleEscorte();
        //        ListeNavires.Add(escorte);
        //    }

        //}

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
