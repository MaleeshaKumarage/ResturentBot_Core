// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rest_O_Bot.Dialogs;

namespace Rest_O_Bot.Bots
{
    public class DialogAndWelcomeBot<T> : DialogBot<T>
        where T : Dialog
    {
        public DialogAndWelcomeBot(ConversationState conversationState, UserState userState, T dialog, IEnumerable<Dialog> otherDialogs, ILogger<DialogBot<T>> logger)
            : base(conversationState, userState, dialog, otherDialogs, logger)
        {
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                // Greet anyone that was not the target (recipient) of this message.
                // To learn more about Adaptive Cards, see https://aka.ms/msbot-adaptivecards for more details.
                if (member.Id != turnContext.Activity.Recipient.Id)
                {

                    var response = MessageFactory.Text($"හායි !!, මම ඔබට සහය වන්නේ කෙසේද ?");
                    await turnContext.SendActivityAsync(response, cancellationToken);
                    await Dialog.RunAsync(Dialogs, turnContext, ConversationState.CreateProperty<DialogState>("DialogState"), cancellationToken);
                }
            }
        }

        // Load attachment from embedded resource.
        private Attachment CreateAdaptiveCardAttachment()
        {
            var cardResourcePath = GetType().Assembly.GetManifestResourceNames().First(name => name.EndsWith("welcomeCard.json"));

            using (var stream = GetType().Assembly.GetManifestResourceStream(cardResourcePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    var adaptiveCard = reader.ReadToEnd();
                    return new Attachment()
                    {
                        ContentType = "application/vnd.microsoft.card.adaptive",
                        Content = JsonConvert.DeserializeObject(adaptiveCard),
                    };
                }
            }
        }

        private System.String GreetMessage()
        {
            DateTime currentTime = DateTime.Now;
            switch (currentTime.Hour)
            {
                case 1: return " සුභ රාත්‍රීයක්..";
                case 2: return " සුභ රාත්‍රීයක්..";
                case 3: return " සුභ රාත්‍රීයක්..";
                case 4: return " සුභ උදෑසනක්..";
                case 5: return " සුභ උදෑසනක්..";
                case 6: return " සුභ උදෑසනක්..";
                case 7: return " සුභ උදෑසනක්..";
                case 8: return " සුභ උදෑසනක්..";
                case 9: return " සුභ උදෑසනක්..";
                case 10: return " සුභ උදෑසනක්..";
                case 11: return " සුභ උදෑසනක්..";
                case 12: return " සුභ මධ්‍යහනක්..";
                case 13: return " සුභ මධ්‍යහනක්..";
                case 14: return " සුභ මධ්‍යහනක්..";
                case 15: return " සුභ මධ්‍යහනක්..";
                case 16: return " සුභ සැන්දෑවක්..";
                case 17: return " සුභ සැන්දෑවක්..";
                case 18: return " සුභ සැන්දෑවක්..";
                case 19: return " සුභ සැන්දෑවක්..";
                case 20: return " සුභ රාත්‍රීයක්..";
                case 21: return " සුභ රාත්‍රීයක්..";
                case 22: return " සුභ රාත්‍රීයක්..";
                case 23: return " සුභ රාත්‍රීයක්..";
                case 24: return " සුභ රාත්‍රීයක්..";

                default:
                    return " සුභ දවසක්..";

            }


        }
    }
}

