using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RightButtonScript : MonoBehaviour {

	GameObject playerObj;
	// playerObjScript;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		//	playerObjScript = playerObj.GetComponent<PlayerMovement> ();
	}

	/*void Update () {
			if (Input.touchCount >= 1) {
				MoveRight ();
			}
		}*/



	public void MoveRight() {
		playerObj.GetComponent<PlayerMovement>().MoveRight ();
	}
}
