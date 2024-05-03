using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLangs
{
    public interface ITranslator
    {
        public Task<string?> TranslateAsync(string word);
    }
}
