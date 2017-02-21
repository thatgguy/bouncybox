using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	public bool isOn;
	GameObject music;
	private Text[] pauseButtons;

	[SerializeField] private Text pauseResume;
	[SerializeField] private Text pauseMain;
	[SerializeField] private Text pauseQuit;

	[SerializeField] private Text activeText;
	private int activeTextNum;

	void Start () {
		pauseButtons = new Text[3];
		music = GameObject.FindGameObjectWithTag ("Music");
		pauseButtons [0] = pauseResume;
		pauseButtons [1] = pauseMain;
		pauseButtons [2] = pauseQuit;

		activeTextNum = 0;
		activeText = pauseButtons[activeTextNum];
		activeText.color = new Color (0, 1, 0);


	}

	void Update () {
		if (Input.GetButtonDown ("Up")) {
			ChangeActive ("up");
		}

		if (Input.GetButtonDown ("Down")) {
			ChangeActive ("down");
		}

		if (Input.GetButtonDown("SubmitJump") || Input.GetKeyDown(KeyCode.Return)) {
			ActivateButton();
		}
	}

	public void ChangeActive (string Dir){
		activeText.color = new Color (1, 1, 1);

		if (Dir == "up") {
			activeTextNum -= 1;
			if (activeTextNum < 0) {
				activeTextNum = pauseButtons.Length - 1;
			}
		}

		if (Dir == "down") {
			activeTextNum += 1;
			if (activeTextNum > pauseButtons.Length - 1) {
				activeTextNum = 0;
			}
		}

		if (Dir == "reset") {
			activeTextNum = 0;
		}

		activeText = pauseButtons [activeTextNum];
		activeText.color = new Color (0, 1, 0);
	}

	void ActivateButton() {
		if (activeText == pauseResume) {
			GetComponentInParent<PauseScript> ().UnpauseGame ();
		}

		if (activeText == pauseMain) {
			GetComponentInParent<PauseScript> ().UnpauseGame ();
			Destroy (music.gameObject);
			SceneManager.LoadScene ("StartMenu");
		}

		if (activeText == pauseQuit) {
			Application.Quit ();
		}
	}
}
