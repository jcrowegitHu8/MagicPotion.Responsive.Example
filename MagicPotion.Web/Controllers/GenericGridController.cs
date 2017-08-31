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

	    private readonly IIngredientManager _ingredientManager;

	    public GenericGridController(IIngredientManager ingredientManager)
	    {
		    _ingredientManager = ingredientManager;
	    }

	    // GET: GenericGridExample
		public ActionResult Index()
        {
            return View();
        }

	    public ActionResult GetData(int dataId)
	    {
		    var ingredients = _ingredientManager.GetAllIngredients();
			var grid = new GenericReactGrid(new List<string> { "Name", "Address", "Date Issued" }, dataId);
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
    }
}