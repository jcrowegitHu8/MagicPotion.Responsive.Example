using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MagicPotion.Business;
using MagicPotion.Web.Models;

namespace MagicPotion.Web.Controllers
{
    public class GenericGridController : Controller
    {
	    private readonly IRecipeManager _recipeManager;
	    private readonly IIngredientManager _ingredientManager;

	    public GenericGridController(IIngredientManager ingredientManager, IRecipeManager recipeManager)
	    {
		    _ingredientManager = ingredientManager;
		    _recipeManager = recipeManager;
	    }

	    // GET: GenericGridExample
		public ActionResult Index()
        {
            return View();
        }

	    public ActionResult GetData(int? dataId)
	    {
		    var ingredients = _ingredientManager.GetAllIngredients();
			var grid = new GenericReactGrid(new List<string> { "Name", "Description", "Color" }, dataId??0);
		    foreach (var rowData in ingredients)
		    {
			    grid.GridRow.Add(new Dictionary<string, object>
			    {
				    {"col1", rowData.Name},
				    {"col2", rowData.Description},
				    {"col3", rowData.Color},
				    {"id", rowData.Id },
				    //{"updateUrl", Url.Action()},
				    //{"deleteUrl", Url.Action("Delete", "") },
				    //{"deleteParams", new Dictionary<string, object>{{"type", "" }}}
			    });

		    }

		    return Json(new GenericReactTableResult(grid), JsonRequestBehavior.AllowGet);
		}

	    public ActionResult GetDataWithSubGrid(int? dataId)
	    {
		    var items = _recipeManager.GetAllRecipes();

			var grid = new GenericReactGrid(new List<string> { "Id", "Name", "" }, dataId??0);
		    foreach (var rowData in items)
		    {
			    var subGrid = new GenericReactGrid(dataId??0);

			    subGrid.GridHeader.Add(new GenericReactTableHeader { Header = "Ingrident Name" });

			    foreach (var subDetail in rowData.Ingredients)
			    {
				    subGrid.GridRow.Add(new Dictionary<string, object>
				    {
					    {"col1", subDetail.Name},
				    });
			    }
			    grid.GridRow.Add(new Dictionary<string, object>
			    {
				    {"col1", rowData.Id},
				    {"col2", rowData.Name},
				    {"id", rowData.Id },
			    });

		    }

		    return Json(new GenericReactTableResult(grid), JsonRequestBehavior.AllowGet);
		}
    }
}