using Patchwork;
using Game;
using Onyx;
using Game.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Game.GameData;


namespace PoE2Mods
{

    [ModifiesType("Game.UI.UIAbilityBar")]
    public class UIAbilityBarNew : Game.UI.UIAbilityBar
    {
        [NewMember]
        [DuplicatesBody("OnyxUpdate")]
        public void orig_OnyxUpdate() { }

        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [ModifiesMember("OnyxUpdate")]
        protected void OnyxUpdateNew()
        {
            // Have to init this way because class initialization doesn't work
            if (!ConfigHasBeenInit) {
                ConfigHasBeenInit = true;
                UseMod = UserConfig.GetValueAsBool("ResetEmpowerAndSpellsMod","enableMod");
            }
            if (! UseMod) {
                orig_OnyxUpdate();
                return;
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E)) {
                Game.Console.AddMessage("Reset Empower");

                //SingletonBehavior<PartyManager>.Instance.GetSelectedPartyMemberGameObjects()

                var selchars = m_selectedCharacters;
                if (selchars != null) // there's a selected char
                {
                    foreach (var selchar in selchars) {
                        //this resets the # used empowered per combat to 0
                        selchar.ResetEmpower();

                        //set empower pts to MaxEmpowerPoints
                        selchar.EmpowerPoints = selchar.MaxEmpowerPoints;
                    }

                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)) {
                Game.Console.AddMessage("Reset Spellcasts");
                var selchars = m_selectedCharacters;
                if (selchars != null) // there's a selected char
                {
                    foreach (var selchar in selchars) {
                        // FROM EmpowerResources, hopefully fixes spellcasts
                        foreach (AccruedResourceTrait accruedResourceTrait in selchar.AbilityList.FindAbilitiesByType<AccruedResourceTrait>()) {
                            //accruedResourceTrait.RestoreResource(accruedResourceTrait.GetSelfEmpowerRestoreCount());
                            accruedResourceTrait.RestoreResource(accruedResourceTrait.GetResourceMax() - accruedResourceTrait.GetResource());
                        }
                        for (CharacterClass characterClass = CharacterClass.None; characterClass < CharacterClass.Count; characterClass++) {
                            for (int i = 1; i <= GlobalGameSettingsGameData.Instance.MaxSpellLevel; i++) {
                                OnyxInt spellCastMax = SingletonBehavior<SpellMax>.Instance.GetSpellCastMax(selchar, characterClass, i);
                                selchar.RestoreSpellCasts(characterClass, i, spellCastMax);
                            }
                            selchar.AddClassAbilityPoolPoints(characterClass, selchar.GetMaxPowerPoolPoints(characterClass, null));
                        }
                    }
                }
            }


            this.m_RefreshTimer -= TimeController.UnscaledDeltaTime;
            if (this.m_NeedsRefresh || this.m_RefreshTimer <= 0f) {
                this.m_NeedsRefresh = false;
                this.m_RefreshTimer = float.PositiveInfinity;
                this.Refresh();
            }
            if (GameInput.GetControlDownWithRepeat(MappedControl.UP_ABILITY, true)) {
                this.NavigateVertical(1);
            }
            else if (GameInput.GetControlDownWithRepeat(MappedControl.DOWN_ABILITY, true)) {
                this.NavigateVertical(-1);
            }
            if (GameInput.GetControlDownWithRepeat(MappedControl.NEXT_ABILITY, true)) {
                if (this.m_SelectionRow < 0) {
                    this.SelectFirstButton();
                }
                else {
                    this.NavigateHorizontal(1);
                }
            }
            else if (GameInput.GetControlDownWithRepeat(MappedControl.PREVIOUS_ABILITY, true)) {
                if (this.m_SelectionRow < 0) {
                    this.SelectLastButton();
                }
                else {
                    this.NavigateHorizontal(-1);
                }
            }
            UIAbilityBarButton selectedButton = this.GetSelectedButton();
            if (selectedButton && !selectedButton.gameObject.activeInHierarchy) {
                this.CancelSelection();
            }
            if (this.m_SelectionTooltipDelay > 0f) {
                this.m_SelectionTooltipDelay -= TimeController.UnscaledDeltaTime;
                if (this.m_SelectionTooltipDelay <= 0f && selectedButton) {
                    selectedButton.ShowTooltip();
                }
            }
            UIAbilityBarButton selectedButton2 = this.GetSelectedButton();
            if (selectedButton2 && !Player.IsCastingOrRetargeting() && GameInput.GetControlUp(MappedControl.CAST_SELECTED_ABILITY, true)) {
                selectedButton2.Trigger();
            }
            if (this.SelectedObject != null) {
                CharacterHotkeyBindings orAddComponent = ResourceManager.GetOrAddComponent<CharacterHotkeyBindings>(this.SelectedObject.gameObject);
                if (GameInput.IsKeyUpAvailable(KeyCode.Mouse0) && orAddComponent != null) {
                    orAddComponent.Activate(SingletonBehavior<GameInput>.Instance.LastKeyUp);
                }
                if ((this.m_hotkeyRow == null || !this.m_hotkeyRow.gameObject.activeSelf) && (this.m_rows.Count < 2 || this.m_rows[1] == this.m_hotkeyRow || this.m_rows[1].IsEmpty) && orAddComponent != null && !ICollectionUtils.IsNullOrEmpty<KeyValuePair<KeyControl, Guid>>(orAddComponent.AbilityHotkeys)) {
                    this.m_hotkeyRow = this.ShowSubrow(null, 1);
                    this.m_hotkeyRow.SetIdentification(GuiStringTable.GetText(1662));
                    this.m_hotkeyRow.AddHotkeySet();
                }
                AIController component = ComponentUtils.GetComponent<AIController>(this.m_selectedCharacter);
                GenericAbility genericAbility = (!component) ? null : component.GetCurrentIntroStateAbility();
                if (genericAbility != null && genericAbility.Attack != null && !genericAbility.Attack.ForcedTarget) {
                    if (this.m_castControlRow == null || !this.m_castControlRow.gameObject.activeSelf) {
                        this.m_castControlRow = this.ShowSubrow(null, 2);
                        this.m_castControlRow.SetIdentification(GuiStringTable.GetText(2994));
                        this.m_castControlRow.AddCurrentCastSet();
                    }
                }
                else {
                    this.HideSubrow(2);
                }
            }
        }
    }
}
