using E_Ticaret.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class SepetController : Controller
    {
        E_TicaretEntities db = new E_TicaretEntities();
        public ActionResult SepeteEkle(int? adet, int id)
        {
            Urunler urun = db.Urunler.Find(id);
            Sepet sepettekiurun = db.Sepet.FirstOrDefault(x => x.UrunID == id);
            string userID = User.Identity.GetUserId();

            if (sepettekiurun == null)
            {
                Sepet yeniurun = new Sepet()
                {
                    Adet = adet ?? 1,
                    UrunID = id,
                    ToplamTutar = urun.UrunFiyatı * (adet ?? 1),
                    UserID = userID
                };
                db.Sepet.Add(yeniurun);
            }
            else
            {
                sepettekiurun.Adet = sepettekiurun.Adet + (adet ?? 1);
                sepettekiurun.ToplamTutar = sepettekiurun.Adet * urun.UrunFiyatı;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            var sepet = db.Sepet.Where(x => x.UserID == userID).Include(u => u.Urunler);
            return View(sepet.ToList());
        }

        public ActionResult SepetGuncelle(int? adet, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Sepet sepet = db.Sepet.Find(id);

            if (sepet == null)
            {
                return HttpNotFound();
            }

            Urunler urun = db.Urunler.Find(sepet.UrunID);

            sepet.Adet = adet ?? 1;
            sepet.ToplamTutar = sepet.Adet * urun.UrunFiyatı;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            db.Sepet.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}