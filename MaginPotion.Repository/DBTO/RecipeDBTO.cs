using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Repository.DBTO
{
	class RecipeDBTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsFatal { get; set; }
		public int MoodType { get; set; }
		public string Mood { get; set; }
		public int EffectType { get; set; }
		public string Effect { get; set; }

		public string Ingredients { get; set; }
	}
}
