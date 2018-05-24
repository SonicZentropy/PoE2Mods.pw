using Patchwork;

using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.GameData;
using Game.UI;
using Onyx;

namespace PoE2Mods
{
    [ModifiesType("Game.Player")]
    class mod_Player : Player
    {
        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        /// <summary>
        /// Modification: Updates the cursor to show player that walkable areas covered by fog are
        /// still valid navigation targets.
        /// </summary>
        [ModifiesMember("UpdateCursor")]
        public void mod_UpdateCursor()
        {
            if (!ConfigHasBeenInit)
            {
                UseMod = UserConfig.GetValueAsBool("ShipMechanics", "clickThroughFog");
                ConfigHasBeenInit = true;
            }

            if (GameCursor.CurrentMode == GameCursor.InputMode.Empowering)
            {
                GameCursor.UiCursor = GameCursor.CursorType.Empower;
                return;
            }
            if (Player.IsCasting() && this.CastingAbility != null)
            {
                if (!this.CastingAbility.ReadyForUI)
                {
                    this.CancelModes(true);
                }
                if (this.CastingAbility != null && !this.CastingAbility.IsValidTarget(GameCursor.CharacterUnderCursor))
                {
                    GameCursor.DesiredCursor = GameCursor.CursorType.NoWalk;
                    return;
                }
            }
            if (InGameUIManager.MouseOverUI)
            {
                GameCursor.DesiredCursor = GameCursor.CursorType.Normal;
                if (Player.IsCastingOrRetargeting())
                {
                    if (this.IsCastingAutoAttack)
                    {
                        GameCursor.DesiredCursor = this.GetAttackCursor();
                    }
                    else
                    {
                        GameCursor.DesiredCursor = Player.GetCursorForCastingStatus(this.GetCanCastCurrentAbility());
                    }
                }
                return;
            }
            if (this.m_isSelecting)
            {
                GameCursor.DesiredCursor = GameCursor.CursorType.Normal;
            }
            else if (this.IsInForceAttackMode)
            {
                if (GameCursor.CharacterUnderCursor != null)
                {
                    Health component = GameCursor.CharacterUnderCursor.GetComponent<Health>();
                    if (component == null || !component.IsTargetableByCursor() || SingletonBehavior<PartyManager>.Instance.IsSelectedPartyMember(GameCursor.CharacterUnderCursor))
                    {
                        GameCursor.DesiredCursor = GameCursor.CursorType.NoWalk;
                    }
                    else
                    {
                        GameCursor.DesiredCursor = this.GetAttackCursor();
                    }
                }
                else
                {
                    GameCursor.DesiredCursor = this.GetAttackCursor();
                }
            }
            else if (Player.IsCastingOrRetargeting())
            {
                if (this.IsCastingAutoAttack)
                {
                    GameCursor.DesiredCursor = this.GetAttackCursor();
                }
                else
                {
                    GameCursor.DesiredCursor = Player.GetCursorForCastingStatus(this.GetCanCastCurrentAbility());
                }
            }
            else if (this.RotatingFormation)
            {
                GameCursor.DesiredCursor = GameCursor.CursorType.RotateFormation;
            }
            else if (Player.MouseFollowActive)
            {
                Vector3 position = base.transform.position;
                List<GameObject> selectedPartyMemberGameObjects = SingletonBehavior<PartyManager>.Instance.GetSelectedPartyMemberGameObjects();
                if (selectedPartyMemberGameObjects.Count > 0)
                {
                    position = SingletonBehavior<PartyManager>.Instance.GetSelectedPartyCenter();
                }
                Camera main = Camera.main;
                Vector2 vector = main.WorldToScreenPoint(GameInput.WorldMousePosition) - main.WorldToScreenPoint(position);
                int num = Mathf.CeilToInt((Mathf.Atan2(vector.y, vector.x) - 0.196349546f) / 0.3926991f);
                GameCursor.DesiredCursor = GameCursor.CursorType.ShipMove_N + (-num + 16 + 4) % 16;
            }
            else
            {
                GameObject objectUnderCursor = GameCursor.ObjectUnderCursor;
                if (objectUnderCursor)
                {
                    Usable component2 = objectUnderCursor.GetComponent<Usable>();
                    WorldMapUsable component3 = objectUnderCursor.GetComponent<WorldMapUsable>();
                    Faction component4 = objectUnderCursor.GetComponent<Faction>();
                    Health component5 = objectUnderCursor.GetComponent<Health>();
                    Destructible component6 = objectUnderCursor.GetComponent<Destructible>();

                    // TPS: Specified Game.Collider2D here
                    Game.Collider2D component7 = objectUnderCursor.GetComponent<Game.Collider2D>();
                    PartyMember component8 = objectUnderCursor.GetComponent<PartyMember>();
                    if (component3 != null)
                    {
                        GameCursor.DesiredCursor = component3.HoverCursor;
                    }
                    else if (!SingletonBehavior<PartyManager>.Instance.IsPartyMemberSelected())
                    {
                        if (component8 != null)
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.Normal;
                        }
                        else
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.NoWalk;
                        }
                    }
                    else if (component2 != null && component2.IconType != ColliderIconType.None)
                    {
                        GameCursor.DesiredCursor = GameCursor.CursorType.Normal;
                    }
                    else if (component6 != null)
                    {
                        if (!component6.IsDestroyed)
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.Attack;
                        }
                        else
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.Normal;
                        }
                    }
                    else if (component4 != null && component4.GetRelationshipToPlayer() == Relationship.Hostile && component5 != null && !component5.IsDeadOrUnconscious && component4.CharacterStats != null && !component4.CharacterStats.IsImmuneToAttacks && !GameCursor.OverrideCharacterUnderCursor)
                    {
                        GameCursor.DesiredCursor = this.GetAttackCursor();
                    }
                    else if (component2 != null)
                    {
                        GameCursor.DesiredCursor = component2.GetDesiredCursor();
                    }
                    else if (component7)
                    {
                        if (!SingletonBehavior<PartyManager>.Instance.IsPrimaryPartyMemberSelected())
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.NoWalk;
                        }
                        else if (GameCursor.CursorOverride != GameCursor.CursorType.None)
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorOverride;
                        }
                        else
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.Normal;
                        }
                    }
                }
                else if (WorldMapPlayer.Instance != null && WorldMapPlayer.Instance.CanReachMousePosition())
                {
                    if (UseMod)
                    {
                        // TPS: Ignore presence of fog when checking walkable state
                        GameCursor.DesiredCursor = GameCursor.CursorType.Walk;
                    }
                    else // default behavior
                    {
                        if (SingletonBehavior<WorldMapFogOfWar>.Instance.IsFogRevealed(GameInput.WorldMousePosition))
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.Walk;
                        }
                        else
                        {
                            GameCursor.DesiredCursor = GameCursor.CursorType.NoWalk;
                        }
                    }
                }
                else if (this.CanAtLeastOneSelectedPartyMemberReachMousePosition())
                {
                    if (GameState.InCombat && Player.IsSelectedPartyMemberEngaged())
                    {
                        GameCursor.DesiredCursor = GameCursor.CursorType.Disengage;
                    }
                    else
                    {
                        GameCursor.DesiredCursor = GameCursor.CursorType.Walk;
                    }
                }
                else
                {
                    GameCursor.DesiredCursor = GameCursor.CursorType.NoWalk;
                }
            }
            if (GameCursor.DesiredCursor == GameCursor.CursorType.Normal && GameInput.GetControl(MappedControl.MULTISELECT, false))
            {
                GameCursor.DesiredCursor = GameCursor.CursorType.SelectionAdd;
            }
            else if (GameCursor.DesiredCursor == GameCursor.CursorType.Normal && GameInput.GetControl(MappedControl.MULTISELECT_NEGATIVE, false))
            {
                GameCursor.DesiredCursor = GameCursor.CursorType.SelectionSubtract;
            }
            this.WantsAttackAdvantageCursor = false;
        }
    }
}