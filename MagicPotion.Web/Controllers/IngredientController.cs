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
		    try
		    {
			    string extension = Path.GetExtension(file.FileName);
				if (".csv".Equals(extension,StringComparison.InvariantCultureIgnoreCase))
				{ 
			    Parse(file.InputStream);
				}
				else
				{
					ViewBag.Message = "File Uploaded Failed!  We currently only support CSV format.";
					return View();
				}


				ViewBag.Message = "File Uploaded Successfully!!";
			    return View();
		    }
		    catch(Exception ex)
		    {
			    ViewBag.Message = "File upload failed!!";
			    return View();
		    }
	    }

	    private void Parse(Stream fileStream)
	    {
		    var importResult = string.Empty;
		    var ingredients = _ingredientManager.GetAllIngredients().ToList();
			var effects = _ingredientManager.GetAllEffects().ToList();

			using (TextFieldParser parser = new TextFieldParser(fileStream))
		    {
			    parser.TextFieldType = FieldType.Delimited;
			    parser.SetDelimiters(",");
			    while (!parser.EndOfData)
			    {
				    //Process row
				    string[] fields = parser.ReadFields();
				    var importId = fields[0];

					//Attempt to match on ImportId or name.

				    var ingredient = ingredients.FirstOrDefault(o => o.ImportId == importId
					|| o.Name.Equals(fields[0].Trim(),StringComparison.InvariantCultureIgnoreCase));
				    if (ingredient == null)
				    {
					    //No match found create a new one.
					    ingredient = new Ingredient
					    {
						    Name = fields[1],
						    Color = fields[2],
						    Description = fields[3],
					    };

				    }
				    else
				    {
						//Update existing
					    ingredient.Name = fields[1];
					    ingredient.Color = fields[2];
					    ingredient.Description = fields[3];

				    }
				    TryGetEffectType(fields[4], effects, ingredient);

					_ingredientManager.UpsertIngredient(ingredient);

			    }
		    }
	    }

	    private void TryGetEffectType(string effectNameToMatch, List<Effect> effects, Ingredient ingredient )
	    {
		    var effect =
			    effects.FirstOrDefault(o => o.Name.Equals(effectNameToMatch, StringComparison.InvariantCultureIgnoreCase));
		    if (effect != null)
		    {
			    ingredient.EffectType = effect.Id;
		    }
	    }
		

	}
}