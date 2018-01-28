using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
	public enum Action { shoot, move };
	public enum Dir { left, right, down, up, COUNT };

	public Action action;
	public Dir dir;

	public Message() {
		GenerateRandom ();
	}

	public Message(Action type, Dir dir) {
		this.action = type;
		this.dir = dir;
	}

	private void GenerateRandom() {
		this.action = (Action)Random.Range (0, 2);
		this.dir = (Dir)Random.Range (0, 4);
	}
}