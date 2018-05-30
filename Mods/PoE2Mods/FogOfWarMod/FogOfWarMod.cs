using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patchwork;
using UnityEngine;

namespace PoE2Mods
{
    [ModifiesType("Game.FogOfWar")]
    class FogOfWarMod : Game.FogOfWar
    {
        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [NewMember]
        public void InitMods() {
            UseMod = UserConfig.GetValueAsBool("FogOfWarMod", "enableMod");
            ConfigHasBeenInit = true;
        }

        [ModifiesMember("PointFullyVisible")]
        public bool PointFullyVisibleNew(Vector3 worldPosition)
        {
            
            if (!FogOfWarRender.FogOfWarRendering)
            {
                return true;
            }
            if (!ConfigHasBeenInit) InitMods();

            int num;
            int num2;
            this.WorldToFogOfWar(worldPosition, out num, out num2);
            if (UseMod) 
                return num >= 0 && num2 >= 0 && num < this.m_fogVertsPerColumn && num2 < this.m_fogVertsPerRow && this.m_fogAlphas[num * this.m_fogVertsPerRow + num2].DisplayedAlpha - 0.01f < 0.7f;
            else {
                return num >= 0 && num2 >= 0 && num < this.m_fogVertsPerColumn && num2 < this.m_fogVertsPerRow && this.m_fogAlphas[num * this.m_fogVertsPerRow + num2].DisplayedAlpha - 0.01f < 0.05f;
            }
        }
    }
}
