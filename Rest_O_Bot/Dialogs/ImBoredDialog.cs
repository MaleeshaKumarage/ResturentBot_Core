using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs
{
    public class ImBoredDialog : Dialog
    {
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            await dc.Context.SendActivityAsync("හොදම විසදුම මොනවාහරි කාලා ඉන්න එක.මොනවද ඔයාට ඕන ???", cancellationToken: cancellationToken);

            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
