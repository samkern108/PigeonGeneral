using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	private Message activeMessage;
	private Queue<Message> messageQueue;

	public void SubmitMessage(Message msg) {
		Debug.Log("Message received at " + Board.GetCellPosition(this.transform.position));

		messageQueue.Enqueue(msg);
	}

	private void Awake() {
		messageQueue = new Queue<Message>();
	}

	private void Update() {
		if (activeMessage == null
		&&	messageQueue.Count > 0) {
			activeMessage = messageQueue.Dequeue();
		}

		if (activeMessage != null) {
			switch (activeMessage.action) {
				case Message.Action.move: {
					Vector2Int start = Board.GetCellPosition(transform.position);
					Vector2Int end = Board.GetAdjacentCellPosition(start, activeMessage.dir);

					Move(start, end);
				} break;
			}

			activeMessage = null;
		}
	}

	private void Move(Vector2Int start, Vector2Int end)
	{
		if (!Board.self.HasObjectAt(end)) {
			Board.self.RemoveObject(this.gameObject);
			Board.self.AddObjectAt(this.gameObject, end);

			transform.position = Board.GetCellCenterWorld(end);
		}
	}
	
}
