// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ApiClient.Model;
using DBConnector;
using Google.Cloud.Translation.V2;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using Rest_O_Bot.Bot;
using Rest_O_Bot.CognitiveModels;
using Rest_O_Bot.Dialogs.About_Restaurent;
using Rest_O_Bot.Resources;
using TranslationHandler;

namespace Rest_O_Bot.Dialogs
{
    public class MainDialog : Dialog
    {

        protected readonly ILogger Logger;
        private readonly ApiClient.Api.IModelApi _modelApi;
        private readonly ApiClient.Api.IMessagingApi _messagingApi;

        // Dependency injection uses this constructor to instantiate MainDialog
        public MainDialog(ILogger<MainDialog> logger, ApiClient.Api.IMessagingApi messagingApi, ApiClient.Api.IModelApi modelApi)
            : base(nameof(MainDialog))
        {

            Logger = logger;
            _modelApi = modelApi;
            _messagingApi = messagingApi;

        }



        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default)
        {


           
            return await HandleMessage(dc, cancellationToken);
        }
        public override async Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default)
        {

            
            return await HandleMessage(dc, cancellationToken);
        }


        private async Task<DialogTurnResult> HandleMessage(DialogContext dc, CancellationToken cancellationToken = default)
        {
            if (BreakingActionHandler.IsBreakingAction(dc.Context.Activity.Text, out BreakingAction? action))
            {
                return await BreakingActionHandler.ExecuteBrakingAction(action.Value, dc, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(dc.Context.Activity.Text))
            {
                if (dc.Context.Activity.Text != null && !IsAllCharSinhala(dc.Context.Activity.Text.Replace(" ", "")))
                {
                    dc.Context.Activity.Text = await new Translation().EnglishTOSinhala(dc.Context.Activity.Text);
                }
                var intent = await new EntityClassifier().GetResultAsync(dc, _modelApi);
                ChatLogger(dc, intent);
                var kbResult = await _messagingApi.SendMessageAsync(dc.Context.Activity.Conversation.Id, dc.Context.Activity.Text);
                switch (intent?.Intent?.Name)
                {
                    case BotIntents.FoodOrder:
                    {
                        try
                        {
                            if (intent.Entities.Count > 0)
                            {
                                return await dc.BeginDialogAsync(nameof(FoodOrderDialog), intent, cancellationToken: cancellationToken);

                            }
                            else
                            {
                                return await dc.BeginDialogAsync(nameof(FoodOrderDialog), cancellationToken: cancellationToken);

                            }

                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }

                    case BotIntents.GetInTouch:
                    {
                        return await dc.BeginDialogAsync(nameof(ContactUsDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.AboutRestaurant:
                    {
                        return await dc.BeginDialogAsync(nameof(AboutRestaurentDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.WhatIs:
                    {
                        if (intent.Entities.Count > 0)
                        {
                            await dc.Context.SendActivityAsync(await new WikiData().getWikiDataAsync(intent.Entities.FirstOrDefault().Value.ToString()) != null ? await new WikiData().getWikiDataAsync(intent.Entities.FirstOrDefault().Value.ToString()) : "මට එය වෑටහුනේ නෑ.කරුණාකර නැවත පවසන්න. ", cancellationToken: cancellationToken);
                            return EndOfTurn;
                        }
                        else
                        {
                            await dc.Context.SendActivityAsync(await new WikiData().getWikiDataAsync(dc.Context.Activity.Text) != null ? await new WikiData().getWikiDataAsync(dc.Context.Activity.Text) : "මට එය වෑටහුනේ නෑ.කරුණාකර නැවත පවසන්න. ", cancellationToken: cancellationToken);
                            return EndOfTurn;
                        }


                        //await dc.BeginDialogAsync(nameof(AboutRestaurentDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.WhatCanYouDo:
                    {
                        return await dc.BeginDialogAsync(nameof(WhatCanYouDoDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.CreatedBy:
                    {
                        return await dc.BeginDialogAsync(nameof(CreatedByDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.ImBored:
                    {
                        return await dc.BeginDialogAsync(nameof(ImBoredDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.Food_Categories:
                    {
                        return await dc.BeginDialogAsync(nameof(FoodCategoriesDialog), cancellationToken: cancellationToken);
                    }
                    case BotIntents.Deny:
                    {
                        await dc.Context.SendActivityAsync("එය වටහා ගැනීමට තරම් මගේ දැනුම ප්‍රමානවත් නැහැ.කරුණාකර මදක් පැහැදිලි කර නැවත පවසන්න.", cancellationToken: cancellationToken);
                        return EndOfTurn;
                    }

                    default:
                        await ShowKBResponse(kbResult, dc.Context, cancellationToken);
                        return EndOfTurn;
                }
            }
            else
            {
                return EndOfTurn;
            }
        }

        private void ChatLogger(DialogContext dc, ParseResult intent)
        {
            new DBConnection().Insert(new ChatHistory()
            {
                id = Guid.NewGuid().ToString(),
                UserName = dc.Context.Activity.Recipient.Id,
                Intent = intent?.Intent?.Name,
                Utterence = dc.Context.Activity.Text
            });
        }

        private async Task ShowKBResponse(List<ApiClient.Model.WebhookMessage> results, ITurnContext context, CancellationToken cancellationToken)
        {
            //TODO: Standadize rasa messaging format
            //var results = await _messagingApi.SendMessageAsync(context.Activity.Conversation.Id, context.Activity.Text);
            foreach (var r in results)
            {
                if (!string.IsNullOrEmpty(r.Text))
                    await context.SendActivityAsync(r.Text, cancellationToken: cancellationToken);
                if (r.Custom != null)
                {
                    foreach (var c in r.Custom)
                    {
                        if (c.Videos != null)
                        {
                            IMessageActivity activity = context.Activity.CreateReply();
                            var attachments = new List<Attachment>();
                            if (c.Videos.Count > 1)
                            {
                                foreach (var item in c.Videos)
                                {
                                    var vCard = new VideoCard(item.Title, media: new List<MediaUrl>() { new MediaUrl { Url = item.Url } });
                                    attachments.Add(vCard.ToAttachment());
                                }
                                activity = MessageFactory.Carousel(attachments);
                            }
                            else if (c.Videos.Count == 1)
                            {
                                var vCard = new VideoCard(c.Videos.First().Title, media: new List<MediaUrl>() { new MediaUrl { Url = c.Videos.First().Url } });
                                attachments.Add(vCard.ToAttachment());
                                activity.Attachments = attachments;
                            }
                            await context.SendActivityAsync(activity, cancellationToken: cancellationToken);
                        }
                    }
                }
            }
        }
        private bool IsAllCharSinhala(string Input)
        {
            foreach (var item in Input.ToCharArray())
            {
                if (char.IsLower(item) || char.IsUpper(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
