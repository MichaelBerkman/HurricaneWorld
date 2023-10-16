using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antspawn : MonoBehaviour {
    public sceneManager scene;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (scene.waterController.state == WaterController.FloodState.Raising)
        {
            Debug.Log("AntSpawn");
            gameObject.SetActive(true);
        }
	}
}
