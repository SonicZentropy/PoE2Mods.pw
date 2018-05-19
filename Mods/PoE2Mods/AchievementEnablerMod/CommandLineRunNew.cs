using Game;
using Patchwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AchievementEnablerMod
{
    [ModifiesType("Game.CommandLineRun")]
    public abstract class CommandLineRunNew //: CommandLineRun
    {

        // [MemberAlias(".cctor", typeof(object))]
        // private static void object_ctor()
        // {
        //     //this is an alias for object::.ctor()
        // }
        //
        // //protected CommandLineRun() {}
        // protected CommandLineRunNew() { }
        //
        // [ModifiesMember(".cctor")]
        // public static void CommandLineRunNewCtor()
        // {
        //     object_ctor();
        //     //IEModOptions.LoadFromPrefs();
        //     //CommandLineRun.LoadHistory();
        // }

         [ModifiesMember("GetNonCheatMethods")]
         public static IEnumerable<MethodInfo> GetNonCheatMethodsNew()
         {
	         return CommandLineRun.GetAllMethods();
         }
        
        [ModifiesMember("GetCheatMethods")]
        public static IEnumerable<MethodInfo> GetCheatMethodsNew()
        {
            //List<MethodInfo> myList = 
            return new List<MethodInfo>();
            //yield break;
        }

        [ModifiesMember("MethodIsAvailable")]
        public static bool MethodIsAvailableNew(MethodInfo method)
        {
            return true;
        }
    }
}
