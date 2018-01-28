using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHighlighter : MonoBehaviour {

	public GameObject cursor;

	public void SetTarget(Vector3 position) {
		
	}

	private void Awake() {
		GameObject.Instantiate(cursor, new Vector3(-999f, 0f, 0f), Quaternion.identity);
	}
}
