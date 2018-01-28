using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	public int playerIndex, pigeonIndex;
	public UnitModel model;
	public GameObject shotVfx;

	private Message activeMessage;
	private Queue<Message> messageQueue;

	public void Init(int playerIndex, int pigeonIndex) {
		this.playerIndex = playerIndex;
		this.pigeonIndex = pigeonIndex;

		model.Init(playerIndex, pigeonIndex);
	}

	public void SubmitMessage(Message msg) {
		//Debug.Log("Message received at " + Board.GetCellPosition(this.transform.position));

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
				case Message.Action.shoot: {
					Vector2Int start = Board.GetCellPosition(transform.position);
					Vector2Int target = Board.GetAdjacentCellPosition(start, activeMessage.dir);

					Shoot(start, target);
				} break;
			}

			activeMessage = null;
		}
	}

	private void Move(Vector2Int start, Vector2Int end) {
		if (Board.IsValidCellPosition(end)) {
			if (!Board.self.HasObjectAt(end)) {
				Board.self.RemoveObject(this.gameObject);
				Board.self.AddObjectAt(this.gameObject, end);

				transform.position = Board.GetCellCenterWorld(end);
			}
		}
	}

	private void Shoot(Vector2Int source, Vector2Int target) {
		if (Board.IsValidCellPosition(target)) {
			GameObject obj = Board.self.GetObjectAt(target);
			if (obj != null) {
				Board.self.RemoveObjectAt (target);

				UnitController uc = obj.GetComponent<UnitController> ();
				if (uc != null) {
					Player.KillBird (uc);
				}

				GameObject.DestroyImmediate (obj);
			}

			spawnShotVfx(source, target);
		}
	}

	private void spawnShotVfx(Vector2Int source, Vector2Int target) {
			Vector3 forward = Vector3.up;
			Vector3 toTarget = (Board.GetCellCenterWorld(target) - transform.position).normalized;

			float dot = Vector3.Dot(forward, toTarget);
			float acos = Mathf.Acos(dot);
			float zRot = Mathf.Rad2Deg * acos;

			if (target.x > source.x) {
				zRot *= -1f;
			}

			GameObject.Instantiate(shotVfx, transform.position, Quaternion.Euler(0f, 0f, zRot));
	}
	
}
