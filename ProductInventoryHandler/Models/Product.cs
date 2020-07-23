using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInventoryHandler.Models
{
    public class Product
    {
        public class Product
        {
            public string Id { get; set; }
            public string Name_SI { get; set; }
            public string Name_EN { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public CategoryType Category { get; set; }
            public string ImageUrl { get; set; }


            public enum CategoryType
            {
                කේක්,
                පාන්,
                බනිස්,
                කෙටි_ආහාර,
                රස_කැවිලි,
                සැන්ඩ්විච්
            }
            public Product()
            {
                Id = Guid.NewGuid().ToString();
            }

            public Product(string _Id, string _Name_SI, string _Name_EN, decimal _Price, string _Description, CategoryType _category, string _ImageUrl)
            {
                Id = _Id;
                Name_SI = _Name_SI;
                Name_EN = _Name_EN;
                Price = _Price;
                Category = _category;
                Description = _Description;
                ImageUrl = _ImageUrl;
                Category = _category;


            }


        }

    }
}
