using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour
{

    private Vector3 offset;

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - camera.transform.position; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = camera.transform.position + offset;
        transform.LookAt(camera.transform);
        transform.RotateAround(camera.transform.position, camera.transform.eulerAngles, camera.transform.eulerAngles.y);
    }
}
