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
    [Authorize]
    public class UserController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /User/

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
            //ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionId == 4), "RegionId", "RegionName");
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(u => u.Region1), "RegionId", "RegionNameShow");
            //ViewBag.SecurityQuestionId = new SelectList(db.SecurityQuestions, "SecurityQuestionId", "SecurityQuestion1");
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
                string stPassword = "K";
                if (stUserID.Length >= 3)
                    stPassword += stUserID.Substring(stUserID.Length - 3, 3);

                user.Password = stPassword;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(u => u.Region1), "RegionId", "RegionNameShow", user.MohallaId);
            //ViewBag.SecurityQuestionId = new SelectList(db.SecurityQuestions, "SecurityQuestionId", "SecurityQuestion1", user.SecurityQuestionId);
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(long id)
        {
            User user = db.Users.Find(id);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(u => u.Region1), "RegionId", "RegionNameShow", user.MohallaId);
            //ViewBag.SecurityQuestionId = new SelectList(db.SecurityQuestions, "SecurityQuestionId", "SecurityQuestion1", user.SecurityQuestionId);
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
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(u => u.Region1), "RegionId", "RegionNameShow", user.MohallaId);
            //ViewBag.SecurityQuestionId = new SelectList(db.SecurityQuestions, "SecurityQuestionId", "SecurityQuestion1", user.SecurityQuestionId);
            return View(user);
        }

        //
        // GET: /User/Delete/5

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