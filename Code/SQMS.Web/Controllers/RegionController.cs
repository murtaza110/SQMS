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
    public class RegionController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /Region/

        public ViewResult Index()
        {
            var regions = db.Regions.Include(r => r.RegionType).Include(r => r.Region1);
            return View(regions.ToList());
        }

        //
        // GET: /Region/Details/5

        public ViewResult Details(int id)
        {
            Region region = db.Regions.Find(id);
            return View(region);
        }

        //
        // GET: /Region/Create

        public ActionResult Create()
        {
            ViewBag.RegionTypeId = new SelectList(db.RegionTypes, "RegionTypeId", "RegionTypeName");
            ViewBag.ParentRegionId = new SelectList(db.Regions, "RegionId", "RegionName");
            return View();
        } 

        //
        // POST: /Region/Create

        [HttpPost]
        public ActionResult Create(Region region)
        {
            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.RegionTypeId = new SelectList(db.RegionTypes, "RegionTypeId", "RegionTypeName", region.RegionTypeId);
            ViewBag.ParentRegionId = new SelectList(db.Regions, "RegionId", "RegionName", region.ParentRegionId);
            return View(region);
        }
        
        //
        // GET: /Region/Edit/5
 
        public ActionResult Edit(int id)
        {
            Region region = db.Regions.Find(id);
            ViewBag.RegionTypeId = new SelectList(db.RegionTypes, "RegionTypeId", "RegionTypeName", region.RegionTypeId);
            ViewBag.ParentRegionId = new SelectList(db.Regions, "RegionId", "RegionName", region.ParentRegionId);
            return View(region);
        }

        //
        // POST: /Region/Edit/5

        [HttpPost]
        public ActionResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionTypeId = new SelectList(db.RegionTypes, "RegionTypeId", "RegionTypeName", region.RegionTypeId);
            ViewBag.ParentRegionId = new SelectList(db.Regions, "RegionId", "RegionName", region.ParentRegionId);
            return View(region);
        }

        //
        // GET: /Region/Delete/5
 
        public ActionResult Delete(int id)
        {
            Region region = db.Regions.Find(id);
            return View(region);
        }

        //
        // POST: /Region/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Region region = db.Regions.Find(id);
            db.Regions.Remove(region);
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