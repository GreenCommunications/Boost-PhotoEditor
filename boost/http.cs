using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace boost
{
    class http
    {
        private object returnValue;
        private bool done;
        private WebResponse response;
        public http(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                response = request.GetResponse();
            }
            catch(WebException e)
            {
                done = true;
            }

            if (!done)
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                returnValue = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            else
            {
                returnValue = false;
            }
        }

        public object getResponce()
        {
            return returnValue;
        }
    }
}
