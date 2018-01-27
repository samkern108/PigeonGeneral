using System.Collections.Generic;

using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace N_extensions
{
	public static class ListEx
	{
		public static List<T> Swap<T>(this List<T> list, int index0, int index1)
		{
			Debug.Assert(list != null, "Null list!");
			Debug.Assert(index0 >= 0 && index0 < list.Count, "Invalid index0!");
			Debug.Assert(index1 >= 0 && index1 < list.Count, "Invalid index1!");

			T temp = list[index0];
			list[index0] = list[index1];
			list[index1] = temp;

			return list;
		}

		// This function will perform a shallow copy of the provided list
		//   and then shuffle the contents of the new list randomly.
		public static List<T> CreateShuffledCopy<T>(this List<T> list)
		{
			Debug.Assert(list != null, "Null list!");

			List<T> new_list = new List<T>(list);

			new_list.Shuffle<T>();

			return new_list;
		}

		// Randomly shuffle the contents of the list parameter in place
		public static void Shuffle<T>(this List<T> list)
		{
			Debug.Assert(list != null, "Null list!");

			for (int i = 0; i < list.Count; ++i)
			{
				int swap_index = Random.Range(i, list.Count);

				list.Swap<T>(i, swap_index);
			}
		}

		// Merge other list elements into this list, disregarding any duplicate elements
		//   Uses shallow equality test.
		public static List<T> MergeWithoutDuplicates<T>(this List<T> list, List<T> other)
		{
			foreach (T element in other)
			{
				if (!list.Contains(element))
				{
					list.Add(element);
				}
			}

			return list;
		}

		// Removes the last instance of an element in the list.
		//  Returns true if element is found, false otherwise.
		public static bool RemoveLast<T>(this List<T> list, T element)
		{
			Debug.Assert(list != null, "Null list!");

			if (list.Count > 0)
			{
				int index = list.LastIndexOf(element);

				if (index > -1)
				{
					list.RemoveAt(index);

					return true;
				}
			}

			return false;
		}
	}
}