using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    class GalionEspagnol : Navire
    {
        public int CantiteOr { get; private set; }

        public GalionEspagnol()
        {
            VitesseDeplacement = 3;
            CanonsDroit = 16;
            CanonsGauche = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            canon.Puissance = 0; //TODO
            canon.TempsRecharge = 0; //TODO
            canon.ChamDeTire = 0; //TODO
            EstEnemiePirate = true;

            CantiteOr = 0; //TODO aussi
        }
    }
}
