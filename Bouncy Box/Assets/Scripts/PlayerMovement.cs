using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody2D rb;
	public bool movingRight;

	[SerializeField] private float jumpSpeed;
	[SerializeField] private float moveSpeed;
	[SerializeField] private bool secondJump;
	[SerializeField] private bool isJumping;
	[SerializeField] private float jumpTimer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		jumpSpeed = 5; //amount of force added when jumping
		moveSpeed = 10; //speed when moving left/right
		jumpTimer = .2f; //amount of time the player can hold the jump button
	}
	
	void Update () {
		


		//Move left
		if (Input.GetKeyDown (KeyCode.A)) {
			Vector3 v3 = rb.velocity;
			v3.x = -moveSpeed;
			v3.z = v3.z;
			rb.velocity = v3;
		}

		//Move Right
		if (Input.GetKeyDown (KeyCode.D)) {

			Vector3 v3 = rb.velocity;
			v3.x = moveSpeed;
			v3.z = v3.z;
			rb.velocity = v3;
		}

		//Jump
		if (Input.GetKey (KeyCode.Space) && secondJump) {
			isJumping = true;
			secondJump = false;
		}



		if (isJumping) {
			jumpTimer -= Time.deltaTime;
			if (jumpTimer > 0) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);

			}
		}
		if (Input.GetKeyUp (KeyCode.Space) && isJumping) {
			isJumping = false;
			rb.velocity = new Vector2 (rb.velocity.x, 0);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		//reset jump
		secondJump = true;
		isJumping = false;
		jumpTimer = 1;

	}
}
