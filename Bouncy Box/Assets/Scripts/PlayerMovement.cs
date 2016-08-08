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

	// Use this for initialization
	void Start () {
		/*cam = Camera.main;
		planes = GeometryUtility.CalculateFrustumPlanes (cam);
		playerColl = GetComponent<BoxCollider2D> ();
*/
		rb = GetComponent<Rigidbody2D> ();
		jumpSpeed = 18; //amount of force added when jumping
		moveSpeed = 15; //speed when moving left/right
		jumpTimer = .2f; //amount of time the player can hold the jump button.5
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
			
		}


		/*
		if (isJumping) {
			jumpTimer -= Time.deltaTime;
			if (jumpTimer > 0) {
				rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
			
			}
		}*/
		if (Input.GetKeyUp (KeyCode.Space) && isJumping) {
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
		if (collTag == "Platform" || collTag == "ChangePlat" || collTag == "MovePlat" || collTag == "MovePlatRight" || collTag == "MovePlatleft") {
			//reset jump
			secondJump = true;
			isJumping = false;
			//jumpTimer = 1;
		}
	}
}
