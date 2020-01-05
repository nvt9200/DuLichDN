using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Controllers
{
    public class TimKiemController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimKiemKS(string txtTimKiem)
        {
            Session["ktra"] = "true";
            if(txtTimKiem != null)
            {
                Session["tukhoa"] = txtTimKiem;
            }
            string stukhoa = Session["tukhoa"].ToString();
            List<KhachSan> ks = db.KhachSans.Where(t => t.TenKhachSan.Contains(stukhoa)).ToList();
            List<DiaDiem> dd = db.DiaDiems.Where(t => t.TenDiaDiem.Contains(stukhoa)).ToList();
            if (ks.Count == 0)
            {
                ViewBag.TBTKKS = "Không có khách sạn";
                if (dd.Count == 0)
                {
                    ViewBag.TBC = "Không có kết quả liên quan đến từ khóa";
                }
            }
            return View(ks);
        }
        public ActionResult TimKiemDD()
        {
            string stukhoa = Session["tukhoa"].ToString();
            List<DiaDiem> dd = db.DiaDiems.Where(t => t.TenDiaDiem.Contains(stukhoa)).ToList();
            if(dd.Count == 0)
            {
                Session["TBTKDD"] = "Không có địa điểm";
            }
            return View(dd);
        }
    }
}