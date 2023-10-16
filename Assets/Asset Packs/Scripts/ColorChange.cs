using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void ChangeColor()
    {

        if (obj.GetComponent<Renderer>().material.color == Color.black)
        {
            obj.GetComponent<Renderer>().material.color = Color.red;
        }

        else
        {
            obj.GetComponent<Renderer>().material.color = Color.black;

        }
    }

}
