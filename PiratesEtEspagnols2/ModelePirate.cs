using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModelePirate : Navire
    {

        public ModelePirate()
        {
            CanonsCote = 5;
            MembresInitial = 80;
            MembresRestant = MembresInitial;
            _canon = new Canon(1.5, 2, 10);
            EstEnemiePirate = false;
        }



        public void VolerMembres()
        {

        }

        public void VolerOr()
        {

        }

    }
}
