using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;

namespace OgrenciNotMvc.Controllers
{
    public class NotController : Controller
    {
        // GET: Not
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var SınavNot = db.TBLNOTLAR.ToList();
            return View(SınavNot);
        }
        [HttpGet]
        public ActionResult YeniSınav()
        {
            return View();
        }
        [HttpPost]    
        public ActionResult YeniSınav(TBLNOTLAR p4)
        {
            db.TBLNOTLAR.Add(p4);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int id)
        {
            var not = db.TBLNOTLAR.Find(id);
            return View("NotGetir", not);
        }
        [HttpPost]
        public ActionResult NotGetir(Class1 model,TBLNOTLAR p,int SINAV1=0, int SINAV2=0,int SINAV3=0,int PROJE=0)
        {
            
            if (model.islem == "hesapla")
            {
                int ortalama = (SINAV1+ SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ortalama;
            }
            if(model.islem =="notGuncelle")
            {
                var nt = db.TBLNOTLAR.Find(p.NOTID);
                nt.SINAV1 = p.SINAV1;
                nt.SINAV2 = p.SINAV2;
                nt.SINAV3 = p.SINAV3;
                nt.PROJE = p.PROJE;
                nt.ORTALAMA = p.ORTALAMA;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}