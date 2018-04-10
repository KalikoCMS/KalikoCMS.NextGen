namespace KalikoCMS.Mvc.Extensions {
#if NETCORE
    using System.Text;
    using Microsoft.AspNetCore.Http;
#else
    using System.Text;
    using System.Web;
#endif

    public static class HttpResponseExtensions {
        public static void RenderMessage(this HttpResponse httpResponse, string header, string text, int statusCode) {
#if NETFULL
            httpResponse.Clear();
#endif
            httpResponse.StatusCode = statusCode;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<html><head><style>body{background:#008ED6;font-family:'Open Sans',sans-serif;color:#ffffff;text-align:center;}h1{font-weight:300;font-size:40px;margin-top:150px;}p{font-weight:400;font-size:16px;margin-top:30px;}div{max-width:600px;display:inline-block;text-align:left;}h1:before{background: #ffffff;border-radius: 0.5em;color: #008ED6;content: \"i\";display: block;font-family: serif;font-style: italic;font-weight: bold;height: 1em;line-height: 1em;margin-left: -1.4em;margin-top: 8px;position: absolute;text-align: center;width: 1em;}</style><link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300' rel='stylesheet' type='text/css'></head><body><div><h1>");
            stringBuilder.Append(header);
            stringBuilder.Append("</h1><p>");
            stringBuilder.Append(text);
            stringBuilder.Append("</p></div></body></html>");

#if NETCORE
            httpResponse.WriteAsync(stringBuilder.ToString());
#else
            httpResponse.Write(stringBuilder.ToString());

            try {
                httpResponse.End();
            }
            catch (System.Threading.ThreadAbortException) {
                // No problem
            }
#endif
        }
    }
}
