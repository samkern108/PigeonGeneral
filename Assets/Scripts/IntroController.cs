using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject obstaclePrefab;
	public ResourceManager resourcePrefab;
	public Board board;

	public BirdSpawner[] birdSpawners;

	private Vector2Int[] spawnPositions = new Vector2Int[] {

	};

	private void Awake() {
		foreach (BirdSpawner bs in birdSpawners) {
			bs.enabled = false;
		}

		// Initialize all important objects just so we don't run into Awake() execution race conditons
		board.Initialize ();
		if (ResourceManager.self == null) {
			GameObject.Instantiate(resourcePrefab);
		}

		Camera.main.transform.position = Board.GetBoardCenterWorld() - 10f * Vector3.forward;

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
	}

	private KeyCode GetKey(int playerIndex, Message.Dir dir) {
		return Player.Get(playerIndex).GetKey(dir);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Return)
		||	Input.GetKeyDown(KeyCode.Space)) {
			Application.LoadLevel("Game");
		}

		for (int i = 0; i < Player.PLAYER_COUNT; ++i) {
			int playerIndex = i;
			if (!ResourceManager.self.isPlayerActive[i]) {
				for (int j = 0; j < (int)Message.Dir.COUNT; ++j) {
					if (Input.GetKeyDown(GetKey(i, (Message.Dir)j))) {
						ResourceManager.self.isPlayerActive[i] = true;

						Player.livingBirds [playerIndex] = new List<UnitController> ();

						while (true) {
							Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

							if (!Board.self.HasObjectAt(cellPosition))
							{
								Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);

								GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);
								UnitController unit = obj.GetComponent<UnitController>();

								unit.Init(playerIndex, 0);
								Player.livingBirds [playerIndex].Add (unit);

								Board.self.AddObjectAt(obj, cellPosition);

								birdSpawners[playerIndex].enabled = true;

								break;
							}
						}
					}
				}
			}
		}
	}

}
