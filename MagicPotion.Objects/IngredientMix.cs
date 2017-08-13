using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
	public class IngredientMix
	{
		public int Id { get; set; }
		public int Ingredient1 { get; set; }
		public string Ingredient1Name { get; set; }
		public int Ingredient2 { get; set; }
		public string Ingredient2Name { get; set; }
		public int MoodType { get; set; }
		public string Mood { get; set; }
		public int EffectType { get; set; }
		public string Effect { get; set; }
		public bool IsFatal { get; set; }
	}
}
