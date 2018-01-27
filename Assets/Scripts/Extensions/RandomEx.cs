using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace N_extensions
{
	public static class RandomEx
	{
		public static int RandomWeightedValue(List<int> weights)
		{
			int totalOptionsCount = 0;
			for (int i = 0; i < weights.Count; ++i)
			{
				totalOptionsCount += weights[i];
			}

			int selectedItemIndex = Random.Range(0, totalOptionsCount);

			for (int i = 0; i < weights.Count; ++i)
			{
				selectedItemIndex -= weights[i];

				if (selectedItemIndex < 0)
				{
					return i;
				}
			}

			return (weights.Count - 1);
		}
	}
}