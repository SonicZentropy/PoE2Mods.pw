using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patchwork.AutoPatching;
using System.IO;

namespace AppInfoDLLProj
{
	[AppInfoFactory]
	public class AppInfoDLL : AppInfoFactory
	{
		public override AppInfo CreateInfo(DirectoryInfo folderInfo)
		{
			AppInfo ai = new AppInfo();
			ai.AppName = "POE2";
			ai.AppVersion = "1.0.1.64";
			ai.BaseDirectory = folderInfo;
			ai.Executable = new FileInfo(Combine(ai.BaseDirectory.ToString(), "PillarsOfEternityII.exe"));
			//ai.IconLocation = new FileInfo("D:/Games/Tyranny/goggame-1266051739.ico");

			return ai;
		}

        public static string Combine(params string[] paths)
     {
            var current = paths.Aggregate(@"", Path.Combine);
            return current;
     }
	}
}