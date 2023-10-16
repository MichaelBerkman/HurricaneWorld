using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsuranceDoneButton : MonoBehaviour {

	public Material offMat;
	public Material onMat;
	Material curMat;

	public void Start() {
		curMat = gameObject.GetComponent<Material>();
	}

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Left")) {
			gameObject.GetComponent<Renderer>().material = onMat;

			if (HandController.left.device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
                FinishSigning();
            }
            else if (HandController.right.Ltrig == true)
            {
                print("left con up");
                FinishSigning();
            }

        }
        if (other.gameObject.CompareTag("Right"))
        {
            gameObject.GetComponent<Renderer>().material = onMat;
            print("right con in");
            if (HandController.right.device != null) { 
                if (HandController.right.device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                  print("right con up");
                    FinishSigning();
                }
            }
        else if (HandController.right.Rtrig == true)
        {
            print("right con up");
            FinishSigning();
        }
        }
    }
	
	void OnTriggerExit (Collider other) {
		if (other.gameObject.CompareTag("Left")) {
			gameObject.GetComponent<Renderer>().material = offMat;			
		}
		if (other.gameObject.CompareTag("Right")) {
			gameObject.GetComponent<Renderer>().material = offMat;
			print("right con out");

		}
	}

		public void FinishSigning() {
        sceneManager.scene.SignInsurance();
    }
}
