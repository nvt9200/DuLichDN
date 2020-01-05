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
    public class KhachSansController : Controller
    {
        private DuLichEntities db = new DuLichEntities();

        // GET: Admin/KhachSans
        public async Task<ActionResult> Index(int? page)
        {
            int num = 11;
            int pagenum = (page ?? 1);
            var khachSans = db.KhachSans.Include(k => k.DiaDiem);
            return View(khachSans.OrderBy(t => t.TenKhachSan).ToPagedList(pagenum, num));
        }

        // GET: Admin/KhachSans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = await db.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // GET: Admin/KhachSans/Create
        public ActionResult Create()
        {
            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem");
            return View();
        }

        // POST: Admin/KhachSans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaKhachSan,TenKhachSan,SDT,MoTa,NgayDang,LuotXem,HinhAnhKS,MaDiaDiem,DonGia,DanhGia,LuotDanhGia,DiaChi")] KhachSan khachSan, HttpPostedFileBase fileimg)
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
                khachSan.HinhAnhKS = fileimg.FileName;
                db.KhachSans.Add(khachSan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem", khachSan.MaDiaDiem);
            return View(khachSan);
        }

        // GET: Admin/KhachSans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = await db.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem", khachSan.MaDiaDiem);
            return View(khachSan);
        }

        // POST: Admin/KhachSans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaKhachSan,TenKhachSan,SDT,MoTa,NgayDang,LuotXem,HinhAnhKS,MaDiaDiem,DonGia,DanhGia,LuotDanhGia,DiaChi")] KhachSan khachSan, HttpPostedFileBase fileimg)
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
                khachSan.HinhAnhKS = fileimg.FileName;
                db.Entry(khachSan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaDiaDiem = new SelectList(db.DiaDiems, "MaDiaDiem", "TenDiaDiem", khachSan.MaDiaDiem);
            return View(khachSan);
        }

        // GET: Admin/KhachSans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = await db.KhachSans.FindAsync(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // POST: Admin/KhachSans/Delete/5
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            KhachSan khachSan = await db.KhachSans.FindAsync(id);
            db.KhachSans.Remove(khachSan);
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
