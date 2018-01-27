using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors {
	public static Color[] playerColors = new Color[] {
		new Color(115f / 255f, 164f / 255f, 189f / 255f, 1f),
		new Color(146f / 255f, 103f / 255f, 183f / 255f, 1f),
		new Color(89f / 255f, 178f / 255f, 172f / 255f, 1f),
		new Color(71f / 255f, 148f / 255f, 103f / 255f, 1f),
	};
}

public class UnitModel : MonoBehaviour {

	public void Init(int playerIndex, int pigeonIndex) {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = ResourceManager.self.GetPigeonSprite(pigeonIndex);

		sr.color = Colors.playerColors[playerIndex];
	}
}
