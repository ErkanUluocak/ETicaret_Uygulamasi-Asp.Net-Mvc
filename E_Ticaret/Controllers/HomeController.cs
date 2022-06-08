using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {

        E_TicaretEntities db = new E_TicaretEntities();

        public ActionResult Index()
        {
            ViewBag.Kategoriler = db.Kategori.ToList();
            ViewBag.Urunler = db.Urunler.OrderByDescending(x => x.UrunID).Take(10).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}