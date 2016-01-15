namespace jtbc
{
    using System;
    using System.Web;
	using System.Configuration;

    public static class config
    {
        public static string adminFolder = ConfigurationManager.AppSettings["adminFolder"];
        public static string ajaxPreContent = "<!--jtbc-->";
        public static string appName = ConfigurationManager.AppSettings["appName"];
        public static string charset = ConfigurationManager.AppSettings["charset"];
        public static string connStr = ConfigurationManager.AppSettings["connStr"];
        public static string dbtype = ConfigurationManager.AppSettings["dbtype"];
        public static string default_language = ConfigurationManager.AppSettings["default_language"];
        public static string default_template = ConfigurationManager.AppSettings["default_template"];
        public static string default_theme = ConfigurationManager.AppSettings["default_theme"];
        public static string imagesRoute = ConfigurationManager.AppSettings["imagesRoute"];
        public static string isApp = ConfigurationManager.AppSettings["isApp"];
        public static string jtbccinfo = "<!--jtbccinfo-->";
        public static string linkmode = ConfigurationManager.AppSettings["linkmode"];
        public static string navSpStr = ConfigurationManager.AppSettings["navSpStr"];
        public static string ntitleSpStr = ConfigurationManager.AppSettings["ntitleSpStr"];
        public static string nurlpr = ConfigurationManager.AppSettings["nurlpr"];
        public static string repath = ConfigurationManager.AppSettings["repath"];
        public static string sysName = "jtbc";
        public static string xmlsfx = ConfigurationManager.AppSettings["xmlsfx"];

		public static void writeOff()
		{
			session.remove("rsAry");
			session.remove("rsbAry");
			session.remove("rscAry");
			session.remove("rstAry");
			session.remove("njtbcelement");
			session.remove("ntitle");
		}

		public static string nurlpre
		{
			get
			{
				string str = nurlpr + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
				string str2 = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
				if (str2 != "80")
				{
					str = str + ":" + str2;
				}
				return str;
			}
		}

		public static string nurs
		{
			get
			{
				return HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
			}
		}

		public static string nuri
		{
			get
			{
				return HttpContext.Current.Request.ServerVariables["URL"];
			}
		}

		public static string nurl
		{
			get
			{
				string str = HttpContext.Current.Request.ServerVariables["URL"];
				string argString = HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
				if (cls.getString(argString) != "")
				{
					str = str + "?" + argString;
				}
				return str;
			}
		}

    }
}

