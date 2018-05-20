using Patchwork;
using Game;
using Game.GameData;
using PoE2Mods;

namespace PoE2Mods
{
    [ModifiesType]
    public class CombatSettingsComponentNew : CombatSettingsComponent
    {
        [NewMember]
        static bool ConfigHasBeenInit;

        [NewMember]
        static bool UseMod;

        //This controls the quest icons only
        [ModifiesMember("GetDifficultyIcons")]
        public LevelDifficultyIcons GetDifficultyIconsNew(int playerLevel, int targetLevel)
        {
            if (!ConfigHasBeenInit) {
                ConfigHasBeenInit = true;
                UseMod = UserConfig.GetValueAsBool("DifficultyIconsMod", "enableQuestIcons");
            }

            LevelDifficultyIcons levelDifficultyIconSettings = null;

            // Original condition: re-enable to hide level difficulty icons when level scaling is on
            if (!UseMod) {
                if (GameState.Option.LevelScaling == LevelScalingOption.None &&
                    !GameState.Option.ShowDifficultyIndicators)
                    return null;
            }

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