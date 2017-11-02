using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VRS.Model;
using VRS.WebSite.Security;

namespace VRS.WebSite.Controllers
{
    [CustomAuthorize(Roles = "Client,Admin")]
    public class RentsController : Controller
    {
        private VRSModel db = new VRSModel();
        private VRS.Logic.Controller.RentController controller = new Logic.Controller.RentController();

        // GET: Rents
        public ActionResult Index()
        {
            var rents = controller.GetRentsForClient(SessionPersister.Client);
            return View(rents.ToList());
        }

        // GET: Rents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rent.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rents/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Client, "Id", "Name");
            ViewBag.VehicleId = new SelectList(db.Vehicle, "Id", "Description");
            return View();
        }
        
        public ActionResult CreateForVehicleId(int vehicleId, string model)
        {
            ViewBag.VehicleId = vehicleId;
            ViewBag.Model = model;
            return View();
        }

        [HttpPost]
        public ActionResult CreateForVehicleId(Rent rent)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Vehicles", null);
            }

            var createdRent = controller.CreateRent(rent, SessionPersister.Client);
            if (createdRent != null)
            {
                return RedirectToAction("Index", "Rents", null);
            }

            return View("error");
        }

        // POST: Rents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,VehicleId,StartDate,FinishDate,Price")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Rent.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Client, "Id", "Name", rent.ClientId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "Id", "Description", rent.VehicleId);
            return View(rent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
