using System;

namespace PiratesEtEspagnols
{
    public class ModeleEscorte : Navire
    {
        /// <summary>
        /// Crée un modèle d'une escorte.
        /// </summary>
        public ModeleEscorte()
        {
            CanonsCote = 16;
            MembresInitial = 100;
            MembresRestant = MembresInitial;
            Canon = new Canon(0.3, 10, 15);
            DeterminerQuantiteArmes();
        }

        /// <summary>
        /// Determine au hasard combien d'armes l'escorte possède.
        /// </summary>
        private void DeterminerQuantiteArmes()
        {
            Random random = new Random();
            QuantiteArmes = random.Next(0,36);
        }

        /// <summary>
        /// Subir l'attaque final des pirates.
        /// </summary>
        /// <param name="pirate">Le navire qui attaque</param>
        public override void EtreEvahis(ModelePirate pirate)
        {
            pirate.VolerArmes(QuantiteArmes);
            pirate.VolerMembres(MembresRestant);

            QuantiteArmes = 0;
            MembresRestant = 0;
        }
    }

    /// <summary>
    /// Le type d'escorte.
    /// Selon le type, l'image va être géneré.
    /// Pour le futur, les escortes seront différentes les unes aux autres.
    /// </summary>
    public enum TypeEscorte
    {
        Epee, //Escorte type épée.
        Cheval, //Escorte type cheval.
        Croix //Escorte type croix.
    }
}
