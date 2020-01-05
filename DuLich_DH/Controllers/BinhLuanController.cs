using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Controllers
{
    public class BinhLuanController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: BinhLuan
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Thembinhluan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Thembinhluan(BinhLuan bl)
        {
            if(ModelState.IsValid)
            {
                bl.NgayDang = DateTime.Now;
                db.BinhLuans.Add(bl);
                db.SaveChanges();
                ViewBag.TBBL = "Review của bạn đã được ghi nhận. Cảm ơn bạn đã review, vui lòng qua trang địa điểm để xem!";
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public PartialViewResult Hienthibinhluan(int? id)
        {
            if(id == null)
            {
                Response.StatusCode = 404;
            }
            List<BinhLuan> bl = db.BinhLuans.Where(t => t.MaDiaDiem == id).ToList();
            return PartialView(bl);
        }
    }
}