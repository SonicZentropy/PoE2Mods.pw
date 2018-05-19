using Game;
using Onyx;
using Patchwork;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace POE2Mods.pw
{
    [ModifiesType("Game.CommandLine")]
    class AchievementEnablerMod
    {
        [ModifiesMember("IRoll20s")]
        public void IRoll20sNew()
        {
            GameState.Instance.CheatsEnabled = !GameState.Instance.CheatsEnabled;
            //if (GameState.Instance.CheatsEnabled && SingletonBehavior<AchievementTracker>.Instance != null) {
            //    SingletonBehavior<AchievementTracker>.Instance.DisableAchievements = false;
            //}
            if (GameState.Instance.CheatsEnabled) {
                Game.Console.AddMessage("Cheats Enabled - Warning - Achievements NOT DISABLED for this game.");
            }
            else {
                Game.Console.AddMessage("Cheats Disabled");
            }
            //if (AnalyticsManager.Instance != null) {
            //    AnalyticsManager.Instance.CheatsEnabled(GameState.Instance.CheatsEnabled);
            //}
        }
    }
}
