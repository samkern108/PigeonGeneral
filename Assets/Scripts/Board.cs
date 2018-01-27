using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	static public readonly float CELL_WORLD_SIZE = 1f;
	static public readonly float HALF_CELL_WORLD_SIZE = 0.5f * CELL_WORLD_SIZE;
	static public readonly Vector2Int DIMS = new Vector2Int(10, 10);

	static public Board self { get { return _self; } }
	static private Board _self;

	private List<Object> contents;

	static public Vector3 GetCellCenterWorld(Vector2Int cellPosition)
	{
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		return new Vector3(HALF_CELL_WORLD_SIZE + cellPosition.x * CELL_WORLD_SIZE,
						   HALF_CELL_WORLD_SIZE + cellPosition.y * CELL_WORLD_SIZE,
						   0f);
	}

	static public Vector3 GetBoardCenterWorld()
	{
		return new Vector3(0.5f * DIMS.x * CELL_WORLD_SIZE,
						   0.5f * DIMS.y * CELL_WORLD_SIZE,
						   0f);
	}

	public void AddObjectAt(Object entity, Vector2Int cellPosition)
	{
		Debug.Assert(entity != null, "Invalid entity!");
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);
		Debug.Assert(GetObjectAt(cellPosition) == null, "Already an entity at cellPosition " + cellPosition);

		contents[GetIndexForPosition(cellPosition)] = entity;
	}

	public void RemoveObject(Object entity)
	{
		Debug.Assert(entity != null, "Invalid entity!");

		contents.Remove(entity);
	}

	public bool HasObjectAt(Vector2Int cellPosition)
	{
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		return (contents[GetIndexForPosition(cellPosition)] != null);
	}

	public Object GetObjectAt(Vector2Int cellPosition)
	{
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		return contents[GetIndexForPosition(cellPosition)];
	}

	private int GetIndexForPosition(Vector2Int cellPosition)
	{
		return (cellPosition.y * DIMS.x + cellPosition.x);
	}

	static private bool IsValidCellPosition(Vector2Int cellPosition)
	{
		return (cellPosition.x >= 0
			&& cellPosition.x < DIMS.x
			&& cellPosition.y >= 0
			&& cellPosition.y < DIMS.y);
	}

	private void Awake() {
		if (self != null)
		{
			Debug.LogError("Grid self already exists!");

			return;
		}
		
		_self = this;

		contents = new List<Object>(DIMS.x * DIMS.y);
		for (int i = 0; i < contents.Capacity; ++i) { contents.Add(null); }
	}
}
