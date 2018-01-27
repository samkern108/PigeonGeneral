using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors {
	public static Color[] playerColors = new Color[] {
		new Color(1f, 0f, 0f, 1f),
		new Color(0f, 1f, 0f, 1f),
		new Color(0f, 0f, 1f, 1f),
		new Color(1f, 1f, 1f, 1f),
	};
}

public class UnitModel : MonoBehaviour {

	public void Init(int playerIndex) {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

		sr.color = Colors.playerColors[playerIndex];
	}
}
