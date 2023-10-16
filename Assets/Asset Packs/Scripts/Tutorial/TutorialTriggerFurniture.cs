using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTriggerFurniture : MonoBehaviour {

	public Scene flood;
	public Animator anim;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	void OnTriggerEnter(Collider c) {
		if (c.transform.name.Contains("Table")) {
			//anim.SetBool("Open", true);
			print("tableeee");
			SceneManager.LoadScene("THIS is the main scene");

		}
	}
}
