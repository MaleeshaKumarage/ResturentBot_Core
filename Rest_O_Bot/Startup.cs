// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Rest_O_Bot.Bots;
using Rest_O_Bot.Dialogs;
using Rest_O_Bot.Dialogs.About_Restaurent;

namespace Rest_O_Bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();


            ApiClient.Client.Configuration apiClientConfiguration = new ApiClient.Client.Configuration { BasePath = Configuration.GetSection("Rasa:HostName").Value };
            apiClientConfiguration.AddApiKey(Configuration.GetSection("Rasa:APIKeyName").Value, Configuration.GetSection("Rasa:APIKey").Value);
            services.AddSingleton<ApiClient.Api.IMessagingApi>(new ApiClient.Api.MessagingApi(apiClientConfiguration));
            services.AddSingleton<ApiClient.Api.IModelApi>(new ApiClient.Api.ModelApi(apiClientConfiguration));

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the storage we'll be using for User and Conversation state. (Memory is great for testing purposes.)
            services.AddSingleton<IStorage, MemoryStorage>();

            // Create the User state. (Used in this bot's Dialog implementation.)
            services.AddSingleton<UserState>();

            // Create the Conversation state. (Used by the Dialog system itself.)
            services.AddSingleton<ConversationState>();
            services.AddHttpClient();

            services.AddSingleton<Dialog, FoodOrderDialog>();
            services.AddSingleton<Dialog, FoodOrderDialog>();
            services.AddSingleton<Dialog, AboutRestaurentDialog>();
            services.AddSingleton<Dialog, AddItemToCartDialog>();
            services.AddSingleton<Dialog, ContactUsDialog>();
            services.AddSingleton<Dialog, WhatCanYouDoDialog>();
            services.AddSingleton<Dialog, CreatedByDialog>();
            services.AddSingleton<Dialog, ImBoredDialog>();
            services.AddSingleton<Dialog, FoodCategoriesDialog>();
       

            // The MainDialog that will be run by the bot.
            services.AddSingleton<MainDialog>();
            //Food Search Dialog
            //services.AddSingleton<Dialog, FoodSearchDialog>();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, DialogAndWelcomeBot<MainDialog>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            //.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=Index}/{id?}");
            //});

            // app.UseHttpsRedirection();
        }
    }
}
