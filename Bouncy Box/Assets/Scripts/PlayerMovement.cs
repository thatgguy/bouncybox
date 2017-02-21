using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody2D rb;
	public bool movingRight;
	public bool isPaused;
	GameObject controllerObj;

	[SerializeField] private float jumpSpeed;
	[SerializeField] private float moveSpeed;
	[SerializeField] private bool secondJump;
	[SerializeField] private bool isJumping;
	[SerializeField] private float jumpTimer;
	[SerializeField] GameObject groundPlat;
	[SerializeField] float groundPlatY;
	[SerializeField] float boxStartPosY;
	[SerializeField] PlayerParticlesScript playerParticleScript;
	[SerializeField] TrailRenderer playerTrail;
	bool trailIsTwo;

	// Use this for initialization
	void Start () {
		controllerObj = GameObject.FindGameObjectWithTag ("Controller");
		playerParticleScript = GetComponentInChildren<PlayerParticlesScript> ();
		playerTrail = GetComponentInChildren<TrailRenderer> ();
		playerTrail.numCornerVertices = 0;
		trailIsTwo = false;
		

		CalcStartPoint ();
		rb = GetComponent<Rigidbody2D> ();
		jumpSpeed = 15; //amount of force added when jumping
		moveSpeed = 10; //speed when moving left/right
		jumpTimer = .2f; //amount of time the player can hold the jump button.5
	}

	void Update () {
		if (!isPaused) {
			//Move left
			if (Input.GetButtonDown ("Left") ) {
				MoveLeft ();
			}

			//Move Right
			if (Input.GetButtonDown ("Right")) {
				MoveRight ();
			}

			//Jump
			if (Input.GetButtonDown ("SubmitJump") && secondJump) {
				Jump ();
				playerParticleScript.JumpParticle ();
			}


			if (Input.GetButtonUp ("SubmitJump") && isJumping) {
				StopJump ();
			}
		}
	}

	public void MoveLeft() {
		Vector3 v3 = rb.velocity;
		v3.x = -moveSpeed;
		v3.z = v3.z;
		rb.velocity = v3;
		if (trailIsTwo == false) {
			playerTrail.numCornerVertices = 2;
			trailIsTwo = true;
		}
	}

	public void MoveRight() {
		Vector3 v3 = rb.velocity;
		v3.x = moveSpeed;
		v3.z = v3.z;
		rb.velocity = v3;
		if (trailIsTwo == false) {
			playerTrail.numCornerVertices = 2;
			trailIsTwo = true;
		}
	}

	public void Jump() {
		if (secondJump) {
			isJumping = true;
			secondJump = false;
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}
	}

	public void StopJump() {
		if (isJumping) {
			isJumping = false;
			rb.velocity = new Vector2 (rb.velocity.x, 0);
		}
	}
	void OnCollisionEnter2D (Collision2D coll) {
		string collTag;
		collTag = coll.gameObject.tag;
		if (collTag == "Platform" || collTag == "ChangePlat" || collTag == "MovePlat" || collTag == "MovePlatRight" || collTag == "MovePlatLeft" || collTag == "GroundPlat") {
			//reset jump
			secondJump = true;
			isJumping = false;
		}
	}

	void CalcStartPoint () {
		//determines which object is the yellow bar, takes its position, and calculates where the camera will stop.
		groundPlat = GameObject.FindGameObjectWithTag ("GroundPlat");
		groundPlatY = groundPlat.transform.position.y;
		boxStartPosY = groundPlatY + groundPlat.GetComponentInChildren<SpriteRenderer> ().bounds.size.y / 2 + 2.25f;
		transform.position = new Vector3 (groundPlat.transform.position.x, boxStartPosY, transform.position.z);
	}
}
