using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs
{
    public class WhatCanYouDoDialog : Dialog
    {
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            await dc.Context.SendActivityAsync("ඔබට අවශ්යය වන ආහාර වර්ගය ඇනවුම් කිරීමට මට ඔබට සහය විය හැක.ඔබට අවශ්යය අහාර වර්ගය ඇතුලත් කරන්න.", cancellationToken: cancellationToken);

            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
