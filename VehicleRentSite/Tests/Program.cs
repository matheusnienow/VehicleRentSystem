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
        static DateTime lastRun = DateTime.MinValue;

        static string filePath = "C:\\Users\\Matheus N. Nienow\\Desktop\\VehicleModelSource.xml";
        static void Main(string[] args)
        {
            Execute();

            while (true)
            {
                if (DateTime.Now.Subtract(lastRun).Seconds > 30)
                {
                    Execute();
                }
                Thread.Sleep(1000);
            }
        }

        static void Execute()
        {
            lastRun = DateTime.Now;
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
            if (dtos.Length == 0)
            {
                return new List<VehicleModel>();
            }
            return ConvertDtoToModel(dtos);
        }

        private static VehicleModelDTO[] ReadXmlFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            string xml = File.ReadAllText(fileName);
            if (string.IsNullOrEmpty(xml))
            {
                return new VehicleModelDTO[] { };
            }
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

        public class PeriodicTask
        {
            public static async Task Run(Action action, TimeSpan period, CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(period, cancellationToken);

                    if (!cancellationToken.IsCancellationRequested)
                        action();
                }
            }

            public static Task Run(Action action, TimeSpan period)
            {
                return Run(action, period, CancellationToken.None);
            }
        }
    }
}
