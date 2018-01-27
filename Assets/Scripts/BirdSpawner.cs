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
		Action, Dir
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

	private void UpdateMessageAction(Message.Action action) {
		message.action = action;
		MessageUI.self.SetActionSelectionChoice (action);
		stage = SelectionStage.Dir;
		MessageUI.self.SetSelectionStage (stage);
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

	private void UpdateMessageDir(Message.Dir dir) {
		message.dir = dir;
		MessageUI.self.SetDirSelectionChoice (dir);
		SpawnBird();
	}

	private void SpawnBird() {
		GameObject flyingBird = Instantiate (birdPrefab);

		target = new GameObject();
		target.name = "Target";
		target.transform.position = Board.self ? Board.self.GetRandomObjectForPlayer(playerIndex).transform.position : new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0f);

		launchPoint = new GameObject();
		launchPoint.name = "Launch";
		launchPoint.transform.position = Board.self ? Board.GetBoardWorld(new Vector2(0.5f, 0f)) : new Vector3(0f, -5f, 0.5f);

		flyingBird.GetComponent <FlyingBird>().Initialize(target, launchPoint, message);

		ResetSelf ();
	}
}
