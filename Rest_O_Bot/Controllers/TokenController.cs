using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rest_O_Bot.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rest_O_Bot.Controllers
{
   
    [Route("api/init")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        IHttpClientFactory _clientFactory;
        IConfiguration _configuration;
        public TokenController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //var spToken = await Helper.GetSpeechTokenAsync(_config.GetSection("SpeechService:SubscriptionKey").Value, _config.GetSection("SpeechService:Region").Value);
            //var spRegion = _config.GetSection("SpeechService:Region").Value;
            var dToken = await GetDirectLineToken(_configuration.GetSection("Bot:DirectLineSecret").Value);
            return new JsonResult(new { d = dToken?.Token });
        }



        private async Task<DirectLineTokenResponse> GetDirectLineToken(string botDirectLineSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://directline.botframework.com/v3/directline/tokens/generate");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botDirectLineSecret);
            //request.Content = new System.Net.Http.StringContent("");
            var response = await _clientFactory.CreateClient().SendAsync(request);
            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<DirectLineTokenResponse>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }
    }
}
