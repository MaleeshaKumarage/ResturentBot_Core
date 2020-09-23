using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinhalaTokenizationLibrary;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace TranslationHandler
{
    public class WikiData
    {
        public async System.Threading.Tasks.Task<string> getWikiDataAsync(string text)
        {
            string englishText = await new Translation().SinhalaTOEnglish(text.ToLower().Trim());
            englishText = englishText.ToLower().Trim();

            englishText = englishText.Replace("a ", " ");
            englishText = englishText.Replace(" a ", " ");
            englishText = englishText.Replace("is", "");
            englishText = englishText.Replace("are", "");
            englishText = englishText.Replace("what", "");
            englishText = englishText.Replace("where", "");
            englishText = englishText.Replace("who", "");
            englishText = englishText.Replace("by", "");
            englishText = englishText.Replace("mean", "");
            englishText = englishText.Replace("meaning", "");
            englishText = englishText.Replace("the", "");
            englishText = englishText.Replace("that", "");
            englishText = englishText.Replace("this", "");
            englishText = Regex.Replace(englishText, @"[\p{P}-[()\-.]]", "");

            var url = $"https://en.wikipedia.org/api/rest_v1/page/summary/{englishText.Trim()}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string strResult = await response.Content.ReadAsStringAsync();
                    try
                    {
                    var k = JsonConvert.DeserializeObject<JObject>(strResult);
                     var l = k["extract"];
                    return await new Translation().EnglishTOSinhala(l.ToString());

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                else
                {
                    return null;
                }
            }

           
        }


    }
}
