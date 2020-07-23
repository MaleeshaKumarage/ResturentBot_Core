using Microsoft.Bot.Builder.Dialogs;
using Rest_O_Bot.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs
{
    public class FoodCategoriesDialog : Dialog
    {
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            await Helper.GetSugeestedActionList("ඔබට අවශ්යය ආහාර කාණ්ඩය පහතින් තෝරන්න.", new List<string>() {"Cat 1","Cat 2" }, dc.Context, cancellationToken);
            return EndOfTurn;
        }
    }
}
