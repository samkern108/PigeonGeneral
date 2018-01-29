using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpawnStage
{
	Action, Dir
};

public class BirdSpawner : MonoBehaviour {

	public Message message;
	public int targetIndex;

	public GameObject birdPrefab;
	public UnitHighlighter highlighter;
	public Transform spawnPoint;

	private FlyingBirdUI stagedBird;

	public int playerIndex;
	//public static int currentPlayerIndex = 0;

	public SpawnStage stage = SpawnStage.Action;

	public MessageUI UI { get { return MessageUI.GetUIForPlayer(playerIndex); } }	

	void Start() {
		//playerIndex = currentPlayerIndex++;
		highlighter.SetPlayerIndex (playerIndex);

		// Hacky method for ensuring staged birds are placed after board/camera are ready.
		Invoke("ResetSelf", .01f);

		targetIndex = 0;
		GameObject target = Player.livingBirds[playerIndex][targetIndex].gameObject;
		highlighter.SetTarget(target);

		for (int i = 0; i < Player.livingBirds[playerIndex].Count; ++i) {
			UnitController unit = Player.livingBirds[playerIndex][i];
			unit.RegisterOnDeath(OnPlayerUnitDeath);
		}
	}

	private void OnPlayerUnitDeath(UnitController unit) {
		UnitController currentSelectedUnit = Player.livingBirds[playerIndex][targetIndex];

		if (currentSelectedUnit == unit) {
			// pick new random selection? closest?
			if (Player.livingBirds[playerIndex].Count == 1) {
				foreach (Transform t in this.transform) {
					if (t.gameObject.name.Contains("DeathIcon")) {
						t.gameObject.SetActive(true);
					}
					else {
						t.gameObject.SetActive(false);
					}
				}
				Destroy(this.gameObject.GetComponent<MessageUI>());
				Destroy(this.gameObject.GetComponent<PlayerController>());
				Destroy(this.gameObject.GetComponent<AIPlayer>());
				Destroy(highlighter.gameObject);
				if (stagedBird != null) {
					Destroy(stagedBird.gameObject);
				}
				Destroy(this);
			}
			else {
				UnitController target = unit;
				while (target == unit) {
					targetIndex = Random.Range(0, Player.livingBirds[playerIndex].Count);

					target = Player.livingBirds[playerIndex][targetIndex];
				}
				highlighter.SetTarget(target.gameObject);
			}
		}

		// adjust target index to match new shifted position of 
		//   currently selected unit
		for (int i = 0; i < Player.livingBirds[playerIndex].Count; ++i) {
			if (Player.livingBirds[playerIndex][i] == unit) {
				if (targetIndex > i) {
					--targetIndex;

					break;
				}
			}
		}
	}

	public void ResetSelf() {
		message = new Message ();
		UI.Reset ();

		stagedBird = Instantiate (birdPrefab).GetComponent<FlyingBirdUI>();
		stagedBird.GetComponentInChildren <SpriteRenderer>().color = Colors.lightColors[playerIndex];
		stagedBird.transform.position = spawnPoint.position;

		int initialStage = 0;
		stage = (SpawnStage)initialStage;
	}

	public void UpdateMessageAction(Message.Action action) {
		message.action = action;
		stage = SpawnStage.Dir;

		stagedBird.SetActionSelectionChoice (action);
		UI.SetSpawnStage (stage);
	}

	public void UpdateMessageDir(Message.Dir dir) {
		message.dir = dir;
		stagedBird.SetDirSelectionChoice (dir);
		SpawnBird ();
	}

	public void SpawnBird() {
		if (Player.livingBirds[playerIndex].Count > 0) {
			GameObject target = Player.livingBirds[playerIndex][targetIndex].gameObject;
			stagedBird.gameObject.AddComponent <FlyingBird>().Initialize(target, message);
		}

		ResetSelf ();
	}


	// Just to give the appearance that the AI are making choices

	public void SetAIActionIcon() {
		stagedBird.SetActionSelectionChoice (message.action);
	}

	public void SetAIDirIcon() {
		stagedBird.SetDirSelectionChoice (message.dir);
	}
}
