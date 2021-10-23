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
        Restaurant Add(Restaurant restaurant);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Delete(int id);
        int GetCount();
        int Commit();
    }
}
