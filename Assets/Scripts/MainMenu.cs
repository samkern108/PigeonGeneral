using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	private readonly Vector2 SMALL = new Vector2(1f, 1f);
	private readonly Vector2 LARGE = new Vector2(1.25f, 1.25f);
	private const float PULSE_TIME = 0.35f;

	public Animate playButton;
	public Animate creditsButton;

	private Animate currentButton;

	private void Awake() {
		SetActiveButton(playButton);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)
		||	Input.GetKeyDown(KeyCode.D)) {
			ResourceManager.self.PlaySound(SFX.birdCall);
			SetActiveButton(GetOtherButton(currentButton));
		}

		if (Input.GetKeyDown(KeyCode.W)
		||	Input.GetKeyDown(KeyCode.Space)
		||	Input.GetKeyDown(KeyCode.Return)) {
			ResourceManager.self.PlaySound(SFX.flap);
			if (currentButton == playButton) {
				Application.LoadLevel("Game");
			}
			else if (currentButton == creditsButton) {
				Application.LoadLevel("Credits");
			}
		}
	}

	private Animate GetOtherButton(Animate button) {
		return (button == playButton ? creditsButton : playButton);
	}

	private void SetActiveButton(Animate button) {
		currentButton = button;

		GetOtherButton(button).StopAnimating();
		GetOtherButton(button).transform.localScale = Vector3.one;

		currentButton.AnimateToSize(SMALL, LARGE, PULSE_TIME, Animate.RepeatMode.PingPong);
	}
}
