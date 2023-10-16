using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseControl : MonoBehaviour {


    public sceneManager scene;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 if (scene.floods == 1) //Include outside condition
        {
            transform.Translate(new Vector3(0, 0.03f, 0));
        }
	}
}
