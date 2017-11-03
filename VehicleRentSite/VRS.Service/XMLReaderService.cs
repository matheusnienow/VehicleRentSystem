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
        private System.Timers.Timer timer;

        DateTime lastRun = DateTime.MinValue;
        string filePath = "C:\\Users\\Matheus N. Nienow\\Desktop\\VehicleModelSource.xml";

        public XMLReaderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.timer = new System.Timers.Timer(30000D);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            this.timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Execute();
        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }

        void Execute()
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
    }
}
