using System.IO;
using System.Net;

namespace vk_Api_groups
{
    public class HttpGetter
    {
        public static string GET_http(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var reqGET = WebRequest.Create(url);
            var resp = reqGET.GetResponse();
            var stream = resp.GetResponseStream();
            var sr = new StreamReader(stream);
            var html = sr.ReadToEnd();
            return html;
        }
    }
}