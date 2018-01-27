using UnityEngine;
using System.Collections;
using System.IO;

public class ResourceManager : MonoBehaviour {

	public static ResourceManager self;

	public void Start() {
		self = this;
	}

	public Sprite upArrow, downArrow, leftArrow, rightArrow;
	public Sprite attackIcon, moveIcon;
}
