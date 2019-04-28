using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using static Bangumi.Api.Core.Extension.StringExtension;

namespace Bangumi.Api.Core.Client
{
    public sealed class CallbackListner
    {
        private readonly HttpListener _listener;

        public CallbackListner(params string[] urls)
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException($"The OS does not support {nameof(CallbackListner)}, please upgrade your system.");
            }

            _listener = new HttpListener();

            foreach (string url in urls)
            {
                if (IsHttpOrHttpsUrl(url))
                {
                    _listener.Prefixes.Add(url);
                }
            }
        }

        public CallbackListner AddPrefix(string url)
        {
            if (IsHttpOrHttpsUrl(url))
            {
                _listener.Prefixes.Add(url);
            }
            return this;
        }

        public string GetCode()
        {
            _listener.Start();
            Console.Write("Please authenticate yourself in the browser tab... ");

            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = _listener.GetContext();
            HttpListenerRequest request = context.Request;
            string requestUrl = request.Url.ToString().ToLower();

            // Get code from URL
            string code = "";
            if (requestUrl.Contains("code=")) // received request from bangumi.
            {
                code = requestUrl.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries).Last();
            }

            // Construct a response.
            string responseString = "<HTML><BODY> Authentication completed! Please close the browser tab.</BODY></HTML>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            // Write response from an output stream.
            HttpListenerResponse response = context.Response;
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // Close the output stream.
            output.Close();

            Console.Write("completed! The authentication code is: " + code + Environment.NewLine);
            return code;
        }
    }
}
