namespace PiratesEtEspagnols
{
    public class Canon
    {
        /// <summary>
        /// Puissance d'un cannon.
        /// </summary>
        internal double Puissance { get; set; }
        /// <summary>
        /// Temps de recharge d'un cannon.
        /// </summary>
        internal int TempsRecharge { get; set; }
        /// <summary>
        /// Le champ de tire d'un cannon. Si la cible se trouve dans le champ de tire du cannon, la cible subit des dégâts.
        /// </summary>
        public int ChamDeTire { get; internal set; }
        /// <summary>
        /// Le dernier tire fait par un cannon.***
        /// </summary>
        internal int DernierTir { get; set; }

        /// <summary>
        /// Crée un cannon avec un puissance, temps de recharge et un champ de tire.
        /// Ce cannon est utilisé pour calculer la puissance de tire d'une navire.
        /// </summary>
        /// <param name="puissance">Puissance.</param>
        /// <param name="tempsRecharge">Temp de recharge du cannon.</param>
        /// <param name="champDeTire">Champ de tire du cannon.</param>
        public Canon(double puissance, int tempsRecharge, int champDeTire)
        {
            Puissance = puissance;
            TempsRecharge = tempsRecharge;
            ChamDeTire = champDeTire;
            DernierTir = -10; //C'est avec cette proprieté qu'on sache se le navire a eu le temps necessaire pour recharcher ses cannons avant tirer un plus fois. 
        }
    }
}
