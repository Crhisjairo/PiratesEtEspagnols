using System.Collections.Generic;
using System.Windows.Controls;
using PiratesEtEspagnols;

namespace Tp3
{
    public interface IVueNavire
    {
        int Tirer();
        void ValiderMouvement(Canvas surface);
        Dictionary<string, double> GetPositionPrevueNavire();
        Dictionary<string, double> GetPositionReelNavire();
        void BloquerMouvement();
        void MouvementerNavire();
        Dictionary<string, double> GetChampDeTir();
        void SubirAttaque(int forceAttaque);
        string GetVie();
        bool EstMort();

    }
}