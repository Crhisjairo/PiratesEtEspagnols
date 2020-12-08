namespace PiratesEtEspagnols
{
    public class Canon
    {
        internal double Puissance { get; set; }
        internal int TempsRecharge { get; set; }
        public int ChamDeTire { get; internal set; }
        internal int DernierTir { get; set; }

        public Canon(double puissance, int tempsRecharge, int champDeTire)
        {
            Puissance = puissance;
            TempsRecharge = tempsRecharge;
            ChamDeTire = champDeTire;
            DernierTir = -2;
        }
    }
}
