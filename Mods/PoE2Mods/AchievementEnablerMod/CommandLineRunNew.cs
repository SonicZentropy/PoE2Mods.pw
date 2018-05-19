using Game;
using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PoE2Mods
{
    [ModifiesType("Game.CommandLineRun")]
    class CommandLineRunNew 
    {

        [ModifiesMember("GetNonCheatMethods")]
        public static IEnumerable<MethodInfo> GetNonCheatMethodsNew()
        {
            List<MethodInfo> myList = new List<MethodInfo>();
            foreach (MethodInfo info in CommandLineRun.s_ScriptsMethods) {
                myList.Add(info);
            }
        
            foreach (MethodInfo info in CommandLineRun.s_CommandLineMethods) {
                if (info.IsPublic) {
        
                    myList.Add(info);
        
                }
            }
            return myList;
        }
        
        [ModifiesMember("GetCheatMethods")]
        public static IEnumerable<MethodInfo> GetCheatMethodsNew()
        {
            List<MethodInfo> myList = new List<MethodInfo>();
            return myList;
            //yield break;
        }

        [ModifiesMember("MethodIsAvailable")]
        public static bool MethodIsAvailableNew(MethodInfo method)
        {
            return true;
        }
    }
}
