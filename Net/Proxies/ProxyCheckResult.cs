using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace LeeSkiBee_ProxyChecker.Net.Proxies
{
    /// <summary>
    /// A container for the result of a proxy check
    /// </summary>
    public class ProxyCheckResult
    {
        public bool Result { get; private set; }
        public WebProxy Proxy { get; private set; }
        public Uri URL { get; private set; }

        /// <summary>
        /// A string containing the proxy without any protocol text, such as http://
        /// </summary>
        public string AddressAndPort { get; private set; }

        public ProxyCheckResult(bool testResult, WebProxy testProxy, Uri testURL)
        {
            this.Result = testResult;
            this.Proxy = testProxy;
            string addressToStore = null;
            if (testProxy != null)
            {
                if (testProxy.Address != null)
                {
                    //WebProxy.Address contains the full Uri address, not the string provided when creating the WebProxy.
                    //I.E. WebProxy.Address stores "http://127.0.0.1:8080/" rather than "127.0.0.1:8080".
                    //Replace the "http://" at the start and the "/" at the end to get the original proxy string.
                    addressToStore = testProxy.Address.ToString().Replace("http://", "").Replace("/", "");
                }
            }
            this.AddressAndPort = addressToStore;  
            this.URL = testURL;
        }
    }
}
