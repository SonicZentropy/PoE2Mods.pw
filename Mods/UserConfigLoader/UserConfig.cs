using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserConfigLoader
{
    [NewType]
    public class UserConfig
    {
        public string LoadGameSpeedConfig()
        {
            UnityEngine.Debug.Log("In LoadGameSpeedConfig");
            Game.Console.AddMessage("In LoadGameSpeedConfig");
            return "GameSpeed";
        }
    }
}
