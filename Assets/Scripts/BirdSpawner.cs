using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSpawner : MonoBehaviour {

	private Message message;
	private int targetIndex;

	public GameObject birdPrefab;
	public UnitHighlighter highlighter;

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

		for (int i = 0; i < Player.UNITS_PER_PLAYER; ++i) {
			UnitController unit = Player.livingBirds[playerIndex][i];
			unit.RegisterOnDeath(OnPlayerUnitDeath);
		}
	}

	private void OnPlayerUnitDeath(UnitController unit) {
		UnitController currentSelectedUnit = Player.livingBirds[playerIndex][targetIndex];

		if (currentSelectedUnit == unit) {
			// pick new random selection? closest?
			UnitController target = unit;
			while (target == unit) {
				targetIndex = Random.Range(0, Player.livingBirds[playerIndex].Count);

				target = Player.livingBirds[playerIndex][targetIndex];
			}
			highlighter.SetTarget(target.gameObject);
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
		/*else if (Input.GetKeyDown (Message.Dir.left)) {
			SelectUnit ();
		}
		else if (Input.GetKeyDown (Message.Dir.right)) {
			SelectUnit ();
		}*/
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

		UI.SetActionSelectionChoice (action);
		UI.SetSelectionStage (stage);
	}

	private void UpdateMessageDir(Message.Dir dir) {
		message.dir = dir;
		UI.SetDirSelectionChoice (dir);
		SpawnBird ();
	}

	public void SpawnBird() {
		GameObject flyingBird = Instantiate (birdPrefab);

		if (Player.livingBirds[playerIndex].Count > 0) {
			GameObject target = Player.livingBirds[playerIndex][targetIndex].gameObject;

			/*GameObject launchPoint = new GameObject();
			launchPoint.name = "Launch";
			launchPoint.transform.position = this.transform.position;//Board.self ? Board.GetRandomPointOnBorder() : new Vector3(0f, -5f, 0.5f);*/

			flyingBird.GetComponent <FlyingBird>().Initialize(target, transform.position, message);

			flyingBird.GetComponentInChildren <SpriteRenderer>().color = Colors.lightColors[playerIndex];
		}

		ResetSelf ();
	}
}
