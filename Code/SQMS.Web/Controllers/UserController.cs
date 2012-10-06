using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQMS.Web.Models;
using SQMS.Web;

namespace SQMS.Web.Controllers
{
    
    public class UserController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /User/
        [Authorize]
        [MyAuthorizeAttribute(Roles = CommonUtility.SubAdminRole)]
        public ViewResult Index()
        {
            var users = db.Users.Include(u => u.Region);
            return View(users.ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(long id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(u => u.Region1), "RegionId", "RegionNameShow");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.IsActive = true;

                string stUserID = user.UserId.ToString();
                string stPassword = "k";
                if (stUserID.Length >= 3)
                    stPassword += stUserID.Substring(stUserID.Length - 3, 3);

                user.Password = stPassword;
                user.Roles.Add(db.Roles.Find((int)ENRole.Member));
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(u => u.Region1), "RegionId", "RegionNameShow", user.MohallaId);
            return View(user);
        }

        //
        // GET: /User/Edit/5
        [Authorize]
        public ActionResult Edit(long id)
        {
            User user = db.Users.Find(id);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(u => u.Region1), "RegionId", "RegionNameShow", user.MohallaId);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(u => u.Region1), "RegionId", "RegionNameShow", user.MohallaId);
            return View(user);
        }

        //
        // GET: /User/Delete/5
        [Authorize]
        public ActionResult Delete(long id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}