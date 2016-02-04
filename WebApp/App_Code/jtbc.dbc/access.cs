namespace jtbc.dbc
{
    using jtbc;
    using System;
	using System.Collections.Generic;
	using System.Data.OleDb;
    using System.Reflection;
    using System.Web;
	using System.Text.RegularExpressions;

    public class access : jtbc.dbc.dbc
    {
		private string sourceConnStr = "";
		private string connStr = "";
        private string eMessage = "";
        private int rState = 0;

        public virtual void Execute(string strSQL)
        {
			this.rState = 0;
			if ((string)strSQL != "")
			{
                try
                {
					OleDbConnection connection = new OleDbConnection(this.connStr);
                    connection.Open();
					new OleDbCommand(strSQL, connection).ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception exception)
                {
                    this.rState = 1;
					this.eMessage = exception.Message;
                }
			}
			else
			{
				this.rState = 999;
			}
        }

		public virtual int Executes(string strSQL)
        {
			this.rState = 0;
			int num = 0;
			if ((string)strSQL != "")
            {
                try
                {
					OleDbConnection connection = new OleDbConnection(this.connStr);
                    connection.Open();
					num = new OleDbCommand(strSQL, connection).ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception exception)
                {
                    num = -101;
                    this.eMessage = exception.Message;
                }
            }
			else
			{
				this.rState = 999;
			}
			return num;
        }

		public virtual int Insert(string strSQL)
		{
			this.rState = 0;
			int num = 0;
			if ((string)strSQL != "")
			{
				Regex getName = new Regex(@"into\s(.*?)\s\(", RegexOptions.IgnoreCase);
				Match matches = getName.Match(strSQL);
				string name = matches.Groups[1].Value;

				OleDbConnection connection = new OleDbConnection(this.connStr);
				connection.Open();
				OleDbCommand cmd = new OleDbCommand();
				OleDbTransaction trans = connection.BeginTransaction();
				cmd.Connection = connection;
				cmd.Transaction = trans;
				try
				{
					cmd.CommandText = strSQL;
					cmd.ExecuteNonQuery();
					cmd.CommandText = "SELECT @@identity FROM " + name;
					trans.Commit();
					num = Convert.ToInt32(cmd.ExecuteScalar());
				}
				catch (Exception exception)
				{
					trans.Rollback();
					this.rState = 2;
					this.eMessage = exception.Message;
				}
				finally
				{
					connection.Close();
				}
			}
			else
			{
				this.rState = 999;
			}
			return num;
		}

		public virtual Dictionary<string, object>[] getDataAry(string strSQL)
        {
            this.rState = 0;
			Dictionary<string, object>[] rsArray = null;
			if ((string)strSQL != "")
            {
				try
				{
					OleDbConnection connection = new OleDbConnection(this.connStr);
					connection.Open();
					OleDbDataReader reader = new OleDbCommand(strSQL, connection).ExecuteReader();
					Dictionary<int, object> rsList = new Dictionary<int, object>();
					int fieldCount = reader.FieldCount;
					int num = 0;
					while (reader.Read())
					{
						Dictionary<string, object> rec = new Dictionary<string, object>();
						for (int i = 0; i < fieldCount; i++)
						{
							rec.Add(reader.GetName(i), reader.GetValue(i));
						}
						rsList.Add(num, rec);
						num ++;
					}
					reader.Close();
					connection.Close();

					rsArray = new Dictionary<string, object>[num];
					foreach (var kv in rsList)
					{
						rsArray[kv.Key] = (Dictionary<string, object>)kv.Value;
					}
				}
				catch (Exception exception)
				{
					this.rState = 1;
					var errMsg = new Dictionary<string, object>();
					errMsg.Add("msg", "Operate DB Error: " + exception.Message);
					rsArray = new Dictionary<string, object>[] { errMsg };
					this.eMessage = exception.Message;
				}
			}
			else
			{
				this.rState = 999;
			}
			return rsArray;
        }

        public virtual string getFieldList(string argString)
        {
            string cmdText = argString;
            string argObject = "";
            try
            {
                OleDbConnection connection = new OleDbConnection(this.connStr);
                connection.Open();
                OleDbDataReader reader = new OleDbCommand(cmdText, connection).ExecuteReader();
                int fieldCount = reader.FieldCount;
                for (int i = 0; i < fieldCount; i++)
                {
                    argObject = argObject + reader.GetName(i) + "|";
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exception)
            {
                argObject = "";
                this.eMessage = exception.Message;
            }
            return argObject;
        }

		public virtual string getEMessage()
		{
			return this.eMessage;
		}

        public virtual int getRState()
        {
            return this.rState;
        }

        public virtual void setConnStr(string connStr)
        {
			this.sourceConnStr = connStr;
			string ds = getParameter(connStr, "Data Source");
			string dbPath = HttpContext.Current.Server.MapPath("/" + ds);
			this.connStr = connStr.Replace(ds, dbPath);
        }

        public static string getParameter(string argString, string argKey, string argSpstr = ";")
        {
            Regex regex = new Regex("((?:^|" + argSpstr + ")" + argKey + "=(.[^" + argSpstr + "]*))");
            return regex.Match(argString).Groups[2].Value;
        }
    }
}

