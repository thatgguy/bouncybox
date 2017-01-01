using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour {


	[SerializeField] private int segAmount;
	[SerializeField] float offSetAmount;
	private float spriteSizeX;
	private float spriteSizeY;

	public int nextSegNum;
	public int segNum;
	public float speed;
	public enum movePlatType{neither, left, right};
	public movePlatType platType;


	void Start () {
		nextSegNum = 0;

		segAmount = 1;
		offSetAmount = 0;

		spriteSizeY = gameObject.GetComponentInChildren<SpriteRenderer> ().bounds.size.y;
		spriteSizeX = gameObject.GetComponentInChildren<SpriteRenderer> ().bounds.size.x;
		Debug.Log ("Sprite: " + spriteSizeX);


		RedSpikeCheck ();

		if (platType == movePlatType.left) {
			gameObject.tag = "MovePlatLeft";
			gameObject.layer = 2;

			LeftDetect ();
		}

		if (platType == movePlatType.right) {
			gameObject.tag = "MovePlatRight";
			gameObject.layer = 2;

			RightDetect ();
		}
	}
	
	// Update is called once per frame 
	void Update () {
		//moves platform
		if (gameObject.transform.parent == null) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		} else {
			transform.localPosition = new Vector2 (segNum * spriteSizeX, 0);
		}
	}

	//makes platform go other direction when it collides with other object
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag != "Player") {
			Debug.Log ("Yes");
			speed *= -1;
		}
	}


	void RedSpikeCheck(){
		RaycastHit2D hit = (Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y + .5f * spriteSizeY), Vector2.up, .5f * spriteSizeY));
		Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y + .5f * spriteSizeX), Vector2.up * 0.5f * spriteSizeX, Color.red, 5);

		if (hit.collider != null && hit.collider.gameObject.tag == "RedSpike") {
			Debug.Log ("Spike");
			hit.collider.gameObject.transform.parent = transform;
			hit.collider.transform.localPosition = new Vector2 (0, spriteSizeY);
		}
	}
	//looks for moving platforms to the left and makes them part of the same platform
	void LeftDetect(){

		RaycastHit2D hit = (Physics2D.Raycast (transform.localPosition, Vector2.left, segAmount * spriteSizeX));
		Debug.DrawRay (transform.position, Vector2.left * segAmount * spriteSizeX, Color.green, 5);


		if (hit.collider != null && hit.collider.gameObject.tag == "MovePlat") {
			nextSegNum -= 1;
			hit.collider.gameObject.GetComponent<MovingPlatformScript> ().segNum = nextSegNum;
			hit.collider.gameObject.transform.parent = transform;

			Destroy (hit.collider);
			segAmount += 1;
			offSetAmount -= .5f * spriteSizeX;
			gameObject.GetComponent <BoxCollider2D> ().size = new Vector2 (segAmount * spriteSizeX, .96f * spriteSizeY);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (offSetAmount, 0);
			LeftDetect ();
			Debug.Log ("MovePlat");
		} /*else {
			speed = 3;
		}*/
	}

	//looks for moving platforms to the right and makes them part of the same platform

	void RightDetect(){
		//Debug.Log ("transform.localposition: " + transform.localPosition);
		RaycastHit2D hit = (Physics2D.Raycast (transform.localPosition, Vector2.right, segAmount * spriteSizeX));
		Debug.DrawRay (transform.position, Vector2.right * segAmount * spriteSizeX, Color.green, 5);

		//Debug.Log ("hit: " + hit.collider.gameObject.tag);

		if (hit.collider != null && hit.collider.gameObject.tag == "MovePlat") {
			//int otherSegNum = hit.collider.gameObject.GetComponent<MovingPlatformScript> ().segNum;
			nextSegNum += 1;
			hit.collider.gameObject.GetComponent<MovingPlatformScript> ().segNum = nextSegNum;
			hit.collider.gameObject.transform.parent = transform;

			Destroy (hit.collider);
			segAmount += 1;
			offSetAmount += .5f * spriteSizeX;
			gameObject.GetComponent <BoxCollider2D> ().size = new Vector2 (segAmount * spriteSizeX, .96f * spriteSizeY);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (offSetAmount, 0);
			RightDetect ();
			Debug.Log ("MovePlat");
		} else {
			speed *= -1;
		}
			
	}

	


}
