using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NDKSkateShopMVC.Models;

namespace NDKSkateShopMVC.Controllers
{
    public class CustomersController : Controller
    {
        QLSSNDKEntities _db = new QLSSNDKEntities();
        // GET: Customers
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(KhachHang cus)
        {
            _db.KhachHangs.Add(cus);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}