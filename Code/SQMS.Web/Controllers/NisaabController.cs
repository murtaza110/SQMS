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
    public class NisaabController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /Nisaab/

        public ViewResult Index()
        {
            return View(db.Nisaabs.ToList());
        }

        //
        // GET: /Nisaab/Details/5

        public ViewResult Details(int id)
        {
            Nisaab nisaab = db.Nisaabs.Find(id);
            return View(nisaab);
        }

        //
        // GET: /Nisaab/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Nisaab/Create

        [HttpPost]
        public ActionResult Create(Nisaab nisaab)
        {
            if (ModelState.IsValid)
            {
                db.Nisaabs.Add(nisaab);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(nisaab);
        }
        
        //
        // GET: /Nisaab/Edit/5
 
        public ActionResult Edit(int id)
        {
            Nisaab nisaab = db.Nisaabs.Find(id);
            return View(nisaab);
        }

        //
        // POST: /Nisaab/Edit/5

        [HttpPost]
        public ActionResult Edit(Nisaab nisaab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nisaab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nisaab);
        }

        //
        // GET: /Nisaab/Delete/5
 
        public ActionResult Delete(int id)
        {
            Nisaab nisaab = db.Nisaabs.Find(id);
            return View(nisaab);
        }

        //
        // POST: /Nisaab/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Nisaab nisaab = db.Nisaabs.Find(id);
            db.Nisaabs.Remove(nisaab);
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