using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuLich_DH.Models;

namespace DuLich_DH.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        DuLichEntities db = new DuLichEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}