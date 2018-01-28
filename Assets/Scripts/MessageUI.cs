using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour {

	public static MessageUI self;

	public GameObject wasdDir, wasdAction;

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

		wasdDir.GetComponent<Image> ().color = Colors.lightColors[spawner.playerIndex];
		wasdAction.GetComponent<Image> ().color = Colors.lightColors[spawner.playerIndex];
	}

	public void Reset() {
		int firstStage = 0;
		SetSelectionStage((BirdSpawner.SelectionStage)firstStage);
	}

	public void SetSelectionStage(BirdSpawner.SelectionStage stage) {
		switch (stage) {
		case BirdSpawner.SelectionStage.Action:
			wasdDir.SetActive (false);
			wasdAction.SetActive (true);
			break;
		case BirdSpawner.SelectionStage.Dir:
			wasdDir.SetActive (true);
			wasdAction.SetActive (false);
			break;
		}
	}
}
