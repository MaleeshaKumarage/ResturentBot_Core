using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInventory
{
    public class Inventory
    {
        public Inventory()
        {

        }

        public List<Product> GetProductList()
        {

            List<Product> productList = new List<Product>() {
            new Product(){
                Id="001",
                Name_SI="මාලු පාන්",
                Name_EN="Fish Bun",
                Description="",
                Category=Product.CategoryType.කෙටි_ආහාර,
                Price=40.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594906718/RestOBot/InventoryImages/hycyy9jdx9yzjzlcauf2.jpg"
            },new Product(){
                Id="002",
                Name_SI="සුදු ලූනු රසැති පාන්",
                Name_EN="Garlic Bread",
                Description="",
                Price=140.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594906755/RestOBot/InventoryImages/xwor1t5z4mrrnyqgbvmd.jpg"
            },
            new Product(){
                Id="003",
                Name_SI="බටර් රසැති පාන්",
                Name_EN="Butter Bread",
                Description="",
                Price=100.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594906799/RestOBot/InventoryImages/qesrstqdkqgkfddjvciy.jpg"
            },
            new Product(){
                Id="004",
                Name_SI="චීස් රසැතී පාන්",
                Name_EN="Cheese Bread",
                Description="",
                Price=175.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594906881/RestOBot/InventoryImages/wwofofpig9t1no8ecmcd.jpg"
            }
            ,new Product(){
                Id="005",
                Name_SI="සාමාන්‍ය කිඹුලා බනිස්",
                Name_EN="Plain Croissant",
                Description="",
                Price=40.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594906957/RestOBot/InventoryImages/bkk42zzx4ckxhkgiclcm.jpg"
            },new Product(){
                Id="006",
                Name_SI="චොකලට් රසැති කිඹුලා බනිස්",
                Name_EN="chocolate Croissant",
                Description="",
                Price=60.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907020/RestOBot/InventoryImages/ubtlmsmpffwvbjcixovj.jpg"
            }
            ,new Product(){
                Id="007",
                Name_SI="චීස් රසැතී කිඹුලා බනිස්",
                Name_EN="Cheese Croissant",
                Description="",
                Price=60.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907109/RestOBot/InventoryImages/hctyfcarhpkx32xu08xv.jpg"
            },new Product(){
                Id="008",
                Name_SI="සොසේජ් රසැති කිඹුලා බනිස්",
                Name_EN="Sausage Croissant",
                Description="",
                Price=65.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907159/RestOBot/InventoryImages/elxoakf2kjndcrsy8xrw.jpg"
            },new Product(){
                Id="009",
                Name_SI="එලවලු රොටී",
                Name_EN="Vegetable Rotty",
                Description="",
                Price=40.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907230/RestOBot/InventoryImages/asb7j405hypp8o6ias8n.jpg"
            },new Product(){
                Id="010",
                Name_SI="බිත්තර රොටී",
                Name_EN="Egg Rotty",
                Description="",
                Price=60.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907296/RestOBot/InventoryImages/segldyj7b4ibshszgsim.jpg"
            },new Product(){
                Id="011",
                Name_SI="මාලු රොටී",
                Name_EN="Fish Rotty",
                Description="",
                Price=50.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907376/RestOBot/InventoryImages/wgue9crktx3vcasyab95.jpg"
            },new Product(){
                Id="012",
                Name_SI="එලවලු සහිත කට්ලට්",
                Name_EN="chocolate Croissant",
                Description="",
                Price=35.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907421/RestOBot/InventoryImages/zv2vpfhvrptqywkua2mr.jpg"
            },new Product(){
                Id="013",
                Name_SI="බිත්තර සහිත කට්ලට්",
                Name_EN="Egg Cutlet",
                Description="",
                Price=40.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907498/RestOBot/InventoryImages/lj2xsv7bgevfo61yo2ch.jpg"
            },new Product(){
                Id="014",
                Name_SI="මාලු සහිත කට්ලට්",
                Name_EN="Fish Cutlet",
                Description="",
                Price=35.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907558/RestOBot/InventoryImages/w2y3oeehyjm0eynw7clu.jpg"
            },new Product(){
                Id="015",
                Name_SI="කුකුල් මස් සහිත කට්ලට්",
                Name_EN="Chicken Cutlet",
                Description="",
                Price=50.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907587/RestOBot/InventoryImages/esro7wzv4nfqm0f3pcbr.jpg"
            },new Product(){
                Id="016",
                Name_SI="මාලු සහිත කට්ලට්",
                Name_EN="Fish Cutlet",
                Description="",
                Price=35.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907671/RestOBot/InventoryImages/biuwk1020koe9jtrxc66.jpg"
            },new Product(){
                Id="017",
                Name_SI="ක්‍රීම් බනිස්",
                Name_EN="Cream Bun",
                Description="",
                Price=35.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907726/RestOBot/InventoryImages/dbklopxazqfy9o9ar36l.jpg"
            },new Product(){
                Id="018",
                Name_SI="මාලු සහිත කට්ලට්",
                Name_EN="Fish Cutlet",
                Description="",
                Price=60.0M,
                ImageUrl=""
            },new Product(){
                Id="019",
                Name_SI="මාලු පැටිස්",
                Name_EN="Fish Patty",
                Description="",
                Price=45.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907801/RestOBot/InventoryImages/h08vauo8dafvhjnltnqz.jpg"
            },new Product(){
                Id="020",
                Name_SI="සීනි සම්බෝල පාන්",
                Name_EN="Seeni Sambol Bread",
                Description="",
                Price=45.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907853/RestOBot/InventoryImages/tc16ilbjczm91vefpewo.jpg"
            },new Product(){
                Id="021",
                Name_SI="සමොසා",
                Name_EN="Samosa",
                Description="",
                Price=45.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1594907885/RestOBot/InventoryImages/rmcjwadk7gxrfuijdgow.jpg"
            },new Product(){
                Id="022",
                Name_SI="චිකන් කොත්තු",
                Name_EN="Chicken Koththu",
                Description="",
                Price=350.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1598580521/RestOBot/InventoryImages/zpskgpzkafavtmk6ans1.jpg"
            },new Product(){
                Id="023",
                Name_SI="චොකලට් කප් කේක්",
                Name_EN="Chocolate Cup Cake",
                Description="",
                Price=350.0M,
                ImageUrl="https://res.cloudinary.com/vevro/image/upload/c_scale,h_720,w_720/v1598580691/RestOBot/InventoryImages/tvghztwt3paoa4ourxyl.jpg"
            }
            };
            return productList;
        }
    }
}
