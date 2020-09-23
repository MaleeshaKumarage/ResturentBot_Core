using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_O_Bot.Resources
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of Payload of the attachment, can either be <see cref="TemplatePayload"/> or <see cref="FileAttachmentPayload"/></typeparam>
    public class FacebookChannelData<T> where T : IFacebookAttachmentPayload
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Type of attachment. Required only if type of Payload is <see cref="FileAttachmentPayload"/>.Value should may be one of <see cref="FacebookAttachmentTypes"/>. For assets, max file size is 25MB.</param>
        public FacebookChannelData(string type = null)
        {
            Attachment = new FacebookAttachment<T>(type);
        }
        [JsonProperty(PropertyName = "attachment")]
        public FacebookAttachment<T> Attachment { get; set; }
    }

    public static class FacebookAttachmentTypes
    {
        internal static readonly string[] Types = new string[] { "audio", "image", "video", "file" };
        public static readonly string Audio = Types[0];
        public static readonly string Image = Types[1];
        public static readonly string Video = Types[2];
        public static readonly string File = Types[3];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of Payload of the attachment, can either be <see cref="TemplatePayload"/> or <see cref="FileAttachmentPayload"/></typeparam>
    public class FacebookAttachment<T> where T : IFacebookAttachmentPayload
    {
        public FacebookAttachment(string type = null)
        {
            if (typeof(T) == typeof(TemplatePayload))
            {
                Type = "template";
            }
            else
            {
                if (FacebookAttachmentTypes.Types.Contains(type))
                    Type = type;
                else
                    throw new ArgumentOutOfRangeException("type", "type must be one of FacebookAttachmentTypes if Payload type is FileAttachmentPayload");
            }
        }

        /// <summary>
        /// Type of attachment, may be image, audio, video, file or template. For assets, max file size is 25MB.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; }
        /// <summary>
        /// Payload of attachment, can either be any of <see cref="TemplatePayload"/> or a <see cref="FileAttachmentPayload"/>
        /// </summary>
        [JsonProperty(PropertyName = "payload")]
        public T Payload { get; set; }
    }

    public interface IFacebookAttachmentPayload { }

    public class FileAttachmentPayload : IFacebookAttachmentPayload
    {
        /// <summary>
        /// Optional. URL of the file to upload. Max file size is 25MB (after encoding). A Timeout is set to 75 sec for videos and 10 secs for every other file type.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        /// <summary>
        /// Optional. Set to true to make the saved asset sendable to other message recipients. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = "is_reusable")]
        public bool IsReusable { get; set; }
    }

    public abstract class TemplatePayload : IFacebookAttachmentPayload
    {
        /// <summary>
        /// Value indicating the template type generic, button, media, receipt, etc
        /// </summary>
        [JsonProperty(PropertyName = "template_type")]
        public string TemplateType { get; protected set; }
    }

    public class GenericTemplatePayload : TemplatePayload
    {
        public GenericTemplatePayload()
        {
            TemplateType = "generic";
            Elements = new List<GenericTemplatePayloadElement>();
        }
        /// <summary>
        /// Optional. The aspect ratio used to render images specified by element.image_url. Must be horizontal (1.91:1) or square (1:1). Defaults to horizontal.
        /// </summary>
        [JsonProperty(PropertyName = "image_aspect_ratio")]
        public string ImageAspectRatio { get; set; }
        /// <summary>
        /// An array of element objects that describe instances of the generic template to be sent. 
        /// Specifying multiple elements will send a horizontally scrollable carousel of templates. A maximum of 10 elements is supported.
        /// </summary>
        [JsonProperty(PropertyName = "elements")]
        public List<GenericTemplatePayloadElement> Elements { get; set; }
    }
    public class ButtonTemplatePayload : TemplatePayload
    {
        public ButtonTemplatePayload()
        {
            TemplateType = "button";
        }
        /// <summary>
        /// UTF-8-encoded text of up to 640 characters. Text will appear above the buttons.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        /// Set of 1-3 buttons that appear as call-to-actions.
        /// Can be one of <see cref="UrlButton"/>, <see cref="PostPackButton"/>, <see cref="CallButton"/>, <see cref="LogInButton"/>, <see cref="LogOutButton"/>
        /// </summary>
        [JsonProperty(PropertyName = "buttons")]
        public List<Button> Buttons { get; set; }
    }
    public class MediaTemplatePayload : TemplatePayload
    {
        public MediaTemplatePayload()
        {
            TemplateType = "media";
        }
        /// <summary>
        /// Optional. Set to true to enable the native share button in Messenger for the template message. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = "sharable")]
        public bool Sharable { get; set; }
        /// <summary>
        /// An array containing 1 <see cref="MediaTemplatePayloadElement"/> object that describe the media in the message. A maximum of 1 element is supported.
        /// </summary>
        [JsonProperty(PropertyName = "elements")]
        public List<MediaTemplatePayloadElement> Elements { get; set; }
    }
    public class ReceiptTemplatePayload : TemplatePayload
    {
        public ReceiptTemplatePayload()
        {
            TemplateType = "receipt";
        }
        /// <summary>
        /// Optional. Set to true to enable the native share button in Messenger for the template message. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = "sharable")]
        public bool Sharable { get; set; }
        /// <summary>
        /// The recipient's name.
        /// </summary>
        [JsonProperty(PropertyName = "recipient_name"), JsonRequired]
        public string RecipientName { get; set; }
        /// <summary>
        /// Optional. The merchant's name. If present this is shown as logo text.
        /// </summary>
        [JsonProperty(PropertyName = "merchant_name")]
        public string MerchantName { get; set; }
        /// <summary>
        /// The order number. Must be unique.
        /// </summary>
        [JsonProperty(PropertyName = "order_number"), JsonRequired]
        public string OrderNumber { get; set; }
        /// <summary>
        /// The currency of the payment.
        /// </summary>
        [JsonProperty(PropertyName = "currency"), JsonRequired]
        public string Currency { get; set; }
        /// <summary>
        /// The payment method used. Providing enough information for the customer to decipher which payment method and account they used is recommended. This can be a custom string, such as, "Visa 1234".
        /// </summary>
        [JsonProperty(PropertyName = "payment_method"), JsonRequired]
        public string PaymentMethod { get; set; }
        /// <summary>
        /// Optional. Timestamp of the order in seconds.
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }
        /// <summary>
        /// Optional. Array of a maximum of 100 element objects that describe items in the order. Sort order of the elements is not guaranteed.
        /// </summary>
        [JsonProperty(PropertyName = "elements")]
        public List<ReceiptTemplatePayloadElement> Elements { get; set; }
        /// <summary>
        /// Optional. The shipping address of the order.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public ReceiptTemplatePayloadAddress Address { get; set; }
        /// <summary>
        /// The payment summary.
        /// </summary>
        [JsonProperty(PropertyName = "summary"), JsonRequired]
        public ReceiptTemplatePayloadSummary Summary { get; set; }
        /// <summary>
        /// Optional. An array of payment objects that describe payment adjustments, such as discounts.
        /// </summary>
        [JsonProperty(PropertyName = "adjustments")]
        public List<ReceiptTemplatePayloadAdjustments> Adjustments { get; set; }
    }



    public class ReceiptTemplatePayloadAdjustments
    {
        /// <summary>
        /// Required if the adjustments array is set. Name of the adjustment.
        /// </summary>
        [JsonProperty(PropertyName = "name"), JsonRequired]
        public string Name { get; set; }
        /// <summary>
        /// Required if the adjustments array is set. The amount of the adjustment
        /// </summary>
        [JsonProperty(PropertyName = "amount"), JsonRequired]
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// The property values of the summary object should be valid, well-formatted decimal numbers, using '.' (dot) as the decimal separator. 
    /// Please note that most currencies only accept up to 2 decimal places.
    /// </summary>
    public class ReceiptTemplatePayloadSummary
    {
        /// <summary>
        /// Optional. The sub-total of the order.
        /// </summary>
        [JsonProperty(PropertyName = "subtotal")]
        public decimal? Subtotal { get; set; }
        /// <summary>
        /// Optional. The shipping cost of the order.
        /// </summary>
        [JsonProperty(PropertyName = "shipping_cost")]
        public decimal? ShippingCost { get; set; }
        /// <summary>
        /// Optional. The tax of the order.
        /// </summary>
        [JsonProperty(PropertyName = "total_tax")]
        public decimal? TotalTax { get; set; }
        /// <summary>
        /// The total cost of the order, including sub-total, shipping, and tax.
        /// </summary>
        [JsonProperty(PropertyName = "total_cost"), JsonRequired]
        public decimal TotalCost { get; set; }
    }

    public class ReceiptTemplatePayloadAddress
    {
        /// <summary>
        /// The street address, line 1.
        /// </summary>
        [JsonProperty(PropertyName = "street_1")]
        public string Street_1 { get; set; }
        /// <summary>
        /// Optional. The street address, line 2.
        /// </summary>
        [JsonProperty(PropertyName = "street_2")]
        public string Street_2 { get; set; }
        /// <summary>
        /// The city name of the address.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        /// <summary>
        /// The postal code of the address.
        /// </summary>
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }
        /// <summary>
        /// The state abbreviation for U.S. addresses, or the region/province for non-U.S. addresses.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        /// <summary>
        /// The two-letter country abbreviation of the address.
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }

    public class ReceiptTemplatePayloadElement
    {
        /// <summary>
        /// The name to display for the item.
        /// </summary>
        [JsonProperty(PropertyName = "title"), JsonRequired]
        public string Title { get; set; }
        /// <summary>
        /// Optional. The subtitle for the item, usually a brief item description.
        /// </summary>
        [JsonProperty(PropertyName = "subtitle")]
        public string Subtitle { get; set; }
        /// <summary>
        /// Optional. The quantity of the item purchased.
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
        /// <summary>
        /// The price of the item. For free items, '0' is allowed.
        /// </summary>
        [JsonProperty(PropertyName = "price"), JsonRequired]
        public decimal Price { get; set; }
        /// <summary>
        /// Optional. The currency of the item price.
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Optional. The URL of an image to be displayed with the item.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
    }

    public class MediaTemplatePayloadElement
    {
        /// <summary>
        /// The type of media being sent - image or video is supported.
        /// </summary>
        [JsonProperty(PropertyName = "media_type"), JsonRequired]
        public string MediaType { get; set; }
        /// <summary>
        /// The attachment ID of the image or video. Cannot be used if url is set.
        /// </summary>
        [JsonProperty(PropertyName = "attachment_id")]
        public string AttachmentId { get; set; }
        /// <summary>
        /// The URL of the image. Cannot be used if attachment_id is set.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        /// <summary>
        /// An array of button objects to be appended to the template. A maximum of 1 button is supported.
        /// Can be one of <see cref="UrlButton"/>, <see cref="PostPackButton"/>, <see cref="CallButton"/>, <see cref="LogInButton"/>, <see cref="LogOutButton"/>
        /// </summary>
        [JsonProperty(PropertyName = "buttons")]
        public List<Button> Buttons { get; set; }
    }

    public class GenericTemplatePayloadElement
    {
        /// <summary>
        /// The title to display in the template. 80 character limit.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        /// <summary>
        /// Optional. The subtitle to display in the template. 80 character limit.
        /// </summary>
        [JsonProperty(PropertyName = "subtitle")]
        public string Subtitle { get; set; }
        /// <summary>
        /// Optional. The URL of the image to display in the template.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        /// <summary>
        /// Optional. The default action executed when the template is tapped.
        /// </summary>
        [JsonProperty(PropertyName = "default_action")]
        public DefaultAction DefaultAction { get; set; }
        /// <summary>
        /// Optional. An array of buttons to append to the template. A maximum of 3 buttons per element is supported.
        /// Can be one of <see cref="UrlButton"/>, <see cref="PostPackButton"/>, <see cref="CallButton"/>, <see cref="LogInButton"/>, <see cref="LogOutButton"/>
        /// </summary>
        [JsonProperty(PropertyName = "buttons")]
        public List<Button> Buttons { get; set; }
    }

    public class DefaultAction : Button
    {
        public DefaultAction()
        {
            Type = "web_url";
        }
        /// <summary>
        /// This URL is opened in a mobile browser when the button is tapped. Must use HTTPS protocol if messenger_extensions is true.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        /// <summary>
        /// Optional. Height of the Webview. Valid values: compact, tall, full. Defaults to full.
        /// </summary>
        [JsonProperty(PropertyName = "webview_height_ratio")]
        public string WebviewHeightRatio { get; set; }
        /// <summary>
        /// Optional. Must be true if using Messenger Extensions.
        /// </summary>
        [JsonProperty(PropertyName = "messenger_extensions")]
        public bool MessengerExtensions { get; set; }
        /// <summary>
        /// The URL to use on clients that don't support Messenger Extensions. 
        /// If this is not defined, the url will be used as the fallback. It may only be specified if messenger_extensions is true.
        /// </summary>
        [JsonProperty(PropertyName = "fallback_url")]
        public string FallbackUrl { get; set; }
        /// <summary>
        /// Optional. Set to hide to disable the share button in the Webview (for sensitive info). This does not affect any shares initiated by the developer using Extensions.
        /// </summary>
        [JsonProperty(PropertyName = "webview_share_button")]
        public string WebviewShareButton { get; set; }
    }





    public abstract class Button
    {
        /// <summary>
        /// Type of button.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
    }

    public class UrlButton : Button
    {
        public UrlButton()
        {
            Type = "web_url";
        }
        /// <summary>
        /// Button title. 20 character limit.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        /// <summary>
        /// This URL is opened in a mobile browser when the button is tapped. Must use HTTPS protocol if messenger_extensions is true.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        /// <summary>
        /// Optional. Height of the Webview. Valid values: compact, tall, full. Defaults to full.
        /// </summary>
        [JsonProperty(PropertyName = "webview_height_ratio")]
        public string WebviewHeightRatio { get; set; }
        /// <summary>
        /// Optional. Must be true if using Messenger Extensions.
        /// </summary>
        [JsonProperty(PropertyName = "messenger_extensions")]
        public bool MessengerExtensions { get; set; }
        /// <summary>
        /// The URL to use on clients that don't support Messenger Extensions. 
        /// If this is not defined, the url will be used as the fallback. It may only be specified if messenger_extensions is true.
        /// </summary>
        [JsonProperty(PropertyName = "fallback_url")]
        public string FallbackUrl { get; set; }
        /// <summary>
        /// Optional. Set to hide to disable the share button in the Webview (for sensitive info). This does not affect any shares initiated by the developer using Extensions.
        /// </summary>
        [JsonProperty(PropertyName = "webview_share_button")]
        public string WebviewShareButton { get; set; }
    }
    public class PostBackButton : Button
    {
        public PostBackButton()
        {
            Type = "postback";
        }
        /// <summary>
        /// Button title. 20 character limit.
        /// </summary>
        [JsonProperty(PropertyName = "title"), JsonRequired]
        public string Title { get; set; }
        /// <summary>
        /// This data will be sent back to your webhook. 1000 character limit.
        /// </summary>
        [JsonProperty(PropertyName = "payload")]
        public string Payload { get; set; }
    }
    public class CallButton : Button
    {
        public CallButton()
        {
            Type = "phone_number";
        }
        /// <summary>
        /// Button title. 20 character limit.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        /// <summary>
        /// Format must have "+" prefix followed by the country code, area code and local number. For example, +16505551234.
        /// </summary>
        [JsonProperty(PropertyName = "payload")]
        public string Payload { get; set; }
    }
    public class LogInButton : Button
    {
        public LogInButton()
        {
            Type = "account_link";
        }
        /// <summary>
        /// <see href="https://developers.facebook.com/docs/messenger-platform/account-linking/authentication">Authentication</see> callback URL. Must use HTTPS protocol.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
    public class LogOutButton : Button
    {
        public LogOutButton()
        {
            Type = "account_unlink";
        }
    }

}
