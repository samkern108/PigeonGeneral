using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject unitPrefab;

	public UnitController[] unitControllers;

	static public GameController self { get { return _self; } } 
	static private GameController _self;

	private void Awake() {
		_self = this;
	}

	static private int NUM_PLAYERS = 2;
	static private int UNITS_PER_PLAYER = 4;

	private void Start() {

		unitControllers = new UnitController[NUM_PLAYERS * UNITS_PER_PLAYER];

		Camera.main.transform.position = Board.GetBoardCenterWorld() - 10f * Vector3.forward;

		for (int i = 0; i < NUM_PLAYERS * UNITS_PER_PLAYER; ++i) {
			while (true) {
				Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

				if (!Board.self.HasObjectAt(cellPosition))
				{
					Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);

					GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);
					UnitController unit = obj.GetComponent<UnitController>();

					unit.Init(i % Player.PLAYER_COUNT, (int)Mathf.Floor(i / Player.PLAYER_COUNT));
					unitControllers[i] = unit;

					Board.self.AddObjectAt(obj, cellPosition);

					break;
				}
			}
		}
	}

}
