using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace MagicPotion.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			bundles.Add(new Bundle("~/bundles/IngredientsApp", new IBundleTransform[]
			{
				new BabelTransform() // jsx order is important!! make sure children load first
			}).Include(
				//Helpers
				"~/Scripts/React/Helpers/DataBindHelper.js",
				//Shared
				"~/Scripts/React/Shared/LabelValueDropdown.jsx",
				"~/Scripts/React/Shared/DropDown.jsx",
				"~/Scripts/React/Shared/LoadingModalView.jsx",
				//Validation
				"~/Scripts/React/ControlsWithValidation/RulesRunner.js",
				"~/Scripts/React/ControlsWithValidation/ErrorView.jsx",
				"~/Scripts/React/ControlsWithValidation/TextWithValidation.jsx",
				"~/Scripts/React/ControlsWithValidation/TextAreaWithValidation.jsx",
				"~/Scripts/React/ControlsWithValidation/DropDownWithValidation.jsx",
				"~/Scripts/React/ControlsWithValidation/LabelValueDropDownWithValidation.jsx",
				//Recipes
				"~/Scripts/React/Recipes/RecipesResultView.jsx",
				"~/Scripts/React/Recipes/RecipesMainView.jsx",
				//Ingredients
				"~/Scripts/React/Ingredients/IngredientEditModal.jsx",
				"~/Scripts/React/Ingredients/IngredientsResultView.jsx",
				"~/Scripts/React/Ingredients/IngredientsMainView.jsx",
				//Mixing
				"~/Scripts/React/IngredientMixing/IngredientMixListView.jsx",
				"~/Scripts/React/IngredientMixing/IngredientMixingTool.jsx",
				"~/Scripts/React/IngredientMixing/IngredientMixingMainView.jsx"
			));


			//CDN bundles
#if DEBUG
			bundles.Add(new ScriptBundle("~/bundles/react", "//unpkg.com/react@15/dist/react.js")
			{
				CdnFallbackExpression = "window.React"
			}.Include("~/Scripts/React/CdnFallback/react.js"));
#else
            bundles.Add(new ScriptBundle("~/bundles/react", "//unpkg.com/react@15/dist/react.min.js")
            {
                CdnFallbackExpression = "window.React"
            }.Include("~/Scripts/React/CdnFallback/react.min.js"));
#endif
			//If you load the minified react.min.js with the unminified react-dom.js then you get this error 
			//Cannot read property 'purgeUnmountedComponents' of undefined
#if DEBUG
			bundles.Add(new ScriptBundle("~/bundles/react-dom", "//unpkg.com/react-dom@15/dist/react-dom.js")
			{
				CdnFallbackExpression = "window.ReactDOM"
			}.Include("~/Scripts/React/CdnFallback/react-dom.js"));
#else
            bundles.Add(new ScriptBundle("~/bundles/react-dom", "//unpkg.com/react-dom@15/dist/react-dom.min.js")
            {
                CdnFallbackExpression = "window.ReactDOM"
            }.Include("~/Scripts/React/CdnFallback/react-dom.min.js"));
#endif
			bundles.Add(new ScriptBundle("~/bundles/react-bootstrap", "//cdnjs.cloudflare.com/ajax/libs/react-bootstrap/0.30.8/react-bootstrap.min.js")
			{
				CdnFallbackExpression = "window.ReactBootstrap"
			}.Include("~/Scripts/React/CdnFallback/react-bootstrap.min.js"));

#if !DEBUG
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;
#else
			BundleTable.EnableOptimizations = false;
#endif
		}
	}
}
