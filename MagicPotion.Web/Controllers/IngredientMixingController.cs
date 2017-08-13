using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MagicPotion.Business;
using MagicPotion.Objects;
using MagicPotion.Web.Models.View;
using MagicPotion.Web.Models.Post;

namespace MagicPotion.Web.Controllers
{
    public class IngredientMixingController : Controller
    {
	    private readonly IIngredientManager _ingredientManager;
	    private readonly IIngredientMixingManager _mixingManager;

	    public IngredientMixingController(IIngredientManager ingredientManager, IIngredientMixingManager mixingManager)
	    {
		    _ingredientManager = ingredientManager;
		    _mixingManager = mixingManager;
	    }

	    [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            return View();
        }

	    [System.Web.Mvc.HttpGet]
		public JsonResult GetInitData()
	    {
		    var viewModel = new IngredientMixingViewModel();
		    viewModel.Ingredients = _ingredientManager.GetAllIngredients();
		    viewModel.Effects = _ingredientManager.GetAllEffects();
		    viewModel.Moods = _ingredientManager.GetAllMoods();
		    viewModel.IngredientMixes = _ingredientManager.GetAllMixes();

		    return Json(viewModel, JsonRequestBehavior.AllowGet);
	    }

	    [System.Web.Mvc.HttpPost]
	    public JsonResult Mix([FromBody] IngredientMixingPostModel model)
	    {
		    var result = _mixingManager.Mix(model.MoodId, model.IngredientId1, model.IngredientId2);
		    return Json(result, JsonRequestBehavior.DenyGet);
	    }
    }
}