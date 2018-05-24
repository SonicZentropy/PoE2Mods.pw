using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patchwork;

namespace PoE2Mods {
    [ModifiesType("Game.GameData.GenericAbilityComponent")]
    class GenericAbilityComponentNew : Game.GameData.GenericAbilityComponent
    {

        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [NewMember] private bool RemoveIsCombatOnly;
        [NewMember] private bool RemoveIsNonCombatOnly;

        [NewMember]
        public void InitMods() {
            UseMod = UserConfig.GetValueAsBool("NoCombatRemovalMod", "enableMod");
            RemoveIsCombatOnly = UserConfig.GetValueAsBool("NoCombatRemovalMod", "RemoveInCombatOnly");
            RemoveIsNonCombatOnly = UserConfig.GetValueAsBool("NoCombatRemovalMod", "RemoveInNonCombatOnly");
            //MinimumMorale = UserConfig.GetValueAsInt("NoCombatRemovalMod", "MinimumMorale");
            ConfigHasBeenInit = true;
        }

        [NewMember] private bool mIsCombatOnly;
        [NewMember] private bool mIsNonCombatOnly;

        public bool IsCombatOnlyNew {
            [ModifiesMember("get_IsCombatOnly")]
            get {
                if (!ConfigHasBeenInit) InitMods();
                if (RemoveIsCombatOnly) {
                    return false;
                }
                else return mIsCombatOnly;
            }
            [ModifiesMember("set_IsCombatOnly")]
            set { mIsCombatOnly = value; }
        }

        public bool IsNonCombatOnlyNew {
            [ModifiesMember("get_IsNonCombatOnly")]
            get {
                if (!ConfigHasBeenInit) InitMods();
                if (RemoveIsNonCombatOnly) {
                    return false;
                }
                else return mIsNonCombatOnly;
            }
            [ModifiesMember("set_IsCombatOnly")]
            set { mIsNonCombatOnly = value; }
        }
    }
}