using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlighter : MonoBehaviour {

	public GameObject cursor;
	private GameObject target;

	public void SetPlayer(int playerIndex) {
		SpriteRenderer sr = cursor.GetComponent<SpriteRenderer>();
		//sr.color = Colors.playerColors[playerIndex];
	}

	public void SetTarget(GameObject target) {
		this.target = target;
		UpdatePosition();
	}

	private void Awake() {
		GameObject.Instantiate(cursor, new Vector3(-999f, 0f, 0f), Quaternion.identity);
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
