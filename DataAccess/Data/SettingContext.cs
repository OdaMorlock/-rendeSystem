using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace DataAccess.Data
{
    public static  class SettingContext
    {
        public static async Task<IEnumerable<string>> GetStatus()
        {
            var status = new List<string>();


            var jsonsomfinnsifilen = "{\"status\": [\"New\",\"OnGoing\",\"Closed\"]}";
            var s = JsonConvert.DeserializeObject<dynamic>(jsonsomfinnsifilen);
            status.Add("New");
            status.Add("On Going");
            status.Add("Closed");

            return status;
        }
    }
}
