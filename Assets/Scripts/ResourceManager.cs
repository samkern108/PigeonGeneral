using UnityEngine;
using System.Collections;
using System.IO;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager self;

	public void Awake() {
		self = this;
	}

	public Sprite upArrow, downArrow, leftArrow, rightArrow;
	public Sprite attackIcon, moveIcon;
	public Sprite questionIcon;
	public Sprite[] pigeons = new Sprite[4];

	public Sprite GetPigeonSprite(int pigeonNum) {
		if (pigeonNum > (pigeons.Length - 1))
			Debug.LogError ("We only have so many pigeons >:(");
		
		return pigeons[pigeonNum];
	}
}
