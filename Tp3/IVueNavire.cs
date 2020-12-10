using System.Collections.Generic;
using System.Windows.Controls;
using PiratesEtEspagnols;

namespace Tp3
{
    public interface IVueNavire
    {
        //void EviterColision(List<> listNavires);
        void MouvementerNavire();
        void ValiderMouvement(Canvas surface);
        List<double> PositionNavire();
        List<double> PositionTireNavire();
        void BloquerMouvement();
        string GetVie();
        int Tirer();
        Navire GetTypeNavire();
        void SubirAttaque(int forceAttaque, bool estEnemiePirate);
    }
}