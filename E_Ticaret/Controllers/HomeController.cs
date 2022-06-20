using E_Ticaret.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {

        E_TicaretEntities db = new E_TicaretEntities();

        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44301/");
            var result = await client.GetAsync("api/Kategoriler");
            var sonuc = await result.Content.ReadAsStringAsync();

            ViewBag.Kategoriler = JsonConvert.DeserializeObject<List<Kategori>>(sonuc);


            ViewBag.Urunler = db.Urunler.OrderByDescending(x => x.UrunID).Take(10).ToList();
            return View();
            
        }

        public ActionResult Kategori(int id)
        {
            Kategori kat = db.Kategori.Find(id);
            ViewBag.Kategori = kat.KategoriAdi;
            ViewBag.Kategoriler = db.Kategori.ToList();
            return View(db.Urunler.Where(x => x.KategoriID == id).OrderBy(x => x.UrunAdi).ToList());
        }

        public ActionResult Urun(int id)
        {
            ViewBag.Kategoriler = db.Kategori.ToList();
            return View(db.Urunler.Find(id));
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