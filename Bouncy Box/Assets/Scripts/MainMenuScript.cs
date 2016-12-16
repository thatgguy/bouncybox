using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public Button startButton;
	//public Button exitButton;

	// Use this for initialization
	void Start () {
		Button strtBtn = startButton.GetComponent<Button> ();
		strtBtn.onClick.AddListener (StartButtonClick);

		//Button xtBtn = exitButton.GetComponent<Button> ();
		//xtBtn.onClick.AddListener (ExitButtonClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartButtonClick() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	/*void ExitButtonClick() {
		
	}*/
}
