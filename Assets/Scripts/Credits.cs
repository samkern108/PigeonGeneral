using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W)
		||	Input.GetKeyDown(KeyCode.Space)
		||	Input.GetKeyDown(KeyCode.Return)
		||	Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}	
	}
}
