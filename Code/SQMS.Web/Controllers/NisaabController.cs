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
    public class NisaabController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /Nisaab/

        public ViewResult Index()
        {
            var nisaabs = db.Nisaabs.Include(r => r.Nissab1);
            return View(nisaabs.ToList());
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
            ViewBag.PrereqNisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName");
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

            ViewBag.PrereqNisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", nisaab.PrereqNisaabId);
            return View(nisaab);
        }
        
        //
        // GET: /Nisaab/Edit/5
 
        public ActionResult Edit(int id)
        {
            Nisaab nisaab = db.Nisaabs.Find(id);
            ViewBag.PrereqNisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", nisaab.PrereqNisaabId);
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

            ViewBag.PrereqNisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", nisaab.PrereqNisaabId);
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

        //
        // GET: /Nisaab/Create

        public ActionResult CreateBook(int id)
        {
            List<Nisaab> nisaab = new List<Nisaab>();
            nisaab.Add(db.Nisaabs.Find(id));
            ViewBag.NisaabId = new SelectList(nisaab, "NisaabId", "NisaabName", id);         
            return View();
        }

        //
        // POST: /Nisaab/Create

        [HttpPost, ActionName("CreateBook")]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = book.NisaabId });
            }

            List<Nisaab> nisaab = new List<Nisaab>();
            nisaab.Add(db.Nisaabs.Find(book.NisaabId));
            ViewBag.NisaabId = new SelectList(nisaab, "NisaabId", "NisaabName", book.NisaabId);
            return View(book);
        }


        //
        // GET: /Nisaab/Edit/5

        public ActionResult EditBook(int id)
        {
            Book book = db.Books.Find(id);
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", book.NisaabId);
            return View(book);
        }

        //
        // POST: /Nisaab/Edit/5

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = book.NisaabId });
            }

            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", book.NisaabId);
            return View(book);
        }

        //
        // GET: /Book/Delete/5

        public ActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            return View(book);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteBookConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = book.NisaabId });
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}