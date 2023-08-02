namespace DelegateHomework
{
	public static class CollectionExtension
	{
		/// <summary>
		/// Extension method for IEnumerable.
		/// Finds max element in collection
		/// </summary>
		/// <exception cref="NullReferenceException"></exception>
		public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> converter) where T : class
		{
			if (converter == null)
				throw new ArgumentNullException(nameof(converter));
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));
			if (collection.Count() == 0)
				return null;

			var indexMax = 0;
			var floatMax = converter(collection.ElementAt(0));

			for (int i = 1; i < collection.Count(); i++)
			{
				var floatToCompare = converter(collection.ElementAt(i));
				if (floatToCompare > floatMax)
				{
					indexMax = i;
					floatMax = floatToCompare;
				}
			}

			return collection.ElementAt(indexMax);
		}
	}
}