using UnityEngine;
using System.Collections;
using System.IO;

public enum PigeonPose { Idle = 0, Move = 1, Shoot = 2, Hurt = 3 };

public class ResourceManager : MonoBehaviour {

	public static ResourceManager self;

	public void Initialize() {
		self = this;
		
		pigeons = new Sprite[4][] {
			pigeons0, pigeons1, pigeons2, pigeons3
		};
	}

	public Sprite upArrow, downArrow, leftArrow, rightArrow;
	public Sprite attackIcon, moveIcon;
	public Sprite questionIcon;
	public Sprite[] pigeons0 = new Sprite[4];
	public Sprite[] pigeons1 = new Sprite[4];
	public Sprite[] pigeons2 = new Sprite[4];
	public Sprite[] pigeons3 = new Sprite[4];
	public Sprite[][] pigeons;

	public Sprite GetPigeonSprite(int playerIndex, PigeonPose pose) {
		if ((int)pose > (pigeons.Length - 1))
			Debug.LogError ("We only have so many pigeons >:(");
		
		return pigeons[playerIndex][(int)pose];
	}
}
