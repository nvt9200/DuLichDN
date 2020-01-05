using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Controllers
{
    public class HienThiController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: HienThi
        public ActionResult Index()
        {
            Session["ktra"] = null;
            return View();
        }
        public PartialViewResult ViTriDongNai()
        {
            return PartialView(db.ViTris.ToList());
        }
        public PartialViewResult KhachSanXemNhieu()
        {
            return PartialView(db.KhachSans.OrderByDescending(n=>n.LuotXem).Take(8).ToList());
        }
    }
}