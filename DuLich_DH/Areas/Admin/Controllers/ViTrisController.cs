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

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class ViTrisController : Controller
    {
        private DuLichEntities db = new DuLichEntities();

        // GET: Admin/ViTris
        public async Task<ActionResult> Index()
        {
            return View(await db.ViTris.ToListAsync());
        }

        // GET: Admin/ViTris/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViTri viTri = await db.ViTris.FindAsync(id);
            if (viTri == null)
            {
                return HttpNotFound();
            }
            return View(viTri);
        }

        // GET: Admin/ViTris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ViTris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaViTri,TenViTri,HinhAnh")] ViTri viTri)
        {
            if (ModelState.IsValid)
            {
                db.ViTris.Add(viTri);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viTri);
        }

        // GET: Admin/ViTris/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViTri viTri = await db.ViTris.FindAsync(id);
            if (viTri == null)
            {
                return HttpNotFound();
            }
            return View(viTri);
        }

        // POST: Admin/ViTris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaViTri,TenViTri,HinhAnh")] ViTri viTri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viTri).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viTri);
        }

        // GET: Admin/ViTris/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViTri viTri = await db.ViTris.FindAsync(id);
            if (viTri == null)
            {
                return HttpNotFound();
            }
            return View(viTri);
        }

        // POST: Admin/ViTris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViTri viTri = await db.ViTris.FindAsync(id);
            db.ViTris.Remove(viTri);
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
