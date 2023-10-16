using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawnScript : MonoBehaviour {

    public GameObject debris1;
    public GameObject debris2;
 
    public int spawned;
    public int ants;    
    public sceneManager scene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned <= 20 && scene.waterController.state == WaterController.FloodState.Raising)
        {
            Instantiate(debris1, gameObject.transform);
           
            spawned++;
        }
        if (ants <= 10 && scene.waterController.state == WaterController.FloodState.Raising)

        {
            Instantiate(debris2, gameObject.transform);

            ants++;
        }      
	}
}
