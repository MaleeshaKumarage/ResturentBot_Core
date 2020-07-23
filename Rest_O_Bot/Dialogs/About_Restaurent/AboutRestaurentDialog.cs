using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Dialogs.About_Restaurent
{
    public class AboutRestaurentDialog : Dialog
    {
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {

            await dc.Context.SendActivityAsync("ශතවර්ෂයකට පමණ පෙර ශ්‍රී ලංකාවේ ප්‍රමුඛතම බේකරි සහ ආහාර සැපයුම්කරු ශ්‍රී ලංකාවට සේවය කිරීම සඳහා හොඳම ආහාර වේලක් සමඟ ගමනක් ආරම්භ කළේය. ABC බේකරිය යටතට ගැනෙන සියලු දෙනා විසින් තවමත් පිළිගෙන ඇති සම්ප්‍රදායන් සහ සාරධර්ම මත පදනම් වූ අපි නිෂ්පාදනය කරන සෑම ආහාර අයිතමයකටම අමතර පෞද්ගලික කැපවීමක් සහ සැලකිල්ලක් ඇති බවට අපි සහතික වෙමු.", cancellationToken: cancellationToken);

            return await dc.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
