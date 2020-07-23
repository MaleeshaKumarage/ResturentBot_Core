using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_O_Bot.Resources
{

    public class DirectLineTokenResponse
    {
        [Newtonsoft.Json.JsonProperty("conversationId")]
        public string ConversationId { get; set; }
        [Newtonsoft.Json.JsonProperty("token")]
        public string Token { get; set; }
        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }

}
