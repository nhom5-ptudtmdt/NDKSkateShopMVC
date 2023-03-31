using NDKSkateShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDKSkateShopMVC.Controllers
{
    public class SkateboardsController : Controller
    {
        QLSSNDKEntities db = new QLSSNDKEntities();
        // GET: Skateboards
        public ActionResult Index()
        {          
            return View();
        }
        public ActionResult BoPhan()
        {
            var bophan = from bp in db.Parts select bp;
            return PartialView(bophan);
        }
        public ActionResult ThuongHieu()
        {
            var thuonghieu = from th in db.Brands select th;
            return PartialView(thuonghieu);
        }
        public ActionResult SPTheoBP(int id)
        {
            var sanpham = from sp in db.SanPhams where sp.MaParts == id select sp;
            return View(sanpham);
        }
        public ActionResult SPTheoBrand(int id)
        {
            var sanpham = from sp in db.SanPhams where sp.MaBrand == id select sp;
            return View(sanpham);
        }
        public ActionResult Details(int id)
        {
            var sanpham = from sp in db.SanPhams where sp.MaSP == id select sp;
            return View(sanpham.Single());
        }
    }
}
