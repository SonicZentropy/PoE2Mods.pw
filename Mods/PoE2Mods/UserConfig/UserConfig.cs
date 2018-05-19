using IniParser;
using IniParser.Model;
using Patchwork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2Mods
{
    [NewType]
    public static class UserConfig
    {
        static IniData parsedData;

        
        static UserConfig()
        {
            FileIniDataParser fileIniData = new FileIniDataParser();
            
            parsedData = fileIniData.ReadFile("TestIniFile.ini");
        }

        public static string GetAllIniDataAsString()
        {
            return parsedData.ToString();
        }

        public static IniData GetIniData()
        {
            return parsedData;
        }

        public static bool GetValueAsBool(string Category, string KeyName)
        {
            return bool.Parse(parsedData[Category][KeyName]);
        }

        public static int GetValueAsInt(string Category, string KeyName)
        {
            return int.Parse(parsedData[Category][KeyName]);
        }
        public static float GetValueAsFloat(string Category, string KeyName)
        {
            return float.Parse(parsedData[Category][KeyName]);
        }
        public static string GetValueAsString(string Category, string KeyName)
        {
            return parsedData[Category][KeyName];
        }


        public static string LoadGameSpeedConfig()
        {
            FileIniDataParser fileIniData = new FileIniDataParser();
            UnityEngine.Debug.Log("In LoadGameSpeedConfig");
            Game.Console.AddMessage("In LoadGameSpeedConfig");
            return Directory.GetCurrentDirectory();
        }
    }
}
