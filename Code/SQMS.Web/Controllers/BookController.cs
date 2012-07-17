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
    [ChildActionOnly]
    [Authorize]
    public class BookController : Controller
    {
        private SQMSDBContext db = new SQMSDBContext();

        //
        // GET: /Book/

        public ViewResult Index()
        {
            var books = db.Books.Include(b => b.Nisaab);
            return View(books.ToList());
        }

        //
        // GET: /Book/Details/5

        public ViewResult Details(int id)
        {
            Book book = db.Books.Find(id);
            return View(book);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName");
            return View();
        } 

        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", book.NisaabId);
            return View(book);
        }
        
        //
        // GET: /Book/Edit/5
 
        public ActionResult Edit(int id)
        {
            Book book = db.Books.Find(id);
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", book.NisaabId);
            return View(book);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NisaabId = new SelectList(db.Nisaabs, "NisaabId", "NisaabName", book.NisaabId);
            return View(book);
        }

        //
        // GET: /Book/Delete/5
 
        public ActionResult Delete(int id)
        {
            Book book = db.Books.Find(id);
            return View(book);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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