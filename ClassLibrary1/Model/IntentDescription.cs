using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ApiClient.Model
{
    /// <summary>
    /// DataContract
    /// </summary>
    [DataContract]
    public partial class IntentDescription : IEquatable<IntentDescription>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntentDescription" /> class.
        /// </summary>
        /// <param name="autoFill">autoFill (required).</param>
        /// <param name="initialValue">initialValue.</param>
        /// <param name="type">type (required).</param>
        /// <param name="values">values.</param>
        [JsonConstructorAttribute]
        protected IntentDescription() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="IntentDescription" /> class.
        /// </summary>
        /// <param name="autoFill">autoFill (required).</param>
        /// <param name="initialValue">initialValue.</param>
        /// <param name="type">type (required).</param>
        /// <param name="values">values.</param>
        public IntentDescription(bool useEntities = default(bool))
        {
            this.UseEntities = useEntities;
        }
        [DataMember(Name = "use_entities", EmitDefaultValue = false)]
        public bool UseEntities { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public override bool Equals(object input)
        {
            return this.Equals(input as IntentDescription);
        }

        public bool Equals(IntentDescription input)
        {
            if (input == null)
                return false;
            return this.UseEntities == input.UseEntities;
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
                hashCode = hashCode * 59 + this.UseEntities.GetHashCode();
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
