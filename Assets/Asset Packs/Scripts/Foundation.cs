using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour {
    public float maxHeight;
    public Vector3 finalPosition;
    Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
