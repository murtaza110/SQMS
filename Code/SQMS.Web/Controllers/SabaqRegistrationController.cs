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
    public class SabaqRegistrationController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /SabaqRegistration/

        public ViewResult Index()
        {
            var sabaqregistrations = db.SabaqRegistrations.Include(s => s.SabaqGroup).Include(s => s.SabaqStatu);
            return View(sabaqregistrations.ToList());
        }

        //
        // GET: /SabaqRegistration/Details/5

        public ViewResult Details(long id)
        {
            SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
            return View(sabaqregistration);
        }

        //
        // GET: /SabaqRegistration/Create

        public ActionResult Create()
        {
            ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName");
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName");
            return View();
        } 

        //
        // POST: /SabaqRegistration/Create

        [HttpPost]
        public ActionResult Create(SabaqRegistration sabaqregistration)
        {
            if (ModelState.IsValid)
            {
                db.SabaqRegistrations.Add(sabaqregistration);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
            return View(sabaqregistration);
        }
        
        //
        // GET: /SabaqRegistration/Edit/5
 
        public ActionResult Edit(long id)
        {
            SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
            ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
            return View(sabaqregistration);
        }

        //
        // POST: /SabaqRegistration/Edit/5

        [HttpPost]
        public ActionResult Edit(SabaqRegistration sabaqregistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sabaqregistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
            return View(sabaqregistration);
        }

        //
        // GET: /SabaqRegistration/Delete/5
 
        public ActionResult Delete(long id)
        {
            SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
            return View(sabaqregistration);
        }

        //
        // POST: /SabaqRegistration/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
            db.SabaqRegistrations.Remove(sabaqregistration);
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