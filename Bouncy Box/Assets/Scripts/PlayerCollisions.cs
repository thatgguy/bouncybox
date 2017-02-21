using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour {

	[SerializeField] private float bounceForceX;
	[SerializeField] private float bounceForceY;
	[SerializeField] bool camBound;
	[SerializeField] Sprite spriteRend;
	private Rigidbody2D rb;
	PlayerParticlesScript playerParticles;
	public GameObject controllerObj;
	Camera mainCam;

	void Start () {
		mainCam = Camera.main;
		playerParticles = gameObject.GetComponentInChildren<PlayerParticlesScript> ();
		//set force when bouncing
		bounceForceX = 2;
		bounceForceY = 2;

		rb = gameObject.GetComponent<Rigidbody2D> ();

		controllerObj = GameObject.FindGameObjectWithTag ("Controller");
	}
	

	void OnCollisionEnter2D (Collision2D coll) {
		
		//yellow bar to next level
		if (coll.gameObject.tag == "YellowBar") {
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		}
		//portal collision
		if (coll.gameObject.tag == "LeftPortal" || coll.gameObject.tag == "RightPortal") {
			gameObject.transform.position = new Vector2 (coll.gameObject.GetComponent<WallScript> ().portalOutX, gameObject.transform.position.y);
			rb.velocity *= -1;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		//respike kills player
		if (coll.CompareTag("RedSpike")) {
			PlayerDeath ();
		}
		//yellow box collectible
		if (coll.gameObject.tag == "YellowBox") {
			Destroy (coll.gameObject);
			controllerObj.gameObject.GetComponent<ControllerScript> ().yellowBoxCount -= 1;
			playerParticles.yellowBoxParticle ();
		}

	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "MainCamera" && camBound) {
			PlayerDeath ();
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

	void PlayerDeath () {
		playerParticles.deathParticles ();
		controllerObj.GetComponent<ControllerScript> ().AddDeath();
		mainCam.GetComponent<CameraScript> ().camSpeed = 0;
		gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
		rb.isKinematic = true;
	}
}
