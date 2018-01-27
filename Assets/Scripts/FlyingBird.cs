using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBird : MonoBehaviour {

	private GameObject target;
	private float speed = .4f;
	public Message message;

	public void Initialize(GameObject target, GameObject launchPoint, Message message) {
		this.message = message;
		transform.position = launchPoint.transform.position;
		this.target = target;
	}

	void Update () {
		if(target != null)
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);
	}
}
