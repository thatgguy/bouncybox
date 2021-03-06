﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControllerScript : MonoBehaviour {

	private int yellowBoxCountMax;

	private List<GameObject> yellowBoxes;
	private List<GameObject> redBoxes;

	private int redBoxCount;


	public int yellowBoxCount;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start () {
		
		YellowBoxCalc ();
		RedBoxCalc ();

	}
	
	void Update () {
		
		if (yellowBoxCount == 0) {
			RedBoxDestroy ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();

		}
		//restart level
		if (Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
	//makes list of yellow boxes
	public void YellowBoxCalc () {
		yellowBoxes = new List<GameObject> ();
		
			yellowBoxes.Clear ();
		
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("YellowBox");
		foreach (GameObject box in temp) {
			yellowBoxes.Add (box);
		}
		if (yellowBoxes.Count == null) {
			yellowBoxCount = 0;
		} else {
			yellowBoxCount = yellowBoxes.Count;
		}
	}
	//makes list of redboxes
	void RedBoxCalc () {
		redBoxes = new List<GameObject> ();
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("RedBox");
		foreach (GameObject box in temp) {
			redBoxes.Add (box);
		}
		if (redBoxes.Count == null) {
			redBoxCount = 0;
		} else {
			redBoxCount = redBoxes.Count;
		}
	}
	//destroys red boxes
	void RedBoxDestroy () {
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("RedBox");
		foreach (GameObject box in temp) {
			Destroy (box);
		}
	}
}
