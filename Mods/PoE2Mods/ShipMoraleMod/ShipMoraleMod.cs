using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Game.GameData;
using Game.UI;
using Onyx;
using Patchwork;
using UnityEngine;

namespace PoE2Mods
{
    [ModifiesType("Game.ShipCrewManager")]
    class ShipMoraleMod : Game.ShipCrewManager
    {
        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [NewMember] 
        int MinimumMorale;

        [NewMember]
        public void InitMods() {
            UseMod = UserConfig.GetValueAsBool("ShipMoraleMod", "enableMod");
            MinimumMorale = UserConfig.GetValueAsInt("ShipMoraleMod", "MinimumMorale");
            ConfigHasBeenInit = true;
        }

        [ModifiesMember("SetMorale")]
        public void SetMoraleNew(int value)
        {
            if (!ConfigHasBeenInit) {
                InitMods();
            }

            if (this.HasCrewOnShip())
            {
                this.Morale = value;
                this.Morale = Mathf.Clamp(this.Morale, MinimumMorale, 100);
            }
        }

        [ModifiesMember("AdjustMorale")]
        public void AdjustMoraleNew(OnyxInt value, string reason, bool log)
        {
            if (!ConfigHasBeenInit) {
                InitMods();
            }
            if (!this.HasCrewOnShip())
            {
                return;
            }
            if (log)
            {
                ShipCrewManager.LogMorale(value);
            }

            this.Morale = Mathf.Clamp(this.Morale + (int)value, MinimumMorale, 100);
            UIShipResourceNotificationManager.PostNotification(new SpriteKey(SingletonBehavior<UIAtlasManager>.Instance.GameSystemIcons, "icon_ship_morale"), GuiStringTable.GetText(3709), value, reason);
            ShipCrewManager.OnShipMoraleChanged.Trigger();
            if (this.GetCurrentMoraleState() == MoraleStateType.Mutinous)
            {
                TutorialManager.STriggerTutorialsOfType(TutorialEventType.LowMorale);
            }
        }

        public int MoraleNew
        {   
            [ModifiesMember("get_Morale")]
            get
            {
                if (!ConfigHasBeenInit) {
                    InitMods();
                }
                var morale = Mathf.Clamp(this.m_persistentShipCrewManager.Morale, MinimumMorale, 100);
                return morale;
            }
            [ModifiesMember("set_Morale")]
            set {
                if (!ConfigHasBeenInit) {
                    InitMods();
                }
                this.m_persistentShipCrewManager.Morale = value < MinimumMorale ? MinimumMorale : value;
            }
        }
    }
}
