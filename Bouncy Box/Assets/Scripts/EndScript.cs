using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {

	GameObject music;

	void Start () {
		music = GameObject.FindGameObjectWithTag ("Music");
		Destroy (music.gameObject);
	}

	void Update () {
		if (Input.GetButtonDown("SubmitJump")) {
			SceneManager.LoadScene("StartMenu");
		}
	}
}
