using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.Logic.Controller
{
    public class VehicleController
    {
        private IRepository<Vehicle> repo = Repository<Vehicle>.NewInstance();

        public IEnumerable<Vehicle> GetAll()
        {
            return repo.GetAll();
        }
    }
}
