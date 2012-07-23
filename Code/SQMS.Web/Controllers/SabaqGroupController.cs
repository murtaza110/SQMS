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
            List<User> users = new List<Models.User>();
            foreach (Role role in _roles)
            {
                if (role.RoleId == (int)ENRole.Moallim)
                {
                    users.AddRange(role.Users.ToList<User>());
                    break;
                }
            }

            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName");
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(d => d.Region1), "RegionId", "RegionNameShow");
            ViewBag.MoallimId = new SelectList(users, "UserId", "DisplayName");
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", (int)ENSabaqStatus.Created);
            return View();
        } 

        //
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

            var _roles = db.Roles.Include(u => u.Users);
            List<User> users = new List<Models.User>();
            foreach (Role role in _roles)
            {
                if (role.RoleId == (int)ENRole.Moallim)
                {
                    users.AddRange(role.Users.ToList<User>());
                    break;
                }
            }

            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", sabaqgroup.NisaabId);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(d => d.Region1), "RegionId", "RegionName", sabaqgroup.MohallaId);
            ViewBag.MoallimId = new SelectList(users, "UserId", "DisplayName", sabaqgroup.MoallimId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqgroup.SabaqStatusId);
            return View(sabaqgroup);
        }
        
        //
        // GET: /SabaqGroup/Edit/5
 
        public ActionResult Edit(long id)
        {
            SabaqGroup sabaqgroup = db.SabaqGroups.Find(id);
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", sabaqgroup.NisaabId);
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(d => d.Region1), "RegionId", "RegionName", sabaqgroup.MohallaId);
            ViewBag.MoallimId = new SelectList(db.Users, "UserId", "DisplayName", sabaqgroup.MoallimId);
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
            ViewBag.MohallaId = new SelectList(db.Regions.Where(c => c.RegionTypeId == (int)ENRegionType.Mohalla).Include(d => d.Region1), "RegionId", "RegionName", sabaqgroup.MohallaId);
            ViewBag.MoallimId = new SelectList(db.Users, "UserId", "DisplayName", sabaqgroup.MoallimId);
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
        // GET: SabaqGroup/Register

        public ActionResult Register(int id)
        {
            List<SabaqGroup> sabaqGroup = new List<SabaqGroup>();
            sabaqGroup.Add(db.SabaqGroups.Find(id));

            ViewBag.SabaqGroupId = new SelectList(sabaqGroup, "SabaqGroupId", "GroupName", id);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", (int)ENSabaqStatus.Created);
            //ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName");
            
            SabaqRegistration model = new SabaqRegistration();
            model.AllMembers = new MultiSelectList(db.Users, "UserId", "UserID_DisplayName").AsEnumerable();
            return View(model);
        }

        //
        // POST: SabaqGroup/Register

        [HttpPost]
        public ActionResult Register(SabaqRegistration sabaqregistration)
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
            //ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName", sabaqregistration.MemberId);
            return View(sabaqregistration);
        }

        //
        // GET: SabaqGroup/EditRegistration/5

        //public ActionResult EditRegistration(long id)
        //{
        //    SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
        //    ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
        //    ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
        //    ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName", sabaqregistration.MemberId);

        //    return View(sabaqregistration);
        //}

        //
        // POST: SabaqGroup/EditRegistration/5

        //[HttpPost]
        //public ActionResult EditRegistration(SabaqRegistration sabaqregistration)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sabaqregistration).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Details", new { id = sabaqregistration.SabaqGroupId });
        //    }
        //    ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
        //    ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
        //    ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName", sabaqregistration.MemberId);
        //    return View(sabaqregistration);
        //}

        ////
        //// GET: /SabaqGroup/DeleteRegistration/5

        public ActionResult DeleteRegistration(long id)
        {
            SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
            return View(sabaqregistration);
        }

        //
        // POST: /Sabaq/DeleteRegistration/5

        [HttpPost, ActionName("DeleteRegistration")]
        public ActionResult DeleteConfirmedSabaqRegistration(long id)
        {
            SabaqRegistration sabaqregistration = db.SabaqRegistrations.Find(id);
            db.SabaqRegistrations.Remove(sabaqregistration);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = sabaqregistration.SabaqGroupId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}