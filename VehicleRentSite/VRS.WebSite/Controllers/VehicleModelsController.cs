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
    public class VehicleModelsController : Controller
    {
        private VRSModel db = new VRSModel();

        // GET: VehicleModels
        public ActionResult Index()
        {
            var vehicleModel = db.VehicleModel.Include(v => v.Brand).Include(v => v.GasType).Include(v => v.VehicleType);
            return View(vehicleModel.ToList());
        }

        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = db.VehicleModel.Find(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
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
