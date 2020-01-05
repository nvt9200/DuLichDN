using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;
using PagedList;
using PagedList.Mvc;

namespace DuLich_DH.Controllers
{
    public class KhachSanController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: KhachSan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Xemchitiet(int? id)
        {
            Session["ktra"] = "true";
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            KhachSan ks = db.KhachSans.Find(id);
            return View(ks);
        }
        public ActionResult Tatcakhachsan(int? page)
        {
            Session["ktra"] = "true";
            int num = 20;
            int pagenum = (page ?? 1);
            return View(db.KhachSans.OrderBy(t => t.TenKhachSan).ToPagedList(pagenum, num));
        }
        public ActionResult Khachsanxemnhieu(int? page)
        {
            Session["ktra"] = "true";
            int num = 20;
            int pagenum = (page ?? 1);
            return View(db.KhachSans.OrderByDescending(t => t.LuotXem).ToPagedList(pagenum, num));
        }
        public ActionResult Khachsangiare(int? page)
        {
            Session["ktra"] = "true";
            int num = 20;
            int pagenum = (page ?? 1);
            return View(db.KhachSans.OrderBy(t => t.DonGia).ToPagedList(pagenum, num));
        }
        public ActionResult Khachsantheodiadiem(int? page, int? id)
        {
            Session["ktra"] = "true";
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            int num = 20;
            int pagenum = (page ?? 1);
            List<KhachSan> ks = db.KhachSans.Where(t => t.MaDiaDiem == id).ToList();
            ViewBag.TB = db.DiaDiems.Find(id).TenDiaDiem;
            return View(ks.OrderBy(t => t.TenKhachSan).ToPagedList(pagenum, num));
        }
    }
}