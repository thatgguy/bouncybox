using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

	[SerializeField] AudioClip[] musicFiles;
	AudioClip currentSong;
	[SerializeField] AudioClip nextSong;
	float songTimer;
	[SerializeField] AudioSource musicSource;
	bool isPlaying;

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		musicSource = GetComponent<AudioSource> ();
		isPlaying = false;
	}

	void Update () {
		if (isPlaying) {
			songTimer -= Time.deltaTime;
			if (songTimer <= 0) {
				isPlaying = false;
				SelectTrack();
			}
		}
	}

	public void SelectTrack () {
		nextSong = musicFiles [Random.Range (0, musicFiles.Length)];
		if (nextSong != currentSong) {
			currentSong = nextSong;
			songTimer = currentSong.length;
			musicSource.clip = currentSong;
			musicSource.Play ();
			isPlaying = true;
		} else {
			SelectTrack ();
		}
	}

}
