using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Vevro.Rasa.ApiClient.Model
{
    /// <summary>
    /// Message
    /// </summary>
    [DataContract]
    public class WebhookMessage : IEquatable<WebhookMessage>, IValidatableObject
    {
        /// <summary>
        /// Message text
        /// </summary>
        /// <value>Message text</value>
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// Conversation id
        /// </summary>
        /// <value>Conversation id</value>
        [DataMember(Name = "sender", EmitDefaultValue = false)]
        public string Sender { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        /// <value>Message text</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// Image url
        /// </summary>
        /// <value>Image url</value>
        [DataMember(Name = "image", EmitDefaultValue = false)]
        public string Image { get; set; }

        /// <summary>
        /// Conversation id
        /// </summary>
        /// <value>Conversation id</value>
        [DataMember(Name = "recipient_id", EmitDefaultValue = false)]
        public string RecipientId { get; set; }

        [DataMember(Name = "buttons", EmitDefaultValue = false)]
        public List<WebhookMessageButton> Buttons { get; set; }

        [DataMember(Name = "custom", EmitDefaultValue = false)]
        public List<WebhookMessageCustom> Custom { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public bool Equals(WebhookMessage input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) &&
                (
                    this.Sender == input.Sender ||
                    (this.Sender != null &&
                    this.Sender.Equals(input.Sender))
                ) &&
                (
                    this.Message == input.Message ||
                    (this.Message != null &&
                    this.Message.Equals(input.Message))
                ) &&
                (
                    this.Image == input.Image ||
                    (this.Image != null &&
                    this.Image.Equals(input.Image))
                ) &&
                (
                (this.Buttons == null && input.Buttons == null) ||
                (this.Buttons != null && input.Buttons != null && this.Buttons.SequenceEqual(input.Buttons))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.Sender != null)
                    hashCode = hashCode * 59 + this.Sender.GetHashCode();
                if (this.Message != null)
                    hashCode = hashCode * 59 + this.Message.GetHashCode();
                if (this.Image != null)
                    hashCode = hashCode * 59 + this.Image.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
