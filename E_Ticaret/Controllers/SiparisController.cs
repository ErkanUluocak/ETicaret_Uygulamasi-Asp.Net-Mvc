using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class SiparisController : Controller
    {
      
        E_TicaretEntities db = new E_TicaretEntities();

        public ActionResult Index()
        {
            return View(db.Siparis.ToList());
        }

        public ActionResult SiparisDetay(int SiparisID)
        {
            var siparisdetay = db.SiparisDetay.Where(x => x.SiparisID == SiparisID).ToList();
            return View(siparisdetay);
        }
    }
}