using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using ProductInventory;
using Rest_O_Bot.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.Bot.Builder.Dialogs.Choices.Channel;

namespace Rest_O_Bot.Dialogs
{
    public class FoodOrderDialog : Dialog
    {
        private readonly ConversationState _conversationState;
        private readonly IStatePropertyAccessor<string> LastAskedAccessor;
        private const string LastAskedAccessorName = "Question_LastAsked_FoodSearch";
        public FoodOrderDialog(ConversationState conversationState)
        {
            _conversationState = conversationState;
            LastAskedAccessor = _conversationState.CreateProperty<string>(LastAskedAccessorName);
        }
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            if (options == null)
            {
                await dc.Context.SendActivityAsync("ඔබට  අවශ්යය ආහාර වර්ගය ඇතුලත් කරන්න...", cancellationToken: cancellationToken);
                //return EndOfTurn;
            }
            else if (options != null && options is ApiClient.Model.ParseResult)
            {
                await ShowProductListAndAskForMore(dc, (options as ApiClient.Model.ParseResult).Entities.FirstOrDefault().Value.ToString(), cancellationToken);
            }
            else
            {
                return await dc.EndDialogAsync(cancellationToken: cancellationToken);
            }
            return EndOfTurn;
        }

        public override async Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default)
        {
            var lastaskedquestion = await LastAskedAccessor.GetAsync(dc.Context, cancellationToken: cancellationToken);
            switch (lastaskedquestion)
            {
                case "anythingelse":
                {
                    var phraseType = Helper.GetPhraseType(dc.Context.Activity.Text);
                    switch (phraseType)
                    {
                        case PhraseType.Positive:
                        {
                            await LastAskedAccessor.DeleteAsync(dc.Context, cancellationToken: cancellationToken);
                            return await dc.ReplaceDialogAsync(nameof(FoodOrderDialog), cancellationToken: cancellationToken);
                        }
                        case PhraseType.Negative:
                        {
                            //var cart = await Mediator.Send(new Application.Queries.ViewCartQuery(dc.Context.Activity.GetConversationReference()));
                            //if (cart != null && cart.NumberOfItems > 0)
                            //{
                            //    await Helper.SendConfirmationPrompt("Do You want to checkout?", dc.Context, cancellationToken);
                            //    await LastAskedAccessor.SetAsync(dc.Context, "wanttocheckout", cancellationToken: cancellationToken);
                            //    return EndOfTurn;
                            //}
                            //else
                            //{
                                await dc.Context.SendActivityAsync("මට ඔබට සහය විය හැක්කේ කෙසේද ?", cancellationToken: cancellationToken);
                                return await dc.EndDialogAsync(cancellationToken: cancellationToken);
                            //}
                        }
                        default:
                        {
                            await LastAskedAccessor.DeleteAsync(dc.Context, cancellationToken: cancellationToken);
                            await dc.CancelAllDialogsAsync(cancellationToken);
                            return await dc.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
                        }
                    }
                }
                case "wanttocheckout":
                {
                    var phraseType = Helper.GetPhraseType(dc.Context.Activity.Text);
                    switch (phraseType)
                    {
                        case PhraseType.Positive:
                        {
                            // return await dc.ReplaceDialogAsync(nameof(ViewCartDialog), cancellationToken: cancellationToken);
                            return EndOfTurn;
                        }
                        case PhraseType.Negative:
                        {
                            await dc.Context.SendActivityAsync("Is there anything else I can do for you?", cancellationToken: cancellationToken);
                            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
                        }
                        default:
                        {
                            await dc.CancelAllDialogsAsync(cancellationToken);
                            return await dc.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
                        }
                    }
                }
                default:
                {
                    await LastAskedAccessor.DeleteAsync(dc.Context, cancellationToken: cancellationToken);
                    await dc.CancelAllDialogsAsync(cancellationToken);
                    return await dc.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
                }
            }
            return EndOfTurn;
        }


        private async Task SendProductList(ITurnContext turnContext, List<Product> productList, CancellationToken cancellationToken)
        {
            //var productList=new ProductSearchService().GetAllProducts();
            var reply = turnContext.Activity.CreateReply();
            if (turnContext.Activity.ChannelId == Microsoft.Bot.Connector.Channels.Facebook)
            {
                reply.ChannelData = Helper.CreateFacebookGenericCardAttachment(productList);
            }
            else
            {
                reply.AttachmentLayout = "carousel";
                reply.Attachments = Helper.CreateHeroCardAttachments(productList);
            }
            await turnContext.SendActivityAsync(reply, cancellationToken: cancellationToken);
        }
        private async Task ShowProductListAndAskForMore(DialogContext dc, String SearchQuery, CancellationToken cancellationToken)
        {
            var productList = new ProductSearchService().SearchProductByName(SearchQuery);
            if (productList != null && productList.Count > 0)
            {
                await SendProductList(dc.Context, productList, cancellationToken);
            }
            else
            {
                await dc.Context.SendActivityAsync("කණගාටුයි මට ඔබ වෙනුවෙන් කිසිදු නිෂ්පාදනයක් සොයාගත නොහැකි විය.", cancellationToken: cancellationToken);
            }
            await Helper.SendConfirmationPrompt("ඔබට වෙනත් යමක් අවශයයිද ??", dc.Context, cancellationToken);
            await LastAskedAccessor.SetAsync(dc.Context, "anythingelse", cancellationToken: cancellationToken);
        }


    }
}
