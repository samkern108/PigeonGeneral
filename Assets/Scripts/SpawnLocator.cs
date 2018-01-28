using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocator : MonoBehaviour {

	public Vector2 relativePosition;
	
	// Update is called once per frame
	void Update () {
		Vector3[] corners = new Vector3[4];

		(this.transform.parent as RectTransform).GetWorldCorners(corners);

		float x = corners[0].x + relativePosition.x * (corners[2].x - corners[0].x);
		float y = corners[0].y + relativePosition.y * (corners[2].y - corners[0].y);

		this.transform.position = new Vector3(x, y, 0f);
	}
}
