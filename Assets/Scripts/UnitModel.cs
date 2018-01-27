using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors {
	public static Color[] playerColors = new Color[] {
		new Color(95f / 255f, 130f / 255f, 49f / 255f, 1f),
		new Color(48f / 255f, 140f / 255f, 145f / 255f, 1f),
		new Color(161f / 255f, 48f / 255f, 76f / 255f, 1f),
		new Color(107f / 255f, 49f / 255f, 125f / 255f, 1f),
	};
}

public class UnitModel : MonoBehaviour {

	public SpriteRenderer frame;

	public void Init(int playerIndex, int pigeonIndex) {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = ResourceManager.self.GetPigeonSprite(playerIndex, pigeonIndex);

		frame.color = Colors.playerColors[playerIndex];
	}
}
