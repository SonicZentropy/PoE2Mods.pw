using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Game.UI;
using Patchwork;
using UnityEngine;

namespace PoEMods.pw
{
    [ModifiesType]
    public class CameraSpringRemovalMod : SmartCamera
    {
        private Vector3 SpringTuneNew
        {
            [ModifiesMember("get_SpringTune")]
            get {
                return MovementSpringTune * 2f;
        
                //Original
                //if (this.m_mouseBiasBeingTracked)
                //{
                //	return this.MovementSpringTune * 2f;
                //}
                //if (this.m_timeToUseStrongSpring > 0f)
                //{
                //	return this.MovementSpringTune;
                //}
                //return this.NoMovementSpringTune;
            }
        }

        [ModifiesMember("OnyxLateUpdate")]
        protected void OnyxLateUpdateNew()
        {
            //base.OnyxLateUpdate();
            this.m_preventTrackingTimer = Mathf.Max(0f, this.m_preventTrackingTimer - Time.deltaTime);
            m_timeToUseStrongSpring = 50.0f;
            //m_timeToAllowMovementLookAhead = 0.0f;
            if (GameInput.GetControl(MappedControl.MOUSE_CAMERA_LOOKAHEAD, false)) {
                this.m_preventTrackingTimer = 0f;
                //this.m_timeToUseStrongSpring = this.TimeToAllowStrongSpringMovementAfterNoMovement;
            }
            if (!this.IsAnyPartyMemberMoving()) {
                //this.m_timeToUseStrongSpring = Mathf.Max(0f, this.m_timeToUseStrongSpring - Time.deltaTime);
                this.m_timeToAllowMovementLookAhead = Mathf.Max(0f, this.m_timeToAllowMovementLookAhead - Time.deltaTime);
            }
            if (this.m_mouseBiasBeingTracked) {
                //this.m_timeToUseStrongSpring = Mathf.Max(this.m_timeToUseStrongSpring, 2f);
            }
            for (int i = 0; i < this.m_trackedObjects.Count; i++) {
                List<GameObject> list = this.m_trackedObjects[(SmartCamera.TrackingTargetType)i];
                for (int j = list.Count - 1; j >= 0; j--) {
                    if (list[j] == null || !list[j].activeSelf) {
                        list.RemoveAt(j);
                    }
                }
            }
            for (int k = this.m_delayedRemoveObjectHashes.Count - 1; k >= 0; k--) {
                List<float> delayedRemoveObjectTimers;
                int index;
                (delayedRemoveObjectTimers = this.m_delayedRemoveObjectTimers)[index = k] = delayedRemoveObjectTimers[index] - Time.deltaTime;
                if (this.m_delayedRemoveObjectTimers[k] <= 0f) {
                    this.RemoveGameObjectToFollow(this.m_delayedRemoveObjectHashes[k], this.m_delayedRemoveObjectTypes[k]);
                    this.m_delayedRemoveObjectHashes.RemoveAt(k);
                    this.m_delayedRemoveObjectTimers.RemoveAt(k);
                    this.m_delayedRemoveObjectTypes.RemoveAt(k);
                }
            }
            this.m_timeUntilRefilterPartyTracking -= TimeController.UnscaledDeltaTime;
            if (this.m_timeUntilRefilterPartyTracking <= 0f) {
                this.FilterPartyMembersToTrack();
            }
            if (this.LerpFocusPosition) {
                Vector3 hitPoint = this.GetHitPoint(this.GetFocusWorldLocation());
                this.m_currentFocusPosition = Vector3.Lerp(this.m_currentFocusPosition, hitPoint, Mathf.Clamp01(this.GetDeltaTime() * (this.CurrentFocusLerpStrengh)));
                //this.m_currentFocusPosition = Vector3.Lerp(this.m_currentFocusPosition, hitPoint, 1.0f);
            }
            else {
                this.m_currentFocusPosition = this.GetHitPoint(this.GetFocusWorldLocation());
            }
            if (SmartCamera.DrawDebug) {
                this.DrawCameraDebug();
            }
        }
    }
}


