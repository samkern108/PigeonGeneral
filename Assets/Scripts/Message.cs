using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
	public enum Type { shoot, move };
	public enum Dir { left, right, down, up };

	public Type type;
	public Dir dir;

	public Message() {
		GenerateRandom ();
	}

	public Message(Type type, Dir dir) {
		this.type = type;
		this.dir = dir;
	}

	private void GenerateRandom() {
		this.type = (Type)Random.Range (0, 2);
		this.dir = (Dir)Random.Range (0, 4);
	}
}