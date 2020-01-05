using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class CTDatPhongsController : Controller
    {
        private DuLichEntities db = new DuLichEntities();

        // GET: Admin/CTDatPhongs
        public ActionResult Index()
        {
            var datPhongs = db.DatPhongs.Include(d => d.KhachSan);
            return View(datPhongs.ToList());
        }

        // GET: Admin/CTDatPhongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatPhong datPhong = db.DatPhongs.Find(id);
            if (datPhong == null)
            {
                return HttpNotFound();
            }
            return View(datPhong);
        }

        // GET: Admin/CTDatPhongs/Create
        public ActionResult Create()
        {
            ViewBag.MaKS = new SelectList(db.KhachSans, "MaKhachSan", "TenKhachSan");
            return View();
        }

        // POST: Admin/CTDatPhongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDP,TenKhachHang,SDT,MaKS,GhiChu,TinhTrang")] DatPhong datPhong)
        {
            if (ModelState.IsValid)
            {
                db.DatPhongs.Add(datPhong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKS = new SelectList(db.KhachSans, "MaKhachSan", "TenKhachSan", datPhong.MaKS);
            return View(datPhong);
        }

        // GET: Admin/CTDatPhongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatPhong datPhong = db.DatPhongs.Find(id);
            if (datPhong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKS = new SelectList(db.KhachSans, "MaKhachSan", "TenKhachSan", datPhong.MaKS);
            return View(datPhong);
        }

        // POST: Admin/CTDatPhongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDP,TenKhachHang,SDT,MaKS,GhiChu,TinhTrang")] DatPhong datPhong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKS = new SelectList(db.KhachSans, "MaKhachSan", "TenKhachSan", datPhong.MaKS);
            return View(datPhong);
        }

        // GET: Admin/CTDatPhongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatPhong datPhong = db.DatPhongs.Find(id);
            if (datPhong == null)
            {
                return HttpNotFound();
            }
            return View(datPhong);
        }

        // POST: Admin/CTDatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatPhong datPhong = db.DatPhongs.Find(id);
            db.DatPhongs.Remove(datPhong);
            db.SaveChanges();
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
