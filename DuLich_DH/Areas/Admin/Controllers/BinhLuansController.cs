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
using PagedList;
using PagedList.Mvc;

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class BinhLuansController : Controller
    {
        private DuLichEntities db = new DuLichEntities();

        // GET: Admin/BinhLuans
        public async Task<ActionResult> Index(int? page)
        {
            int num = 11;
            int pagenum = (page ?? 1);
            var binhLuans = db.BinhLuans.Include(b => b.DiaDiem);
            return View(binhLuans.OrderByDescending(t => t.NgayDang).ToPagedList(pagenum, num));
        }

        // GET: Admin/BinhLuans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = await db.BinhLuans.FindAsync(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Create
        public ActionResult Create()
        {
            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem");
            return View();
        }

        // POST: Admin/BinhLuans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaBinhLuan,TenNguoiBinhLuan,BinhLuan1,NgayDang,MaDiaDiem,SDT,Email")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.BinhLuans.Add(binhLuan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem", binhLuan.MaDiaDiem);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = await db.BinhLuans.FindAsync(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem", binhLuan.MaDiaDiem);
            return View(binhLuan);
        }

        // POST: Admin/BinhLuans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaBinhLuan,TenNguoiBinhLuan,BinhLuan1,NgayDang,MaDiaDiem,SDT,Email")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem", binhLuan.MaDiaDiem);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = await db.BinhLuans.FindAsync(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: Admin/BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = await db.BinhLuans.FindAsync(id);
            db.BinhLuans.Remove(binhLuan);
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
