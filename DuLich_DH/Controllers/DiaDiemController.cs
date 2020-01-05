using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Controllers
{
    public class DiaDiemController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: DiaDiem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult tatcadiadiem(int? id)
        {
            Session["ktra"] = "true";
            List<KhachSan> ks = db.KhachSans.Where(t => t.DiaDiem.MaViTri == id).ToList();
            ViewBag.TB = db.ViTris.Find(id).TenViTri;
            return View(ks);
        }
        public ActionResult thongtindiadiem(int? id)
        {
            Session["vitri"] = db.DiaDiems.Find(id).MaViTri;
            Session["ktra"] = "true";
            List<KhachSan> ks = db.KhachSans.Where(t => t.MaDiaDiem == id).ToList();
            ViewBag.TB = db.DiaDiems.Find(id).ViTri.TenViTri + " - " + db.DiaDiems.Find(id).TenDiaDiem;
            return View(ks);
        }
        public PartialViewResult Luachondiadiem()
        {
            return PartialView(db.DiaDiems.ToList());
        }
    }
}