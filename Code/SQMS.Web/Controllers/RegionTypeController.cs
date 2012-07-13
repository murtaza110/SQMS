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
    public class RegionTypeController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /RegionType/

        public ViewResult Index()
        {
            return View(db.RegionTypes.ToList());
        }

        //
        // GET: /RegionType/Details/5

        public ViewResult Details(short id)
        {
            RegionType regiontype = db.RegionTypes.Find(id);
            return View(regiontype);
        }

        //
        // GET: /RegionType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /RegionType/Create

        [HttpPost]
        public ActionResult Create(RegionType regiontype)
        {
            if (ModelState.IsValid)
            {
                db.RegionTypes.Add(regiontype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(regiontype);
        }
        
        //
        // GET: /RegionType/Edit/5
 
        public ActionResult Edit(short id)
        {
            RegionType regiontype = db.RegionTypes.Find(id);
            return View(regiontype);
        }

        //
        // POST: /RegionType/Edit/5

        [HttpPost]
        public ActionResult Edit(RegionType regiontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regiontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regiontype);
        }

        //
        // GET: /RegionType/Delete/5
 
        public ActionResult Delete(short id)
        {
            RegionType regiontype = db.RegionTypes.Find(id);
            return View(regiontype);
        }

        //
        // POST: /RegionType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(short id)
        {            
            RegionType regiontype = db.RegionTypes.Find(id);
            db.RegionTypes.Remove(regiontype);
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