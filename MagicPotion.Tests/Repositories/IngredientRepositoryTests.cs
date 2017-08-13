using System;
using System.Linq;
using MagicPotion.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicPotion.Tests
{
	[TestClass]
	public class Describe_IngredientRepository
	{
		[TestInitialize]
		public void TestInitialize()
		{
			//todo set DataDirectory to use shared mdf from web folder here.
			//AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases"));
		}

		[TestCategory("Integration")]//Don't run integration tests on gated checkin. ONly for local development
		[TestMethod]
		public void GetAllIngredient_should_return_data()
		{
			var repo = new IngredientRepository();
			var ingredients = repo.GetAllIngredients();
			Assert.IsNotNull(ingredients);
			Assert.IsTrue(ingredients.Any());
		}

		[TestCategory("Integration")]//Don't run integration tests on gated checkin. ONly for local development
		[TestMethod]
		public void GetAllIngredientMex_should_return_data()
		{
			var repo = new IngredientRepository();
			var mixes = repo.GetAllMixes();
			Assert.IsNotNull(mixes);
			Assert.IsTrue(mixes.Any());
		}
	}
}
