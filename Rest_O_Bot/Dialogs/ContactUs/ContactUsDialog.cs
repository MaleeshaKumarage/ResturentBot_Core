using ApiClient.Model;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Rest_O_Bot.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs
{
    public class ContactUsDialog : Dialog
    {
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            var reply = dc.Context.Activity.CreateReply();
            await dc.Context.SendActivityAsync("අපව සම්බන්ද කරගැනීම සදහා පහත ක්‍රම භාවිතා කල හැක.", cancellationToken: cancellationToken);
            reply.AttachmentLayout = "carousel";
            reply.Attachments = Helper.CreateHeroCardContactDetails(new List<ContactDetails> (){ new ContactDetails() { Type = "Land Phone", value = "034- XX XX XXX" }, new ContactDetails() { Type = "E Mail", value = "Hello @abc.lk" } });
            await dc.Context.SendActivityAsync(reply, cancellationToken: cancellationToken);



            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
