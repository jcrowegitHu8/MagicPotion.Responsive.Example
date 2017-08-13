using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
	/// <summary>
	/// The type option represents a Id, Value pair driven from the DB
	/// so that reports and additional options can be added without a code change.
	/// </summary>
	public abstract class TypeOption
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
