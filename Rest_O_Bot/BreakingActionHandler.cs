
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Bot
{
    internal enum BreakingAction
    {
        OpenDialog_AddItemToCart,
        OpenDialog_ViewCart,
        Clear_Cart
    }
    internal class BreakingActionHandler
    {
        internal static bool IsBreakingAction(string text, out BreakingAction? breakingAction)
        {
            breakingAction = null;
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains("buynow!@#$"))
                {
                    breakingAction = Bot.BreakingAction.OpenDialog_AddItemToCart;
                    return true;
                }
                else if (text.Contains("viewcart@%?!"))
                {
                    breakingAction = Bot.BreakingAction.OpenDialog_ViewCart;
                    return true;
                }
                else if (text.Contains("clearcart@%?!"))
                {
                    breakingAction = Bot.BreakingAction.Clear_Cart;
                    return true;
                }
            }
            return false;
        }

        internal static async Task<DialogTurnResult> ExecuteBrakingAction(BreakingAction action, DialogContext dc,  CancellationToken cancellationToken = default)
        {
            switch (action)
            {
                case BreakingAction.OpenDialog_AddItemToCart:
                {
                    var splitText = dc.Context.Activity.Text.Split("!@#$");
                    await dc.CancelAllDialogsAsync(cancellationToken);
                   
                    return await dc.BeginDialogAsync(nameof(Dialogs.AddItemToCartDialog), options: new Resources.OrderItemObj { ProductId = splitText[1], QuantityString = splitText[2] }, cancellationToken: cancellationToken);
                }
                //case BreakingAction.OpenDialog_ViewCart:
                //{
                //    await dc.CancelAllDialogsAsync(cancellationToken);
                //    return await dc.BeginDialogAsync(nameof(Dialogs.ViewCartDialog), cancellationToken: cancellationToken);
                //}
                //case BreakingAction.Clear_Cart:
                //{
                //    await mediator.Send(new Application.Commands.ClearCartCommand(dc.Context.Activity.GetConversationReference()), cancellationToken: cancellationToken);
                //    await dc.Context.SendActivityAsync("I cleared your shopping cart. Let's start again. What do you want to order?", cancellationToken: cancellationToken);
                //    return await dc.CancelAllDialogsAsync(cancellationToken);
                //}
                default:
                    return null;
            }
        }
    }

}
