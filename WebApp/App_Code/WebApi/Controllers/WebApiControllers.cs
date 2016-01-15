using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

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

	public class Ret : Dictionary<string, object>{
		
	}

	public class UsersController : ApiController
	{
		public Dictionary<string, object>[] users;

		public IHttpActionResult GetAllUsers()
		{
			Dictionary<string, object> user = new Dictionary<string, object>();
			user.Add("ID", 1);
			user.Add("username", "Tomato");
			user.Add("age", 5);
			user.Add("gender", "女");

			users = new Dictionary<string, object>[1];
			users[0] = user;

			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			ret.Add("users", users);
			return Ok(ret);
		}

		public IHttpActionResult GetUesr(int id)
		{
			users = new Dictionary<string, object>[2];

			Dictionary<string, object> user = new Dictionary<string, object>();
			user.Add("ID", 1);
			user.Add("username", "Tomato");
			user.Add("age", 5);
			user.Add("gender", "女");
			users[0] = user;

			user = new Dictionary<string, object>();
			user.Add("ID", 2);
			user.Add("username", "Jerry");
			user.Add("age", 25);
			user.Add("gender", "男");
			users[1] = user;

			var _user = users.FirstOrDefault(u => u.ContainsValue(id));
			Dictionary<string, object> ret = new Dictionary<string, object>();
			ret.Add("errorCode", 0);
			if (_user == null)
			{
				ret.Add("users", new Dictionary<string, object>[0]);
			}
			else {
				ret.Add("users", new Dictionary<string, object>[]{_user});
			}
			return Ok(ret);
		}
	}
}
