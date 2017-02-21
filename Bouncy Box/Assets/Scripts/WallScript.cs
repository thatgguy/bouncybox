using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	public enum wallPortalType{neutral, left, right};
	public wallPortalType wallType;
	public float portalOutX;

	void Awake () {
		SetTag ();
	}

	void Start () {
		if (wallType == wallPortalType.left) {
			SetPortalOutX();
		} 
	}

	void SetPortalOutX () {
		GameObject otherPortal = GameObject.FindGameObjectWithTag ("RightPortal");
		float otherPortalX; 
		float halfPortalWidth;

		otherPortalX = otherPortal.transform.position.x;
		halfPortalWidth = otherPortal.GetComponentInChildren<Collider2D>().bounds.size.x / 2	;
		portalOutX = otherPortalX - halfPortalWidth;
		otherPortal.GetComponent<WallScript>().portalOutX = gameObject.transform.position.x + halfPortalWidth;
	}

	void SetTag () {
		SpriteRenderer render = gameObject.GetComponentInChildren<SpriteRenderer> ();
		if (wallType == wallPortalType.left) {
			gameObject.tag = "LeftPortal";
			render.color = new Color (0, 0, 1);
		} else if (wallType == wallPortalType.right) {
			gameObject.tag = "RightPortal";
			render.color = new Color (1, .625f, 0);
		} else {
			render.color = new Color (0, 0, 0);
		}
	}
}
