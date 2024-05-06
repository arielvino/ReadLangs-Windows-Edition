using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLangs
{
    public class Language(string name, string code)
    {
        public string Name { get { return name; } }
        public string Code { get { return code; } }

        public override string ToString()
        {
            return name;
        }
    }
}
