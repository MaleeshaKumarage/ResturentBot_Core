using ApiClient.Model;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.Dialogs.Choices;
using ProductInventory;
using RestSharp.Extensions;
using SinhalaTokenizationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rest_O_Bot.Resources
{
    public class EntityClassifier
    {
        public List<Product> _productList;
        public EntityClassifier()
        {
            

        }
        public async Task<ApiClient.Model.ParseResult> GetResultAsync(Microsoft.Bot.Builder.Dialogs.DialogContext context, ApiClient.Api.IModelApi _modelApi)
        {
            ApiClient.Model.ParseResult intent = await _modelApi.ModelParsePostAsync(new ApiClient.Model.InlineObject { Text = context.Context.Activity.Text.ToLower() });
            if (intent.Entities.Count<0)
            {
            var list=new ProductInventory.ProductSearchService().FoodEntityClassiffire(context.Context.Activity.Text);
            if (list.Count>0)
            {
                foreach (var item in list)
                {
                    var entity = new Entity(5, 10, list?.FirstOrDefault(), "Food_Type", Decimal.Parse(new Random().NextDouble().ToString("0.##")));
                    entity.Extractor = "DIETClassifier";
                    intent.Entities.Add(entity);
                } 
            }

            }
            return intent;
           

        }
    }
}
