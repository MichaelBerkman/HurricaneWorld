using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour {
    Vector3 topWindow;
    //bool windowOpen = false;
    Vector3 bottomWindow;
    bool windowClose = false;
    // Use this for initialization
    void Start () {
        bottomWindow = new Vector3(0f, -.26f, 0f);
        //bottomWindow = new Vector3(0f, 0.22f, 0f);
        //windowOpen = true;
    }
	
	// Update is called once per frame
	void Update () {
        /*if(windowOpen == true && this.transform.localPosition.y <= bottomWindow.y)
        {
          this.transform.Translate(new Vector3(0, 0.03f, 0));
        }*/
        if (windowClose == true && this.transform.localPosition.y >= bottomWindow.y) {
            this.transform.Translate(new Vector3(0, -.03f, 0));
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Right" || other.tag == "Left")
        {
            print("down");
            //windowOpen = false;
            windowClose = true;
        }
    }

}
