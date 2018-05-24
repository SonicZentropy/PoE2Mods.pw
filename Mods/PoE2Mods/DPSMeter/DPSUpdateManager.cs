using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Patchwork;
using UnityEngine;

namespace PoE2Mods.DPSMeter
{
    class DPSUpdateManager
    {
    }

    [NewType]
    public class ModUpdateManager : MonoBehaviour {

        private static ModUpdateManager _instance;
        
        public static ModUpdateManager Instance {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(ModUpdateManager)) as ModUpdateManager;
 
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<ModUpdateManager>();
                        singleton.name = "ModUpdateManager";
 
                        DontDestroyOnLoad(singleton);
                    } 
                }
                return _instance;
            }
        }

        private bool PreviouslyInCombat;

        private string FixName(string inName) {
            string newname = inName.Replace("Player_", "").Replace("Companion_", "").Replace("(Clone)", "");
            return newname;
        }

        //float holds how much damage that guy did to begin with
        private Dictionary<string, float> DamagePerMember;

        [NewMember]
        bool ConfigHasBeenInit;

        [NewMember]
        bool UseMod;

        [NewMember] private bool PrintDPS;

        [NewMember]
        public void InitMods() {
            UseMod = UserConfig.GetValueAsBool("ShipMoraleMod", "enableMod");
            
            ConfigHasBeenInit = true;
        }

        private float TimeSinceCombatStarted;

        private void HandleBeginCombat() {
            //Game.Console.AddMessage("IN COMBAT");
            PreviouslyInCombat = true;

            TimeSinceCombatStarted = 0.0f;

            DamagePerMember = new Dictionary<string, float>();

            var party = PartyManager.Instance.GetActivePartyMembers();
            foreach (var mem in party) {
                var pms = mem.m_statTracker;
                var dmg = pms.TotalDamageDone;
                var newname = FixName(mem.name);
                DamagePerMember.Add(newname, dmg);
                //Game.Console.AddMessage(newname +" has done: " + dmg);
            }
        }

        private void HandleEndCombat() {
            var party = PartyManager.Instance.GetActivePartyMembers();
            //Game.Console.AddMessage("Stopped Combat");
            //foreach (var mem in party) {
            //    var pms = mem.m_statTracker;
            //    var dmg = pms.TotalDamageDone;
            //    var newname = FixName(mem.name);
            //    //DamagePerMember.Add(newname, dmg);
            //    var totdmg = dmg - DamagePerMember[newname] ;
            //    var dps = totdmg / TimeSinceCombatStarted;
            //    Game.Console.AddMessage(newname +" has done: " + dmg);
            //    //Game.Console.AddMessage(newname +" DPS: " + dps);
            //}

            foreach (var mem in party) {
                var pms = mem.m_statTracker;
                var dmg = pms.TotalDamageDone;
                var newname = FixName(mem.name);
                //DamagePerMember.Add(newname, dmg);
                var totdmg = dmg - DamagePerMember[newname] ;
                var dps = totdmg / TimeSinceCombatStarted;
                //Game.Console.AddMessage(newname +" has done: " + dmg);
                if (PrintDPS) {
                    Game.Console.AddMessage(newname + " DPS: " + dps);
                }
            }
        }

        public void Update() {

            if (!ConfigHasBeenInit) {
                InitMods();
            }

            if (!UseMod) {
                return;
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P)) {
                PrintDPS = !PrintDPS;
            }

            if (GameState.InCombat && !PreviouslyInCombat) {
                HandleBeginCombat();
            } else if (!GameState.InCombat && PreviouslyInCombat) {
                
                PreviouslyInCombat = false;
                HandleEndCombat();
            }

            if (GameState.InCombat) {
                TimeSinceCombatStarted += Time.deltaTime;
            }
            
            
        }
    }

    [ModifiesType("Game.SceneLoadManager")]
    public class SceneLoadManagerNew {

        public static SceneLoadManager GetSceneLoadManagerNew {
            [ModifiesMember("get_Instance")]
            get {
                if (SceneLoadManager.s_instance == null)
                {
                    SceneLoadManager.s_instance = ResourceManager.FindObjectOfType<SceneLoadManager>();
                    
                }
                var mum = ModUpdateManager.Instance; // Create one of these bros
                return SceneLoadManager.s_instance;
            }
        }
    }
}
