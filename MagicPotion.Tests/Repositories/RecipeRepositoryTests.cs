using System;
using System.Linq;
using AutoMapper;
using MagicPotion.Objects;
using MagicPotion.Repository;
using MagicPotion.Web.Models.Post;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicPotion.Tests.Repositories
{
	[TestClass]
	public class Describe_RecipeRepositoryTests
	{
		[TestMethod]
		public void Get_recipes_should_work()
		{
			//Arrange
			//For each Profile, include that profile in the MapperConfiguration
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperRepositoryProfile());
			});
			//Create a mapper that will be used by the DI container
			var mapper = config.CreateMapper();
			var repo = new RecipeRepository(mapper);
			
			
			//Act
			var recipes = repo.GetAllRecipes().ToList();


			//Assert
			Assert.IsNotNull(recipes);
			Assert.IsNotNull(recipes.FirstOrDefault().Ingredients);
			Assert.IsNotNull(recipes.FirstOrDefault().Ingredients.FirstOrDefault());
		}
	}
}
