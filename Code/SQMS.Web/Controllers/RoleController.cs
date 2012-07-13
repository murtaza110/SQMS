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
    public class RoleController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /Role/

        public ViewResult Index()
        {
            return View(db.Roles.ToList());
        }

        //
        // GET: /Role/Details/5

        public ViewResult Details(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(role);
        }
        
        //
        // GET: /Role/Edit/5
 
        public ActionResult Edit(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Role/Delete/5
 
        public ActionResult Delete(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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