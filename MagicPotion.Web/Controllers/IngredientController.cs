using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagicPotion.Business;
using MagicPotion.Objects;
using Microsoft.VisualBasic.FileIO;

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

		[HttpGet]
		public ActionResult Listview()
		{
			return View();
		}

		[HttpGet]
		public JsonResult GetListviewInitData()
		{
			var ingredients = _ingredientManager.GetAllIngredients();
			return Json(ingredients, JsonRequestBehavior.AllowGet);

		}

		[HttpGet]
		public ActionResult UploadFile()
		{
			return View();
		}

		[HttpPost]
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