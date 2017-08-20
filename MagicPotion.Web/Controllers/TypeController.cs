using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagicPotion.Business;

namespace MagicPotion.Web.Controllers
{
    public class TypeController : Controller
    {
	    private readonly IIngredientManager _ingredientManager;

	    public TypeController(IIngredientManager ingredientManager)
	    {
		    _ingredientManager = ingredientManager;
	    }

		[HttpGet]
	    public JsonResult GetEffectsList()
	    {
		    var result = _ingredientManager.GetAllEffects();
			return Json(result,JsonRequestBehavior.AllowGet);
        }

	    [HttpGet]
	    public JsonResult GetMoodsList()
	    {
		    var result = _ingredientManager.GetAllMoods();
		    return Json(result, JsonRequestBehavior.AllowGet);
	    }
	}
}