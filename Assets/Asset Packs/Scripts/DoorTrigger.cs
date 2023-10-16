using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    //triggers when controller enters knob
    public bool canOpen;
    public bool isOpen = false;
    public Animator doorAnim;
    public ParticleSystem knobHighlight;

    void OnTriggerEnter(Collider other) {
        //check if left or right controller
        if ( !isOpen && (other.gameObject.CompareTag("Left") || other.gameObject.CompareTag("Right"))){
            if (canOpen) {
                Open();
            }
        }
    }

    public void Open() {
        doorAnim.SetBool("openDoor", true);
        isOpen = true;
    }

    public void closeDoor() {
        doorAnim.SetBool("openDoor", false);
    }
}
