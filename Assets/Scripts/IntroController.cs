using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject obstaclePrefab;
	public ResourceManager resourcePrefab;
	public Board board;

	public BirdSpawner[] birdSpawners;
	public PlayerController[] playerControllers;
	public GameObject[] instructionUIs;
	public GameObject[] actionUIs;
	public Text enterText;

	private Vector2Int[] spawnPositions = new Vector2Int[] {
		new Vector2Int(1, 6),
		new Vector2Int(6, 6),
		new Vector2Int(6, 1),
		new Vector2Int(1, 1),
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
		for (int i = 0; i < Board.DIMS.x; ++i) {
			Vector2Int cellPosition = new Vector2Int(i, Board.DIMS.y / 2 - i % 2);

			if (!Board.self.HasObjectAt(cellPosition)) {
				Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);
				
				GameObject obj = GameObject.Instantiate(obstaclePrefab, worldPosition, Quaternion.identity);

				Board.self.AddObjectAt(obj, cellPosition);			
			}
		}
		for (int i = 0; i < Board.DIMS.y; ++i) {
			Vector2Int cellPosition = new Vector2Int(Board.DIMS.x / 2 - i % 2, i);

			if (!Board.self.HasObjectAt(cellPosition)) {
				Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);
				
				GameObject obj = GameObject.Instantiate(obstaclePrefab, worldPosition, Quaternion.identity);

				Board.self.AddObjectAt(obj, cellPosition);
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
							Vector2Int cellPosition = spawnPositions[playerIndex];

							if (!Board.self.HasObjectAt(cellPosition))
							{
								Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);

								GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);
								UnitController unit = obj.GetComponent<UnitController>();

								unit.Init(playerIndex, 0);
								Player.livingBirds [playerIndex].Add (unit);

								Board.self.AddObjectAt(obj, cellPosition);

								instructionUIs[playerIndex].SetActive(false);
								actionUIs[playerIndex].SetActive(true);

								enterText.enabled = true;
								birdSpawners[playerIndex].enabled = true;
								playerControllers[playerIndex].enabled = true;

								break;
							}
						}
					}
				}
			}
		}
	}

}
