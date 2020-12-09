using System;
using System.Collections.Generic;
using System.Text;
using PiratesEtEspagnols2;

namespace PiratesEtEspagnols
{
    public class Jeu
    {
        public List<Navire> ListeNavires { get; set; } = new List<Navire>();
       
        //EST-CE QUE TU A BESOIN DE CES TROIS ITEMS?
        private ModeleGalion _galion = new ModeleGalion();
        private List<ModeleEscorte> _escortes = new List<ModeleEscorte>();
        private ModelePirate _pirate = new ModelePirate();
        

        static void Main(string[] args)
        {

        }

        public Jeu()
        {
            
        }

        public void AddListeNavires(int typeNavire)
        {
            if (typeNavire == 1)
            {
                Navire pirate = new ModelePirate();
                ListeNavires.Add(pirate);
            }
            else if (typeNavire == 2)
            {
                Navire galion = new ModeleGalion();
                ListeNavires.Add(galion);
            }
            else if (typeNavire == 3)
            {
                Navire escorte = new ModeleEscorte();
                ListeNavires.Add(escorte);
            }

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
    }
}
