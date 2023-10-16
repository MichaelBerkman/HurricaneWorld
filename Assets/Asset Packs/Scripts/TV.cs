using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class TV : MonoBehaviour {

	public GameObject screen;
	public bool isPlaying;
	public bool played;
	public VideoPlayer player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play() {
		player.Play();
	}

	public void TurnOn(bool b) {
		screen.SetActive(b);
	}
}
