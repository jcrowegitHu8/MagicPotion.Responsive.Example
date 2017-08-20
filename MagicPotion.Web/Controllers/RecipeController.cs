using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MagicPotion.Business;
using MagicPotion.Objects;
using MagicPotion.Web.Models.View;

namespace MagicPotion.Web.Controllers
{
	public class RecipeController : Controller
	{
		private readonly IIngredientManager _ingredientManager;
		private readonly IRecipeManager _recipeManager;
		private readonly IMapper _mapper;

		public RecipeController(IRecipeManager recipeManager, IMapper mapper, IIngredientManager ingredientManager)
		{
			_recipeManager = recipeManager;
			_mapper = mapper;
			_ingredientManager = ingredientManager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public JsonResult Edit(int id)
		{
			var result = new RecipeEditViewModel();

			result.Recipe = _recipeManager.GetRecipe(id);
			result.Effects = _ingredientManager.GetAllEffects();
			result.Moods = _ingredientManager.GetAllMoods();
			result.Ingredients = _ingredientManager.GetAllIngredients();

			return Json(result, JsonRequestBehavior.AllowGet);

		}

		[HttpPost]
		public JsonResult Edit(Recipe model)
		{
			var result = _recipeManager.UpsertRecipe(model);
			return Json(result, JsonRequestBehavior.DenyGet);

		}

		[HttpGet]
		public JsonResult GetListviewInitData()
		{
			var dtoResult = _recipeManager.GetAllRecipes();
			var viewResult = _mapper.Map<List<RecipesViewModel>>(dtoResult);
			return Json(viewResult, JsonRequestBehavior.AllowGet);

		}
	}
}