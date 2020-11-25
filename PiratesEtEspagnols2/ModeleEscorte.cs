using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleEscorte : Navire
    {


        public ModeleEscorte()
        {
            VitesseDeplacement = 2;
            CannonsDroit = 16;
            CannonsGauche = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            cannon.Puissance = 0; //TODO
            cannon.TempsRecharge = 0; //TODO
            cannon.ChamDeTire = 0; //TODO
            EstEnemiePirate = true;
        }



    }
}
