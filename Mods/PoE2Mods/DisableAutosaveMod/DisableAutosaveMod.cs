using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;


namespace PoE2Mods
{
    [ModifiesType("Game.GameState")]
    public class DisableAutosaveMod : Game.GameState
    {
        [NewMember]
        static bool ConfigHasBeenInit;

        [NewMember]
        static bool UseMod;

        [NewMember]
        public static void orig_AutoSave() {
            if (GameState.Option.TrialOfIron)
            {
                GameState.TrialOfIronSave(false);
            }
            else
            {
                SaveLoadManager.SaveGame(SaveLoadManager.SaveGameType.Autosave, string.Empty);
                GameState.Instance.AutosaveCycleNumber++;
            }
        }

        [ModifiesMember("Autosave")]
        public static void AutosaveNew()
        {
            if (!ConfigHasBeenInit) {
                ConfigHasBeenInit = true;
                UseMod = UserConfig.GetValueAsBool("DisableAutosave", "enableMod");
            }

            if (!UseMod) {
                orig_AutoSave();
            }
            return;
        }

    }
}
