using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeftButtonScript : MonoBehaviour {

	GameObject playerObj;
	// playerObjScript;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag ("Player");
	//	playerObjScript = playerObj.GetComponent<PlayerMovement> ();
	}
	
	public void MoveLeft() {
		playerObj.GetComponent<PlayerMovement>().MoveLeft ();
	}
}
