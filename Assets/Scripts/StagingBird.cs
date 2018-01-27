using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagingBird : MonoBehaviour {

	public Message message;
	public GameObject target;
	public GameObject launchPoint;

	public Image actionIcon, directionIcon;

	// First select action
	// (up for shoot, down for move

	// Then select action direction
	// direction is obvious
	public enum SelectionStage
	{
		Action, Dir
	};

	public SelectionStage stage = SelectionStage.Action;

	void Start() {
		message = new Message ();
		MessageUI.self.SetSelectionStage (stage);
	}

	void Update() {
		switch (stage) {
		case SelectionStage.Action:
			SelectAction ();
			break;
		case SelectionStage.Dir:
			SelectDir ();
			break;
		}
	}

	private void SelectAction() {
		if (Input.GetKeyDown (KeyCode.S)) {
			UpdateMessageAction(Message.Action.move);
		}
		else if (Input.GetKeyDown (KeyCode.W)) {
			UpdateMessageAction(Message.Action.shoot);
		}
	}

	private void UpdateMessageAction(Message.Action action) {
		message.action = action;
		MessageUI.self.SetActionSelectionChoice (action);
		stage = SelectionStage.Dir;
		MessageUI.self.SetSelectionStage (stage);
	}

	private void SelectDir() {
		if (Input.GetKeyDown (KeyCode.W)) {
			UpdateMessageDir(Message.Dir.up);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			UpdateMessageDir(Message.Dir.down);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			UpdateMessageDir(Message.Dir.left);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			UpdateMessageDir(Message.Dir.right);
		}
	}

	private void UpdateMessageDir(Message.Dir dir) {
		message.dir = dir;
		MessageUI.self.SetDirSelectionChoice (dir);
		Go ();
	}

	private void Go() {
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

		gameObject.AddComponent <FlyingBird>();

		target = new GameObject();
		target.name = "Target";
		target.transform.position = Board.self ? Board.self.GetRandomObject().transform.position : new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0f);

		launchPoint = new GameObject();
		launchPoint.name = "Launch";
		launchPoint.transform.position = Board.self ? Board.GetBoardWorld(new Vector2(0.5f, 0f)) : new Vector3(0f, -5f, 0.5f);

		gameObject.GetComponent <FlyingBird>().Initialize(target, launchPoint, message);

		Destroy (this);
	}
}
