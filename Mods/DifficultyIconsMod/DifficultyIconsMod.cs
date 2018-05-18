/**
 * Difficulty Icons Mod
 * Created by Tyler Stevens
 * May 2018
 * 
 * This mod allows difficulty icons to be toggled on and off when level scaling is turned on.
 */

using Patchwork;
using Game;
using Game.GameData;

namespace DifficultyIconsMod
{
    [ModifiesType]
    public class CombatSettingsComponentNew : CombatSettingsComponent
    {
        [ModifiesMember("GetDifficultyIcons")]
        public LevelDifficultyIcons GetDifficultyIconsNew(int playerLevel, int targetLevel)
        {
            LevelDifficultyIcons levelDifficultyIconSettings = null;

            // Original condition: re-enable to hide level difficulty icons when level scaling is on
            // if (GameState.Option.LevelScaling == LevelScalingOption.None && !GameState.Option.ShowDifficultyIndicators)
            if (!GameState.Option.ShowDifficultyIndicators)
            {
                return null;
            }

            int num = int.MinValue;
            int num1 = targetLevel - playerLevel;
            for (int i = 0; i < this.LevelDifficultyIconSettings.Length; i++)
            {
                int levelDifference = this.LevelDifficultyIconSettings[i].LevelDifference;
                if (num1 >= levelDifference && levelDifference > num)
                {
                    num = levelDifference;
                    levelDifficultyIconSettings = this.LevelDifficultyIconSettings[i];
                }
            }

            return levelDifficultyIconSettings;
        }
    }
}