using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLangs
{
    public class SupportedLanguages:ISupportedLanguagesListProvider
    {
        public List<Language> Languages
        {
            get
            {
                //path to asset file:
                string path = Environment.CurrentDirectory + "/supported_languages";

                List<Language> languages = [];

                //read lines from the file:
                string[] values = File.ReadAllLines(path);

                //parse each line:
                foreach(string line in values)
                {
                    //an empty space is separating between the language description and its code. The language description may contain white spaces, too.
                    string name = line.Substring(0, line.LastIndexOf(' '));
                    string code = line.Substring(line.LastIndexOf(' ') + 1);

                    languages.Add(new(name, code));
                }

                return languages;
            }
        }
    }
}