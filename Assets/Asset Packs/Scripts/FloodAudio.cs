using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodAudio : MonoBehaviour {

     AudioSource a;
     public bool ready;

	// Use this for initialization
	void Start () {
        a = GetComponent<AudioSource>();
        ready = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (ready) 
        a.volume = Mathf.MoveTowards(a.volume,.125f,.0001f);
	}

}
