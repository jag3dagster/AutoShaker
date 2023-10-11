using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShaker.Classes
{
	internal sealed class ForageableItemTracker
	{
		// ---------- Instance ---------- //

		private static readonly Lazy<ForageableItemTracker> _lazyInstance = new Lazy<ForageableItemTracker>(() => new());
		public static ForageableItemTracker Instance => _lazyInstance.Value;

		// ---------- Trackers ---------- //

		private readonly List<ForageableItem> _artifactForageables;
		public IReadOnlyList<ForageableItem> ArtifactForageables => _artifactForageables;

		private readonly List<ForageableItem> _fruitTreeForageables;
		public IReadOnlyList<ForageableItem> FruitTreeForageables => _fruitTreeForageables;

		private readonly List<ForageableItem> _objectForageables;
		public IReadOnlyList<ForageableItem> ObjectForageables => _objectForageables;

		private readonly List<ForageableItem> _wildTreeForageables;
		public IReadOnlyList<ForageableItem> WildTreeForageables => _wildTreeForageables;

		// ---------- Constructor ---------- //

		private ForageableItemTracker()
		{
			_artifactForageables = new();
			_fruitTreeForageables = new();
			_objectForageables = new();
			_wildTreeForageables = new();
		}
	}
}
