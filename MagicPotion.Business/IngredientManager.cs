using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicPotion.Objects;
using MagicPotion.Repository;
using Microsoft.VisualBasic.FileIO;

namespace MagicPotion.Business
{
	public class IngredientManager : IIngredientManager
	{
		private readonly IIngredientRepository _potionRepo;
		private readonly ITypeOptionRepository _typeOptionRepo;

		public IngredientManager(IIngredientRepository potionRepo, ITypeOptionRepository typeOptionRepo)
		{
			_potionRepo = potionRepo;
			_typeOptionRepo = typeOptionRepo;
		}

		public Ingredient GetIngredient(int id)
		{
			return _potionRepo.GetIngredient(id);
		}

		public IEnumerable<Ingredient> GetAllIngredients()
		{
			return _potionRepo.GetAllIngredients();
		}

		public IEnumerable<IngredientMix> GetAllMixes()
		{
			return _potionRepo.GetAllMixes();
		}

		public IEnumerable<Mood> GetAllMoods()
		{
			return _typeOptionRepo.GetAllMoods();
		}

		public IEnumerable<Effect> GetAllEffects()
		{
			return _typeOptionRepo.GetAllEffects();
		}



		public Ingredient UpsertIngredient(Ingredient ingredient)
		{
			return _potionRepo.UpsertIngredient(ingredient);
		}

		public IngredientUploadResult ParseCsvAndUpdateIngredients(Stream fileStream)
		{
			var importResult = new IngredientUploadResult();
			var ingredients = _potionRepo.GetAllIngredients().ToList();
			var effects = _typeOptionRepo.GetAllEffects().ToList();
			try
			{

				using (TextFieldParser parser = new TextFieldParser(fileStream))
				{
					parser.TextFieldType = FieldType.Delimited;
					parser.SetDelimiters(",");
					while (!parser.EndOfData)
					{
						//Process row
						string[] fields = parser.ReadFields();
						try
						{
							var importId = fields[0];
							var name = fields[1];
							if (string.IsNullOrWhiteSpace(importId) || string.IsNullOrWhiteSpace(name))
							{
								var row = string.Join(",", fields);
								importResult.Errors.Add($"Skipped row due to no ImportId or Name.  CSV ROW: {row}");
								continue;
							}

							//Attempt to match on ImportId or name.

							var ingredient = ingredients.FirstOrDefault(o => o.ImportId == importId
																			 || o.Name.Equals(name.Trim(),
																				 StringComparison.InvariantCultureIgnoreCase));
							if (ingredient == null)
							{
								//No match found create a new one.
								ingredient = new Ingredient
								{
									Name = fields[1],
									Color = fields[2],
									Description = fields[3],
									ImportId = importId
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

							UpsertIngredient(ingredient);
							importResult.SuccessCount += 1;
						}
						catch (Exception e)
						{
							var row = string.Join(",", fields);
							importResult.Errors.Add($"Error while parsing {row}");
							Console.WriteLine(e);

						}
					}
				}
			}
			catch (Exception e)
			{
				importResult.Errors.Add("An internal server error occured.");
				Console.WriteLine(e);
			}
			return importResult;
		}

		private void TryGetEffectType(string effectNameToMatch, List<Effect> effects, Ingredient ingredient)
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
