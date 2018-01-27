using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingBird : MonoBehaviour {

	public Message message;
	public GameObject target;
	public GameObject launchPoint;

	void Start () {
		message = new Message ();
		gameObject.AddComponent <FlyingBird>();
		gameObject.GetComponent <FlyingBird>().Initialize(target, launchPoint, message);
		Destroy (this);
	}
}
