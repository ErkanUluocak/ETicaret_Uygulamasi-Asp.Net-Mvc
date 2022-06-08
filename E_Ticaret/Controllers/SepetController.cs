using E_Ticaret.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
    }
}