namespace OdeToFood.Data
{
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

        Restaurant IRestaurantData.Add(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;           
            restaurants.Add(restaurant);
            return restaurant;
        }

        // fake commit
        int IRestaurantData.Commit()
        {
            return 0;
        }

        IEnumerable<Restaurant> IRestaurantData.GetRestaurantsByName(string name)
        {
            return from r in restaurants
                   // return all the restaurant data if search string is null
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        Restaurant IRestaurantData.Update(Restaurant updatedRestaurant)
        {
            Restaurant res = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (res != null)
            {
                res.Name = updatedRestaurant.Name;
                res.Location = updatedRestaurant.Location;
                res.Cuisine = updatedRestaurant.Cuisine;
            }
            return res;
        }

        Restaurant IRestaurantData.Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null) restaurants.Remove(restaurant);
            return restaurant;
        }
    }
}
