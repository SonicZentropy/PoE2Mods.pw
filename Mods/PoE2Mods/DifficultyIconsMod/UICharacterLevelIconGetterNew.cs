﻿using Game;
using Game.GameData;
using Onyx;
using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.UI;
using PoE2Mods;
using UnityEngine;

namespace PoE2Mods
{
    //This controls character icons, not quest icons
    [ModifiesType("Game.UI.UICharacterLevelIconGetter")]
    class UICharacterLevelIconGetterNew : Game.UI.UICharacterLevelIconGetter
    {

        [NewMember]
        static bool UseMod;

        [NewMember] 
        static bool PrintEnemyLevel;


        [ModifiesMember("OnyxEnable")]
        public void OnyxEnableNew()
        {
            UseMod = UserConfig.GetValueAsBool("DifficultyIconsMod", "enableCharacterIcons");
            PrintEnemyLevel = UserConfig.GetValueAsBool("DifficultyIconsMod", "printEnemyLevel");
            ReloadCharacter();
        }

        [NewMember]
        protected void orig_ReloadCharacter() {
            if (base.CharacterObject == null)
            {
                return;
            }
            this.m_enableDifficultyIndicators = (GameState.Option.LevelScaling == LevelScalingOption.None && GameState.Option.ShowDifficultyIndicators);
            if (!this.m_enableDifficultyIndicators)
            {
                this.m_sprite.alpha = 0f;
                return;
            }
            Destructible component = ComponentUtils.GetComponent<Destructible>(base.CharacterObject as MonoBehaviour);
            if (component != null || SingletonBehavior<PartyManager>.Instance.IsActivePartyMember(base.CharacterObject as CharacterStats))
            {
                this.m_sprite.alpha = 0f;
                return;
            }
            if (this.m_onlyIfHostile)
            {
                Faction component2 = ComponentUtils.GetComponent<Faction>(base.CharacterObject as MonoBehaviour);
                if (component2 && component2.GetRelationshipToPlayer() != Relationship.Hostile)
                {
                    this.m_sprite.alpha = 0f;
                    return;
                }
            }
            else
            {
                string levelDifficultySprite = UICharacterLevelIconGetter.GetLevelDifficultySprite(base.CharacterObject);
                this.m_sprite.spriteName = levelDifficultySprite;
                this.m_sprite.MakePixelPerfect();
                this.m_sprite.alpha = (string.IsNullOrEmpty(levelDifficultySprite) ? 0f : 1f);
            }
        }

        [ModifiesMember("ReloadCharacter")]
        protected void ReloadCharacterNew()
        {
            //base.ReloadCharacter();
            if (base.CharacterObject == null) {
                return;
            }

            if (!UseMod) {
                 orig_ReloadCharacter();
                return;
            }
            
                m_enableDifficultyIndicators = true; // always force
            

            Destructible component = ComponentUtils.GetComponent<Destructible>(base.CharacterObject as MonoBehaviour);
            if (component != null || SingletonBehavior<PartyManager>.Instance.IsActivePartyMember(base.CharacterObject as CharacterStats)) {
                this.m_sprite.alpha = 0f;
                return;
            }
            if (this.m_onlyIfHostile) {
                Faction component2 = ComponentUtils.GetComponent<Faction>(base.CharacterObject as MonoBehaviour);
                if (component2 && component2.GetRelationshipToPlayer() != Relationship.Hostile) {
                    this.m_sprite.alpha = 0f;
                    return;
                }
            }
            else {
                string levelDifficultySprite = GetLevelDifficultySprite(base.CharacterObject);
                this.m_sprite.spriteName = levelDifficultySprite;
                this.m_sprite.MakePixelPerfect();
                this.m_sprite.alpha = (string.IsNullOrEmpty(levelDifficultySprite) ? 0f : 1f);
            }
        }

        [ModifiesMember("GetLevelDifficultySprite")]
        public static string GetLevelDifficultySpriteNew(object arg)
        {
            if (arg == null) {
                return string.Empty;
            }
            int characterLevel = ((IHasCharacterClasses)arg).GetCharacterLevel();
            CharacterStats component = ComponentUtils.GetComponent<CharacterStats>(GameState.PlayerCharacter);
            if (component) {
                if (PrintEnemyLevel) {
                    Game.Console.AddMessage("Enemy Level: " + characterLevel);
                }
                
                LevelDifficultyIcons difficultyIcons = GlobalGameSettingsGameData.Instance.CombatSettingsComponent.GetDifficultyIcons(component.Level, characterLevel);
                if (difficultyIcons != null) {
                    return difficultyIcons.IconName;
                }
            }
            return string.Empty;
        }
    }
}
