using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisableAutosave
{
    [ModifiesType("Game.GameState")]
    public class DisableAutosaveMod : Game.GameState
    {
        [ModifiesMember("Autosave")]
        public static void AutosaveNew()
        {
            return;
        }

    }
}
