using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace ProjetoSQLCLR
{
    public static class Web
    {
        public static string Post(string data,
                                string url,
                                string contentType = "application/json")
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var httpWebRequest = WebResquest(url, MethodsHTTP.POST.Name(), contentType);

            if (!string.IsNullOrEmpty(data))
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(data.Replace("utf-16", "utf-8"));
                    streamWriter.Flush();
                }
            }
            return WebResponse(httpWebRequest);
        }

        public static string Get(string url,
                                 string contentType = "application/json")
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var httpWebRequest = WebResquest(url, MethodsHTTP.GET.Name(), contentType);
            return WebResponse(httpWebRequest);
        }

        private static string WebResponse(HttpWebRequest httpWebRequest)
        {
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        private static HttpWebRequest WebResquest(string url, string httpMethod, string contentType)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = httpMethod;
            httpWebRequest.KeepAlive = false;
            httpWebRequest.Proxy = null;
            return httpWebRequest;
        }
    }
}
