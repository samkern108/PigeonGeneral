using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private static float birdLaunchInterval = 5.0f;
	private static float timer = 0.0f;

	public BirdSpawner[] birdSpawners = new BirdSpawner[4];

	void Update () {
		timer += Time.deltaTime;
		if (timer > birdLaunchInterval) {
			timer = 0.0f;
			foreach (BirdSpawner spawner in birdSpawners) {
				spawner.SpawnBird ();
			}
		}
	}
}
