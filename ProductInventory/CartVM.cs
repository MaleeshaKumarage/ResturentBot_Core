using System;
using System.Collections.Generic;
using System.Text;

namespace ProductInventory
{
    public static class CartVM
    {
        public static List<Product> _cartList;


        public static void AddItemToCart(Product product)
        {

            _cartList.Add(product);
        }

        public static List<Product> ViewCart(Product product)
        {

            return _cartList;
        }
        public static void ClearCart(Product product)
        {

            _cartList.Clear();
        }
    }
}
