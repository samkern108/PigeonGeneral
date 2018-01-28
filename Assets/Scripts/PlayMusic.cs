using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

	public AudioClip clip;
	public AudioSource source;

	// Use this for initialization
	void Start () {
		source.clip = clip;
		source.loop = true;
		source.Play();
	}
}
