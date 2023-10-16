using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerClose : MonoBehaviour {

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
			anim.SetBool("Open", false);


		}
	}
}
