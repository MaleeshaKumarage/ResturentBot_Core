using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using ProductInventory;
using Rest_O_Bot.Resources;
using SinhalaTokenizationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs
{
    public class AddItemToCartDialog : ComponentDialog
    {
        private readonly ConversationState _conversationState;
        private readonly IStatePropertyAccessor<OrderItemObj> OrderObjectStatePropertyAccessor;
        private const string OrderItemDetail = "OrderObj";
     
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext outerDc, object options = null, CancellationToken cancellationToken = default)
        {
            await OrderObjectStatePropertyAccessor.SetAsync(outerDc.Context, (OrderItemObj)options, cancellationToken: cancellationToken);
            return await base.BeginDialogAsync(outerDc, options, cancellationToken);
        }
        public AddItemToCartDialog(ConversationState conversationState)
        {
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                AddedItemToCartStep,
                GetQuantityStep,
                ConfirmStep,
                AnythingElseStep,
                ConfirmCheckoutStep
            }));
           
            _conversationState = conversationState;
            OrderObjectStatePropertyAccessor = _conversationState.CreateProperty<OrderItemObj>("OrderObj");
           

            AddDialog(new NumberPrompt<int>("QtyPrompt"));
            AddDialog(new ConfirmPrompt("AddCartConfirm"));
        }

        private async Task<DialogTurnResult> ConfirmCheckoutStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var phraseType = Helper.GetPhraseType(stepContext.Context.Activity.Text);
            switch (phraseType)
            {
                case PhraseType.Positive:
                {
                    return EndOfTurn;
                    //return await stepContext.ReplaceDialogAsync(nameof(ViewCartDialog), cancellationToken: cancellationToken);
                }
                case PhraseType.Negative:
                {
                    await stepContext.Context.SendActivityAsync("Ok. Let's checkout later.", cancellationToken: cancellationToken);
                    return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
                }
                default:
                {
                    await stepContext.CancelAllDialogsAsync(cancellationToken);
                    return await stepContext.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
                }
            }
        }

        private async Task<DialogTurnResult> AnythingElseStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var phraseType = Helper.GetPhraseType(stepContext.Context.Activity.Text);
            switch (phraseType)
            {
                case PhraseType.Positive:
                {
                    return await stepContext.ReplaceDialogAsync(nameof(FoodOrderDialog), cancellationToken: cancellationToken);
                }
                case PhraseType.Negative:
                {
                    //var cart = await Mediator.Send(new Application.Queries.ViewCartQuery(stepContext.Context.Activity.GetConversationReference()));
                    if (CartVM._cartList != null && CartVM._cartList.Count > 0)
                    {
                        await Helper.SendConfirmationPrompt("ඔබ මිලදී ගෙන අවසන් ද ?", stepContext.Context, cancellationToken);
                        return EndOfTurn;
                    }
                    else
                    {
                        await stepContext.Context.SendActivityAsync("ඔබට වෙනත් යමක් මිලදී ගැනීමට අවශයයි ද ?", cancellationToken: cancellationToken);
                        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
                    }
                }
                default:
                {
                    await stepContext.CancelAllDialogsAsync(cancellationToken: cancellationToken);
                    return await stepContext.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
                }
            }
        }

        private async Task<DialogTurnResult> AddedItemToCartStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var shoppingMessage = MessageFactory.Text("");
            var orderDetail = await OrderObjectStatePropertyAccessor.GetAsync(stepContext.Context, cancellationToken: cancellationToken);
            if (orderDetail.QuantityString.Contains("more"))
            {
                shoppingMessage = MessageFactory.Text("ඔබට අවශයය ප්‍රමාණය ඇතුලත් කරන්න.....");
                return await stepContext.PromptAsync("QtyPrompt", new PromptOptions { Prompt = shoppingMessage }, cancellationToken: cancellationToken);
            }
            else
            {
                var products = new ProductSearchService().GetAllProducts();

                var product = products.FirstOrDefault(a => a.Id == orderDetail.ProductId);

                
                shoppingMessage = MessageFactory.Text($" {product.Name_SI} {orderDetail.QuantityString} ක් ඇතුලත් කරන ල්දී.ඔබට වෙනත් යමක් මිලදී ගැනීමට අවශ්ය නම් සටහන් කරන්න.");
                await stepContext.Context.SendActivityAsync(shoppingMessage, cancellationToken);
                await OrderObjectStatePropertyAccessor.DeleteAsync(stepContext.Context, cancellationToken);

                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }
        }
        private async Task<DialogTurnResult> GetQuantityStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            int qty = 0;
            try
            {
                //qty = int.Parse(new TokenizationLibrary().GetNumericalValues((string)stepContext.Result));
                qty = (int)stepContext.Result;
            }
            catch (System.InvalidCastException)
            {
                await stepContext.CancelAllDialogsAsync(cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
            }
            catch (System.NullReferenceException)
            {
                await stepContext.CancelAllDialogsAsync(cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
            }
            var orderDetail = await OrderObjectStatePropertyAccessor.GetAsync(stepContext.Context, cancellationToken: cancellationToken);
            if (qty > 0)
            {
                var products =  new ProductSearchService().GetAllProducts();
                var product = products.FirstOrDefault(a => a.Id == orderDetail.ProductId);
                orderDetail.Quantity = qty;
                orderDetail.ProductName = product.Name_SI;
                await OrderObjectStatePropertyAccessor.SetAsync(stepContext.Context, orderDetail, cancellationToken: cancellationToken);
                var shoppingMessage = MessageFactory.Text($"{product.Name_SI} {qty} ක් ඇතුලත් කරන්නද ??");
                return await stepContext.PromptAsync("AddCartConfirm", new PromptOptions { Prompt = shoppingMessage }, cancellationToken: cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync($"හරි..එසේනම් {orderDetail.ProductName} ඇතුලත් කරන්නෙ නෑ.ඔබට වෙනත් යමක් අවශ්යයිද?");
                await OrderObjectStatePropertyAccessor.DeleteAsync(stepContext.Context, cancellationToken);
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }
        }
        private async Task<DialogTurnResult> ConfirmStep(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            bool result = false;
            try
            {
                result = (bool)stepContext.Result;
            }
            catch (System.InvalidCastException)
            {
                await stepContext.CancelAllDialogsAsync(cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
            }
            catch (System.NullReferenceException)
            {
                await stepContext.CancelAllDialogsAsync(cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(MainDialog), "no_intro_msg", cancellationToken: cancellationToken);
            }
            var orderDetail = await OrderObjectStatePropertyAccessor.GetAsync(stepContext.Context, cancellationToken: cancellationToken);
            if (result)
            {
                //var cart = await Mediator.Send(new AddItemToCartCommand(stepContext.Context.Activity.GetConversationReference(), orderDetail.ProductId, orderDetail.Quantity));
                var shoppingMessage = MessageFactory.Text($" {orderDetail.ProductName} {orderDetail.Quantity} ක් ඇතුලත් කරන ල්දී.ඔබට වෙනත් යමක් මිලදී ගැනීමට අවශ්ය නම් සටහන් කරන්න.");
                await stepContext.Context.SendActivityAsync(shoppingMessage, cancellationToken: cancellationToken);
                await OrderObjectStatePropertyAccessor.DeleteAsync(stepContext.Context, cancellationToken);
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync($"හරි..එසේනම්  {orderDetail.ProductName} ඇතුලත් කරන්නෙ නෑ. ඔබට වෙනත් යමක් අවශ්යයිද?");
                await OrderObjectStatePropertyAccessor.DeleteAsync(stepContext.Context, cancellationToken);
                return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }
        }
    }

}
