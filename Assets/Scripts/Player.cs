﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	public int index;
	public Vector3 launchPosition;
	public KeyCode[] inputs;

	public KeyCode GetKey(Message.Dir dir) {
		return inputs[(int)dir];
	}
}

public class Player : MonoBehaviour {

	static public readonly int PLAYER_COUNT = 4;

	static public Player self { get { return _self; } }
	static private Player _self;

	public PlayerData[] playerData = new PlayerData[PLAYER_COUNT];

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
	}
}
