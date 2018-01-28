using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBird : MonoBehaviour {

	private GameObject target;
	public float speed = 3f;
	public Message message;

	public Transform model;

	public void Initialize(GameObject target, Message message) {
		this.speed = Random.Range (1.0f, 4.0f);
		this.message = message;
		this.target = target;

		model = transform.Find ("Model");

		Vector3 forward = Vector3.up;
		Vector3 toTarget = (target.transform.position - transform.position).normalized;

		float dot = Vector3.Dot(forward, toTarget);
		float acos = Mathf.Acos(dot);
		float zRot = Mathf.Rad2Deg * acos;

		if (target.transform.position.x > transform.position.x) {
			zRot *= -1f;
		}
			
		model.Rotate(new Vector3(0f, 0f, zRot));
	}

	void Update () {
		if(target != null) {
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);

			if (Vector3.Distance(transform.position, target.transform.position) < 0.1f) {
				if (Board.self) {
					GameObject go = Board.self.GetObjectAt(transform.position);
					if (go != null) {
						UnitController unit = go.GetComponent<UnitController>();
						if (unit != null) {
							unit.SubmitMessage(message);
						}
					}
				}

				GameObject.DestroyImmediate(this.gameObject);
			}
		}
		else {
			GameObject.DestroyImmediate(this.gameObject);			
		}
	}
}
