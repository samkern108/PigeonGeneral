using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour {

	public static MessageUI self;

	public GameObject wasdDir, wasdAction;
	public Image actionIcon, dirIcon;

	void Start() {
		self = this;
	}

	public void SetSelectionStage(StagingBird.SelectionStage stage) {
		switch (stage) {
		case StagingBird.SelectionStage.Action:
			wasdDir.SetActive (false);
			wasdAction.SetActive (true);
			break;
		case StagingBird.SelectionStage.Dir:
			wasdDir.SetActive (true);
			wasdAction.SetActive (false);
			break;
		}
	}

	public void SetActionSelectionChoice(Message.Action action) {
		switch (action) {
		case Message.Action.shoot:
			actionIcon.sprite = ResourceManager.self.attackIcon;
			break;
		case Message.Action.move:
			actionIcon.sprite = ResourceManager.self.moveIcon;
			break;
		}
	}

	public void SetDirSelectionChoice(Message.Dir dir) {
		switch (dir) {
		case Message.Dir.down:
			dirIcon.sprite = ResourceManager.self.downArrow;
			break;
		case Message.Dir.left:
			dirIcon.sprite = ResourceManager.self.leftArrow;
			break;
		case Message.Dir.right:
			dirIcon.sprite = ResourceManager.self.rightArrow;
			break;
		case Message.Dir.up:
			dirIcon.sprite = ResourceManager.self.upArrow;
			break;
		}
	}
}
