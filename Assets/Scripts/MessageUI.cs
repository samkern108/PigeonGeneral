using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour {

	public static MessageUI self;

	public GameObject wasdDir, wasdAction, wasdUnit;
	public Image actionIcon, dirIcon;

	public static MessageUI GetUIForPlayer(int playerIndex) {
		return messageUIs[playerIndex];
	}

	private static MessageUI[] messageUIs;

	void Awake() {
		self = this;

		if (messageUIs == null) {
			messageUIs = new MessageUI[Player.PLAYER_COUNT];
		}

		BirdSpawner spawner = this.gameObject.GetComponent<BirdSpawner>();
		messageUIs[spawner.playerIndex] = this;

		Image img = this.gameObject.GetComponent<Image>();
		img.color = Colors.midColors[spawner.playerIndex];
	}

	public void Reset() {
		actionIcon.sprite = ResourceManager.self.questionIcon;
		dirIcon.sprite = ResourceManager.self.questionIcon;

		int firstStage = 0;
		SetSelectionStage((BirdSpawner.SelectionStage)firstStage);
	}

	public void SetSelectionStage(BirdSpawner.SelectionStage stage) {
		switch (stage) {
		case BirdSpawner.SelectionStage.Action:
			wasdDir.SetActive (false);
			wasdAction.SetActive (true);
			wasdUnit.SetActive (false);
			break;
		case BirdSpawner.SelectionStage.Dir:
			wasdDir.SetActive (true);
			wasdAction.SetActive (false);
			wasdUnit.SetActive (false);
			break;
		case BirdSpawner.SelectionStage.Unit:
			wasdDir.SetActive (false);
			wasdAction.SetActive (false);
			wasdUnit.SetActive (true);
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
