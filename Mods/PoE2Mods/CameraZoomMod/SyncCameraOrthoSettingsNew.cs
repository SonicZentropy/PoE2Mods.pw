using Game;
using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PoE2Mods
{
    [ModifiesType("Game.SyncCameraOrthoSettings")]
    public class SyncCameraOrthoSettingsNew : SyncCameraOrthoSettings {
        
        [NewMember] private float MaxZoom;
        [NewMember]
        bool ConfigHasBeenInit;

        [ModifiesMember("SetZoomLevel")]
        public void SetZoomLevelNew(float zoomLevel, bool force)
        {
            if (float.IsNaN(zoomLevel)) {
                zoomLevel = 1f;
            }
            if (SyncCameraOrthoSettings.s_IgnoreCameraZoomRange) {
                this.m_targetZoomLevel = zoomLevel;
            }
            else {
                this.m_targetZoomLevel = Mathf.Clamp(zoomLevel, GameState.Option.MinZoom, MaxZoom);
                //this.m_targetZoomLevel = zoomLevel;
            }
            if (force) {
                this.m_currentZoomLevel = this.m_targetZoomLevel;
            }
            //Game.Console.AddMessage("Current zoom level is now: " + this.m_currentZoomLevel);
            this.UpdateZoom();
        }

        [ModifiesMember("Update")]
        public void UpdateNew()
        {

            if (!ConfigHasBeenInit) {
                ConfigHasBeenInit = true;
                MaxZoom = UserConfig.GetValueAsFloat("CameraZoomMod","MaxZoom");
                m_targetZoomLevel = 1.0f;
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z)) {
                m_targetZoomLevel = 1.0f;
            }


            if (this.ScreenWidth != this.m_previousScreenWidth || this.ScreenHeight != this.m_previousScreenHeight) {
                this.m_previousScreenWidth = this.ScreenWidth;
                this.m_previousScreenHeight = this.ScreenHeight;
                this.SetZoomLevel(this.GetZoomLevel(), true);
            }
            bool flag = this.m_currentZoomLevel != this.m_targetZoomLevel;
            this.m_currentZoomLevel = Mathf.Lerp(this.m_currentZoomLevel, this.m_targetZoomLevel, Mathf.Clamp01(TimeController.UnscaledDeltaTime * this.ZoomLerpStrength));
            if (flag) {
                this.UpdateZoom();
            }
        }
    }
}