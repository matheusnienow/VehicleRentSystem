using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.WebApi
{
    public class GasTypesController : Controller
    {
        private Repository<GasType> repository = Repository<GasType>.GetInstance();

        // GET: GasTypes
        public ActionResult Index()
        {
            var repo = Repository<Brand>.GetInstance();
            var gasTypes = repository.GetAll().ToList();
            return View(gasTypes);
        }

        // GET: GasTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            GasType gasType = repository.GetById(id.Value);

            if (gasType == null)
            {
                return HttpNotFound();
            }
            return View(gasType);
        }

        // GET: GasTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GasTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] GasType gasType)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(gasType);
                return RedirectToAction("Index");
            }

            return View(gasType);
        }

        // GET: GasTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GasType gasType = repository.GetById(id.Value);
            if (gasType == null)
            {
                return HttpNotFound();
            }
            return View(gasType);
        }

        // POST: GasTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] GasType gasType)
        {
            if (ModelState.IsValid)
            {
                repository.Update(gasType);
                return RedirectToAction("Index");
            }
            return View(gasType);
        }

        // GET: GasTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GasType gasType = repository.GetById(id.Value);
            
            if (gasType == null)
            {
                return HttpNotFound();
            }
            return View(gasType);
        }

        // POST: GasTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GasType gasType = repository.GetById(id);
            repository.Delete(gasType);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
