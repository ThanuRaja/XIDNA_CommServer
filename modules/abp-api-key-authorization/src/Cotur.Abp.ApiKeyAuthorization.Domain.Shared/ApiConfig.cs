using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cotur.Abp.ApiKeyAuthorization
{
    public class ApiConfig 
    {
        //p- invoke

        private const string APIConfigLib = "apiconfig.dll"; //--enum
        [DllImport(APIConfigLib, CharSet = CharSet.Ansi)]
        private static extern int CreateAPIConfig(string path, string server_address, string server_port, string api_key);

        public int CreateAPIConfigFile(string Path, string Server_address, string Server_port, string Api_key)
        {
            // path -- bin u need put console
            try
            {
                var path = Path;
                string serveraddress = Server_address;
                string serverport = Server_port;
                string apikey = Api_key;
                int status = CreateAPIConfig(path, serveraddress, serverport, apikey);
                return status;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
