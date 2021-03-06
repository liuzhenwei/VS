namespace jtbc
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Web;

    public static class com
    {

        public static int dataDelete(db argDb, string argDatabase, string argIdfield, string argId)
        {
            return dataDelete(argDb, argDatabase, argIdfield, argId, "");
        }

        public static int dataDelete(db argDb, string argDatabase, string argIdfield, string argId, string argOsql)
        {
            db db = argDb;
            string str = cls.getString(argDatabase);
            string str2 = cls.getString(argIdfield);
            string argString = cls.getString(argId);
            string argObject = cls.getString(argOsql);
            if (cls.cidary(argString))
            {
                string str5 = "delete from " + str + " where " + str2 + " in (" + argString + ")";
                if (!cls.isEmpty(argObject))
                {
                    str5 = str5 + argObject;
                }
                return db.Executes(str5);
            }
            return -101;
        }

        public static int dataSwitch(db argDb, string argDatabase, string argField, string argIdfield, string argId)
        {
            return dataSwitch(argDb, argDatabase, argField, argIdfield, argId, "");
        }

        public static int dataSwitch(db argDb, string argDatabase, string argField, string argIdfield, string argId, string argOsql)
        {
            db db = argDb;
            string str = cls.getString(argDatabase);
            string str2 = cls.getString(argField);
            string str3 = cls.getString(argIdfield);
            string argString = cls.getString(argId);
            string argObject = cls.getString(argOsql);
            if (cls.cidary(argString))
            {
                string str6 = "update " + str + " set " + str2 + "=abs(" + str2 + "-1) where " + str3 + " in (" + argString + ")";
                if (!cls.isEmpty(argObject))
                {
                    str6 = str6 + argObject;
                }
                return db.Executes(str6);
            }
            return -101;
        }

		public static object getAryValue(object[,] argAry, string argString)
		{
			object[,] objArray = argAry;
			string str = argString;
			for (int i = 0; i < objArray.GetLength(0); i++)
			{
				if (((string)objArray[i, 0]) == str)
				{
					return objArray[i, 1];
				}
			}
			return null;
		}

		public static string itake(string str, string stype)
		{
			return str;
		}

		public static string getNroute()
		{
			string str = "";
			string argString = HttpContext.Current.Request.ServerVariables["URL"];
			string argKey = encode.base64.encodeBase64(cls.getLRStr(argString, "/", "leftr"));
			string str4 = cls.getLRStr(cls.getLRStr(argString, "/", "leftr"), "/", "right");
			if (str4 == "")
			{
				str4 = ":root";
			}
			argKey = argKey + encode.base64.encodeBase64(str4);
			str = getNroute(argKey);
			if (str == "")
			{
				if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("web.config")))
				{
					str = "root";
				}
				else if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("../web.config")))
				{
					str = "node";
				}
				else if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("../../web.config")))
				{
					str = "child";
				}
				else if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("../../../web.config")))
				{
					str = "grandchild";
				}
				else if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("../../../../web.config")))
				{
					str = "greatgrandchild";
				}
				string[,] strArray = new string[,] { { argKey, str } };
				application.set("route", cls.mergeAry((string[,])application.get("route"), strArray));
			}
			return str;
		}

		public static string getNroute(string argKey)
		{
			string[,] strArray = (string[,])application.get("route");
			if (strArray != null)
			{
				string str2 = cls.getString(argKey);
				for (int i = 0; i < strArray.GetLength(0); i++)
				{
					if (strArray[i, 0] == str2)
					{
						return strArray[i, 1];
					}
				}
			}
			return "";
		}
	}
}

