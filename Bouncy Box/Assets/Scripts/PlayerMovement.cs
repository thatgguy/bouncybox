using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody2D rb;
	public bool movingRight;

	//[SerializeField] private float maxVelocityY;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float moveSpeed;
	[SerializeField] private bool secondJump;
	[SerializeField] private bool isJumping;
	[SerializeField] private float jumpTimer;
	[SerializeField] GameObject groundPlat;
	[SerializeField] float groundPlatY;
	[SerializeField] float boxStartPosY;
	float accelThresh;

	// Use this for initialization
	void Start () {
		/*cam = Camera.main;
		planes = GeometryUtility.CalculateFrustumPlanes (cam);
		playerColl = GetComponent<BoxCollider2D> ();
*/
		CalcStartPoint ();
		rb = GetComponent<Rigidbody2D> ();
		jumpSpeed = 18; //amount of force added when jumping
		moveSpeed = 15; //speed when moving left/right
		jumpTimer = .2f; //amount of time the player can hold the jump button.5
		accelThresh = .2f;
	}
	/*
	void FixedUpdate () {

		if (rb.velocity.y >= maxVelocityY) {
			rb.velocity = new Vector2 (rb.velocity.x, maxVelocityY);
		}

		if (rb.velocity.y <= -maxVelocityY) {
			rb.velocity = new Vector2 (rb.velocity.x, maxVelocityY);
		}
	


	}*/

	void Update () {

		/*
		//Move left
		if (Input.GetKeyDown (KeyCode.A)) {
			MoveLeft ();
		}

		//Move Right
		if (Input.GetKeyDown (KeyCode.D)) {
			MoveRight ();
		}

		//Jump
		if (Input.GetKeyDown (KeyCode.Space) && secondJump) {
			Jump (); 
		}

		//Stop Jump
		if (Input.GetKeyUp (KeyCode.Space) && isJumping) {
			StopJump ();
			}*/

		//acce

		if (Input.acceleration.x < -accelThresh) {
			MoveLeft ();
		}

		if (Input.acceleration.x > accelThresh) {
			MoveRight ();
		}

		if (Input.touchCount > 0) {
			Jump ();
		}

		if (Input.touchCount == 0 && isJumping) {
			StopJump ();
		}
	}


	public void MoveLeft() {
		Vector3 v3 = rb.velocity;
		v3.x = -moveSpeed;
		v3.z = v3.z;
		rb.velocity = v3;
	}

	public void MoveRight() {
		Vector3 v3 = rb.velocity;
		v3.x = moveSpeed;
		v3.z = v3.z;
		rb.velocity = v3;
	}

	public void Jump() {
		if (secondJump) {
			isJumping = true;
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
			secondJump = false;
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
		if (collTag == "Platform" || collTag == "ChangePlat" || collTag == "MovePlat" || collTag == "MovePlatRight" || collTag == "MovePlatleft" || collTag == "GroundPlat") {
			//reset jump
			secondJump = true;
			isJumping = false;
			//jumpTimer = 1;
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
