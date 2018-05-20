using Patchwork;

using UnityEngine;
using UnityEngine.AI;
using Game;
using Game.GameData;
using Onyx;

namespace PoE2Mods
{
    [ModifiesType("Game.WorldMapPlayer")]
    public class mod_WorldMapPlayer : WorldMapPlayer
    {
        [NewMember]
        public bool UseMod;

        /// <summary>
        /// Read the config file, then call OnyxStart().
        /// </summary>
        [ModifiesMember("OnyxStart")]
        public void mod_OnyxStart()
        {
            UseMod = UserConfig.GetValueAsBool("ShipMechanics", "clickThroughFog");

            // TPS: Original code
            this.m_destinationDummyObject = new GameObject("WorldMapDestinationDummy");
            this.m_destinationDummyObject.transform.SetParent(base.transform.parent, false);
            this.Respawn();
            SingletonBehavior<CameraControl>.Instance.FocusOnPoint(base.transform.position, 0f);
            SingletonBehavior<CameraControl>.Instance.UpdateCamera();
        }

        /// <summary>
        /// Modification: Allows player to navigate to walkable areas currently hidden by fog.
        /// </summary>
        [ModifiesMember("OnyxUpdate")]
        public void mod_OnyxUpdate()
        {
            // TPS: this will call WorldMapPlayer.OnyxUpdate(), so leave out
            // base.OnyxUpdate();

            if (this.m_firstUpdate)
            {
                this.OnActiveShipChanged(SingletonBehavior<ShipManager>.Instance.ActiveShipType);
                this.m_firstUpdate = false;
            }
            GameState.PlayerCharacter.UpdateCursor();
            this.SetAgentAreaMask();
            this.SetAgentSpeed();
            bool flag = false;
            bool flag2 = false;
            if (SingletonBehavior<WorldMapGlobal>.Instance.AllowTravel)
            {
                bool flag3; // TPS: represents when the player has queued a movement action
                bool flag4;
                if (SingletonBehavior<SmartCamera>.Instance.IsFollowActive)
                {
                    flag3 = (GameInput.GetControl(MappedControl.MOVE, false) || GameInput.GetControl(MappedControl.FOLLOW_CURSOR_MOVE, false) || GameInput.GetControl(MappedControl.ROTATE_FORMATION, false));
                    flag4 = false;
                    if ((GameInput.GetControlDown(MappedControl.MOVE, true) || GameInput.GetControlDown(MappedControl.FOLLOW_CURSOR_MOVE, true) || GameInput.GetControlDown(MappedControl.ROTATE_FORMATION, true)) && !this.m_isPlayingAudio)
                    {
                        UIAudioManager.Instance.PlayWorldMapPathingBegin(this.CurrentTransitMode);
                        this.m_isPlayingAudio = true;
                    }
                    flag3 = (flag3 && GameInput.IsKeyUpAvailable(KeyCode.Mouse0));
                    flag = (GameInput.GetControlUp(MappedControl.MOVE, true) || GameInput.GetControlUp(MappedControl.FOLLOW_CURSOR_MOVE, true) || GameInput.GetControlUp(MappedControl.ROTATE_FORMATION, true));
                }
                else
                {
                    flag3 = (GameInput.GetControlUp(MappedControl.MOVE, true) || GameInput.GetControlUp(MappedControl.FOLLOW_CURSOR_MOVE, true) || GameInput.GetControlUp(MappedControl.ROTATE_FORMATION, true));
                    flag4 = true;
                    flag = true;
                }
                // TPS: Allow movement action to take place even if the fog of war is active at the cursor's current position
//              if (flag3 && this.CanReachMousePosition() && SingletonBehavior<WorldMapFogOfWar>.Instance.IsFogRevealed(GameInput.WorldMousePosition) && this.NavigateTo(GameInput.WorldMousePosition))
                if (flag3 
                    && this.CanReachMousePosition() 
                    && this.NavigateTo(GameInput.WorldMousePosition)
                    && (UseMod || SingletonBehavior<WorldMapFogOfWar>.Instance.IsFogRevealed(GameInput.WorldMousePosition)))
                {
                    if (flag4 && !this.m_isPlayingAudio)
                    {
                        UIAudioManager.Instance.PlayWorldMapPathingBegin(this.CurrentTransitMode);
                        this.m_isPlayingAudio = true;
                    }
                    if (!flag4)
                    {
                        Vector2 vector = Camera.main.WorldToScreenPoint(GameInput.WorldMousePosition) - Camera.main.WorldToScreenPoint(base.transform.position);
                        int num = Mathf.CeilToInt((Mathf.Atan2(vector.y, vector.x) - 0.196349546f) / 0.3926991f);
                        GameCursor.DesiredCursor = GameCursor.CursorType.ShipMove_N + (-num + 16 + 4) % 16;
                        flag2 = true;
                    }
                }
            }
            if (GameInput.GetControlUp(MappedControl.CANCEL_ACTION, true))
            {
                this.ClearDestination();
                UIWorldMapPlayerDestinationNugget.Show(false);
            }
            bool flag5 = false;
            if (this.HasDestination)
            {
                if (this.m_targetIcon != null)
                {
                    if (this.m_targetInteractionPoint != null)
                    {
                        NavMeshHit navMeshHit;
                        if (!this.UseNavMesh || !NavMesh.Raycast(base.transform.position, this.m_targetInteractionPoint.transform.position, out navMeshHit, this.m_agent.areaMask))
                        {
                            float num2 = this.m_targetInteractionPoint.transform.position.x - base.transform.position.x;
                            float num3 = this.m_targetInteractionPoint.transform.position.z - base.transform.position.z;
                            float num4 = num2 * num2 + num3 * num3;
                            if (num4 <= 0.25f)
                            {
                                flag5 = true;
                            }
                        }
                    }
                    else
                    {
                        float num4 = (this.m_targetIcon.transform.position - base.transform.position).sqrMagnitude;
                        float useRadius = this.m_targetIcon.GetUseRadius(this.CurrentTransitMode);
                        if (num4 <= useRadius * useRadius)
                        {
                            flag5 = true;
                        }
                    }
                }
                else if (this.m_target != null)
                {
                    float num5 = 0.5f;
                    if (!DataReference.IsNullOrEmpty(this.m_target.ShipCaptain))
                    {
                        num5 = this.m_target.ShipCaptain.Data.TriggerDistance;
                    }
                    float num4 = (this.m_target.transform.position - base.transform.position).sqrMagnitude;
                    if (num4 <= num5 * num5)
                    {
                        flag5 = true;
                    }
                    else if (Onyx.Math.Distance2DSquared(this.m_target.transform.position, this.m_targetLocation) > 1.401298E-45f)
                    {
                        this.SetDestination(this.m_target);
                    }
                }
                else
                {
                    float num4 = (this.m_targetLocation - base.transform.position).sqrMagnitude;
                    if (num4 <= 0.25f)
                    {
                        flag5 = true;
                    }
                }
                if (!this.UseNavMesh)
                {
                    if (this.m_target != null)
                    {
                        this.m_targetLocation = this.m_target.transform.position;
                    }
                    Vector3 vector2 = new Vector3(this.m_targetLocation.x - base.transform.position.x, 0f, this.m_targetLocation.z - base.transform.position.z);
                    Vector3 rhs = new Vector3(Mathf.Sign(vector2.x), 0f, Mathf.Sign(vector2.z));
                    base.transform.position += vector2.normalized * this.m_agent.speed * Time.deltaTime;
                    Vector3 lhs = new Vector3(Mathf.Sign(this.m_targetLocation.x - base.transform.position.x), 0f, Mathf.Sign(this.m_targetLocation.z - base.transform.position.z));
                    if (lhs != rhs)
                    {
                        base.transform.position = this.m_targetLocation;
                    }
                }
                if (flag)
                {
                    UIWorldMapPlayerDestinationNugget.Show(true, this.m_targetLocation, flag);
                }
                else if (flag2)
                {
                    UIWorldMapPlayerDestinationNugget.Show(false);
                }
            }
            else
            {
                UIWorldMapPlayerDestinationNugget.Show(false);
            }
            base.transform.localRotation = Quaternion.AngleAxis(base.transform.localRotation.eulerAngles.y, Vector3.up);
            if (!this.m_agent.isStopped)
            {
                this.UpdateTimeByMovement(true);
            }
            WorldMapGameData data = SingletonBehavior<WorldMapController>.Instance.WorldMapData.Data;
            SingletonBehavior<WorldMapGlobal>.Instance.IncreaseAreaMapStageToInclude(data, base.transform.position);
            if (this.m_debugAntiStaging)
            {
                int num6 = 0;
                for (int i = 0; i < 3; i++)
                {
                    Rect bounds = SingletonBehavior<WorldMapController>.Instance.WorldMapData.Data.AreaMapStages[i].Bounds;
                    Vector2 point = new Vector2(base.transform.position.x, base.transform.position.z);
                    if (bounds.Contains(point))
                    {
                        num6 = i;
                        break;
                    }
                }
                if (num6 < SingletonBehavior<WorldMapGlobal>.Instance.GetAreaMapStage(data))
                {
                    SingletonBehavior<WorldMapGlobal>.Instance.DEBUG_DescreaseAreaMapStage(data);
                }
                else if (num6 > SingletonBehavior<WorldMapGlobal>.Instance.GetAreaMapStage(data))
                {
                    SingletonBehavior<WorldMapGlobal>.Instance.IncreaseAreaMapStage(data, num6);
                }
            }
            if (flag5)
            {
                UIWorldMapPlayerDestinationNugget.Show(false);
                this.Arrive();
            }
            if (GameState.Instance && GameState.Instance.CheatsEnabled && GameInput.GetKeyDown(KeyCode.T) && GameInput.GetShiftkey())
            {
                this.TeleportTo(GameCursor.WorldPickPosition, false);
            }
        }
    }
}
