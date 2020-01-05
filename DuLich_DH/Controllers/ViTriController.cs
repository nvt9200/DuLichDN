using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Controllers
{
    public class ViTriController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: ViTri
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult danhmucdiadiem(int? id)
        {
            if(id == null)
            {
                Response.StatusCode = 404;
            }
            Session["vitri"] = id;
            List<DiaDiem> dd = db.DiaDiems.Where(t => t.MaViTri == id).ToList();
            return PartialView(dd);
        }
        public ActionResult Tatcadiadiem()
        {
            Session["ktra"] = "true";
            return View(db.ViTris.ToList());
        }
        public ActionResult Lietkediadiem(int? id)
        {
            Session["ktra"] = "true";
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            Session["vitri"] = id;
            List<DiaDiem> dd = db.DiaDiems.Where(t => t.MaViTri == id).ToList();
            return PartialView(dd);
        }
    }
}