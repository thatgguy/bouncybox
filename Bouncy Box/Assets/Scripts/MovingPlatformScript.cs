using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour {


	[SerializeField] private int segAmount;
	[SerializeField] float offSetAmount;

	public int nextSegNum;
	public int segNum;
	public float speed;
	public enum movePlatType{neither, left, right};
	public movePlatType platType;


	void Start () {
		nextSegNum = 0;

		segAmount = 1;
		offSetAmount = 0;

		RedSpikeCheck ();

		if (platType == movePlatType.left) {
			gameObject.tag = "MovePlatLeft";
			gameObject.layer = 2;
			speed = 3;
			LeftDetect ();
		}

		if (platType == movePlatType.right) {
			gameObject.tag = "MovePlatRight";
			gameObject.layer = 2;
			speed = -3;
			RightDetect ();
		}
	}
	
	// Update is called once per frame 
	void Update () {
		//moves platform
		if (gameObject.transform.parent == null) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		} else {
			transform.localPosition = new Vector2 (segNum, 0);
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
	RaycastHit2D hit = (Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y + .5f), Vector2.up, .5f));
	Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y + .5f), Vector2.up * 0.5f, Color.red, 5);

	if (hit.collider != null && hit.collider.gameObject.tag == "RedSpike") {
			Debug.Log ("Spike");
			hit.collider.gameObject.transform.parent = transform;
			hit.collider.transform.localPosition = new Vector2 (0, 1);
		}
	}
	//looks for moving platforms to the left and makes them part of the same platform
	void LeftDetect(){

		RaycastHit2D hit = (Physics2D.Raycast (transform.localPosition, Vector2.left, segAmount));
		Debug.DrawRay (transform.position, Vector2.left * segAmount, Color.green, 5);


	if (hit.collider != null && hit.collider.gameObject.tag == "MovePlat") {
			nextSegNum -= 1;
			hit.collider.gameObject.GetComponent<MovingPlatformScript> ().segNum = nextSegNum;
			hit.collider.gameObject.transform.parent = transform;

			Destroy(hit.collider);
			segAmount += 1;
			offSetAmount -= .5f;
			gameObject.GetComponent <BoxCollider2D> ().size = new Vector2 (segAmount, .96f);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (offSetAmount, 0);
			LeftDetect ();
			Debug.Log ("MovePlat");
		}
	}

	//looks for moving platforms to the right and makes them part of the same platform

	void RightDetect(){

		RaycastHit2D hit = (Physics2D.Raycast (transform.localPosition, Vector2.right, segAmount));
		Debug.DrawRay (transform.position, Vector2.right * segAmount, Color.green, 5);

		Debug.Log ("hit: " + hit.collider.gameObject.tag);

	if (hit.collider != null && hit.collider.gameObject.tag == "MovePlat") {
			nextSegNum += 1;
			hit.collider.gameObject.GetComponent<MovingPlatformScript> ().segNum = nextSegNum;
			hit.collider.gameObject.transform.parent = transform;

			Destroy(hit.collider);
			segAmount += 1;
			offSetAmount += .5f;
			gameObject.GetComponent <BoxCollider2D> ().size = new Vector2 (segAmount, .96f);
			gameObject.GetComponent<BoxCollider2D> ().offset = new Vector2 (offSetAmount, 0);
			RightDetect ();
			Debug.Log ("MovePlat");
		}
	}

	


}
