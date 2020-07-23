using Microsoft.Bot.Builder.Dialogs;
using Rest_O_Bot.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs
{
    public class CreatedByDialog : Dialog
    {
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {
            //await dc.Context.SendActivityAsync("ශතවර්ෂයකට පමණ පෙර ශ්‍රී ලංකාවේ ප්‍රමුඛතම බේකරි සහ ආහාර සැපයුම්කරු ශ්‍රී ලංකාවට සේවය කිරීම සඳහා හොඳම ආහාර වේලක් සමඟ ගමනක් ආරම්භ කළේය. ABC බේකරිය යටතට ගැනෙන සියලු දෙනා විසින් තවමත් පිළිගෙන ඇති සම්ප්‍රදායන් සහ සාරධර්ම මත පදනම් වූ අපි නිෂ්පාදනය කරන සෑම ආහාර අයිතමයකටම අමතර පෞද්ගලික කැපවීමක් සහ සැලකිල්ලක් ඇති බවට අපි සහතික වෙමු.", cancellationToken: cancellationToken);
            var reply = dc.Context.Activity.CreateReply();
            await dc.Context.SendActivityAsync("මාව සාදන ලද්දේ මලීෂ කුමාරගේ විසින්.ඔහුව සම්බන්ද කරගැනීමට පහත ක්‍රම භාවිතා කළ හැක.", cancellationToken: cancellationToken);
            reply.AttachmentLayout = "carousel";
            reply.Attachments = Helper.CreateHeroCardContactDetails(new List<ContactDetails>() { new ContactDetails() { Type = "LinkedIN", value = "https://www.linkedin.com/in/maleesha-kumarage-b44220120/" }, new ContactDetails() { Type = "E Mail", value = "maleesha@msk@gmail.com" }, new ContactDetails() { Type = "Facebook", value = "https://www.facebook.com/maleesha.mk" } });
            await dc.Context.SendActivityAsync(reply, cancellationToken: cancellationToken);
            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
