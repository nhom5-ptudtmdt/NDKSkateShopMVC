using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NDKSkateShopMVC.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace NDKSkateShopMVC.Controllers
{
    public class AdminController : Controller
    {
        QLSSNDKEntities db = new QLSSNDKEntities();
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

         public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin model, string returnUrl)
        {
            QLSSNDKEntities db = new QLSSNDKEntities();
            var dataItem = db.Admins.Where(x => x.UserAdmin == model.UserAdmin && x.PassAdmin == model.PassAdmin).First();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.UserAdmin, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Admin");
        }
        public ActionResult SanPham(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(db.SanPhams.ToList().OrderBy(n=>n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Themmoisp()
        {
            ViewBag.MaParts = new SelectList(db.Parts.ToList().OrderBy(n => n.TenParts), "MaParts", "TenParts");
            ViewBag.MaBrand = new SelectList(db.Brands.ToList().OrderBy(n => n.TenBrand), "MaBrand", "TenBrand");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisp(SanPham sanpham, HttpPostedFileBase fileupload)
        {
            //đưa dữ liệu vào dropdownload
            ViewBag.MaParts = new SelectList(db.Parts.ToList().OrderBy(n => n.TenParts), "MaParts", "TenParts");
            ViewBag.MaBrand = new SelectList(db.Brands.ToList().OrderBy(n => n.TenBrand), "MaBrand", "TenBrand");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh đại diện ";
                return View();
            }
            // thêm vào csdl
            else
            {
                if (ModelState.IsValid)
                {

                    //lưu tên file , lưu ý bổ sung thư viện using System.IO
                    var fileName = Path.GetFileName(fileupload.FileName);
                    // lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/Asset/Images/"), fileName);
                    // kiểm tra hình tồn tại chưa?
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        // lưu hình ảnh vào đường dẫn
                        fileupload.SaveAs(path);
                    }
                    sanpham.AnhDD = fileName;
                    db.SanPhams.Add(sanpham);
                    db.SaveChanges();
                }
                return RedirectToAction("SanPham");
            }
        }
        public ActionResult Chitietsp(int id)
        {
            // Lấy ra đối tượg sp theo mã
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.Masach = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpGet]
        public ActionResult Xoasp(int id)
        {
            // lấy ra đối tượng sp cần xóa theo mã
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            // lấy ra đối tượng sp cần xóa theo mã
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("SanPham");
        }
        [HttpGet]
        public ActionResult Suasp(int id)
        {
            // lấy ra đối tượng sp theo mã
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //đưa dữ liệu vào dropdown list
            ViewBag.MaParts = new SelectList(db.Parts.ToList().OrderBy(n => n.TenParts), "MaParts", "TenParts", sanpham.MaParts);
            ViewBag.MaBrand = new SelectList(db.Brands.ToList().OrderBy(n => n.TenBrand), "MaBrand", "TenBrand", sanpham.MaBrand);
            return View(sanpham);
        }
        public ActionResult Part()
        {
            return View(db.Parts.ToList());
        }
        [HttpGet]
        public ActionResult Thempart()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Thempart(Part item)
        {
            db.Parts.Add(item);
            db.SaveChanges();
            return RedirectToAction("Part");
        }
        [HttpGet]
        public ActionResult Suapart(int id)
        {
            // lấy ra đối tượng loại theo mã
            Part item = db.Parts.SingleOrDefault(n => n.MaParts == id);
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(item);
        }
        public ActionResult Chitietpart(int id)
        {
            // Lấy ra đối tượg loại theo mã
            Part item = db.Parts.SingleOrDefault(n => n.MaParts == id);
            ViewBag.MaParts = item.MaParts;
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(item);
        }
        public ActionResult Xoapart(int id)
        {
            // lấy ra đối tượng loại cần xóa theo mã
            Part item = db.Parts.SingleOrDefault(n => n.MaParts == id);
            ViewBag.MaParts = item.MaParts;
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(item);
        }
        [HttpPost, ActionName("Xoapart")]
        public ActionResult Xacnhanxoa1(int id)
        {
            // lấy ra đối tượng loại cần xóa theo mã
            Part item = db.Parts.SingleOrDefault(n => n.MaParts == id);
            ViewBag.MaParts = item.MaParts;
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Parts.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Part");
        }
        public ActionResult Brand()
        {
            return View(db.Brands.ToList());
        }
        [HttpGet]
        public ActionResult Thembrand()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Thembrand(Brand item)
        {
            db.Brands.Add(item);
            db.SaveChanges();
            return RedirectToAction("Brand");

        }
        [HttpGet]
        public ActionResult Suabrand(int id)
        {
            // lấy ra đối tượng brand theo mã
            Brand item = db.Brands.SingleOrDefault(n => n.MaBrand == id);
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(item);
        }
        public ActionResult Chitietbrand(int id)
        {
            // Lấy ra đối tượng brand theo mã
            Brand item = db.Brands.SingleOrDefault(n => n.MaBrand == id);
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(item);
        }
        public ActionResult Xoabrand(int id)
        {
            // lấy ra đối tượng brand cần xóa theo mã
            Brand item = db.Brands.SingleOrDefault(n => n.MaBrand == id);
            ViewBag.MaBrand = item.MaBrand;
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(item);
        }
        [HttpPost, ActionName("Xoabrand")]
        public ActionResult Xacnhanxoa2(int id)
        {
            // lấy ra đối tượng brand cần xóa theo mã
            Brand item = db.Brands.SingleOrDefault(n => n.MaBrand == id);
            ViewBag.MaBrand = item.MaBrand;
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Brands.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Brand");
        }
    }
}