using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MagicPotion.Business;
using MagicPotion.Web.Models.View;

namespace MagicPotion.Web.Controllers
{
    public class RecipeController : Controller
    {
	    private readonly IRecipeManager _recipeManager;
	    private readonly IMapper _mapper; 

	    public RecipeController(IRecipeManager recipeManager, IMapper mapper)
	    {
		    _recipeManager = recipeManager;
		    _mapper = mapper;
	    }

	    public ActionResult Index()
	    {
		    return View();
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