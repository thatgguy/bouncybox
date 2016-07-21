using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpButtonScript : MonoBehaviour {

	GameObject playerObj;
	// playerObjScript;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		//	playerObjScript = playerObj.GetComponent<PlayerMovement> ();
	}
	/*
	void Update () {
		
	}*/

	public void PlayerJump() {
		playerObj.GetComponent<PlayerMovement>().Jump();
	}

	public void StopJump() {
		playerObj.GetComponent<PlayerMovement> ().StopJump ();
	}
}