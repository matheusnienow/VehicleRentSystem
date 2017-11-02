using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using VRS.Model;
using VRS.Model.DTO;
using VRS.Model.Repository;
using System.Configuration;
using System.Threading;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Execute();
                Thread.Sleep(10000);
            }         
        }

        private static void Execute()
        {
            string filePath = "C:\\Users\\Matheus N. Nienow\\Desktop\\VehicleModelSource.xml";
            var modelList = GetListFromFile(filePath);
            if (modelList.Count == 0)
            {
                return;
            }

            var result = InsertInfo(modelList);
            if (result > 0)
            {
                ClearFile(filePath);
            }

            Console.Write(modelList);
            Console.ReadKey();
        }

        private static int InsertInfo(List<VehicleModel> list)
        {
            var repository = Repository<VehicleModel>.GetInstance();
            return repository.Insert(list);
        }

        private static void ClearFile(string file) 
        {
            var emptyArray = new List<VehicleModelDTO>();
            var emptyContent = SerializeHelper.Serialize(emptyArray);
            File.WriteAllText(file, emptyContent);
        }

        private static List<VehicleModel> GetListFromFile(string fileName)
        {
            var dtos = ReadXmlFile(fileName);
            return ConvertDtoToModel(dtos);
        }

        private static VehicleModelDTO[] ReadXmlFile(string fileName)
        {
            string xml = File.ReadAllText(fileName);
            return SerializeHelper.Deserialize<VehicleModelDTO[]>(xml);
        }

        private static List<VehicleModel> ConvertDtoToModel(VehicleModelDTO[] dtos)
        {
            var result = new List<VehicleModel>();
            if (dtos.Length > 0)
            {
                dtos.ToList().ForEach(m => result.Add(m.ToModel()));
            }
            return result;
        }
    }
}
