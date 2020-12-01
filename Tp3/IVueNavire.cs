using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Tp3
{
    public interface IVueNavire
    {
        //void EviterColision(List<> listNavires);
        void MouvementerNavire();
        void ValiderMouvement(Canvas surface);
        List<double> PositionNavire();
        void BloquerMouvement();
    }
}