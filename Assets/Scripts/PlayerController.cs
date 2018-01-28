using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private BirdSpawner spawner;

	void Start() {
		spawner = GetComponent<BirdSpawner> ();
	}

	void Update() {
		switch (spawner.stage) {
		case SpawnStage.Action:
			SelectAction ();
			break;
		case SpawnStage.Dir:
			SelectDir ();
			break;
		}
	}

	private KeyCode GetKey(Message.Dir dir) {
		return Player.Get(spawner.playerIndex).GetKey(dir);
	}

	private void SelectAction() {

		// Select Action
		if (Input.GetKeyDown (GetKey (Message.Dir.down))) {
			spawner.UpdateMessageAction (Message.Action.move);
		} else if (Input.GetKeyDown (GetKey (Message.Dir.up))) {
			spawner.UpdateMessageAction (Message.Action.shoot);
		} 

		// Select Unit
		else {
			SelectUnit ();
		}
	}

	private void SelectDir() {
		Message.Dir newDir = spawner.message.dir;
		bool changed = false;
		if (Input.GetKeyDown (GetKey(Message.Dir.up))) {
			newDir = Message.Dir.up;
			changed = true;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.down))) {
			newDir = Message.Dir.down;
			changed = true;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.left))) {
			newDir = Message.Dir.left;
			changed = true;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.right))) {
			newDir = Message.Dir.right;
			changed = true;
		}

		if(changed) {
			spawner.UpdateMessageDir(newDir);
		}
	}

	private void SelectUnit() {
		int targetLength = Player.livingBirds[spawner.playerIndex].Count;

		bool changed = false;
		if (Input.GetKeyDown (GetKey(Message.Dir.left))) {
			spawner.targetIndex = (spawner.targetIndex + targetLength - 1) % targetLength;
			changed = true;
		} else if (Input.GetKeyDown (GetKey(Message.Dir.right))) {
			spawner.targetIndex = (spawner.targetIndex + targetLength + 1) % targetLength;
			changed = true;
		}

		if (changed) {
			GameObject target = Player.livingBirds[spawner.playerIndex][spawner.targetIndex].gameObject;
			spawner.highlighter.SetTarget(target);
		}
	}
}
