using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleEscorte : Navire
    {
        private TypeEscorte type;

        public ModeleEscorte()
        {
            VitesseDeplacement = 2;
            CanonsDroit = 16;
            CanonsGauche = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            canon.Puissance = 0; //TODO
            canon.TempsRecharge = 0; //TODO
            canon.ChamDeTire = 0; //TODO
            EstEnemiePirate = true;
        }

        
    }

    public enum TypeEscorte
    {
        Epee,
        Cheval,
        Croix
    }
}
