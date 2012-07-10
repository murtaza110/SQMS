using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQMS.Web.Models;

namespace SQMS.Web.Controllers
{ 
    /*
    public class UsersController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(long id)
        {
            User usermodel = db.Users.Find(id);
            return View(usermodel);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User usermodel)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(usermodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usermodel);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(long id)
        {
            User usermodel = db.Users.Find(id);
            return View(usermodel);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User usermodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usermodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usermodel);
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(long id)
        {
            User usermodel = db.Users.Find(id);
            return View(usermodel);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            User usermodel = db.Users.Find(id);
            db.Users.Remove(usermodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }*/
}