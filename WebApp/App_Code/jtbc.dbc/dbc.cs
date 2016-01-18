namespace jtbc.dbc
{
    using System;
	using System.Collections.Generic;

    public interface dbc
    {
        void Execute(string argString);
        int Executes(string argString);
		int Insert(string argString);
		Dictionary<string, object>[] getDataAry(string argString);
		string getFieldList(string argString);
		string getEMessage();
        int getRState();
        void setConnStr(string argString);
    }
}

