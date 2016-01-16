using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using jtbc;

namespace WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[] 
        { 
            new Product { ID = 1, prodname = "Tomato Soup", category = "Groceries", price = 1 }, 
            new Product { ID = 2, prodname = "Yo-yo", category = "Toys", price = 3.75M }, 
            new Product { ID = 3, prodname = "Hammer", category = "Hardware", price = 16.99M } 
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
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
			users = (Dictionary<string, object>[])db.getDataAry("select * from users");

			var user = users.FirstOrDefault(u => u.ContainsValue(id));

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			if (user == null)
			{
				ret.Add("users", new Dictionary<string, object>[0]);
			}
			else {
				ret.Add("users", new Dictionary<string, object>[]{user});
			}
			return Ok(ret);
		}

		// POST api/<controller>
		public IHttpActionResult Post([FromBody]string value)
		{
			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			return Ok(ret);
		}

		// PUT api/<controller>/<id>
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/<id>
		public void Delete(int id)
		{
		}
	}
}
