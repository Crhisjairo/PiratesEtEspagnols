using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleGalion : Navire
    {
        

        internal int CantiteOr { get; private set; }
        internal int CanonsArrier { get; set; }

        public ModeleGalion()
        {
            CanonsCote = 36;
            CanonsArrier = 2; 
            MembresInitial = 200;
            MembresRestant = MembresInitial;
            _canon = new Canon(0.3, 10, 20);
            EstEnemiePirate = true;
            CantiteOr = 0; //TODO
        }

    }
}
