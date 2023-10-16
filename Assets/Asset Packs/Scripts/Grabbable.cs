using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private HandController leftHand;
    [SerializeField]
    private HandController rightHand;

    // Start is called before the first frame update
    void Start()
    {
        leftHand.canGrab = true;
        rightHand.canGrab = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
