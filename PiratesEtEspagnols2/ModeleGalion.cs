﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PiratesEtEspagnols
{
    public class ModeleGalion : Navire
    {
        public int CantiteOr { get; private set; }

        public ModeleGalion()
        {
            VitesseDeplacement = 3;
            CannonsDroit = 16;
            CannonsGauche = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            cannon.Puissance = 0; //TODO
            cannon.TempsRecharge = 0; //TODO
            cannon.ChamDeTire = 0; //TODO
            EstEnemiePirate = true;

            CantiteOr = 0; //TODO aussi
        }
    }
}
