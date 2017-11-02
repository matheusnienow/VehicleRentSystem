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
    public class VehiclesController : Controller
    {
        private VRSModel db = new VRSModel();

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicle = db.Vehicle.Include(v => v.VehicleModel);
            return View(vehicle.ToList());
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
