using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace LeeSkiBee_ProxyChecker.Net.Proxies
{
    /// <summary>
    /// A class for checking proxy functionality - such as if the proxy supports HTTP traffic.
    /// </summary>
    public class ProxyChecker
    {
        public Action<ProxyCheckResult> HTTPCheckResult = null;
        public Uri TestURL = null;
        public int HTTPCheckTimeout = HTTP_CHECK_TIMEOUT;

        private const int HTTP_CHECK_TIMEOUT = 2000;
        private const string ARRAY_TYPE_ID_STRING = "[]";


        public void TestConnection(Uri URL, int timeout = HTTP_CHECK_TIMEOUT)
        {
            //dummyProxy required because CheckProxyHTTPAccess ignores requests with null values as the proxy
            //but does not ignore WebProxy objects without the proxy address set.
            WebProxy dummyProxy = new WebProxy();
            CheckProxyHTTPAccess(dummyProxy, URL, timeout);
        }

        /// <summary>
        /// Calls CheckProxyHTTPAccess method on a list of proxies.
        /// 
        /// Exceptions:
        ///     System.ArgumentException
        /// </summary>
        /// <param name="proxyList">The proxy list to check through. Acceptable parameter types are string, Uri, and, WebProxy. </param>
        public void CheckProxyHTTPAccess_List(object proxyList)
        {
            if (proxyList.GetType().ToString().Contains(ARRAY_TYPE_ID_STRING))
            {
                object[] proxies = (object[])proxyList;
                for (int i = 0; i < proxies.Length; i++)
                {
                    CheckProxyHTTPAccess(proxies[i]);
                }
            }
            else
            {
                throw (new ArgumentException("Argument not an array data type."));
            }
        }

        /// <summary>
        /// Checks if a proxy server allows HTTP requests to be sent through it.
        /// </summary>
        /// <param name="proxyAndPort">The proxy to check. Acceptable parameter types are string, Uri, and, WebProxy.m</param>
        public void CheckProxyHTTPAccess(object proxyAndPort)
        {
            WebProxy convertedProxy = null;
            if (proxyAndPort.GetType() == typeof(string))
            {
                try
                {
                    convertedProxy = new WebProxy((string)proxyAndPort);
                }
                catch (UriFormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (proxyAndPort.GetType() == typeof(Uri))
            {
                convertedProxy = new WebProxy((Uri)proxyAndPort);
            }
            else
            {
                try
                {
                    convertedProxy = (WebProxy)proxyAndPort;
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            CheckProxyHTTPAccess(convertedProxy);
        }

        //@Overload
        public ProxyCheckResult CheckProxyHTTPAccess(WebProxy proxyAndPort)
        {
            return CheckProxyHTTPAccess(proxyAndPort, TestURL, HTTPCheckTimeout);
        }

        /// <summary>
        /// Checks if a proxy server allows HTTP requests to be sent through it.
        /// </summary>
        /// <param name="proxyAndPort">The WebProxy for the connection to be sent through. </param>
        /// <param name="URL">The IP Address/Domain Name to send a test HTTP request to. </param>
        /// <param name="timeout">How long before the HTTP request times out and is considered to have failed, in miliseconds. </param>
        /// <returns>Returns a ProxyCheckResult container with the results of the check. </returns>
        public ProxyCheckResult CheckProxyHTTPAccess(WebProxy proxyAndPort, Uri URL, int timeout = HTTP_CHECK_TIMEOUT)
        {
            bool successful = false;
            //if statement prevents time being wasted waiting for a timeout to occur when
            //the request will fail due to an unacceptable proxy/url.
            if ((proxyAndPort != null) && (URL != null))
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.Method = WebRequestMethods.Http.Head;   //Uses Head method to reduce bandwidth usage for requests.
                    request.Proxy = proxyAndPort;
                    request.AllowAutoRedirect = false;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:25.0) Gecko/20100101 Firefox/25.0";
                    request.Timeout = timeout;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        successful = (response.StatusCode == HttpStatusCode.OK);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            ProxyCheckResult result = new ProxyCheckResult(successful, proxyAndPort, URL);
            if (HTTPCheckResult != null)
            {
                try
                {
                    HTTPCheckResult(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }
    }
}
