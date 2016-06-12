using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ChangePlatScript : MonoBehaviour {


	private int segAmount;
	private float offSetAmount;
	private float spriteSizeX;
	private float spriteSizeY;

	public bool changed;
	public int nextSegNum;
	public int segNum;
	public float speed;
	public enum changePlatType{stat, left, right}
	public changePlatType cPlatType;

	// Use this for initialization
	void Start () {
		nextSegNum = 0;
		segAmount = 1;
		offSetAmount = 0;

		spriteSizeY = gameObject.GetComponentInChildren<SpriteRenderer> ().bounds.size.y;
		spriteSizeX = gameObject.GetComponentInChildren<SpriteRenderer> ().bounds.size.x;
		Debug.Log ("Sprite: " + spriteSizeX);

		changed = false;
		//stationary platform
		if (cPlatType == changePlatType.stat) {
			speed = 0;
			gameObject.transform.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
		//move left
		if (cPlatType == changePlatType.left) {
			speed = 3;
			gameObject.layer = 2;
			LeftDetect ();
		}
		//move right
		if (cPlatType == changePlatType.right) {
			speed = -3;
			gameObject.layer = 2;
			RightDetect ();
		}
	}

	void Update () {
		//moves platform
		if (gameObject.transform.parent == null) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		} else {
			transform.localPosition = new Vector2 (segNum * spriteSizeX, 0);
		}

		/*if (transform.parent != null) {
			if (GetComponentInParent<ChangePlatScript> ().changed) {
				changed = true;
			}
		}*/

		//change sprite when hit
	


	}

	void OnCollisionExit2D(Collision2D coll) {
		//determines when box must change
		if (coll.gameObject.tag == "Player" && changed == false) {
			changed = true;

			SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer> ();
			renderer.color = new Color (1, 1, 1);

			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
				sr.color = new Color (1, 1, 1);
			}
			//GetComponentInChildren<ChangePlatScript> ().changed = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//makes platform bounce when it hits another object
		if (coll.gameObject.tag != "Player") {
			speed *= -1;
		}
		//resets scene when hit and changed
		if (coll.gameObject.tag == "Player" && changed) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);

		}
	}

	//looks for changing platforms to the left and makes them part of the same platform (for moving platforms)
	void LeftDetect(){
		

		RaycastHit2D hit = (Physics2D.Raycast (transform.localPosition, Vector2.left, segAmount * spriteSizeX));
		Debug.DrawRay (transform.position, Vector2.left * segAmount * spriteSizeX, Color.green, 5);


		if (hit.collider != null && hit.collider.gameObject.tag == "ChangePlat") {
			nextSegNum -= 1;
			hit.collider.gameObject.GetComponent<ChangePlatScript> ().segNum = nextSegNum;
			hit.collider.gameObject.transform.parent = transform;

			Destroy(hit.collider);
			segAmount += 1;
			offSetAmount -= .5f * spriteSizeX;
			gameObject.GetComponent <BoxCollider2D> ().size = new Vector2 (segAmount * spriteSizeX, .96f * spriteSizeY);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (offSetAmount, 0);
			LeftDetect ();
		}
	}
	//looks for changing platforms to the right and makes them part of the same platform (for moving platforms)
	void RightDetect(){

		RaycastHit2D hit = (Physics2D.Raycast (transform.localPosition, Vector2.right, segAmount * spriteSizeX));
		Debug.DrawRay (transform.position, Vector2.right * segAmount * spriteSizeX, Color.green, 5);

		if (hit.collider != null && hit.collider.gameObject.tag == "ChangePlat") {
			nextSegNum += 1;
			hit.collider.gameObject.GetComponent<ChangePlatScript> ().segNum = nextSegNum;
			hit.collider.gameObject.transform.parent = transform;

			Destroy (hit.collider);
			segAmount += 1;
			offSetAmount += .5f;
			gameObject.GetComponent <BoxCollider2D> ().size = new Vector2 (segAmount * spriteSizeX, .96f);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (offSetAmount, 0);
			RightDetect ();
		}
	}
}
