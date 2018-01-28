using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignRandomSprite : MonoBehaviour {

	public Sprite[] sprites;

	private void Start() {
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		sr.sprite = sprites[Random.Range(0, sprites.Length)];
	}
}
