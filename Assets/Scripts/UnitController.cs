using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	public delegate void OnDeath(UnitController unit);

	private const float FLEX_DELAY = 3f;

	public int playerIndex, pigeonIndex;
	public UnitModel model;
	public GameObject shotVfx, featherVfx;

	private Message activeMessage;
	private Queue<Message> messageQueue;

	private Animate animate;
	private OnDeath onDeath;

	private float timeSinceLastMessage = 0f;
	private Vector2 origScale;
	private Color origColor;

	public void Init(int playerIndex, int pigeonIndex) {
		this.playerIndex = playerIndex;
		this.pigeonIndex = pigeonIndex;

		model.Init(playerIndex, pigeonIndex);
		animate = GetComponentInChildren<Animate>();

		origScale = new Vector2(model.pigeon.transform.localScale.x, model.pigeon.transform.localScale.y);
		origColor = model.pigeon.color;
	}

	public void SubmitMessage(Message msg) {
		//Debug.Log("Message received at " + Board.GetCellPosition(this.transform.position));

		messageQueue.Enqueue(msg);

		timeSinceLastMessage = 0f;
	}

	public void RegisterOnDeath(OnDeath callback) {
		onDeath = callback;
	}

	public void Die() {
		ResourceManager.self.PlaySound(SFX.birdCall);

		timeSinceLastMessage = -999f;

		model.pigeon.sprite = ResourceManager.self.GetPigeonSprite (playerIndex, PigeonPose.Hurt);

		ResetVisuals();
		animate.AnimateToColor (model.pigeon.color, Color.red, 5.0f, Animate.RepeatMode.Once);
		Invoke ("DestroySelf", 2.0f);

		onDeath(this);
	}

	public void DestroySelf() {
		GameObject.DestroyImmediate (this.gameObject);
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
		else {
			timeSinceLastMessage += Time.deltaTime;

			if (timeSinceLastMessage > FLEX_DELAY) {
				model.pigeon.sprite = ResourceManager.self.GetPigeonSprite(playerIndex, Random.Range(0f, 1f) > 0.5f ? PigeonPose.Flex1 : PigeonPose.Flex2);

				ResetVisuals();
				animate.AnimateToSize(new Vector2(0.5f, 0.5f), new Vector2(.55f, .55f), 0.5f, Animate.RepeatMode.OnceAndBack);

				timeSinceLastMessage = FLEX_DELAY / 2f;
			}
		}
	}

	// So the invoke call can use it :|
	private Vector2Int endPosition;
	private void Move(Vector2Int start, Vector2Int end) {
		if (Board.IsValidCellPosition (end)) {
			if (!Board.self.HasObjectAt (end)) {
				Board.self.RemoveObject (this.gameObject);

				Vector3 newPosition = Board.GetCellCenterWorld (end);

				ResetVisuals();
				animate.AnimateToPosition (transform.position, newPosition, .3f, Animate.RepeatMode.Once);
				// TODO(samkern): Simple shader to animate this to a flat white? :)
				//animate.AnimateToColor (model.pigeon.color, Color.red, .2f, Animate.RepeatMode.OnceAndBack);
				model.pigeon.sprite = ResourceManager.self.GetPigeonSprite (playerIndex, PigeonPose.Move);

				spawnDirectedVfx(featherVfx, end, start);

				endPosition = end;
				Invoke ("StopMoving", .3f);
			} else {
				InvalidMove ();
			}
		} else {
			InvalidMove ();
		}
	}

	private void InvalidMove() {
		ResetVisuals();
		animate.AnimateToColor (model.pigeon.color, Color.red, .2f, Animate.RepeatMode.OnceAndBack);
		//model.pigeon.sprite = ResourceManager.self.GetPigeonSprite (playerIndex, PigeonPose.Move);
	}

	// I do this to give some time for the bird to animate into a new position. Also i frames feel cool.
	private void StopMoving() {
		Board.self.AddObjectAt(this.gameObject, endPosition);
		model.pigeon.sprite = ResourceManager.self.GetPigeonSprite (playerIndex, PigeonPose.Idle);

		this.transform.position = model.transform.position;
		model.transform.localPosition = Vector3.zero;
	}

	private void Shoot(Vector2Int source, Vector2Int target) {
		if (Board.IsValidCellPosition(target)) {
			GameObject obj = Board.self.GetObjectAt(target);
			if (obj != null) {
				Board.self.RemoveObjectAt (target);

				UnitController uc = obj.GetComponent<UnitController> ();
				if (uc != null) {
					uc.Die();
					Player.KillBird (uc);
				} else {
					GameObject.DestroyImmediate (obj);
				}
			}

			ResetVisuals();
			animate.AnimateToRotation (Quaternion.identity, Quaternion.Euler(0f, 0f, -30), .2f, Animate.RepeatMode.OnceAndBack);
			model.pigeon.sprite = ResourceManager.self.GetPigeonSprite(playerIndex, PigeonPose.Shoot);
			Invoke ("StopShoot", .5f);
			spawnDirectedVfx(shotVfx, source, target);

			ResourceManager.self.PlaySound(SFX.shot);
		}
	}

	private void StopShoot() {
		model.pigeon.sprite = ResourceManager.self.GetPigeonSprite(playerIndex, PigeonPose.Idle);
	}

	private void spawnDirectedVfx(GameObject vfx, Vector2Int source, Vector2Int target) {
			Vector3 forward = Vector3.up;
			Vector3 toTarget = (Board.GetCellCenterWorld(target) - transform.position).normalized;

			float dot = Vector3.Dot(forward, toTarget);
			float acos = Mathf.Acos(dot);
			float zRot = Mathf.Rad2Deg * acos;

			if (target.x > source.x) {
				zRot *= -1f;
			}

			GameObject.Instantiate(vfx, transform.position, Quaternion.Euler(0f, 0f, zRot));
	}

	private void ResetVisuals() {
		model.pigeon.color = origColor;
		model.pigeon.transform.localScale = origScale;
	}
	
}
