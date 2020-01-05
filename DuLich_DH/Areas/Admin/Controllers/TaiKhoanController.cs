using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            Session["demnd"] = db.QuanTris.Count();
            Session["demdd"] = db.DiaDiems.Count();
            Session["demks"] = db.KhachSans.Count();
            Session["demdp"] = db.DatPhongs.Count();
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string stk = f["txtTaiKhoan"].ToString();
            string smk = f["txtMatKhau"].ToString();
            QuanTri ad = db.QuanTris.Where(t => t.QuyenQuanTri == true).FirstOrDefault(t => t.TaiKhoan == stk && t.MatKhau == smk);
            QuanTri ad1 = db.QuanTris.Where(t => t.QuyenQuanTri == false).FirstOrDefault(t => t.TaiKhoan == stk && t.MatKhau == smk);
            if (ad != null)
            {
                Session["ad"] = ad;
                return Redirect("/Admin/Admin/Index");
            }
            if (ad1 != null)
            {
                Session["ad1"] = ad1;
                return Redirect("/Admin/Admin/Index");
            }
            ViewBag.TB = "Tài khoản hoặc mật khẩu không đúng";
            return View();
        }
        public JsonResult ktraTK(string TaiKhoan)
        {
            bool isExists = db.QuanTris.FirstOrDefault(t => t.TaiKhoan == TaiKhoan) != null;
            return Json(!isExists, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DangXuat()
        {
            Session["ad"] = Session["ad1"] = null;
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}