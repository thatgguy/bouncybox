using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour {

	[SerializeField] private float bounceForceX;
	[SerializeField] private float bounceForceY;
	[SerializeField] bool camBound;
	private Rigidbody2D rb;

	public GameObject controllerObj;



	void Start () {
		//set force when bouncing
		bounceForceX = 2;
		bounceForceY = 2;

		rb = gameObject.GetComponent<Rigidbody2D> ();

		controllerObj = GameObject.FindGameObjectWithTag ("Controller");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D coll) {
		
		//yellow bar to next level
		if (coll.gameObject.tag == "YellowBar") {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		}
		//portal collision
		if (coll.gameObject.tag == "LeftPortal" || coll.gameObject.tag == "RightPortal") {
			Debug.Log ("yes");
			gameObject.transform.position = new Vector2 (coll.gameObject.GetComponent<WallScript> ().portalOutX, gameObject.transform.position.y);
			rb.velocity *= -1;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		//respike kills player
		if (coll.CompareTag("RedSpike")) {
			controllerObj.GetComponent<ControllerScript> ().AddDeath();
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
		//yellow box collectible
		if (coll.gameObject.tag == "YellowBox") {
			Destroy (coll.gameObject);
			controllerObj.gameObject.GetComponent<ControllerScript> ().yellowBoxCount -= 1;
		}

	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "MainCamera" && camBound) {
			controllerObj.GetComponent<ControllerScript> ().AddDeath();
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
		
	void OnCollisionExit2D (Collision2D coll) {
		//adding force when bouncing
		if (rb.velocity.y > 0) {
			rb.AddForce (new Vector2 (0, bounceForceY));
		}

		if (rb.velocity.y < 0) {
			rb.AddForce (new Vector2 (0, -bounceForceY));
		}

		if (rb.velocity.x > 0) {
			rb.AddForce (new Vector2 (bounceForceX, 0));
		}

		if (rb.velocity.x < 0) {
			rb.AddForce (new Vector2 (-bounceForceX, 0));
		}
	}
}
