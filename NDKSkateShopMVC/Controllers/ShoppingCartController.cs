using NDKSkateShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDKSkateShopMVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        QLSSNDKEntities _db = new QLSSNDKEntities();
        // GET: Cart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart==null || Session["Cart"]==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //--- Phuong thuc add item vao gio hang
        public ActionResult AddtoCart(int id)
        {
            var pro = _db.SanPhams.SingleOrDefault(s => s.MaSP == id);
            if(pro!= null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //--- Trang gio hang
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        } 
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart!=null)
            
                total_item = cart.Total_Quantity_in_Cart();
                ViewBag.QuantityCart = total_item;
                return PartialView("BagCart");
            
        }
        public ActionResult Shopping_Success()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                DonDatHang _order = new DonDatHang();
                _order.NgayDH = DateTime.Now;
                _order.CodeCus = int.Parse(form["CodeCustomer"]);
                _order.DCGH = form["Address_Delivery"];
                _db.DonDatHangs.Add(_order);
                foreach (var item in cart.Items)
                {
                    ChiTietDatHang _order_Detail = new ChiTietDatHang();
                    _order_Detail.IDOrder = _order.IDOrder;
                    _order_Detail.MaSP = item._shopping_product.MaSP;
                    _order_Detail.DonGia = item._shopping_product.GiaBan;
                    _order_Detail.SoLuong = item._shopping_quantity;
                    _db.ChiTietDatHangs.Add(_order_Detail);
                }
                _db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Shopping_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Error Checkout. Please information of Customer...");
            }
        }
    }
}
