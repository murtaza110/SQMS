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
    public class SabaqGroupController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /SabaqGroup/

        public ViewResult Index()
        {
            var sabaqgroups = db.SabaqGroups.Include(s => s.Nisaab).Include(s => s.Region).Include(s => s.User).Include(s => s.SabaqStatus);
            return View(sabaqgroups.ToList());
        }

        //
        // GET: /SabaqGroup/Details/5

        public ViewResult Details(long id)
        {
            SabaqGroup sabaqgroup = db.SabaqGroups.Find(id);
            return View(sabaqgroup);
        }

        //
        // GET: /SabaqGroup/Create

        public ActionResult Create()
        {
            var _roles = db.Roles.Include( u => u.Users );
            List<User> user = new List<Models.User>();
            foreach (Role role in _roles)
            {
                if (role.RoleId == 4)
                {
                    user.AddRange(role.Users.ToList<User>());
                    break;
                }
            }

            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName");
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(d => d.Region1), "RegionId", "RegionNameShow", 5);
            ViewBag.MoallimId = new SelectList(user, "UserId", "NameToShow");
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", 2);
            return View();
        } 

        //.
        // POST: /SabaqGroup/Create

        [HttpPost]
        public ActionResult Create(SabaqGroup sabaqgroup)
        {
            if (ModelState.IsValid)
            {
                db.SabaqGroups.Add(sabaqgroup);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            List<User> user = new List<Models.User>();
            foreach (Role role in db.Roles)
            {
                if (role.RoleId == 4)
                    user = role.Users.ToList<User>();
            }

            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", sabaqgroup.NisaabId);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(d => d.Region1), "RegionId", "RegionName", sabaqgroup.MohallaId);
            ViewBag.MoallimId = new SelectList(user, "UserId", "Title", sabaqgroup.MoallimId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqgroup.SabaqStatusId);
            return View(sabaqgroup);
        }
        
        //
        // GET: /SabaqGroup/Edit/5
 
        public ActionResult Edit(long id)
        {
            SabaqGroup sabaqgroup = db.SabaqGroups.Find(id);
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", sabaqgroup.NisaabId);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(d => d.Region1), "RegionId", "RegionName", sabaqgroup.MohallaId);
            ViewBag.MoallimId = new SelectList(db.Users, "UserId", "Title", sabaqgroup.MoallimId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqgroup.SabaqStatusId);
            return View(sabaqgroup);
        }

        //
        // POST: /SabaqGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(SabaqGroup sabaqgroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sabaqgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", sabaqgroup.NisaabId);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == 5).Include(d => d.Region1), "RegionId", "RegionName", sabaqgroup.MohallaId);
            ViewBag.MoallimId = new SelectList(db.Users, "UserId", "Title", sabaqgroup.MoallimId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqgroup.SabaqStatusId);
            return View(sabaqgroup);
        }

        //
        // GET: /SabaqGroup/Delete/5
 
        public ActionResult Delete(long id)
        {
            SabaqGroup sabaqgroup = db.SabaqGroups.Find(id);
            return View(sabaqgroup);
        }

        //
        // POST: /SabaqGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {            
            SabaqGroup sabaqgroup = db.SabaqGroups.Find(id);
            db.SabaqGroups.Remove(sabaqgroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /SabaqRegistration/Create

        public ActionResult CreateSabaqRegistration(int id)
        {
            List<SabaqGroup> sabaqGroup = new List<SabaqGroup>();
            sabaqGroup.Add(db.SabaqGroups.Find(id));

            ViewBag.SabaqGroupId = new SelectList(sabaqGroup, "SabaqGroupId", "GroupName", id);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", 2);
            ViewBag.MemberId = new SelectList(db.Users, "UserId", "NameToShow");
            return View();
        }

        //
        // POST: /SabaqRegistration/Create

        [HttpPost]
        public ActionResult CreateSabaqRegistration(SabaqRegistration sabaqregistration)
        {
            if (ModelState.IsValid)
            {
                db.SabaqRegistrations.Add(sabaqregistration);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = sabaqregistration.SabaqGroupId });
            }

            List<SabaqGroup> sabaqGroup = new List<SabaqGroup>();
            sabaqGroup.Add(db.SabaqGroups.Find(sabaqregistration.SabaqGroupId));

            ViewBag.SabaqGroupId = new SelectList(sabaqGroup, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
            ViewBag.MemberId = new SelectList(db.Users, "UserId", "NameToShow", sabaqregistration.MemberId);
            return View(sabaqregistration);
        }

        ////
        //// GET: /SabaqRegistration/Edit/5

        //public ActionResult EditSabaqRegistration(long id)
        //{
        //    SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
        //    ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
        //    ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
        //    return View(sabaqregistration);
        //}

        ////
        //// POST: /SabaqRegistration/Edit/5

        //[HttpPost]
        //public ActionResult EditSabaqRegistration(SabaqRegistration sabaqregistration)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sabaqregistration).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
        //    ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
        //    return View(sabaqregistration);
        //}

        ////
        //// GET: /SabaqRegistration/Delete/5

        //public ActionResult DeleteSabaqRegistration(long id)
        //{
        //    SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
        //    return View(sabaqregistration);
        //}

        ////
        //// POST: /SabaqRegistration/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmedSabaqRegistration(long id)
        //{
        //    SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
        //    db.SabaqRegistrations.Remove(sabaqregistration);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}