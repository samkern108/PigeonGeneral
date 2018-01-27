using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScenario : MonoBehaviour {

	public GameObject unitPrefab;

	private void Start() {
		
		Camera.main.transform.position = Board.GetBoardCenterWorld() - 10f * Vector3.forward;

		for (int i = 0; i < 1; ++i)
		{
			Vector2Int cellPosition = new Vector2Int(Random.Range(0, Board.DIMS.x), Random.Range(0, Board.DIMS.y));

			if (!Board.self.HasObjectAt(cellPosition))
			{
				Vector3 worldPosition = Board.GetCellCenterWorld(cellPosition);
				
				GameObject obj = GameObject.Instantiate(unitPrefab, worldPosition, Quaternion.identity);

				Board.self.AddObjectAt(obj, cellPosition);
			}
		}
	}

}
