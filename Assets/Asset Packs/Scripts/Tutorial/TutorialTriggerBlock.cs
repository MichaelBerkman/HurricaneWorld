using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialTriggerBlock : MonoBehaviour {

	public Animator preDoor;
	public Animator anim;
	public GameObject go;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject == go) {
			anim.SetBool("Open", true);
			preDoor.SetBool("Open", false);

			
		}
	}
}
