
using Random = UnityEngine.Random;

namespace N_extensions
{

public static class ArrayEx
{
	public static int[] ArrayFromRange(int first, int last, int delta)
	{
		int count = last - first + delta;
		int[] values = new int[count];
		
		for (int i = 0; i < count; ++i)
		{
			values[i] = first + i * delta;
		}

		return values;
	}

	public static void Shuffle<T>(this T[] array)
	{
		for (int i = 0; i < array.Length; ++i)
		{
			int swap_index = Random.Range(i, array.Length);

			array.Swap<T>(i, swap_index);
		}
	}

	public static T[] Swap<T>(this T[] array, int index0, int index1)
	{
		T temp = array[index0];
		array[index0] = array[index1];
		array[index1] = temp;

		return array;
	}

	public static bool Contains<T>(this T[] array, T query)
	{
		for (int i = 0; i < array.Length; ++i)
		{
			if (array[i].Equals(query))
			{
				return true;
			}
		}

		return false;
	}
}

}	// N_extensions