using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSpawner : MonoBehaviour {

	private Message message;
	private int targetIndex;

	public GameObject birdPrefab;
	public UnitHighlighter highlighter;
	public Transform spawnPoint;

	private FlyingBirdUI stagedBird;

	public int playerIndex;

	public enum SelectionStage
	{
		Action, Dir
	};

	public SelectionStage stage = SelectionStage.Action;

	public MessageUI UI { get { return MessageUI.GetUIForPlayer(playerIndex); } }	

	void Start() {
		highlighter.SetPlayerIndex (playerIndex);
		ResetSelf ();

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
				Destroy(highlighter.gameObject);
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

	private void ResetSelf() {
		message = new Message ();
		UI.Reset ();

		stagedBird = Instantiate (birdPrefab).GetComponent<FlyingBirdUI>();
		stagedBird.GetComponentInChildren <SpriteRenderer>().color = Colors.lightColors[playerIndex];
		stagedBird.transform.position = spawnPoint.position;

		int initialStage = 0;
		stage = (SelectionStage)initialStage;
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

	private KeyCode GetKey(Message.Dir dir) {
		return Player.Get(playerIndex).GetKey(dir);
	}

	private void SelectAction() {
		
		// Select Action
		if (Input.GetKeyDown (GetKey (Message.Dir.down))) {
			UpdateMessageAction (Message.Action.move);
		} else if (Input.GetKeyDown (GetKey (Message.Dir.up))) {
			UpdateMessageAction (Message.Action.shoot);
		} 

		// Select Unit
		else {
			SelectUnit ();
		}
	}

	private void SelectDir() {
		if (Input.GetKeyDown (GetKey(Message.Dir.up))) {
			UpdateMessageDir(Message.Dir.up);
		} else if (Input.GetKeyDown (GetKey(Message.Dir.down))) {
			UpdateMessageDir(Message.Dir.down);
		} else if (Input.GetKeyDown (GetKey(Message.Dir.left))) {
			UpdateMessageDir(Message.Dir.left);
		} else if (Input.GetKeyDown (GetKey(Message.Dir.right))) {
			UpdateMessageDir(Message.Dir.right);
		}
	}
		
	private void SelectUnit() {
		int targetLength = Player.livingBirds[playerIndex].Count;

		bool changed = false;
		if (Input.GetKeyDown (GetKey(Message.Dir.left))) {
			targetIndex = (targetIndex + targetLength - 1) % targetLength;
			changed = true;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.right))) {
			targetIndex = (targetIndex + targetLength + 1) % targetLength;
			changed = true;
		}

		if (changed) {
			GameObject target = Player.livingBirds[playerIndex][targetIndex].gameObject;
			highlighter.SetTarget(target);
		}
	}

	private void UpdateMessageAction(Message.Action action) {
		message.action = action;
		stage = SelectionStage.Dir;

		stagedBird.SetActionSelectionChoice (action);
		UI.SetSelectionStage (stage);
	}

	private void UpdateMessageDir(Message.Dir dir) {
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
