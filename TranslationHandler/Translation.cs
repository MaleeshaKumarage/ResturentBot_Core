
using Google.Cloud.Translation.V2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Translation
{
    public  async Task<string> EnglishTOSinhala(string text)
    {
        var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=EN&tl=SI&dt=t&q={text}";
        
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string strResult = await response.Content.ReadAsStringAsync();
               var k= JsonConvert.DeserializeObject<JArray>(strResult);
                var l = k[0][0][0];
                return l.ToString();
            }
            else
            {
                return null;
            }
        }
    }
    public async Task<String> SinhalaTOEnglish(string text)
    {
        var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=SI&tl=EN&dt=t&q={text}";

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string strResult = await response.Content.ReadAsStringAsync();
                var k = JsonConvert.DeserializeObject<JArray>(strResult);
                var l = k[0][0][0];
                return l.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}