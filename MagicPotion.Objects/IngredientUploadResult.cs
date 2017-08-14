using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
	public class IngredientUploadResult
	{
		public bool Success => !Errors.Any();
		public List<string> Errors { get; set; } = new List<string>();
		public int SuccessCount { get; set; }
	}
}
