using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Model.DTO
{
    public class VehicleModelDTO : BaseEntity
    {
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? BrandId { get; set; }
        public int? VehicleTypeId { get; set; }
        public int? GasTypeId { get; set; }

        public VehicleModelDTO(VehicleModel vehicleModel)
        {
            Description = vehicleModel.Description;
            Year = vehicleModel.Year;
            BrandId = vehicleModel.BrandId;
            VehicleTypeId = vehicleModel.VehicleTypeId;
            GasTypeId = vehicleModel.GasTypeId;
        }

        public VehicleModelDTO()
        {

        }

        public VehicleModel ToModel()
        {
            var model = new VehicleModel();
            model.Id = this.Id;
            model.GasTypeId = this.GasTypeId;
            model.Description = this.Description;
            model.BrandId = this.BrandId;
            model.VehicleTypeId = this.VehicleTypeId;
            model.Year = Year;

            return model;
        }
    }
}
