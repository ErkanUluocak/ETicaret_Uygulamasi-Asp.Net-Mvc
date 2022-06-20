using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    
    public class KategorilerController : ApiController
    {
        E_TicaretEntities db = new E_TicaretEntities();
       
        public List<Kategorim> Get()
        {

            List<Kategori> kategoriler = db.Kategori.ToList();

            List<Kategorim> liste = new List<Kategorim>();

            foreach (var kategori in kategoriler)
            {
                liste.Add(new Kategorim() { KategoriID = kategori.KategoriID, KategoriAdi = kategori.KategoriAdi });
            }

            return liste;
        }
    }
}
