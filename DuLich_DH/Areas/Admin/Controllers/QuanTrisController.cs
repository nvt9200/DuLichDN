using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class QuanTrisController : Controller
    {
        private DuLichEntities db = new DuLichEntities();

        // GET: Admin/QuanTris
        public async Task<ActionResult> Index(int? page)
        {
            int num = 11;
            int pagenum = (page ?? 1);
            return View(db.QuanTris.OrderBy(t => t.TaiKhoan).Where(t => t.QuyenQuanTri == false).ToPagedList(pagenum, num));
        }

        // GET: Admin/QuanTris/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = await db.QuanTris.FindAsync(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuanTris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaQuanTri,TaiKhoan,MatKhau,NhapLaiMatKhau,NgayTao,QuenMatKhau,QuyenQuanTri,AnhDaiDien")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                quanTri.AnhDaiDien = "user.png";
                quanTri.NgayTao = DateTime.Now;
                quanTri.QuenMatKhau = Guid.NewGuid().ToString();
                quanTri.QuyenQuanTri = false;
                db.QuanTris.Add(quanTri);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(quanTri);
        }

        // GET: Admin/QuanTris/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = await db.QuanTris.FindAsync(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaQuanTri,TaiKhoan,MatKhau,NhapLaiMatKhau,NgayTao,QuenMatKhau,QuyenQuanTri")] QuanTri quanTri, HttpPostedFileBase fileimg)
        {
            if (ModelState.IsValid)
            {
                var img = Path.GetFileName(fileimg.FileName);
                var path = Path.Combine(Server.MapPath("~/HinhANH/"), img);
                if(fileimg == null)
                {
                    ViewBag.IMG = "Chọn hình ảnh";
                    return View();
                }
                else if(System.IO.File.Exists(path))
                {
                    ViewBag.IMG = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileimg.SaveAs(path);
                }
                quanTri.AnhDaiDien = fileimg.FileName;
                quanTri.NgayTao = DateTime.Now;
                quanTri.QuenMatKhau = Guid.NewGuid().ToString();
                quanTri.QuyenQuanTri = false;
                db.Entry(quanTri).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = await db.QuanTris.FindAsync(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Delete/5
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuanTri quanTri = await db.QuanTris.FindAsync(id);
            db.QuanTris.Remove(quanTri);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
