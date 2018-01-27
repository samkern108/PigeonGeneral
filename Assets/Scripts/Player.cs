using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {
	public int index;
	public Vector3 launchPosition;
}

public class Player : MonoBehaviour {

	static public readonly int PLAYER_COUNT = 2;

	static public Player self { get { return _self; } }
	static private Player _self;

	private PlayerData[] playerData;

	static public PlayerData Get(int index)
	{
		Debug.Assert(index >= 0 && index < PLAYER_COUNT, "Invalid index");

		return self.playerData[index];
	}

	private void Awake() {
		if (self != null) {
			Debug.LogError("Player singleton already exists!");

			return;
		}

		_self = this;

		playerData = new PlayerData[PLAYER_COUNT];
		for (int i = 0; i < PLAYER_COUNT; ++i)
		{
			PlayerData pd = new PlayerData();
			{
				pd.index = i;
				pd.launchPosition = Board.GetBoardWorld(new Vector2(0.5f, 0f));
			}
			playerData[i] = pd;
		}
	}
}
