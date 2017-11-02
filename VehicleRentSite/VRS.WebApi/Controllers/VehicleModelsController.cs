using System.Linq;
using System.Net;
using System.Web.Mvc;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.WebApi
{
    public class VehicleModelsController : Controller
    {
        private IRepository<VehicleModel> repo = Repository<VehicleModel>.GetInstance();

        // GET: VehicleModels
        public ActionResult Index()
        {
            var vehicleModels = repo.GetAll(v => v.Brand, v => v.GasType, v => v.VehicleType).ToList();
            return View(vehicleModels);
        }

        // GET: VehicleModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = repo.GetById(id.Value);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(Repository<Brand>.GetInstance().GetAll(), "Id", "Name");
            ViewBag.GasTypeId = new SelectList(Repository<GasType>.GetInstance().GetAll(), "Id", "Description");
            ViewBag.ModelId = new SelectList(Repository<VehicleType>.GetInstance().GetAll(), "Id", "Name");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Year,BrandId,ModelId,GasTypeId")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(vehicleModel);
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(Repository<Brand>.GetInstance().GetAll(), "Id", "Name", vehicleModel.BrandId);
            ViewBag.GasTypeId = new SelectList(Repository<GasType>.GetInstance().GetAll(), "Id", "Description", vehicleModel.GasTypeId);
            ViewBag.ModelId = new SelectList(Repository<VehicleType>.GetInstance().GetAll(), "Id", "Name", vehicleModel.VehicleTypeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = repo.GetById(id.Value);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(Repository<Brand>.GetInstance().GetAll(), "Id", "Name", vehicleModel.BrandId);
            ViewBag.GasTypeId = new SelectList(Repository<GasType>.GetInstance().GetAll(), "Id", "Description", vehicleModel.GasTypeId);
            ViewBag.ModelId = new SelectList(Repository<VehicleType>.GetInstance().GetAll(), "Id", "Name", vehicleModel.VehicleTypeId);
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Year,BrandId,ModelId,GasTypeId")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                repo.Update(vehicleModel);
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(Repository<Brand>.GetInstance().GetAll(), "Id", "Name", vehicleModel.BrandId);
            ViewBag.GasTypeId = new SelectList(Repository<GasType>.GetInstance().GetAll(), "Id", "Description", vehicleModel.GasTypeId);
            ViewBag.ModelId = new SelectList(Repository<VehicleType>.GetInstance().GetAll(), "Id", "Name", vehicleModel.VehicleTypeId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = repo.GetById(id.Value);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleModel vehicleModel = repo.GetById(id);
            repo.Delete(vehicleModel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
