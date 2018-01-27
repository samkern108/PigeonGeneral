using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour {

	public static MessageUI self;

	void Start() {
		self = this;
		currentSelectionLabel.text = "";
		currentSelectionChoice.text = "";
		instruction.text = "";
	}

	public Text currentSelectionLabel, currentSelectionChoice, instruction;

	public void SetSelectionStage(StagingBird.SelectionStage stage) {
		switch (stage) {
		case StagingBird.SelectionStage.Action:
			currentSelectionLabel.text = "Action";
			instruction.text = "E to confirm";
			break;
		case StagingBird.SelectionStage.Dir:
			currentSelectionLabel.text = "Dir";
			instruction.text = "E to FLY";
			break;
		}
	}

	public void SetActionSelectionChoice(Message.Action action) {
		switch (action) {
		case Message.Action.shoot:
			currentSelectionLabel.text = "Shoot";
			break;
		case Message.Action.move:
			currentSelectionLabel.text = "Move";
			break;
		}
	}

	public void SetDirSelectionChoice(Message.Dir dir) {
		switch (dir) {
		case Message.Dir.down:
			currentSelectionChoice.text = "Down";
			break;
		case Message.Dir.left:
			currentSelectionChoice.text = "Left";
			break;
		case Message.Dir.right:
			currentSelectionChoice.text = "Right";
			break;
		case Message.Dir.up:
			currentSelectionChoice.text = "Up";
			break;
		}
	}
}
