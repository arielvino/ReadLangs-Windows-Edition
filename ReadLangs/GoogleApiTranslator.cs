using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReadLangs
{
    public class GoogleApiTranslator : ITranslator
    {
        const string googleApiEndpoint = "https://translation.googleapis.com/language/translate/v2";

        public async Task<string?> TranslateAsync(string word)
        {
            if(word.Length == 0 || word.Length > 200)
            {
                return string.Empty;
            }

            string url = googleApiEndpoint;
            url += "?source=" + Settings.SourceLanguage;
            url += "&target=" + Settings.TargetLanguage;
            url += "&q=" + word;

            //key:
            string? key = GoogleApiKey.Key;
            if(key is null)
            {
                return null;
            }
            url += "&key=" + key;

            HttpClient httpClient = new();
            string rawJson = await httpClient.GetStringAsync(url);

            TranslationResponse translationResponse = JsonSerializer.Deserialize<TranslationResponse>(rawJson);
            string translatedText = translationResponse.data.translations[0].translatedText;

            return translatedText;
        }

        public class TranslationResponse
        {
            public TranslationData data { get; set; }
        }

        public class TranslationData
        {
            public Translation[] translations { get; set; }
        }

        public class Translation
        {
            public string translatedText { get; set; }
        }

        public static class GoogleApiKey
        {
            static readonly string name = Environment.CurrentDirectory + "/google_api_key";

            public static string? Key
            {

                get
                {
                    if (File.Exists(name))
                    {
                        return File.ReadAllText(name);
                    }
                    else
                    {
                        return null;
                    }
                }
                set
                {
                    File.WriteAllText(name, value);
                }
            }
        }
    }
}
