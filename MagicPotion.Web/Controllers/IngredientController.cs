using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagicPotion.Business;

namespace MagicPotion.Web.Controllers
{
    public class IngredientController : Controller
    {
	    private readonly IIngredientManager _ingredientManager;

	    public IngredientController(IIngredientManager ingredientManager)
	    {
		    _ingredientManager = ingredientManager;
	    }

	    // GET: Ingredient
        public ActionResult Index()
        {
	        var ingredients = _ingredientManager.GetAllIngredients();


			return View(ingredients);
        }
    }
}