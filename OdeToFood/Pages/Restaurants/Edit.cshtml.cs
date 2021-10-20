using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
       

        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
                Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
            else
                Restaurant = new Restaurant();
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Restaurant.Id > 0)
            {
                Restaurant = restaurantData.Update(Restaurant);
                TempData["Message"] = "Successfully Updated!";
            }
            else
            {
                Restaurant = restaurantData.Add(Restaurant);
                TempData["Message"] = "Successfully Added!";
            }
                
            // need to do it again because HTTP request is stateless
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
