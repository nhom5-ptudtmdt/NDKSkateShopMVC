using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDKSkateShopMVC.Models
{
    public class CartItem
    {
        public SanPham _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(SanPham _pro, int _quantity = 1 )
        {
            var item = items.FirstOrDefault(s => s._shopping_product.MaSP == _pro.MaSP);
            if(item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }
        public void Update_Quantity_Shopping(int id, int _quantity)
        {
            var item = items.Find(s => s._shopping_product.MaSP == id);
            if (item != null)
            {
                item._shopping_quantity = _quantity;
            }    
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._shopping_product.MaSP == id);
        }      
        public void ClearCart()
        {
            items.Clear();
        }
        // Tong so luong mua sam
        public int Total_Quantity_in_Cart()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        public double Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.GiaBan * s._shopping_quantity);
            return (double)total;
        }
    }
}