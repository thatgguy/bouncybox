using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour {

	[SerializeField] private float bounceForceX;
	[SerializeField] private float bounceForceY;

	public GameObject controllerObj;



	void Start () {
		//set force when bouncing
		bounceForceX = 1;
		bounceForceY = 1;

		controllerObj = GameObject.FindGameObjectWithTag ("Controller");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		
		//yellow bar to next level
		if (coll.gameObject.tag == "YellowBar") {
			SceneManager.LoadScene ("END");
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		//respike kills player
		if (coll.CompareTag("RedSpike")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
		//yellow box collectible
		if (coll.gameObject.tag == "YellowBox") {
			Destroy (coll.gameObject);
			controllerObj.gameObject.GetComponent<ControllerScript> ().yellowBoxCount -= 1;
		}
	}
		
	void OnCollisionExit2D (Collision2D coll) {
		//adding force when bouncing
		if (gameObject.GetComponent<Rigidbody2D> ().velocity.y > 0) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, bounceForceY));
		}

		if (gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -bounceForceY));
		}

		if (gameObject.GetComponent<Rigidbody2D> ().velocity.x > 0) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (bounceForceX, 0));
		}

		if (gameObject.GetComponent<Rigidbody2D> ().velocity.x < 0) {
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-bounceForceX, 0));
		}
	}
}
