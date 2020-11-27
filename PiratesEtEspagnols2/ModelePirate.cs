using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModelePirate : Navire
    {

        public ModelePirate()
        {
            VitesseDeplacement = 3;
            CanonsDroit = 16;
            CanonsGauche = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            canon.Puissance = 0; //TODO
            canon.TempsRecharge = 0; //TODO
            canon.ChamDeTire = 0; //TODO
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
