using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class Pirate : Navire
    {

        public Pirate()
        {
            VitesseDeplacement = 3;
            CannonsDroit = 16;
            CannonsGauche = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            cannon.Puissance = 0; //TODO
            cannon.TempsRecharge = 0; //TODO
            cannon.ChamDeTire = 0; //TODO
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
