using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
	/// <summary>
	/// This result object is meant to separate the presentation information
	/// from the business logic.  We should only pass back business logic data points
	/// so that the screen can present whatever flashy things it needs to.
	/// </summary>
	public class IngredientMixingResult
	{
		public bool IsMixFatal { get; set; }
		public bool IsOutComePositive { get; set; }
		public bool IsMixDocumented { get; set; }
		public string Effect { get; set; }
	}
}
