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
            if (testProxy == null)
            {
                this.AddressAndPort = "null";
            }
            else
            {
                this.AddressAndPort = testProxy.Address.ToString().Replace("http://", "").Replace("/", "");
            }        
            this.URL = testURL;
        }
    }
}
