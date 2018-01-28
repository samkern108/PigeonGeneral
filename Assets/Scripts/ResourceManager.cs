using UnityEngine;
using System.Collections;
using System.IO;

public enum PigeonPose { Idle = 0, Move = 1, Shoot = 2, Hurt = 3, Flex1 = 4, Flex2 = 5 };
public enum SFX { shot, flap, birdCall };

public class ResourceManager : MonoBehaviour {

	public static ResourceManager self;

	private void Awake() {
		self = this;
		
		pigeons = new Sprite[4][] {
			pigeons0, pigeons1, pigeons2, pigeons3
		};

		DontDestroyOnLoad(this.gameObject);
	}

	public Sprite upArrow, downArrow, leftArrow, rightArrow;
	public Sprite attackIcon, moveIcon;
	public Sprite questionIcon;
	public Sprite[] pigeons0 = new Sprite[4];
	public Sprite[] pigeons1 = new Sprite[4];
	public Sprite[] pigeons2 = new Sprite[4];
	public Sprite[] pigeons3 = new Sprite[4];
	public Sprite[][] pigeons;



	public AudioClip sfxShot;
	public AudioClip sfxWingFlap;
	public AudioClip[] sfxBirdCalls;

	public Sprite GetPigeonSprite(int playerIndex, PigeonPose pose) {
		if ((int)pose > (pigeons[playerIndex].Length - 1))
			Debug.LogError ("We only have so many pigeons >:(");
		
		return pigeons[playerIndex][(int)pose];
	}
	
	public void PlaySound(SFX sfx) {
		GameObject go = new GameObject();

		go.AddComponent<DestroyAfter>().destroyTime = 5f;

		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = GetClipForSFX(sfx);
		source.Play();

		DontDestroyOnLoad(go);
	}

	public AudioClip GetClipForSFX(SFX sfx) {
		switch (sfx) {
			case SFX.shot:
				return sfxShot;
			case SFX.flap:
				return sfxWingFlap;
			default:
			case SFX.birdCall:
				return sfxBirdCalls[Random.Range(0, sfxBirdCalls.Length)];
		}
	}
}
