/** 
 * GameSpeedMod.cs
 * Dylan Bailey
 * 11/14/16
 * License - Do whatever you want with this code as long as no puppies are set aflame
*/

using Game;
using Game.UI;
using Patchwork;
using PoE2Mods;
using UnityEngine;
//using SDK;

namespace PoE2Mods
{
    /// <summary>
    /// Modifies the game speed toggles to add a new 6x speed.
    /// </summary>
    [ModifiesType]
    public class CharacterProgressionGameDataNew : Game.GameData.CharacterProgressionGameData
    {            
        [NewMember]
        static bool ConfigHasBeenInit;

        [NewMember]
        static bool UseMod;

        [ModifiesMember("GetClassPowerLevel")]
        public int GetClassPowerLevelNew(bool isMulticlassed, int characterLevel)
		{
		    if (!ConfigHasBeenInit) {
		        ConfigHasBeenInit = true;
		        UseMod = UserConfig.GetValueAsBool("MulticlassPenaltyRemover", "enableMod");
		    }

		    if (UseMod) {
		        // this is literally the only required changed line
		        isMulticlassed = false; 
		    }
            
			int charLevelAsIndex = characterLevel - 1;
			if (charLevelAsIndex < 0)
			{
				return 0;
			}
			if (isMulticlassed)
			{
				if (charLevelAsIndex >= this.MultiClassPowerLevelByCharacterLevel.Count)
				{
					UIDebug.LogOnScreenWarning(string.Concat(new object[]
					{
						"Multi Class Power Level By Character Level list only has ",
						this.MultiClassPowerLevelByCharacterLevel.Count,
						" entries. Trying to lookup character with class level: ",
						characterLevel
					}), UIDebug.Department.Design, 10f);
					return this.MultiClassPowerLevelByCharacterLevel[this.MultiClassPowerLevelByCharacterLevel.Count - 1];
				}
				return this.MultiClassPowerLevelByCharacterLevel[charLevelAsIndex];
			}
			else
			{
				if (charLevelAsIndex >= this.SingleClassPowerLevelByCharacterLevel.Count)
				{
					UIDebug.LogOnScreenWarning(string.Concat(new object[]
					{
						"Single Class Power Level By Character Level list only has ",
						this.SingleClassPowerLevelByCharacterLevel.Count,
						" entries. Trying to lookup character with class level: ",
						characterLevel
					}), UIDebug.Department.Design, 10f);
					return this.SingleClassPowerLevelByCharacterLevel[this.MultiClassPowerLevelByCharacterLevel.Count - 1];
				}
				return this.SingleClassPowerLevelByCharacterLevel[charLevelAsIndex];
			}
		}


    }
}













