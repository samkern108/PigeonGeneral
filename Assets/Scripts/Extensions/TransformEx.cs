using System.Collections.Generic;

using UnityEngine;

public static class TransformEx
{
	public static void DoOnSelfAndChildren(this Transform t, System.Action<Transform> operation)
	{
		List<Transform> queue = new List<Transform>();

		queue.Add(t);

		while (queue.Count > 0)
		{
			Transform query = queue[queue.Count - 1];
			queue.RemoveAt(queue.Count - 1);

			operation(query);

			foreach (Transform child in query)
			{
				queue.Add(child);
			}		
		}
	}

	public static Transform GetChildByName(this Transform t, string childName)
	{
		List<Transform> queue = new List<Transform>();
		
		queue.Add(t);
		
		while (queue.Count > 0)
		{
			Transform query = queue[queue.Count - 1];
			queue.RemoveAt(queue.Count - 1);

			if (query.name.Equals(childName))
			{
				return query;
			}

			foreach (Transform child in query)
			{
				queue.Add(child);
			}
		}

		return null;
	}
}