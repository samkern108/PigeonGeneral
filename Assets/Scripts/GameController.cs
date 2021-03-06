﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject obstaclePrefab;
	public ResourceManager resourcePrefab;
	public Board board;

	public GameObject[] spawners;

	static public GameController self { get { return _self; } } 
	static private GameController _self;

	private void Awake() {

		// Initialize all important objects just so we don't run into Awake() execution race conditons
		board.Initialize ();
		if (ResourceManager.self == null) {
			GameObject.Instantiate(resourcePrefab);
		}

		for (int i = 0; i < Player.PLAYER_COUNT; ++i) {
			if (ResourceManager.self.isPlayerActive[i]) {
				spawners[i].GetComponent<PlayerController>().enabled = true;
			}
			else {
				spawners[i].GetComponent<AIPlayer>().enabled = true;
			}
		}

		_self = this;

		Camera.main.transform.position = Board.GetBoardCenterWorld() - 10f * Vector3.forward;
		VictoryCam victoryCam = Camera.main.gameObject.AddComponent<VictoryCam>();
		victoryCam.speed = 10f;

		// obstacles
		for (int i = 0; i < 10; ++i) {
			while (true) {
				Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

				if (!Board.self.HasObjectAt(cellPosition)) {
					Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);
					
					GameObject obj = GameObject.Instantiate(obstaclePrefab, worldPosition, Quaternion.identity);

					Board.self.AddObjectAt(obj, cellPosition);	

					break;				
				}
			}
		}

		for (int i = 0; i < Player.PLAYER_COUNT; ++i) {
			Player.livingBirds [i] = new List<UnitController> ();

			for (int j = 0; j < Player.UNITS_PER_PLAYER; ++j) {
				while (true) {
					Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

					if (!Board.self.HasObjectAt(cellPosition))
					{
						Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);

						GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);
						UnitController unit = obj.GetComponent<UnitController>();

						unit.Init(i, j);
						Player.livingBirds [i].Add (unit);

						Board.self.AddObjectAt(obj, cellPosition);

						break;
					}
				}
			}
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Destroy(ResourceManager.self.gameObject);

			Application.LoadLevel("MainMenu");
		}

		int playersRemaining = 0;
		int livingPlayerIndex = -1;

		for (int i = 0; i < Player.livingBirds.Length; ++i) {
			if (Player.livingBirds[i].Count > 0) {
				++playersRemaining;
				livingPlayerIndex = i;
			}
		}

		if (playersRemaining == 1) {
			ResourceManager.self.PlaySound(SFX.victory);
			Camera.main.GetComponent<VictoryCam>().ZoomToTarget(Player.livingBirds[livingPlayerIndex][0].transform);
		}
	}

}
