using UnityEngine;
using System.Collections;
using SimpleJSON;

public class JSONStuff : MonoBehaviour {

	[SerializeField] private GameObject PlatPrefab;
	[SerializeField] private GameObject YellowBoxPrefab;
	[SerializeField] private GameObject RedSpikePrefab;
	[SerializeField] private GameObject RedBoxPrefab;
	[SerializeField] private GameObject PlayerPrefab;
	[SerializeField] private GameObject MovePlatPrefab;
	[SerializeField] private GameObject ChangePlatPrefab;
	[SerializeField] private GameObject YellowBarPrefab;

	[SerializeField] private GameObject LeftWall;
	[SerializeField] private GameObject RightWall;

	TextAsset jsonFile; // raw json file in text format
	JSONNode jsonData; // json data from file

	public int currentLevel;

	void Start () {
		currentLevel = 1;

		PopulateLevel ();


	}

	string RemoveQuotesAndSpaces (string input) {
		string[] temp = new string[input.Length];
		for (int i = 0; i < input.Length; i++) {
			if (input[i].ToString() == "" || input[i].ToString() == "\"" || input[i].ToString() == " ") {

			} else {
				temp[i] = input[i].ToString();
			}
		}

		string result = string.Join("", temp);
		return result;
	}

	void PopulateLevel () {
		jsonFile = Resources.Load("testLevel") as TextAsset; // load file from "Resources" folder
		jsonData = JSONClass.Parse(jsonFile.text); // parse data from text file



		for (int i = 0; i < jsonData.Count; i++) {
			for (int j = 0; j <= 29; j++) { // loop through json file and retrieve tile data.
				switch (RemoveQuotesAndSpaces(jsonData[i]["val" + j])) {
				case "Plat": // if the tile in space (i, j) is a wall tile, instantiate wall tile at (i,j)
					GameObject plat = Instantiate(PlatPrefab, new Vector3(j, -i), Quaternion.identity) as GameObject;
					break;
				case "Spawn": // if the tile in space (i, j) is a spawner tile, instantiate spawner tile at (i,j)
					GameObject player = Instantiate(PlayerPrefab, new Vector3(j, -i), Quaternion.identity) as GameObject;// instantiate spawner object
					break;
				case "YellowBox":
					GameObject yellowBox = Instantiate (YellowBoxPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					break;
				case "RedSpike":
					GameObject redSpike = Instantiate (RedSpikePrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					break;
				case "RedBox":
					GameObject redBox = Instantiate (RedBoxPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					break;
				case "YellowBar":
					GameObject yellowBar = Instantiate (YellowBarPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					break;
				case "MovePlat": 
					GameObject movePlat = Instantiate (MovePlatPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					movePlat.GetComponent<MovingPlatformScript> ().platType = MovingPlatformScript.movePlatType.neither;
					break;
				case "MovePlatLeft":
					GameObject movePlatLeft = Instantiate (MovePlatPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					movePlatLeft.GetComponent<MovingPlatformScript> ().platType = MovingPlatformScript.movePlatType.left;
					break;
				case "MovePlatRight":
					GameObject movePlatRight = Instantiate (MovePlatPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					movePlatRight.GetComponent<MovingPlatformScript> ().platType = MovingPlatformScript.movePlatType.right;
					break;
				case "ChangePlat": 
					GameObject changePlat = Instantiate (ChangePlatPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					changePlat.GetComponent<ChangePlatScript> ().cPlatType = ChangePlatScript.changePlatType.stat;
					break;
				case "ChangePlatLeft":
					GameObject changePlatLeft = Instantiate (ChangePlatPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					changePlatLeft.GetComponent<ChangePlatScript> ().cPlatType = ChangePlatScript.changePlatType.left;
					break;
				case "ChangePlatRight":
					GameObject changePlatRight = Instantiate (ChangePlatPrefab, new Vector3 (j, -i), Quaternion.identity) as GameObject;
					changePlatRight.GetComponent<ChangePlatScript> ().cPlatType = ChangePlatScript.changePlatType.right;
					break;
				default:
					break;

				}
			}
		}
		//sets size of walls on either side
		LeftWall.GetComponentInChildren<BoxCollider2D> ().size = new Vector2 (1, jsonData.Count);
		LeftWall.transform.position = new Vector2 (-1, jsonData.Count / -2);
		RightWall.GetComponentInChildren<BoxCollider2D> ().size = new Vector2 (1, jsonData.Count);
		RightWall.transform.position = new Vector2 (30, jsonData.Count / -2);
	}
}
