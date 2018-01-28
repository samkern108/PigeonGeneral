using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour {

	private static float birdLaunchInterval = 6.0f;
	private float changeActionTimer, changeDirTimer;
	private static float timer = 0.0f;

	private BirdSpawner spawner;

	void Start() {
		spawner = GetComponent<BirdSpawner> ();
		SetLaunchInterval ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer > birdLaunchInterval) {
			SetLaunchInterval ();
			timer = 0.0f;
			spawner.SpawnBird ();
		}
	}

	private void SetLaunchInterval() {
		birdLaunchInterval = Random.Range (5.0f, 8.0f);
		changeActionTimer = 1.0f;//Random.Range (2.0f, (birdLaunchInterval - 2.0f));
		changeDirTimer = 1.0f; // birdLaunchInterval - changeActionTimer;
		Invoke ("SetActionIcon",changeActionTimer);
	}

	private void SetActionIcon() {
		spawner.SetAIActionIcon ();
		Invoke ("SetDirIcon",changeDirTimer);
	}

	private void SetDirIcon() {
		spawner.SetAIDirIcon ();
	}
}
