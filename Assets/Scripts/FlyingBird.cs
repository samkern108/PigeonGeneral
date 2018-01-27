using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBird : MonoBehaviour {

	private GameObject target;
	public float speed = 3f;
	public Message message;

	public void Initialize(GameObject target, GameObject launchPoint, Message message) {
		this.message = message;
		transform.position = launchPoint.transform.position;
		this.target = target;

		GameObject.DestroyImmediate(launchPoint);
	}

	void Update () {
		if(target != null) {
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);

			if (Vector3.Distance(transform.position, target.transform.position) < 0.1f) {
				if (Board.self) {
					GameObject go = Board.self.GetObjectAt(transform.position);
					UnitController unit = go.GetComponent<UnitController>();
					if (unit != null) {
						unit.SubmitMessage(message);
					}
				}

				GameObject.DestroyImmediate(target);
				GameObject.DestroyImmediate(this.gameObject);
			}
		}
	}
}
