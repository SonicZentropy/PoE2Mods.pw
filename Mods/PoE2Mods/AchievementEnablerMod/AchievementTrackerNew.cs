using Game;
using Game.GameData;
using Onyx;
using Patchwork;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PoE2Mods
{
    [ModifiesType("Game.AchievementTracker")]
    class AchievementTrackerNew : Game.AchievementTracker
    {
        public bool DisableAchievementsNew
        {
            [ModifiesMember("get_DisableAchievements")]
            get
            {
                this.m_disabledAchievements.Clear();
                return false;
            }
            [ModifiesMember("set_DisableAchievements")]
            set {
                this.m_disableAchievements = false;
                this.m_disabledAchievements.Clear();
            }
        }

        [ModifiesMember("OnyxAwake")]
        public void OnyxAwakeNew()
        {
            base.OnyxAwake();
            this.DisableAchievements = false;
        }
        
        [ModifiesMember("DisableAchievement")]
        public void DisableAchievementNew(ChallengeGameData achievement)
        {
            return;
        }
       
    }
}
