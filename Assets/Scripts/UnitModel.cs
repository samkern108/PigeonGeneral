using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Order is G, B, R, P

public class Colors {
	public static Color[] lightColors = new Color[] {
		new Color(151f / 255f, 214f / 255f, 68f / 255f, 1f),
		new Color(84f / 255f, 212f / 255f, 219f / 255f, 1f),
		new Color(227f / 255f, 69f / 255f, 107f / 255f, 1f),
		new Color(182f / 255f, 93f / 255f, 221f / 255f, 1f),
	};

	public static Color[] midColors = new Color[] {
		new Color(95f / 255f, 130f / 255f, 49f / 255f, 1f),
		new Color(48f / 255f, 140f / 255f, 145f / 255f, 1f),
		new Color(161f / 255f, 48f / 255f, 76f / 255f, 1f),
		new Color(107f / 255f, 49f / 255f, 125f / 255f, 1f),
	};

	public static Color[] darkColors = new Color[] {
		new Color(80f / 255f, 109f / 255f, 43f / 255f, 1f),
		new Color(44f / 255f, 113f / 255f, 117f / 255f, 1f),
		new Color(134f / 255f, 45f / 255f, 68f / 255f, 1f),
		new Color(84f / 255f, 39f / 255f, 98f / 255f, 1f),
	};
}
	
public class UnitModel : MonoBehaviour {

	public SpriteRenderer frame;
	public SpriteRenderer pigeon;

	public void Init(int playerIndex, int pigeonIndex) {
		pigeon = gameObject.GetComponent<SpriteRenderer>();
		pigeon.sprite = ResourceManager.self.GetPigeonSprite (playerIndex, PigeonPose.Idle);

		frame.color = Colors.darkColors[playerIndex];
	}
}
