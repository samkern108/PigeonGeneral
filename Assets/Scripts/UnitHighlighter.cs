using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlighter : MonoBehaviour {

	public GameObject cursor;
	private GameObject target;
	private SpriteRenderer cursorSR;
	private Animate animate;

	public void SetPlayerIndex(int playerIndex) {
		cursorSR = cursor.GetComponent<SpriteRenderer> ();
		cursorSR.color = Colors.lightColors[playerIndex];
	}

	public void SetTarget(GameObject target) {
		this.target = target;
		UpdatePosition();
	}

	private void Start() {
		GameObject.Instantiate(cursor, new Vector3(-999f, 0f, 0f), Quaternion.identity);
		animate = GetComponentInChildren <Animate>();
		Vector3 startSize = this.transform.localScale;
		Vector3 endSize = new Vector3 (startSize.x * .5f, startSize.y * .5f, 1.0f);
		animate.AnimateToSize (startSize, endSize, 1.0f, Animate.RepeatMode.PingPong);
	}

	private void Update() {
		UpdatePosition();
	}

	private void UpdatePosition() {
		if (target != null) {
			cursor.transform.position = target.transform.position;
		}
		else {
			target = null;
			cursor.transform.position = new Vector3(-999f, 0f, 0f);
		}
	}
}
