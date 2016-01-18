namespace jtbc
{
    using jtbc.dbc;
    using System;

    public class db
    {
        private jtbc.dbc.dbc tDbc = null;

        public db(int dbType = 0, string connStr = "")
        {
			switch (dbType)
            {
                case 0:
                    this.tDbc = new access();
                    break;
                case 1:
                    // this.tDbc = new mssql();
                    break;
            }
			if (connStr != "")
			{
				setConnStr(connStr);
			}  
        }

		public void setConnStr(string connStr)
		{
			this.tDbc.setConnStr(connStr);
		}

        public void Execute(string argString)
        {
            this.tDbc.Execute(argString);
        }

        public int Executes(string argString)
        {
            return this.tDbc.Executes(argString);
        }

		public int Insert(string argString)
		{
			return this.tDbc.Insert(argString);
		}

        public object[] getDataAry(string argString)
        {
            return this.tDbc.getDataAry(argString);
        }

        public string getEMessage()
        {
            return this.tDbc.getEMessage();
        }

        public string getFieldList(string argString)
        {
            return this.tDbc.getFieldList(argString);
        }

        public int getRState()
        {
            return this.tDbc.getRState();
        }
    }
}

