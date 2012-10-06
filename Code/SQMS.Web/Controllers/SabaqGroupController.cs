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
    [MyAuthorizeAttribute(Roles = CommonUtility.SubAdminRole)]
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
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", (int)ENSabaqStatus.Approved);
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
            SabaqGroup sabaqGroup = db.SabaqGroups.Find(id);
            List<SabaqGroup> lstSabaqGroup = new List<SabaqGroup>();
            lstSabaqGroup.Add(sabaqGroup);

            ViewBag.SabaqGroupId = new SelectList(lstSabaqGroup, "SabaqGroupId", "GroupName", id);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", (int)ENSabaqStatus.Approved);
            //ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName");
            
            var users = from u in db.Users
                        where !(from u1 in db.SabaqRegistrations
                                where u1.SabaqGroupId == sabaqGroup.SabaqGroupId
                                select u1.MemberId).Contains(u.UserId)
                        select u;
            
            SabaqRegistration model = new SabaqRegistration();
            model.AllMembers = new MultiSelectList(users, "UserId", "UserID_DisplayName").AsEnumerable();
            return View(model);
        }

        //
        // POST: SabaqGroup/Register

        [HttpPost]
        public ActionResult Register(SabaqRegistration sabaqregistration)
        {
            if (ModelState.IsValid)
            {
                foreach (int memberId in sabaqregistration.SelectedMembers)
                {
                    sabaqregistration.MemberId = memberId;
                    db.SabaqRegistrations.Add(sabaqregistration);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", new { id = sabaqregistration.SabaqGroupId });
            }

            List<SabaqGroup> sabaqGroup = new List<SabaqGroup>();
            sabaqGroup.Add(sabaqregistration.SabaqGroup);

            ViewBag.SabaqGroupId = new SelectList(sabaqGroup, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
            ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", (int)ENSabaqStatus.Approved);
            //ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName");

            var users = from u in db.Users
                        where !(from u1 in db.SabaqRegistrations
                                where u1.SabaqGroupId == sabaqregistration.SabaqGroupId
                                select u1.MemberId).Contains(u.UserId)
                        select u;

            sabaqregistration.AllMembers = new MultiSelectList(db.Users, "UserId", "UserID_DisplayName").AsEnumerable();

            return View(sabaqregistration);
        }

        [HttpGet]
        public PartialViewResult CardPrintImage(long id)
        {
            var sabaqregistration = db.SabaqRegistrations.Where(d => d.SabaqRegId == id).Include(d => d.SabaqGroup).Include(d => d.User);
            SabaqRegistration _sabaqReg = sabaqregistration.First();
            return PartialView(_sabaqReg);//View(_sabaqReg);
        }

        //
        // GET: SabaqGroup/EditRegistration/5

        public ActionResult EditRegistration(long id)
        {
            var sabaqregistration = db.SabaqRegistrations.Where(d => d.SabaqRegId == id).Include(d => d.SabaqGroup).Include(d => d.User);
            SabaqRegistration _sabaqReg = sabaqregistration.First();
            //ViewBag.SabaqGroupId = new SelectList(db.SabaqGroups, "SabaqGroupId", "GroupName", sabaqregistration.SabaqGroupId);
            //ViewBag.SabaqStatusId = new SelectList(db.SabaqStatus, "SabaqStatusId", "SabaqStatusName", sabaqregistration.SabaqStatusId);
            //ViewBag.MemberId = new SelectList(db.Users, "UserId", "UserID_DisplayName", sabaqregistration.MemberId);

            //User user = db.Users.Find(sabaqregistration.MemberId);
            //SabaqGroup sabaqGroup = db.SabaqGroups.Find(sabaqregistration.SabaqGroupId);
            System.Drawing.Image image = CardGenerationModule.CardGenerationModule.Instance.GetCardImage(_sabaqReg.MemberId.ToString(), _sabaqReg.User.DisplayName, _sabaqReg.SabaqGroup.GroupName, _sabaqReg.SabaqGroup.WeekDays, _sabaqReg.User.Phone1); 
            return new ImageResult(image);
        }

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

        //
        // GET: /SabaqGroup/DeleteRegistration/5

        public ActionResult DeleteRegistration(long id)
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