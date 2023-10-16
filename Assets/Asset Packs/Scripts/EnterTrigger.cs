using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour {

    public DoorTrigger door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider c)
    {
        if (c.GetComponent<Camera>() != null)
        {
            door.closeDoor();
            //print("intrigger");
        }
    }

}
