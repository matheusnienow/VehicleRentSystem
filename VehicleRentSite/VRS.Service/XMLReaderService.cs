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
using System.ComponentModel;
using System.Diagnostics;

namespace VRS.Service
{
    public partial class XMLReaderService : ServiceBase
    {
        private System.Timers.Timer timer;
        private DateTime lastRun;
        static string filePath = @"C:\temp\VehicleModelSource.xml";

        public XMLReaderService()
        {
            InitializeComponent();

            this.ServiceName = "VRS.Service";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = false;
            ((ISupportInitialize)this.EventLog).BeginInit();
            if (!EventLog.SourceExists(this.ServiceName))
            {
                EventLog.CreateEventSource(this.ServiceName, "Application");
            }
            ((ISupportInitialize)this.EventLog).EndInit();
            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = "Application";
        }

        protected override void OnStart(string[] args)
        {
            lastRun = DateTime.MinValue;

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

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }

        void Execute()
        {
            try { 
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
            } catch (Exception e)
            {
                this.EventLog.WriteEntry(e.Message);
            }
        }

        private int InsertInfo(List<VehicleModel> list)
        {
            var repository = Repository<VehicleModel>.NewInstance();
            var result = repository.Insert(list);
            if (result == 0)
            {
                this.EventLog.WriteEntry("InsertInfo(): nothing was inserted from a list of "+list.Count);
            }
            return result;
        }

        private void ClearFile(string file)
        {
            try { 
                var emptyArray = new List<VehicleModelDTO>();
                var emptyContent = SerializeHelper.Serialize(emptyArray);
                File.WriteAllText(file, emptyContent);
            } catch (Exception e)
            {
                this.EventLog.WriteEntry("Error clearing file: " + e.Message);
            }
        }

        private List<VehicleModel> GetListFromFile(string fileName)
        {
            var dtos = ReadXmlFile(fileName);
            if (dtos.Length == 0)
            {
                return new List<VehicleModel>();
            }
            return ConvertDtoToModel(dtos);
        }

        private VehicleModelDTO[] ReadXmlFile(string fileName)
        {
            var result = new VehicleModelDTO[] { };
            try { 
                if (string.IsNullOrEmpty(fileName))
                {
                    this.EventLog.WriteEntry(string.Format("File {0} is empty or null", fileName));
                    return result;
                }

                string xml = File.ReadAllText(fileName);
                if (string.IsNullOrEmpty(xml))
                {
                    this.EventLog.WriteEntry(string.Format("Xml {0} is empty", fileName));
                    return result;
                }

                result = SerializeHelper.Deserialize<VehicleModelDTO[]>(xml);
            } catch (Exception e)
            {
                this.EventLog.WriteEntry(string.Format("Error reading xml file: {0}", e.Message));
            }
            return result;
        }

        private List<VehicleModel> ConvertDtoToModel(VehicleModelDTO[] dtos)
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
