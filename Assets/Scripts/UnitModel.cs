using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitModel : MonoBehaviour {

	private Color[] playerColors = new Color[] {
		new Color(1f, 0f, 0f, 1f),
		new Color(0f, 1f, 0f, 1f),
		new Color(0f, 0f, 1f, 1f),
		new Color(1f, 1f, 1f, 1f),
	};

	public void Init(int playerIndex) {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

		sr.color = playerColors[playerIndex];
	}
}
