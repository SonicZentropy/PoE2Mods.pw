using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Game.UI;
using Patchwork;
using PoE2Mods;
using UnityEngine;

namespace PoE2Mods
{
    [ModifiesType]
    public class CipherFocus : FocusTrait
    {
        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [NewMember] private int MaxFocusBonusAmount;

        private float BaseFocusNew
        {
            [ModifiesMember("get_BaseFocus")]
            get {
                if (!ConfigHasBeenInit) {
                    ConfigHasBeenInit = true;
                    UseMod = UserConfig.GetValueAsBool("CipherFocus", "enableMod");
                    MaxFocusBonusAmount = UserConfig.GetValueAsInt("CipherFocus", "MaxFocusBonus");
                }
                
                float num = 0f;
                if (base.OwnerStats != null) {
                    num = (float)base.OwnerStats.Level * 5f;
                    num += 10f;  
                    if (!UseMod) {
                        num /= 2f; //just checking
                    }
                    
                }
                return num;
            }
        }

        public float MaxFocusBonusNew
        {
            [ModifiesMember("get_MaxFocusBonus")]
            get {
                if (!ConfigHasBeenInit) {
                    ConfigHasBeenInit = true;
                    UseMod = UserConfig.GetValueAsBool("CipherFocus", "enableMod");
                    MaxFocusBonusAmount = UserConfig.GetValueAsInt("CipherFocus", "MaxFocusBonus");
                }
                if (!UseMod) {
                    return (!(base.OwnerStats != null)) ? 0f : base.OwnerStats.StatusEffectManager.CalculateValueForStat(Game.GameData.StatusEffectType.MaxFocus, null);
                }
                return (!(base.OwnerStats != null)) ? 0f : base.OwnerStats.StatusEffectManager.CalculateValueForStat(Game.GameData.StatusEffectType.MaxFocus, null) + MaxFocusBonusAmount;
            }
        }


    }
}


