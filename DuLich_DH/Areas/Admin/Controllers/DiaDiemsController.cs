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
using System.IO;

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class DiaDiemsController : Controller
    {
        private DuLichEntities db = new DuLichEntities();

        // GET: Admin/DiaDiems
        public async Task<ActionResult> Index(int? page)
        {
            int num = 11;
            int pagenum = (page ?? 1);
            var diaDiems = db.DiaDiems.Include(d => d.ViTri);
            return View(diaDiems.OrderBy(t => t.TenDiaDiem).ToPagedList(pagenum, num));
        }

        // GET: Admin/DiaDiems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDiem diaDiem = await db.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }
            return View(diaDiem);
        }

        // GET: Admin/DiaDiems/Create
        public ActionResult Create()
        {
            ViewBag.MaViTri = new SelectList(db.ViTris, "MaViTri", "TenViTri");
            return View();
        }

        // POST: Admin/DiaDiems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaDiaDiem,TenDiaDiem,HinhAnh,MaViTri")] DiaDiem diaDiem, HttpPostedFileBase fileimg)
        {
            if (ModelState.IsValid)
            {
                var img = Path.GetFileName(fileimg.FileName);
                var path = Path.Combine(Server.MapPath("~/HinhANH/"), img);
                if (fileimg == null)
                {
                    ViewBag.IMG = "Chọn hình ảnh";
                    return View();
                }
                else if (System.IO.File.Exists(path))
                {
                    ViewBag.IMG = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileimg.SaveAs(path);
                }
                diaDiem.HinhAnh = fileimg.FileName;
                db.DiaDiems.Add(diaDiem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaViTri = new SelectList(db.ViTris, "MaViTri", "TenViTri", diaDiem.MaViTri);
            return View(diaDiem);
        }

        // GET: Admin/DiaDiems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDiem diaDiem = await db.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaViTri = new SelectList(db.ViTris, "MaViTri", "TenViTri", diaDiem.MaViTri);
            return View(diaDiem);
        }

        // POST: Admin/DiaDiems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaDiaDiem,TenDiaDiem,HinhAnh,MaViTri")] DiaDiem diaDiem, HttpPostedFileBase fileimg)
        {
            if (ModelState.IsValid)
            {
                var img = Path.GetFileName(fileimg.FileName);
                var path = Path.Combine(Server.MapPath("~/HinhANH/"), img);
                if (fileimg == null)
                {
                    ViewBag.IMG = "Chọn hình ảnh";
                    return View();
                }
                else if (System.IO.File.Exists(path))
                {
                    ViewBag.IMG = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileimg.SaveAs(path);
                }
                diaDiem.HinhAnh = fileimg.FileName;
                db.Entry(diaDiem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaViTri = new SelectList(db.ViTris, "MaViTri", "TenViTri", diaDiem.MaViTri);
            return View(diaDiem);
        }

        // GET: Admin/DiaDiems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaDiem diaDiem = await db.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }
            return View(diaDiem);
        }

        // POST: Admin/DiaDiems/Delete/5
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DiaDiem diaDiem = await db.DiaDiems.FindAsync(id);
            db.DiaDiems.Remove(diaDiem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public JsonResult ktraxoa(int? id)
        {
            bool tmpks= db.KhachSans.Where(t => t.MaDiaDiem == id).ToList().Count != 0;
            return Json(!tmpks, JsonRequestBehavior.AllowGet);
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
