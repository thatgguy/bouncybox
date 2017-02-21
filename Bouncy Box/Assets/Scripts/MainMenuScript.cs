using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	Text[] MainMenuSelection;
	[SerializeField] Text title;
	[SerializeField] Text newGameBut;
	[SerializeField] Text continueBut;
	[SerializeField] Text controlsBut;
	[SerializeField] Text quitBut;
	[SerializeField] bool canContinue;
	private Text activeText;
	private int activeTextNum;
	[SerializeField] Image controlsScreen;
	bool controlsActive;
	[SerializeField] GameObject musicController;

	void Start () {
		controlsScreen.gameObject.SetActive (false);
		controlsActive = false;
		if (PlayerPrefs.GetInt ("saveGame") == 1) {
			canContinue = true;
		} else {
			canContinue = false;
		}
		PopMenuArray ();

		title.supportRichText = true;
		title.text = "<color=#ffffff>Bouncy</color> <color=#00ff00>Box</color> <color=#ffffff>Yeah</color>";
	}

	void Update () {
		if (!controlsActive) {
			if (Input.GetButtonDown ("Up")) {
				ChangeActive ("up");
			}

			if (Input.GetButtonDown ("Down")) {
				ChangeActive ("down");
			}
		}
		if (Input.GetButtonDown("SubmitJump") || Input.GetKeyDown(KeyCode.Return)) {
			ActivateButton();
		}
	}
		
	void PopMenuArray () {
		if (!canContinue) {
			MainMenuSelection = new Text[3];
			MainMenuSelection [0] = newGameBut;
			MainMenuSelection [1] = controlsBut;
			MainMenuSelection [2] = quitBut;
			continueBut.color = new Color32 (0x8E, 0x8E, 0x8E, 0xFF);
		} else {
			MainMenuSelection = new Text[4];
			MainMenuSelection [0] = newGameBut;
			MainMenuSelection [1] = continueBut;
			MainMenuSelection [2] = controlsBut;
			MainMenuSelection [3] = quitBut;
		}
		activeTextNum = 0;
		activeText = MainMenuSelection[activeTextNum];
		activeText.color = new Color (0, 1, 0);
	}

	public void ChangeActive (string Dir){
		activeText.color = new Color (1, 1, 1);

		if (Dir == "up") {
			activeTextNum -= 1;
			if (activeTextNum < 0) {
				activeTextNum = MainMenuSelection.Length - 1;
			}
		}

		if (Dir == "down") {
			activeTextNum += 1;
			if (activeTextNum > MainMenuSelection.Length - 1) {
				activeTextNum = 0;
			}
		}

		if (Dir == "reset") {
			activeTextNum = 0;
		}

		activeText = MainMenuSelection [activeTextNum];
		activeText.color = new Color (0, 1, 0);
	}

	void ActivateButton () {
		if (!controlsActive) {
			if (activeText == newGameBut) {
				PlayerPrefs.SetInt ("saveGame", 1);
				PlayerPrefs.SetInt ("PlayerDeaths", 0);
				musicController.GetComponent<MusicScript> ().SelectTrack ();
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			}
			if (activeText == continueBut) {
				musicController.GetComponent<MusicScript> ().SelectTrack ();
				SceneManager.LoadScene (PlayerPrefs.GetString ("currentLevel"));
			}
			if (activeText == controlsBut) {
				controlsScreen.gameObject.SetActive (true);
				controlsActive = true;
			}
			if (activeText == quitBut) {
				Application.Quit ();
			}
		} else {
			controlsScreen.gameObject.SetActive (false);
			controlsActive = false;
		}
	}
}
