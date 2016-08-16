using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	[SerializeField] private float camSpeed;
	[SerializeField] private float moveDelay;
	private float timeLeft;
	private GameObject yellowBar;
	private GameObject groundPlat;
	[SerializeField] private float yellowBarY;
	[SerializeField] private float groundPlatY;
	[SerializeField] private float cameraTop;
	[SerializeField] private float camFinalPos;
	[SerializeField] private float cameraBottom;
	[SerializeField] private float camStartPos;
	private BoxCollider2D camColl;

	// Use this for initialization
	void Start () {
		CalcStartPoint ();
		CalcEndPoint ();

		timeLeft = moveDelay;

		camColl = GetComponent<BoxCollider2D> ();
		camColl.size = new Vector2 (Camera.main.orthographicSize * Camera.main.aspect * 2, Camera.main.orthographicSize * 2);
	}
	
	// Update is called once per frame
	void Update () {
		//delay timer
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
		}
		//moves camera
		if (transform.position.y < camFinalPos && timeLeft <= 0) {
			transform.Translate (Vector3.up * Time.deltaTime * camSpeed);
		}
	}

	void CalcStartPoint () {
		//determines which object is the yellow bar, takes its position, and calculates where the camera will stop.
		groundPlat = GameObject.FindGameObjectWithTag ("GroundPlat");
		groundPlatY = groundPlat.transform.position.y;
		cameraBottom = groundPlatY - groundPlat.GetComponentInChildren<SpriteRenderer> ().bounds.size.y / 2;
		camStartPos = cameraBottom + Camera.main.orthographicSize;
		transform.position = new Vector3 (groundPlat.transform.position.x, camStartPos, transform.position.z);
	}

	void CalcEndPoint () {
		//determines which object is the yellow bar, takes its position, and calculates where the camera will stop.
		yellowBar = GameObject.FindGameObjectWithTag ("YellowBar");
		yellowBarY = yellowBar.transform.position.y;
		cameraTop = yellowBarY + yellowBar.GetComponentInChildren<SpriteRenderer> ().bounds.size.y / 2;
		camFinalPos = cameraTop - Camera.main.orthographicSize;
	}
}
