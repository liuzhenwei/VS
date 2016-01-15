using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApp
{
	public partial class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			HttpContext.Current.Application.Lock();
			HttpContext.Current.Application["test"] = "App";
			HttpContext.Current.Application.UnLock();

			RouteTable.Routes.MapHttpRoute(
			   name: "DefaultApi",
			   routeTemplate: "api/{controller}/{id}",
			   defaults: new
			   {
				   id = RouteParameter.Optional
			   }
			);

			GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());
		}
	}

	public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
	{
		private string callbackQueryParameter;

		public JsonpMediaTypeFormatter()
		{
			SupportedMediaTypes.Add(DefaultMediaType);
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

			MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", DefaultMediaType));
		}

		public string CallbackQueryParameter
		{
			get { return callbackQueryParameter ?? "callback"; }
			set { callbackQueryParameter = value; }
		}

		public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
		{
			string callback;

			if (IsJsonpRequest(out callback))
			{
				return Task.Factory.StartNew(() =>
				{
					var writer = new StreamWriter(stream);
					writer.Write(callback + "(");
					writer.Flush();

					base.WriteToStreamAsync(type, value, stream, content, transportContext).Wait();

					writer.Write(")");
					writer.Flush();
				});
			}
			else
			{
				return base.WriteToStreamAsync(type, value, stream, content, transportContext);
			}
		}


		private bool IsJsonpRequest(out string callback)
		{
			callback = null;

			if (HttpContext.Current.Request.HttpMethod != "GET")
				return false;

			callback = HttpContext.Current.Request.QueryString[CallbackQueryParameter];

			return !string.IsNullOrEmpty(callback);
		}
	}
}
