using System;
using System.Collections.Generic;
using FakeItEasy;
using MagicPotion.Business;
using MagicPotion.Objects;
using MagicPotion.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicPotion.Tests.Business
{
	[TestClass]
	public class Describe_IngredientMixingManager
	{
		[TestMethod]
		public void When_ingredient_1_is_fatal()
		{
			//Arrange
			var mockIngredientRepo = A.Fake<IIngredientRepository>();
			A.CallTo(() => mockIngredientRepo.GetAllMixes()).Returns(
				new List<IngredientMix>()
				{
					new IngredientMix
					{
						Ingredient1 = (int)MockIngredients.A,
						IsFatal = true
					}
				});

			var mixer = new IngredientMixingManager(mockIngredientRepo);

			//Act
			var mixResult = mixer.Mix(0, (int) MockIngredients.A, 0);

			//Assert
			Assert.IsNotNull(mixResult);
			Assert.IsTrue(mixResult.IsMixFatal);
			Assert.IsTrue(mixResult.IsMixDocumented);

		}

		[TestMethod]
		public void When_ingredient_2_is_fatal()
		{
			//Arrange
			var mockIngredientRepo = A.Fake<IIngredientRepository>();
			A.CallTo(() => mockIngredientRepo.GetAllMixes()).Returns(
				new List<IngredientMix>()
				{
					new IngredientMix
					{
						Ingredient2 = (int)MockIngredients.A,
						IsFatal = true
					}
				});

			var mixer = new IngredientMixingManager(mockIngredientRepo);

			//Act
			var mixResult = mixer.Mix(0, (int)MockIngredients.A, 0);

			//Assert
			Assert.IsNotNull(mixResult);
			Assert.IsTrue(mixResult.IsMixFatal);
			Assert.IsTrue(mixResult.IsMixDocumented);

		}

		[TestMethod]
		public void When_an_ingredient_is_not_in_a_mix()
		{
			//Arrange
			var mockIngredientRepo = A.Fake<IIngredientRepository>();
			A.CallTo(() => mockIngredientRepo.GetAllMixes()).Returns(
				new List<IngredientMix>());

			var mixer = new IngredientMixingManager(mockIngredientRepo);

			//Act
			var mixResult = mixer.Mix(0, (int)MockIngredients.A, 0);

			//Assert
			Assert.IsNotNull(mixResult);
			Assert.IsTrue(mixResult.IsMixFatal);
			Assert.IsFalse(mixResult.IsMixDocumented);

		}

		[TestMethod]
		public void When_both_ingredients_are_found_in_a_mix_with_a_mood()
		{
			//Arrange
			var mockIngredientRepo = A.Fake<IIngredientRepository>();
			A.CallTo(() => mockIngredientRepo.GetAllMixes()).Returns(
				new List<IngredientMix>()
				{
					new IngredientMix
					{
						Ingredient1 = (int)MockIngredients.A,
						Ingredient2 = (int)MockIngredients.B,
						MoodType = (int)MockMoods.Postive,
						Effect = MockMoods.Postive.ToString(),
						IsFatal = false
					},
					new IngredientMix
					{
						Ingredient1 = (int)MockIngredients.A,
						Ingredient2 = (int)MockIngredients.B,
						MoodType = (int)MockMoods.Negative,
						Effect = MockMoods.Negative.ToString(),
						IsFatal = false
					}
				});

			var mixer = new IngredientMixingManager(mockIngredientRepo);

			//Act
			var mixResult = mixer.Mix((int)MockMoods.Postive, (int)MockIngredients.A, (int)MockIngredients.B);

			//Assert
			Assert.IsNotNull(mixResult);
			Assert.IsFalse(mixResult.IsMixFatal);
			Assert.IsTrue(mixResult.IsMixDocumented);
			Assert.AreEqual(mixResult.Effect, MockMoods.Postive.ToString());

		}

		[TestMethod]
		public void When_the_same_ingredients_are_found_in_reverse_order_a_mix_with_a_mood()
		{
			//Arrange
			var mockIngredientRepo = A.Fake<IIngredientRepository>();
			A.CallTo(() => mockIngredientRepo.GetAllMixes()).Returns(
				new List<IngredientMix>()
				{
					new IngredientMix
					{
						Ingredient1 = (int)MockIngredients.A,
						Ingredient2 = (int)MockIngredients.B,
						MoodType = (int)MockMoods.Postive,
						Effect = MockMoods.Postive.ToString(),
						IsFatal = false
					},
					new IngredientMix
					{
						Ingredient1 = (int)MockIngredients.A,
						Ingredient2 = (int)MockIngredients.B,
						MoodType = (int)MockMoods.Negative,
						Effect = MockMoods.Negative.ToString(),
						IsFatal = false
					}
				});

			var mixer = new IngredientMixingManager(mockIngredientRepo);

			//Act
			var mixResult = mixer.Mix((int)MockMoods.Postive, (int)MockIngredients.B, (int)MockIngredients.A);

			//Assert
			Assert.IsNotNull(mixResult);
			Assert.IsFalse(mixResult.IsMixFatal);
			Assert.IsTrue(mixResult.IsMixDocumented);
			Assert.AreEqual(mixResult.Effect, MockMoods.Postive.ToString());

		}



		private enum MockIngredients
		{
			A = 1,
			B = 2,
			C = 3
		}

		private enum MockMoods
		{
			Postive = 1,
			Negative = 2
		}
	}

	
}
