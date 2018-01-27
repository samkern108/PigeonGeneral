using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScenario2 : MonoBehaviour {

	public GameObject unitPrefab;

	static public TestScenario2 self { get { return _self; } } 
	static private TestScenario2 _self;

	private void Awake() {
		_self = this;
	}

	static private int NUM_PLAYERS = 2;
	static private int UNITS_PER_PLAYER = 3;

	private void Start() {

		Camera.main.transform.position = Board.GetBoardCenterWorld() - 10f * Vector3.forward;

		for (int i = 0; i < NUM_PLAYERS * UNITS_PER_PLAYER; ++i)
		{
			Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

			if (!Board.self.HasObjectAt(cellPosition))
			{
				Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);

				GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);
				UnitController unit = obj.GetComponent<UnitController>();

				unit.Init(i % NUM_PLAYERS);

				Board.self.AddObjectAt(obj, cellPosition);
			}
		}
	}

}
