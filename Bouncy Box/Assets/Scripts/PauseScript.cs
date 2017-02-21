using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

	public bool isPaused;

	GameObject playerObj;
	PlayerMovement moveScript;

	[SerializeField] Canvas pauseMenu;

	void Start () {
		
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		moveScript = playerObj.GetComponent<PlayerMovement> ();
		bool isPaused = false;
	}

	void Update () {
		if (Input.GetButtonDown("Pause")) {
			if (!isPaused) { 
				PauseGame ();
			} else {
				UnpauseGame ();
			}
		}


	}

	public void PauseGame () {
		
		Time.timeScale = 0;
		//activate Pause UI
		pauseMenu.gameObject.SetActive(true);
		moveScript.isPaused = true;
		isPaused = true;
	}

	public void UnpauseGame() {
		
		Time.timeScale = 1;
		GetComponentInChildren<PauseMenuScript> ().ChangeActive ("reset");
		pauseMenu.gameObject.SetActive(false);
		moveScript.isPaused = false;
		isPaused = false;
	}
}
