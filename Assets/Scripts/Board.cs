using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using N_extensions;

public class Board : MonoBehaviour {

	static public readonly float CELL_WORLD_SIZE = 1f;
	static public readonly float HALF_CELL_WORLD_SIZE = 0.5f * CELL_WORLD_SIZE;
	static public readonly Vector2Int DIMS = new Vector2Int(10, 10);

	static public Board self { get { return _self; } }
	static private Board _self;

	private List<GameObject> contents;

	static public Vector3 GetCellCenterWorld(Vector2Int cellPosition)
	{
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		return new Vector3(HALF_CELL_WORLD_SIZE + cellPosition.x * CELL_WORLD_SIZE,
						   HALF_CELL_WORLD_SIZE + cellPosition.y * CELL_WORLD_SIZE,
						   0f);
	}

	static public bool IsValidCellPosition(Vector2Int cellPosition)
	{
		return (cellPosition.x >= 0
			&& cellPosition.x < DIMS.x
			&& cellPosition.y >= 0
			&& cellPosition.y < DIMS.y);
	}

	static public Vector2Int GetCellPosition(Vector3 worldPosition)
	{
		Vector3 cellPosition = worldPosition / CELL_WORLD_SIZE;

		return new Vector2Int((int)cellPosition.x, (int)cellPosition.y);
	}

	static public Vector2Int GetAdjacentCellPosition(Vector2Int cellPosition, Message.Dir dir) {
		Vector2Int delta = Vector2Int.zero;

		switch (dir) {
			case Message.Dir.left: {
				delta = new Vector2Int(-1, 0);
			} break;

			case Message.Dir.right: {
				delta = new Vector2Int(1, 0);
			} break;

			case Message.Dir.up: {
				delta = new Vector2Int(0, 1);
			} break;

			case Message.Dir.down: {
				delta = new Vector2Int(0, -1);
			} break;
		}

		return (cellPosition + delta);
	}

	static public Vector3 GetBoardCenterWorld()
	{
		return GetBoardWorld(0.5f * Vector2.one);
	}

	static public Vector3 GetBoardWorld(Vector2 relativePosition)
	{
		return new Vector3(relativePosition.x * DIMS.x * CELL_WORLD_SIZE,
						   relativePosition.y * DIMS.y * CELL_WORLD_SIZE,
						   0f);
	}

	public GameObject GetRandomObject()
	{
		List<GameObject> shuffledContents = contents.CreateShuffledCopy();

		return shuffledContents.FirstOrDefault(go => go != null);
	}

	public GameObject GetRandomObjectForPlayer(int playerIndex) {
		List<GameObject> shuffledContents = contents.CreateShuffledCopy();

		return shuffledContents.FirstOrDefault(go => go != null			
		&&	(go.GetComponent<UnitController>() != null)
		&&	go.GetComponent<UnitController>().playerIndex == playerIndex);
	}

	public void AddObjectAt(GameObject entity, Vector2Int cellPosition)
	{
		Debug.Assert(entity != null, "Invalid entity!");
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);
		Debug.Assert(GetObjectAt(cellPosition) == null, "Already an entity at cellPosition " + cellPosition);

		contents[GetIndexForPosition(cellPosition)] = entity;
	}

	public void RemoveObject(GameObject entity)
	{
		Debug.Assert(entity != null, "Invalid entity!");

		contents.Remove(entity);
	}

	public void RemoveObjectAt(Vector2Int cellPosition) {
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		contents.RemoveAt(GetIndexForPosition(cellPosition));
	}

	public bool HasObjectAt(Vector2Int cellPosition)
	{
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		return (contents[GetIndexForPosition(cellPosition)] != null);
	}

	public GameObject GetObjectAt(Vector3 worldPosition)
	{
		return GetObjectAt(GetCellPosition(worldPosition));
	}

	public GameObject GetObjectAt(Vector2Int cellPosition)
	{
		Debug.Assert(IsValidCellPosition(cellPosition), "Invalid cellPosition! " + cellPosition);

		return contents[GetIndexForPosition(cellPosition)];
	}

	private int GetIndexForPosition(Vector2Int cellPosition)
	{
		return (cellPosition.y * DIMS.x + cellPosition.x);
	}

	private void Awake() {
		if (self != null)
		{
			Debug.LogError("Grid self already exists!");

			return;
		}
		
		_self = this;

		contents = new List<GameObject>(DIMS.x * DIMS.y);
		for (int i = 0; i < contents.Capacity; ++i) { contents.Add(null); }
	}
}
