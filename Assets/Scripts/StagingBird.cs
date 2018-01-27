using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingBird : MonoBehaviour {

	public Message message;
	public GameObject target;
	public GameObject launchPoint;

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
		Message.Action action;
		if (Input.GetKeyDown (KeyCode.W)) {
			UpdateMessageAction(Message.Action.move);
		}
		else if (Input.GetKeyDown (KeyCode.S)) {
			UpdateMessageAction(Message.Action.shoot);
		}
		else if (Input.GetKeyDown(KeyCode.E)) {
			stage = SelectionStage.Dir;
		}
	}

	private void UpdateMessageAction(Message.Action action) {
		message.action = action;
		MessageUI.self.SetActionSelectionChoice (action);
	}

	private void SelectDir() {
		Message.Dir dir;
		if (Input.GetKeyDown (KeyCode.W)) {
			UpdateMessageDir(Message.Dir.up);
		} else if (Input.GetKeyDown (KeyCode.S)) {
			UpdateMessageDir(Message.Dir.down);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			UpdateMessageDir(Message.Dir.left);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			UpdateMessageDir(Message.Dir.right);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			Go ();
		}
	}

	private void UpdateMessageDir(Message.Dir dir) {
		message.dir = dir;
		MessageUI.self.SetDirSelectionChoice (dir);
	}

	private void Go() {
		gameObject.AddComponent <FlyingBird>();
		gameObject.GetComponent <FlyingBird>().Initialize(target, launchPoint, message);
		Destroy (this);
	}
}
