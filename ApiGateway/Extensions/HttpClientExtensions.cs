using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApps.ApiGateway.Extensions
{
    public static class HttpClientExtensions
    {
        public static readonly string AcceptEncode = "Accept-Encoding";

        public static void SetAcceptEncodingHeader(this HttpClient client, IHeaderDictionary headers)
        {
            if (headers.ContainsKey(AcceptEncode))
            {
                var enc = headers[AcceptEncode];

                if (!string.IsNullOrEmpty(enc))
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(enc));
                }
            }
        }
    }
}
