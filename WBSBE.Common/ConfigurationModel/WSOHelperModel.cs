using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.ConfigurationModel
{
    public class WSOHelperModel
    {
        public string wso_api_gateway { get; set; }
        public string wso_consumer_key { get; set; }
        public string wso_consumer_secret { get; set; }
        public string txtGlobalAPI_Url { get; set; }
    }
}
