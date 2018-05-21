using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Patchwork;

namespace PoE2Mods
{
    [ModifiesType("Game.ShipDuelManager")]
    class FreeSurrenderMod
    {
        [ModifiesMember("DeductSurrenderCost")]
        public void DeductSurrenderCostNew() {
            return;
        }

    }
}
