using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using MagicPotion.Business;
using MagicPotion.Objects;
using MagicPotion.Web.Models.Post;
using MagicPotion.Web.Models.View;
using Microsoft.VisualBasic.FileIO;

namespace MagicPotion.Web.Controllers
{
	public class IngredientController : Controller
	{
		private readonly IIngredientManager _ingredientManager;
		private readonly IMapper _mapper;

		public IngredientController(IIngredientManager ingredientManager, IMapper mapper)
		{
			_ingredientManager = ingredientManager;
			_mapper = mapper;
		}

		// GET: Ingredient
		public ActionResult Index()
		{
			var ingredients = _ingredientManager.GetAllIngredients();


			return View(ingredients);
		}

		[System.Web.Mvc.HttpGet]
		public ActionResult Listview()
		{
			return View();
		}

		[System.Web.Mvc.HttpGet]
		public JsonResult GetListviewInitData()
		{
			var ingredients = _ingredientManager.GetAllIngredients();
			return Json(ingredients, JsonRequestBehavior.AllowGet);

		}

		[System.Web.Mvc.HttpGet]
		public JsonResult Edit(int id)
		{
			var ingredient = _ingredientManager.GetIngredient(id);
			return Json(ingredient, JsonRequestBehavior.AllowGet);

		}

		[System.Web.Mvc.HttpPost]
		public JsonResult Edit([FromBody] IngredientPostModel model)
		{
			var busModel = _mapper.Map<Ingredient>(model);
			var upsertedModel = _ingredientManager.UpsertIngredient(busModel);
			return Json(upsertedModel, JsonRequestBehavior.DenyGet);
		}

		[System.Web.Mvc.HttpGet]
		public JsonResult GetEffectsList()
		{
			var effects = _ingredientManager.GetAllEffects();
			return Json(effects, JsonRequestBehavior.AllowGet);

		}



		[System.Web.Mvc.HttpGet]
		public ActionResult UploadFile()
		{
			return View();
		}

		[System.Web.Mvc.HttpPost]
		public ActionResult UploadFile(HttpPostedFileBase file)
		{
			string extension = Path.GetExtension(file.FileName);
			if (".csv".Equals(extension, StringComparison.InvariantCultureIgnoreCase))
			{
				var result = _ingredientManager.ParseCsvAndUpdateIngredients(file.InputStream);

				if (result.Success)
				{
					ViewBag.Message = "File Uploaded Successfully! ";
				}
				else
				{
					ViewBag.Message = "File parsing had issues. ";
					ViewBag.Message += string.Join("\n", result.Errors);
				}
			}
			else
			{
				ViewBag.Message = "File Uploaded Failed!  We currently only support CSV format.";
			}

			return View();

		}






	}
}