using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using VRS.Model;
using VRS.Model.DTO;
using VRS.Model.Repository;
using System.Configuration;

namespace VRS.Service
{
    public partial class XMLReaderService : ServiceBase
    {
        string filePath = "C:\\Users\\Matheus N. Nienow\\Desktop\\VehicleModelSource.xml";
        private int interval = 60;

        private bool execute = true;
        private const int SECONDS = 1000;

        public XMLReaderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            while (execute)
            {
                Execute();
                Thread.Sleep(interval * SECONDS);
            }
        }

        protected override void OnStop()
        {
            execute = false;
        }

        void Execute()
        {
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
