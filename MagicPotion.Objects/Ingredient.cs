using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
    public class Ingredient
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Color { get; set; }
		public string Description { get; set; }
		public string Effect { get; set; }
	    public int EffectType { get; set; }
		public string ImportId { get; set; }
    }
}
