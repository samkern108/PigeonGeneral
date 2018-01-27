using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
	public enum Type { move, shoot };
	public enum Dir { left, right, down, up };

	public Type type;
	public Dir dir;

	public Message(Type type, Dir dir) {
		this.type = type;
		this.dir = dir;
	}
}