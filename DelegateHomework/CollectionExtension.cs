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
			if (collection == null || collection.Count() == 0)
				throw new NullReferenceException();

			int indexMax = 0;
			float floatMax = converter(collection.ElementAt(0));

			for (int i = 1; i < collection.Count(); i++)
			{
				float floatToCompare = converter(collection.ElementAt(i));
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