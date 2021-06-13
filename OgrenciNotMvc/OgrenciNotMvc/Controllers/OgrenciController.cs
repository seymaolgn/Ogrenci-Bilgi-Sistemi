using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var ogrenci = db.TBLOGRENCILER.ToList();
            return View(ogrenci);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {

            List<SelectListItem> kulup = (from i in db.TBLKULUPLER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KULUPAD,
                                              Value = i.KULUPID.ToString()
                                          }).ToList();
            ViewBag.klp = kulup;              
            return View();
        }
        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCILER p3)
        {
            var klp = db.TBLKULUPLER.Where(x => x.KULUPID == p3.TBLKULUPLER.KULUPID).FirstOrDefault();
            p3.TBLKULUPLER = klp;
            db.TBLOGRENCILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var ogr = db.TBLOGRENCILER.Find(id);
            db.TBLOGRENCILER.Remove(ogr);
            db.SaveChanges(); 
            return RedirectToAction("Index");
        }
        public ActionResult OgrenciGetir(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Find(id);
            List<SelectListItem> kulup = (from i in db.TBLKULUPLER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KULUPAD,
                                              Value = i.KULUPID.ToString()
                                          }).ToList();
            ViewBag.klp = kulup;
            return View("OgrenciGetir", ogrenci);
        }
        public ActionResult guncelle(TBLOGRENCILER p)
        {
            var ogr = db.TBLOGRENCILER.Find(p.OGRENCIID);
            ogr.OGRAD = p.OGRAD;
            ogr.OGRSOYAD = p.OGRSOYAD;
            ogr.OGRFOTO = p.OGRFOTO;
            ogr.OGRKULUP = p.OGRKULUP;
            ogr.OGRCINSIYET = p.OGRCINSIYET;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}