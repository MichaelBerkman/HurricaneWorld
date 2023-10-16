using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{

    public SceneController sceneControl;
    public bool right;

// Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
            if (right)
            {
                RightUpdate();

            }
            else
                LeftUpdate();
        
            if (Input.GetKeyDown(KeyCode.A))
        {
            sceneControl.UIDebug("AAAAA");
        }
        
    }

    public void RightUpdate()
    {
        

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            //gameObject.SetActive(false);
            sceneControl.UIDebug("Right");
           Debug.Log("Right Input Working");
        }                       
    
                
    }
    public void LeftUpdate()
    {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //gameObject.SetActive(true);
            sceneControl.UIDebug("Left");

            Debug.Log("Left Input Working");

            

        }
        
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
            {
                
            }
        
    }
    
    private void OnTriggerEnter(Collider collision)
    {
       
    }

   
}
