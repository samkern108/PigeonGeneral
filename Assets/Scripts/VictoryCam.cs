using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCam : MonoBehaviour {

	public float speed;
	private Transform target;

	private float targetSize = 1.25f;

	public void ZoomToTarget(Transform t) {
		target = t;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {

			Vector3 prevPos = this.transform.position;
			Vector3 newPos = Vector3.Lerp(prevPos, target.position, speed * Time.deltaTime);
			newPos.z = prevPos.z;

			this.transform.position = newPos;

			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 1.25f, speed * Time.deltaTime);
		}		
	}
}
