using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ApiClient.Model
{
    public class WebhookMessageCustom //: IEquatable<WebhookMessageCustom>, IValidatableObject
    {
        [DataMember(Name = "videos", EmitDefaultValue = false)]
        public List<WebhookMessageCustomVideo> Videos { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
    public class WebhookMessageCustomVideo
    {
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
