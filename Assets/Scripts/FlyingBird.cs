using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingBird : MonoBehaviour {

	private GameObject target;
	public float speed = 3f;
	public Message message;

	public Image actionIcon, directionIcon;

	public void Initialize(GameObject target, GameObject launchPoint, Message message) {
		this.message = message;
		transform.position = launchPoint.transform.position;
		this.target = target;

		SetMessageIcons ();

		GameObject.DestroyImmediate(launchPoint);
	}

	void Update () {
		if(target != null) {
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);

			if (Vector3.Distance(transform.position, target.transform.position) < 0.1f) {
				if (Board.self) {
					GameObject go = Board.self.GetObjectAt(transform.position);
					UnitController unit = go.GetComponent<UnitController>();

					unit.SubmitMessage(message);
				}

				GameObject.DestroyImmediate(target);
				GameObject.DestroyImmediate(this.gameObject);
			}
		}
	}

	private void SetMessageIcons() {
		switch (message.action) {
		case Message.Action.shoot:
			actionIcon.sprite = ResourceManager.self.attackIcon;
			break;
		case Message.Action.move:
			actionIcon.sprite = ResourceManager.self.moveIcon;
			break;
		}

		switch (message.dir) {
		case Message.Dir.down:
			directionIcon.sprite = ResourceManager.self.downArrow;
			break;
		case Message.Dir.up:
			directionIcon.sprite = ResourceManager.self.upArrow;
			break;
		case Message.Dir.right:
			directionIcon.sprite = ResourceManager.self.rightArrow;
			break;
		case Message.Dir.left:
			directionIcon.sprite = ResourceManager.self.leftArrow;
			break;
		}
	}
}
