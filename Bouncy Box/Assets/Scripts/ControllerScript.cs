using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour {

	private int yellowBoxCountMax;
	private int redBoxCount;
	private int totalLevels;

	private List<GameObject> yellowBoxes;
	private List<GameObject> redBoxes;

	[SerializeField] private Text deathCountText;
	[SerializeField] private Text levelText;

	[SerializeField] private Text pauseResume;
	[SerializeField] private Text pauseMain;
	[SerializeField] private Text pauseQuit;

	public int yellowBoxCount;
	public int deathCounter;

	void Start () {
		PlayerPrefs.SetString ("currentLevel", SceneManager.GetActiveScene ().name);
		totalLevels = 15;
		YellowBoxCalc ();
		RedBoxCalc ();
		deathCounter = PlayerPrefs.GetInt("PlayerDeaths");
		deathCountText.text = "Deaths:" + deathCounter.ToString ();
		levelText.text = SceneManager.GetActiveScene ().name + "/" + totalLevels;
	}
	
	void Update () {
		
		if (yellowBoxCount == 0) {
			RedBoxDestroy ();
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

	public void AddDeath () {
		deathCounter += 1;
		PlayerPrefs.SetInt ("PlayerDeaths", deathCounter);
	}
}
