using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NDKSkateShopMVC.Models
{
    public class CommonInfo
    {
        private QLSSNDKEntities db;
        public CommonInfo()
        {
            this.db = new QLSSNDKEntities();
        }
        public IEnumerable<Part> BoPhan
        {
            get
            {
                return this.db.Parts;
            }
        }
        public IEnumerable<Brand> ThuongHieu
        {
            get
            {
                return this.db.Brands;
            }
        }
        public IEnumerable<SanPham> SanPhamMoi(int n)
        {
            return this.db.SanPhams;
        }
        public IEnumerable<SanPham> SanPhamComplete(int n)
        {
            return db.SanPhams.OrderByDescending(x => x.MaParts==6).Take(n);
        }
        public IEnumerable<SanPham> SanPhamDeck(int n)
        {
            return db.SanPhams.OrderByDescending(x => x.MaParts == 1).Take(n);
        }
        public IEnumerable<SanPham> SanPhamTruck(int n)
        {
            return db.SanPhams.OrderByDescending(x => x.MaParts == 2).Take(n);
        }
        public IEnumerable<SanPham> SanPhamWheel(int n)
        {
            return db.SanPhams.OrderByDescending(x => x.MaParts == 3).Take(n);
        }
        public IEnumerable<SanPham> SanPhamBearing(int n)
        {
            return db.SanPhams.OrderByDescending(x => x.MaParts == 4).Take(n);
        }
        public IEnumerable<SanPham> SanPhamGripTape(int n)
        {
            return db.SanPhams.OrderByDescending(x => x.MaParts == 5).Take(n);
        }
        
    }
}