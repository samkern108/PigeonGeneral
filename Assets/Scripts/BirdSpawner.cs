using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSpawner : MonoBehaviour {

	private Message message;
	private int targetIndex = -1;

	public GameObject birdPrefab;

	public int playerIndex;

	public enum SelectionStage
	{
		Action, Dir, Unit
	};

	public SelectionStage stage = SelectionStage.Action;

	public MessageUI UI { get { return MessageUI.GetUIForPlayer(playerIndex); } }	

	void Start() {
		ResetSelf ();
	}

	private void ResetSelf() {
		message = new Message ();
		targetIndex = Random.Range (0, Player.UNITS_PER_PLAYER);
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
		case SelectionStage.Unit:
			SelectUnit ();
			break;
		}
	}

	private KeyCode GetKey(Message.Dir dir) {
		return Player.Get(playerIndex).GetKey(dir);
	}

	private void SelectAction() {
		if (Input.GetKeyDown (GetKey(Message.Dir.down))) {
			UpdateMessageAction(Message.Action.move);
		}
		else if (Input.GetKeyDown (GetKey(Message.Dir.up))) {
			UpdateMessageAction(Message.Action.shoot);
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
		if (Input.GetKeyDown (GetKey(Message.Dir.up))) {
			targetIndex = 0;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.left))) {
			targetIndex = 1;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.down))) {
			targetIndex = 2;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.right))) {
			targetIndex = 3;
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
		stage = SelectionStage.Unit;

		UI.SetDirSelectionChoice (dir);
		UI.SetSelectionStage (stage);
	}

	public void SpawnBird() {
		GameObject flyingBird = Instantiate (birdPrefab);

		int index = targetIndex * Player.PLAYER_COUNT + playerIndex;

		GameObject target = GameController.self.unitControllers[targetIndex].gameObject;

		GameObject launchPoint = new GameObject();
		launchPoint.name = "Launch";
		launchPoint.transform.position = Board.self ? Board.GetRandomPointOnBorder() : new Vector3(0f, -5f, 0.5f);

		flyingBird.GetComponent <FlyingBird>().Initialize(target, launchPoint, message);

		ResetSelf ();
	}
}
