using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLangs
{
    public static class Settings
    {
        static readonly string settingsPath = Environment.CurrentDirectory + "/settings";

        public static void Init()
        {
            Directory.CreateDirectory(settingsPath);
        }

        public static string SourceLanguage
        {
            get
            {
                FileInfo srcFile = new(settingsPath + "/source_language");
                if (!srcFile.Exists)
                {
                    SourceLanguage = "de";
                }
                return File.ReadAllText(srcFile.FullName);
            }
            set
            {
                if(true)//validate
                {
                    File.WriteAllText(settingsPath + "/source_language", value);
                }
            }
        }

        public static string TargetLanguage
        {
            get
            {
                FileInfo targetFile = new(settingsPath + "/target_language");
                if (!targetFile.Exists)
                {
                    TargetLanguage = "en";
                }
                return File.ReadAllText(targetFile.FullName);
            }
            set
            {
                if (true)//validate
                {
                    File.WriteAllText(settingsPath + "/target_language", value);
                }
            }
        }
    }
}