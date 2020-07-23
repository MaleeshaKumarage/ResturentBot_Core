using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInventory
{
    public class ProductSearchService
    {
        List<Product> resultList = new List<Product>();
        public ProductSearchService()
        {

        }
        public List<Product> GetAllProducts()
        {
            var ProductList = new Inventory().GetProductList();
            resultList = ProductList;
            return resultList;
        }
        public List<Product> SearchProductByName(String Name)
        {

            var ProductList = new Inventory().GetProductList();
            resultList = ProductList.Where(a => a.Name_SI.Contains(Name)).ToList();
            return resultList;
        }
        public List<Product> SearchProductByCategory(String Category)
        {


            var ProductList = new Inventory().GetProductList();
            resultList = ProductList.Where(a => a.Category == FindCategoryType(Category)).ToList();
            return null;
        }


        public Product.CategoryType FindCategoryType(String Category)
        {
            return Product.CategoryType.කේක්;
        }

        public List<string> GetAllProductNames()
        {
            return new Inventory().GetProductList().Select(a => a.Name_SI).ToList();
        }

        public List<String> FoodEntityClassiffire(string utterence)
        {
            var wordTokens = new SinhalaTokenizationLibrary.TokenizationLibrary().Tokenize(utterence);
            var productTokens = new SinhalaTokenizationLibrary.TokenizationLibrary().Tokenize(GetAllProductNames());

            //===============Level 1=================

            return productTokens.Intersect(wordTokens).ToList();

        }

        
    }
}
