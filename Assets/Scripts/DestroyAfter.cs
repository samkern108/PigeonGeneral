using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

	public float destroyTime = 1f;

	// Use this for initialization
	void Start () {
		Invoke("DestroyMe", destroyTime);
	}

	void DestroyMe() {
		Destroy(this.gameObject);
	}
}
