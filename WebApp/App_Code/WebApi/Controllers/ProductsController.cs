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

	public class UsersController : ApiController
	{
		User[] users = new User[] 
        { 
            new User { ID = 1, username = "Tomato Soup", gender = "女", age = 1 }, 
        };

		public IEnumerable<User> GetAllUsers()
		{
			return users;
		}

		public IHttpActionResult GetUesr(int id)
		{
			var user = users.FirstOrDefault((u) => u.ID == id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}
	}
}
