using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetRestaurantById(int id);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Luna's Restaurant", Location = "Maryland 1", Cuisine = CuisineType.Italian },
                new Restaurant {Id = 2, Name = "Andy's Restaurant", Location = "Maryland 2", Cuisine = CuisineType.Mexican },
                new Restaurant {Id = 3, Name = "Jane's Restaurant", Location = "Maryland 3", Cuisine = CuisineType.Indian }
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        IEnumerable<Restaurant> IRestaurantData.GetRestaurantsByName(string name)
        {
            return from r in restaurants
                   // return all the restaurant data if search string is null
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }
}
