using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public static float birdLaunchInterval = 6.0f;
	private static float timer = 0.0f;

	public List<BirdSpawner> aiSpawners = new List<BirdSpawner>();

	void Start() {
		birdLaunchInterval = Random.Range (5.0f, 8.0f);
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer > birdLaunchInterval) {
			birdLaunchInterval = Random.Range (5.0f, 8.0f);
			timer = 0.0f;
			foreach (BirdSpawner spawner in aiSpawners) {
				spawner.SpawnBird ();
			}
		}
	}
}
