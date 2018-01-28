using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour {

	void Start () {
		Invoke ("ReturnToMain", 35.0f);
	}

	public void ReturnToMain() {
		Application.LoadLevel("MainMenu");
	}

	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.y += .5f * Time.deltaTime;
		transform.position = newPosition;

		if (Input.GetKeyDown(KeyCode.W)
			||	Input.GetKeyDown(KeyCode.Space)
			||	Input.GetKeyDown(KeyCode.Return)
			||	Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}	
	}
}
