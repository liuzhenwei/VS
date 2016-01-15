
namespace WebApi.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string prodname { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }
    }

	public class User
	{
		public int ID { get; set; }
		public string username { get; set; }
		public decimal age { get; set; }
		public string gender { get; set; }
	}
}
