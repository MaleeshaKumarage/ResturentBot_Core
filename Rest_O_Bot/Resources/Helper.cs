using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using ProductInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace Rest_O_Bot.Resources
{
    internal enum PhraseType
    {
        Positive,
        Negative,
        Other
    }
    public class Helper
    {
        internal static PhraseType GetPhraseType(string phrase)
        {
            if (!string.IsNullOrEmpty(phrase))
            {
                switch (phrase.ToLower().Trim())
                {
                    case "no":
                    case "nope":
                    case "nah":
                    case "no thanks":
                    case "cancel":
                    {
                        return PhraseType.Negative;
                    }

                    case "yes":
                    case "yep":
                    case "yeah":
                    case "yea":
                    case "yes please":
                    case "true":
                        return PhraseType.Positive;
                }
                if (phrase.ToLower().Contains("yes") || phrase.ToLower().Contains("right"))
                    return PhraseType.Positive;
            }
            return PhraseType.Other;
        }


        //internal static string GetFormattedItemSearchQueryFromEntities(Rasa.ApiClient.Model.ParseResult parseResult)
        //{
        //    if (!string.IsNullOrWhiteSpace(parseResult?.Text))
        //    {
        //        if (parseResult.Entities != null && parseResult.Entities.Count > 0)
        //        {
        //            var etis = parseResult.Entities.Where(a => a._Entity == "brand" || a._Entity == "product").ToList();
        //            if (etis.Count(a => a._Entity == "brand") == 1 && etis.Count(a => a._Entity == "product") == 1)
        //            {
        //                return etis.First(a => a._Entity == "brand").Value.ToString() + " " + etis.First(a => a._Entity == "product").Value.ToString();
        //            }
        //            else if (etis.Count == 1 && etis.Any(a => a._Entity == "product"))
        //            {
        //                return etis.First(a => a._Entity == "product").Value.ToString();
        //            }
        //            else if (etis.Count > 2)
        //            {
        //                return string.Join(" ", etis.OrderBy(a => a.Start).Select(a => a.Value?.ToString() ?? string.Empty).Where(a => !string.IsNullOrWhiteSpace(a)));
        //            }
        //            else if ((etis.Count == 1 && etis.Any(a => a._Entity == "brand")) || etis.All(a => a._Entity == "brand"))
        //            {
        //                return null;
        //                //return parseResult.Text.ToLower().Trim();
        //            }
        //        }
        //    }
        //    return null;
        //}

        //internal static FacebookChannelData<TemplatePayload> CreateFacebookReciptAttachmentFromCartVM(CartVM cartVM, string recipientName)
        //{
        //    var data = new FacebookChannelData<TemplatePayload>();
        //    var payload = new ReceiptTemplatePayload();
        //    payload.Elements = new List<ReceiptTemplatePayloadElement>();

        //    foreach (var item in cartVM.CartItems)
        //    {
        //        payload.Elements.Add(new ReceiptTemplatePayloadElement { Title = item.ProductName, /*Subtitle = "100% Soft and Luxurious Cotton",*/ Quantity = item.Quantity, Price = item.Price, Currency = "LKR", ImageUrl = item.ImageUrl });
        //    }

        //    payload.Sharable = false;
        //    payload.RecipientName = recipientName;
        //    payload.OrderNumber = cartVM.Id.ToString("N");
        //    payload.Currency = "LKR";
        //    payload.PaymentMethod = "Cash on Delivery";
        //    //payload.OrderURL = "http=//petersapparel.parseapp.com/order?order_id=123456";
        //    //payload.Timestamp = "1428444852";

        //    //payload.Elements = receiptTemplatePayloadElements;
        //    //payload.Address = new ReceiptTemplatePayloadAddress()
        //    //{
        //    //    Street_1 = "1 Hacker Way",
        //    //    //Street_2 = "",
        //    //    City = "Menlo Park",
        //    //    PostalCode = "94025",
        //    //    State = "CA",
        //    //    Country = "US"
        //    //};
        //    payload.Summary = new ReceiptTemplatePayloadSummary
        //    {
        //        //Subtotal = 75.00m,
        //        //ShippingCost = 4.95m,
        //        //TotalTax = 6.19m,
        //        TotalCost = cartVM.TotalPrice,
        //    };
        //    //payload.Adjustments = new List<ReceiptTemplatePayloadAdjustments>
        //    //{
        //    //    new ReceiptTemplatePayloadAdjustments
        //    //    {
        //    //        Name="New Customer Discount",
        //    //        Amount=20m
        //    //    },
        //    //    new ReceiptTemplatePayloadAdjustments
        //    //    {
        //    //        Name="$10 Off Coupon",
        //    //        Amount=10m
        //    //    }
        //    //};

        //    data.Attachment.Payload = payload;
        //    return data;
        //}

        internal static FacebookChannelData<TemplatePayload> CreateFacebookGenericCardAttachment(List<Product> ProductList)
        {
            var data = new FacebookChannelData<TemplatePayload>();
            var payload = new GenericTemplatePayload();
            ProductList = ProductList?.Take(6).ToList();

            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (var product in ProductList)
                {
                    payload.Elements.Add(new GenericTemplatePayloadElement
                    {
                        Title = product.Name_SI,
                        Subtitle = $"{"රු "}{product.Price}{"/="}",
                        ImageUrl =  product.ImageUrl  ,
                        Buttons = new List<Button>{
                                new PostBackButton(){Title="Buy One",Payload=$"{"buynow"}{"!@#$"}{product.Id}{"!@#$"}{1}"},
                                new PostBackButton(){Title="Buy Two",Payload=$"{"buynow"}{"!@#$"}{product.Id}{"!@#$"}{2}"},
                                new PostBackButton(){Title="Buy More",Payload=$"{"buynow"}{"!@#$"}{product.Id}{"!@#$"}{"more"}"},
                              }
                    });
                }
                data.Attachment.Payload = payload;
                return data;
            }
            else
            {
                return null;
            }
        }

        internal static List<Attachment> CreateHeroCardAttachments(List<Product> ProductList)
        {
            var attachments = new List<Attachment>();
            ProductList = ProductList?.Take(6).ToList();

            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (var product in ProductList)
                {
                    var card = new HeroCard
                    {
                        Images = new List<CardImage> { new CardImage { Url = product.ImageUrl } },
                        Title = product.Name_SI,
                        Subtitle = $"{"රු "}{product.Price}{"/="}",
                        Buttons = new List<CardAction>
                        {
                            new CardAction { Title="Buy One",Value=$"{"buynow"}{"!@#$"}{product.Id}{"!@#$"}{1}",Type=ActionTypes.PostBack,DisplayText="Buy One"},
                            new CardAction { Title="Buy Two",Value=$"{"buynow"}{"!@#$"}{product.Id}{"!@#$"}{2}",Type=ActionTypes.PostBack,DisplayText="Buy Two"},
                            new CardAction { Title="Buy More",Value=$"{"buynow"}{"!@#$"}{product.Id}{"!@#$"}{"more"}",Type=ActionTypes.PostBack,DisplayText="Buy More"}
                        }
                    };
                    attachments.Add(card.ToAttachment());
                }
                return attachments;
            }
            else
            {
                return null;
            }
        }
        //internal static List<Attachment> CreateReciptCardAttachmentFromCartVM(CartVM cartVM, string recipientName)
        //{
        //    var card = new ReceiptCard();
        //    throw new NotImplementedException();
        //}

        internal static async Task SendConfirmationPrompt(string prompt, ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            var activity = turnContext.Activity.CreateReply(prompt);
            activity.SuggestedActions = new Microsoft.Bot.Schema.SuggestedActions
            {
                Actions = new List<CardAction>
                    {
                        new CardAction { Title = "ඔව්", Value = "yes", Type = ActionTypes.PostBack },
                        new CardAction { Title = "නැහැ", Value = "no", Type = ActionTypes.PostBack }
                    }
            };
            await turnContext.SendActivityAsync(activity, cancellationToken: cancellationToken);
        }

        internal static async Task GetSugeestedActionList(string message, List<string> buttonNames, ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            var activity = turnContext.Activity.CreateReply(message);
            List<CardAction> _nameList = new List<CardAction>();
            foreach (var item in buttonNames)
            {
                _nameList.Add(new CardAction { Title = item, Value = item, Type = ActionTypes.PostBack });
            }
            activity.SuggestedActions = new Microsoft.Bot.Schema.SuggestedActions
            {
                Actions = _nameList
            }; 
                    
                
           
            await turnContext.SendActivityAsync(activity, cancellationToken: cancellationToken);
        }

        //internal static FacebookChannelData<TemplatePayload> CreateFacebookGenericCardAttachmentUserDetails(List<String> textList, string header)
        //{
        //    var data = new FacebookChannelData<TemplatePayload>();
        //    var payload = new GenericTemplatePayload();
        //    payload.Elements.Add(new GenericTemplatePayloadElement
        //    {
        //        Title = header,
        //        Subtitle = string.Join(Environment.NewLine, textList),

        //    });

        //    data.Attachment.Payload = payload;
        //    return data;


        //}
        //internal static List<CardAction> GetCardActionListFromSearchResultList(List<CitySearchResult> list)
        //{

        //    var cardList = new List<CardAction>() { };
        //    foreach (var item in list)
        //    {
        //        cardList.Add(new CardAction() { Title = item.Name, Type = ActionTypes.ImBack, Value = item.Name });
        //    }


        //    return cardList;
        //}

        internal static List<Attachment> CreateHeroCardContactDetails(List<ContactDetails> ContactList)
        {
            var attachments = new List<Attachment>();
            

            if (ContactList != null && ContactList.Count > 0)
            {
                foreach (var contact in ContactList)
                {
                    var card = new HeroCard
                    {
                       
                        Title = contact.Type,
                        Subtitle = contact.value,
                        
                    };
                    attachments.Add(card.ToAttachment());
                }
                return attachments;
            }
            else
            {
                return null;
            }
        }


    }
}