using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
			var itemsList = items.ToList();
			itemsList.Sort();

			int listLength = itemsList.Count;
			if (listLength == 0)
				throw new InvalidOperationException();
			else if (listLength % 2 == 0)
				return (itemsList[listLength / 2 - 1] + itemsList[listLength / 2]) / 2;
			else
				return itemsList[listLength / 2];
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			T firstItem = default;
			bool firstItemIsTaken = true;
			foreach (var item in items)
			{
				if (firstItemIsTaken)
				{
					firstItem = item;
					firstItemIsTaken = false;
				}
				else
				{
					yield return new Tuple<T, T>(firstItem, item);
					firstItem = item;
				}
			}
		}
	}
}