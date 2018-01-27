using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject obstaclePrefab;

	public UnitController[] unitControllers;

	static public GameController self { get { return _self; } } 
	static private GameController _self;

	private void Awake() {
		_self = this;
	}

	private void Start() {

		unitControllers = new UnitController[Player.PLAYER_COUNT * Player.UNITS_PER_PLAYER];

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
		
		for (int i = 0; i < Player.PLAYER_COUNT * Player.UNITS_PER_PLAYER; ++i) {
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
