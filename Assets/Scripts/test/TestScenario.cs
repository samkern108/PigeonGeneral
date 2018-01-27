using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScenario : MonoBehaviour {

	public GameObject unitPrefab;

	static public TestScenario self { get { return _self; } } 
	static private TestScenario _self;

	private void Awake() {
		_self = this;
	}

	private void Start() {
		
		Camera.main.transform.position = Board.GetBoardCenterWorld() - 10f * Vector3.forward;

		for (int i = 0; i < 12; ++i) {
			while (true) {
				Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

				if (!Board.self.HasObjectAt(cellPosition))
				{
					Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);
					
					GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);
					UnitController unit = obj.GetComponent<UnitController>();

					unit.Init(i % Player.PLAYER_COUNT);

					Board.self.AddObjectAt(obj, cellPosition);

					break;
				}
			}
		}
	}

}
