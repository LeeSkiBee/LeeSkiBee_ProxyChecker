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

        private const long PING_FAIL = -1;
        private const int PING_PROXY_TIME_OUT = 500;

        /// <summary>
        /// Pings a server to test if the device is online and packet travel time to the server.
        /// </summary>
        /// <param name="proxyAddress">The IP Address/Domain Name of the server to ping</param>
        /// <param name="timeout">How long before the ping times out, in miliseconds.</param>
        /// <returns>The time the ping response took to be recieved, is MS. Returns -1 for failed pings.</returns>
        public long PingProxy(string proxyAddress, int timeout = PING_PROXY_TIME_OUT)
        {
            try
            {
                Ping pingTest = new Ping();
                PingReply reply = pingTest.Send(proxyAddress, timeout);
                if (reply != null)
                {
                    //The ping was only successful if they reply Status value matches IPStatus.Success
                    if (reply.Status == IPStatus.Success)
                    {
                        return reply.RoundtripTime;
                    }
                }
            }
            catch (Exception e) //Doesn't matter why the request failed - so just catch all exceptions.
            {
                Console.WriteLine(e.Message);
            }
            return PING_FAIL;   //If this point is reached then the ping failed/timed out.
        }

        /// <summary>
        /// Checks if a proxy server allows HTTP requests to be send through it.
        /// </summary>
        /// <param name="URL">The IP Address/Domain Name of to send a test HTTP request to. </param>
        /// <param name="proxyAndPort">The IPAddress/Domain Name of the proxy server and the port to use (separated by a colon) - such as "127.0.0.1:8080". </param>
        /// <param name="timeout">How long before the HTTP request times out and is considered to have failed, in miliseconds. </param>
        /// <returns>Returns true if the HTTP request was successful and false if it was not. </returns>
        public bool CheckProxyHTTPAccess(string URL, string proxyAndPort, int timeout)
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
                return (result == HttpStatusCode.OK);
            }
            catch (Exception e) //Doesn't matter why the request failed - so just catch all exceptions.
            {
                Console.WriteLine(e.Message);
            }
            return false;       //If this point is reached then the request failed due to an exception.
        }
    }
}
