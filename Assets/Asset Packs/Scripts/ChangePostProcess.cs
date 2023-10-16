using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ChangePostProcess : MonoBehaviour
{
    public PostProcessingProfile normal, underwater;
    private PostProcessingBehaviour camerafx;

    // Use this for initialization
    void Start()
    {
        camerafx = FindObjectOfType<PostProcessingBehaviour>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("MainCamera"))
        {
            camerafx.profile = underwater;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("MainCamera"))
        {
            camerafx.profile = normal;
        }
    }
}