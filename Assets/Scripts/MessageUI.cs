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
		SetSpawnStage((SpawnStage)firstStage);
	}

	public void SetSpawnStage(SpawnStage stage) {
		switch (stage) {
		case SpawnStage.Action:
			wasdDir.SetActive (false);
			wasdAction.SetActive (true);
			break;
		case SpawnStage.Dir:
			wasdDir.SetActive (true);
			wasdAction.SetActive (false);
			break;
		}
	}
}
