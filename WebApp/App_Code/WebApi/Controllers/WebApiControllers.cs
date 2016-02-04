using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Text.RegularExpressions;
using jtbc;

namespace WebApi.Controllers
{
	
	public class ProductsController : ApiController
    {
		public Dictionary<string, object>[] products;

		// GET api/<controller>
		public IHttpActionResult GetAllProducts()
		{
			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			products = (Dictionary<string, object>[])db.getDataAry("select * from products");

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			ret.Add("products", products);
			return Ok(ret);
		}

		// GET api/<controller>/<id>
		public IHttpActionResult GetProduct(int id)
		{
			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			products = (Dictionary<string, object>[])db.getDataAry("select * from products where ID=" + id.ToString());

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			ret.Add("products", products);
			return Ok(ret);
		}

		// POST api/<controller>
		public IHttpActionResult Post([FromBody]Dictionary<string, object> value)
		{
			string sql = "INSERT INTO products ";
			string tbn = "";
			string val = "";
			foreach (var kv in value)
			{
				tbn += kv.Key + ",";
				Regex isNumeric = new Regex(@"^[\d\.]+$");
				if (isNumeric.IsMatch((string)kv.Value))
				{
					val += kv.Value + ",";
				}
				else
				{
					val += "'" + kv.Value + "',";
				}
			}
			sql += "(" + tbn.Substring(0, tbn.Length - 1) + ") VALUES (" + val.Substring(0, val.Length - 1) + ")";

			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			int num = db.Insert(sql);

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", num != 0 ? 0 : db.getRState());
			ret.Add("insertId", num);
			return Ok(ret);
		}

		// PUT api/<controller>/<id>
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/<id>
		public IHttpActionResult Delete(int id)
		{
			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			string sql = "DELETE FROM products WHERE ID=" + id.ToString();
			db.Execute(sql);

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			return Ok(ret);
		}
    }

	public class UsersController : ApiController
	{
		public Dictionary<string, object>[] users;

		// GET api/<controller>
		public IHttpActionResult GetAllUsers()
		{
			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			users = (Dictionary<string, object>[])db.getDataAry("select * from users");

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			ret.Add("users", users);
			return Ok(ret);
		}

		// GET api/<controller>/<id>
		public IHttpActionResult GetUesr(int id)
		{
			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			users = (Dictionary<string, object>[])db.getDataAry("select * from users where ID=" + id.ToString());

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			ret.Add("users", users);
			return Ok(ret);
		}

		// POST api/<controller>
		public IHttpActionResult Post([FromBody]Dictionary<string, object> value)
		{
			string sql = "INSERT INTO users ";
			string tbn = "";
			string val = "";
			foreach (var kv in value)
			{
				tbn += kv.Key + ",";
				Regex isNumeric = new Regex(@"^[\d\.]+$");
				if (isNumeric.IsMatch((string)kv.Value))
				{
					val += kv.Value + ",";
				}
				else
				{
					val += "'" + kv.Value + "',";
				}
			}
			sql += "(" + tbn.Substring(0, tbn.Length - 1) + ") VALUES (" + val.Substring(0, val.Length - 1) + ")";

			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			int num = db.Insert(sql);

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", num != 0 ? 0 : db.getRState());
			ret.Add("insertId", num);
			return Ok(ret);
		}

		// PUT api/<controller>/<id>
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/<id>
		public IHttpActionResult Delete(int id)
		{
			jtbc.db db = new jtbc.db(0, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=App_Data/test.mdb;");
			string sql = "DELETE FROM users WHERE ID=" + id.ToString();
			db.Execute(sql);

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			return Ok(ret);
		}
	}
}
