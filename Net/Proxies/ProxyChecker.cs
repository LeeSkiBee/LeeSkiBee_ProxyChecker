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
        public Action<int, bool, int> Result { get; set; }

        /// <summary>
        /// Checks if a proxy server allows HTTP requests to be send through it.
        /// </summary>
        /// <param name="URL">The IP Address/Domain Name of to send a test HTTP request to. </param>
        /// <param name="proxyAndPort">The IPAddress/Domain Name of the proxy server and the port to use (separated by a colon) - such as "127.0.0.1:8080". </param>
        /// <param name="timeout">How long before the HTTP request times out and is considered to have failed, in miliseconds. </param>
        /// <returns>Returns true if the HTTP request was successful and false if it was not. </returns>
        public bool CheckProxyHTTPAccess(int proxyIndex, int threadIndex, string URL, string proxyAndPort, int timeout)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = WebRequestMethods.Http.Head;   //Uses Head method to reduce bandwidth usage for requests.
                request.Proxy = new WebProxy(proxyAndPort);
                request.KeepAlive = false;
                request.Timeout = timeout;
                HttpStatusCode result = new HttpStatusCode();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    result = response.StatusCode;
                }
                bool successful = (result == HttpStatusCode.OK);
                try
                {
                    Result(proxyIndex, successful, threadIndex);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }     
                return successful;
            }
            catch (Exception e) //Doesn't matter why the request failed - so just catch all exceptions.
            {
                Console.WriteLine(e.Message);
            }
            return false;       //If this point is reached then the request failed due to an exception.
        }
    }
}
