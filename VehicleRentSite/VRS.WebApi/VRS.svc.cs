using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VRS.WebApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VRS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select VRS.svc or VRS.svc.cs at the Solution Explorer and start debugging.
    public class VRS : IVRS
    {
        public void DoLogin(string username, string password)
        {
        }
    }
}
