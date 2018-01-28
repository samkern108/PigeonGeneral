using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

	public bool animateSize;
	public Vector2 sizeStart;
	public Vector2 sizeFinish;
	public float sizeDuration;
	public Animate.RepeatMode sizeRepeatMode;

	// Use this for initialization
	void Start () {
		Animate animate = this.gameObject.GetComponent<Animate>();

		if (animate != null) {
			if (animateSize) {
				animate.AnimateToSize(sizeStart, sizeFinish, sizeDuration, sizeRepeatMode);
			}
		}
	}
}
