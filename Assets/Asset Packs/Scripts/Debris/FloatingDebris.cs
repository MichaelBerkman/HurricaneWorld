using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDebris : MonoBehaviour
{
    public Transform controlPoint;
    public float speed;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != controlPoint.position)
        {
            Vector3 position = Vector3.MoveTowards(transform.position, controlPoint.position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(position);
        } 
        else 
        {
            Reset();
        }
    }

    void Reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
