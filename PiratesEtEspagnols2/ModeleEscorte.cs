using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleEscorte : Navire
    {
        /*private TypeEscorte type = TypeEscorte.Cheval;
        */
        public ModeleEscorte()
        {
            CanonsCote = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            _canon = new Canon(0.3, 6, 15);
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
