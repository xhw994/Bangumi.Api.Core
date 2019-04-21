﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Bangumi.Api.Core.Client
{
    public sealed class CallbackListner
    {
        public string DomainName { get; set; }
        public string Ipv4Address { get; set; }
        public int Port { get; set; } = 5994;
        public bool UseHttps { get; set; } = false;
        public string Route { get; set; }

        private readonly HttpListener _listener;

        public CallbackListner(int port)
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException($"The OS does not support {nameof(CallbackListner)}, please upgrade your system.");
            }

            _listener = new HttpListener();
        }

        public CallbackListner AddPrefixes()
        {
            foreach (string pf in ComposePrefixes())
            {
                _listener.Prefixes.Add(pf);
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

            Console.Write("completed!" + Environment.NewLine);
            return code;
        }

        private IEnumerable<string> ComposePrefixes()
        {
            string protocolPrefix = UseHttps ? "https" : "http";

            if (!string.IsNullOrEmpty(DomainName))
            {
                yield return $"{protocolPrefix}://{DomainName}:{Port}/{Route}";
            }
            if (!string.IsNullOrEmpty(Ipv4Address))
            {
                yield return $"{protocolPrefix}://{Ipv4Address}:{Port}/{Route}";
            }
            yield return $"{protocolPrefix}://localhost:{Port}/{Route}";
        }
    }
}
