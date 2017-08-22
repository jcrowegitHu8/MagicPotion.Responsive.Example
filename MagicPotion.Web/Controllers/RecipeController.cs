using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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

		[System.Web.Mvc.HttpGet]
		public JsonResult Edit(int? id)
		{
			var result = new RecipeEditViewModel();

			if (id.HasValue)
			{
				result.Recipe = _recipeManager.GetRecipe(id.Value);
			}
			else
			{
				result.Recipe = new Recipe();
			}
			result.Effects = _ingredientManager.GetAllEffects();
			result.Moods = _ingredientManager.GetAllMoods();
			result.Ingredients = _ingredientManager.GetAllIngredients();

			return Json(result, JsonRequestBehavior.AllowGet);

		}

		[System.Web.Mvc.HttpPost]
		public JsonResult Delete(int id)
		{
			var result = _recipeManager.DeleteRecipe(id);
			return Json(result, JsonRequestBehavior.DenyGet);
		}

		[System.Web.Mvc.HttpPost]
		public JsonResult Save([FromBody]Recipe model)
		{
			var result = _recipeManager.UpsertRecipe(model);
			return Json(result, JsonRequestBehavior.DenyGet);

		}

		[System.Web.Mvc.HttpGet]
		public JsonResult GetListviewInitData()
		{
			var dtoResult = _recipeManager.GetAllRecipes();
			var viewResult = _mapper.Map<List<RecipesViewModel>>(dtoResult);
			return Json(viewResult, JsonRequestBehavior.AllowGet);

		}
	}
}