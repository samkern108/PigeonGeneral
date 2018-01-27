using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSpawner : MonoBehaviour {

	private Message message;
	private GameObject target;
	private GameObject launchPoint;

	public GameObject birdPrefab;

	public int playerIndex;

	public enum SelectionStage
	{
		Action, Dir, Unit
	};

	public SelectionStage stage = SelectionStage.Action;

	void Start() {
		ResetSelf ();
	}

	private void ResetSelf() {
		message = new Message ();
		MessageUI.self.Reset ();

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

		GameObject target = Board.self.GetRandomObjectForPlayer(playerIndex);

		if (Input.GetKeyDown (GetKey(Message.Dir.up))) {
			SendBirdToUnit (target);
		} else if (Input.GetKeyDown (GetKey(Message.Dir.down))) {
			SendBirdToUnit (target);
		} else if (Input.GetKeyDown (GetKey(Message.Dir.left))) {
			SendBirdToUnit (target);
		} else if (Input.GetKeyDown (GetKey(Message.Dir.right))) {
			SendBirdToUnit (target);
		}
	}

	private void UpdateMessageAction(Message.Action action) {
		message.action = action;
		stage = SelectionStage.Dir;

		MessageUI.self.SetActionSelectionChoice (action);
		MessageUI.self.SetSelectionStage (stage);
	}

	private void UpdateMessageDir(Message.Dir dir) {
		message.dir = dir;
		stage = SelectionStage.Unit;

		MessageUI.self.SetDirSelectionChoice (dir);
		MessageUI.self.SetSelectionStage (stage);
	}
		
	private void SendBirdToUnit(GameObject unit) {
		GameObject flyingBird = Instantiate (birdPrefab);

		target = unit;

		launchPoint = new GameObject();
		launchPoint.name = "Launch";
		launchPoint.transform.position = Board.self ? Board.GetBoardWorld(new Vector2(0.5f, 0f)) : new Vector3(0f, -5f, 0.5f);

		flyingBird.GetComponent <FlyingBird>().Initialize(target, launchPoint, message);

		ResetSelf ();
	}
}
