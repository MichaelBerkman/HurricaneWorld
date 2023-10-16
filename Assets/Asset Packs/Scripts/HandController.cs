using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class HandController : MonoBehaviour {


	public enum DeviceType { Quest, Steam };
	public DeviceType devType = DeviceType.Steam;
	public enum ControllerMode {Grab, Raise, Write, FEMA, UtilW, UtilE, UtilG, Wood, Light };
	public ControllerMode mode = ControllerMode.Grab;
	public enum Hand { Left, Right };
	public Hand hand;
    public Vector3 velocity; 

    public static HandController left;
    public static HandController right;
	

    public TextMeshPro textMesh;
	public string text;

	[HideInInspector]
	public SteamVR_TrackedObject trackedObj;
	[HideInInspector]
	public SteamVR_Controller.Device device;
	[HideInInspector]
	public GameObject cameraEye;

	public float throwForce = 1.7f;
    public GameObject model;
	public GameObject origParent;

	//Swipe
	[Header("Swipe stuff")]
	float swipeSum;
	float touchLast;
	float touchCurrent;
	float distance;
	bool swipedLeft;
	bool swipedRight;
	public ObjMenuManager objMenuManager;

	//teleport
	public LineRenderer laser;
    [Header("Teleport stuff")]
    public GameObject teleportMarkerPrefab;
	GameObject telAimObj;
	public Vector3 telLocation;
	public GameObject player;
	public LayerMask laserMask;
	public LayerMask teleStopMask;
	public LayerMask grabMask;
	public float yNudge;
    public Vector3 diff;
	public bool canMove;
	public bool teleport;
    // Number of segments to calculate - more gives a smoother line
    public int segmentCount = 20;
    // Length scale for each segment
    public float segmentScale = 1;
    private Collider _hitObject;
    private Vector3 _hitVector;

    public float segScale;
    //LeftGrabStuff
	private LineRenderer leftLaser; //Laser for left controller pickup
    [Header("Left Cont. Grab Stuff")]
    public GameObject grabAimObj;
	public Vector3 grabLocation;
	public bool canGrab;
	public bool grabbed;
	public int held;
	public static int itms;

	[Header("Mechanics stuff")]
	public Transform house;
	public LayerMask mask_house;
    bool holdingHouse = false;
	float housePositionerInitial;
	float housePositionInitial;
	public Vector2 housePositionRange;
    public GameObject insure;
	public bool nearInsure;	

    public LineRenderer sig;
    [HideInInspector]
    public List<LineRenderer> sigs;
    public GameObject signatureDot;

    public sceneManager scene;
    public Prep prep;

    [Header("PrepGear")]

    public bool nearWater, nearElec, nearGas, nearWood, nearFema;

    [Header("Effects")]
    public ParticleSystem lightPad;
    public ParticleSystem lightTrigger;
	public bool canPlace;
    public Light flashlight;


    [Header("Oculus Debug")]
    public OVRInput.Controller ocHand;
    public bool Rtrig;
    public bool Ltrig;
    public bool Abutton;
	public bool Bbutton;


	// Use this for initialization
	void Start() {
        
		// Check which controller is being used.
		if (this.gameObject.CompareTag("Left")) {
			hand = Hand.Left;
            left = this;
		} else if (this.gameObject.CompareTag("Right")) {
			hand = Hand.Right;
            right = this;
		}
        ocHand = OVRInput.GetActiveController();
        scene = GameObject.FindObjectOfType<sceneManager>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        if (laser) {
		    laser = GameObject.Find("TelLine").GetComponent<LineRenderer>();
        }

		leftLaser = GetComponentInChildren<LineRenderer>();
		cameraEye = GameObject.Find("CenterEyeAnchor");
        if (house != null)
        housePositionInitial = house.position.y;

	}

	// Update is called once per frame
	void Update() {
		if (devType == DeviceType.Steam) {
			device = SteamVR_Controller.Input((int)trackedObj.index);
		}
        else
        {
           velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        }

		//If this is the left controller, enable the object menu.
		if (hand == Hand.Left) {
			//LeftControllerUpdate();
		}
		//If this is the right controller, enable the teleportation.
		else if (hand == Hand.Right)
        {
			RightControllerUpdate();
		}
      
        //InputTest
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
           // scene.debugText.text = "";
            //Debug.Log("Working, but only kind of");
            //scene.canStart = true;
        }


    }
   
    /**
     * OVRInputs: 
     * Button.(Num): One = A, Two = B, Three = X, Four = Y
     * 
    * SteamVR inputs to replace"
    * device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger -> (OVRInput.Axis1D.PrimaryIndexTrigger)
    * 
    * device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger -> (OVRInput.Axis1D.PrimaryIndexTrigger)
    * 
    * device.GetPress(SteamVR_Controller.ButtonMask.Touchpad -> (
    * 
    * device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad ->
    * 
    * device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0 -> 
    * 
    * */

	void LeftControllerUpdate() {
        if (devType == DeviceType.Quest)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
                Ltrig = true;
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
                Ltrig = false;
            }
            //SpawnObject
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) //Trigger
            {
                Debug.Log("TriggerL");
                TouchpadPress(0);
            }
            //MenuLeft
            if (OVRInput.GetDown(OVRInput.Button.Three)) //
            {
                Debug.Log("Button3");
                TouchpadPress(3);
            }
            //MenuRight
            if (OVRInput.GetDown(OVRInput.Button.Four))
            {
                Debug.Log("Button4");
                TouchpadPress(1);
            }
        }
		else if (devType == DeviceType.Steam) {
			if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && canPlace) {

				Vector2 touchpad = (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));
				if (touchpad.y > 0.7f) {
					//print("Moving Up");
					TouchpadPress(0);
				} else if (touchpad.y < -0.7f) {
					//print("Moving Down");
					TouchpadPress(2);
				}

				if (touchpad.x > 0.7f) {
					//MovingRight
					TouchpadPress(1);

				} else if (touchpad.x < -0.7f) {
					//MovingLeft
					TouchpadPress(3);
				}

			}
		}
	}
	void RightControllerUpdate() {

		if (devType == DeviceType.Quest) {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Rtrig = true;
            }
            else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                Rtrig = false;
            }

            //Two = B, One = 
            if (OVRInput.Get(OVRInput.Button.Two)) {               
                Debug.Log("RightHand");
				SimulatePath();
			}

			if (OVRInput.GetUp(OVRInput.Button.Two)) {
			StartMove();
		    }

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                if (flashlight) {
                    flashlight.enabled = !flashlight.enabled;
                }
            }

            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            {
                Debug.Log("Trigger Right Hand");
            };
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                
                Debug.Log("HouseRay");
            }
        }

		else if (devType == DeviceType.Steam) {
			if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
				//StartLaser();
				SimulatePath();
			}
			// When the user releases the trackpad, move the user to the destination indicated by the laser/object guide.
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {

				StartMove();
				

			}
		}

		if (mode == ControllerMode.Raise && !sceneManager.scene.houseRaised) {
            //when trigger is pressed do raycast looking for house

            scene.houseHeight.text = ((1.45 * (house.position.y - housePositionInitial)).ToString("F1"));
            
            if (devType == DeviceType.Quest)
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {

                    houseRay();
                    
                    Debug.Log("HouseRay");
                }
                else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
                {
                    holdingHouse = false;
                }
                if (holdingHouse)
                {
                    house.position += new Vector3(0, .01f);
                    /**
                    Vector3 newHousePosition = house.position;
                    float p = transform.position.y - housePositionerInitial - 2;
                    newHousePosition.y = Mathf.Clamp(housePositionInitial + p, housePositionRange.x, housePositionRange.y);
                    house.position = newHousePosition;
                    */
                }
                if (house.position.y >= -7.85f)
                {
                    if (house.position.y > 7.85f)
                    { 
                        house.position = new Vector3(0, 7.85f);
                    }
                    sceneManager.scene.houseRaised = true;
                }
            }
            else if (devType == DeviceType.Steam)
            {
                if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {                 
                    //ray cast to house to grab it

                    houseRay();

                }
                else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    holdingHouse = false;
                }
                if (holdingHouse)
                {
                        
                        Vector3 newHousePosition = house.position;
                        float p = transform.position.y - housePositionerInitial;
                        newHousePosition.y = Mathf.Clamp(housePositionInitial + p, housePositionRange.x, housePositionRange.y);
                        house.position = newHousePosition;
                    
                }
                //if house greater than a little less than max house y pos
                if (house.position.y >= -4.4f)
                {
                   
                    sceneManager.scene.houseRaised = true;
                }
            }    
            
        }

        if (mode == ControllerMode.Write) {
            signatureDot.SetActive(true);
            if (devType == DeviceType.Quest)
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    if (nearInsure)
                    {
                        sig = Instantiate<GameObject>(Resources.Load<GameObject>("Signature")).GetComponent<LineRenderer>();
                        sig.positionCount = sig.positionCount + 1;
                        sig.SetPosition(sig.positionCount - 1, signatureDot.transform.position);
                    }
                }
                else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)){
                    if (nearInsure)
                    {
                        if (Vector3.Distance(transform.position, sig.GetPosition(sig.positionCount - 1)) > .05f && Vector3.Distance(transform.position, sig.GetPosition(sig.positionCount - 1)) != null)
                        {
                            sig.positionCount = sig.positionCount + 1;
                            sig.SetPosition(sig.positionCount - 1, signatureDot.transform.position);
                        }
                    }
                }
                else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
                {

                }
            }
            else if (devType == DeviceType.Steam)
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {

                    if (nearInsure)
                    {
                        sig = Instantiate<GameObject>(Resources.Load<GameObject>("Signature"),GameObject.Find("Signature").transform).GetComponent<LineRenderer>();
                        sig.positionCount = sig.positionCount + 1;
                        sig.SetPosition(sig.positionCount - 1, signatureDot.transform.position);
                        
                    }
                }
                else if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if (nearInsure)
                    {
                        if (Vector3.Distance(transform.position, sig.GetPosition(sig.positionCount - 1)) > .05f && Vector3.Distance(transform.position, sig.GetPosition(sig.positionCount - 1)) != null)
                        {
                            sig.positionCount = sig.positionCount + 1;
                            sig.SetPosition(sig.positionCount - 1, signatureDot.transform.position);
                        }
                    }
                }
                else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                }
            }
          
        } 

        
            if (devType == DeviceType.Quest)
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                if (mode == ControllerMode.FEMA)
                    prep.FemaKit();

                else if (mode == ControllerMode.Wood)
                    prep.BoardWindows();
                else if (mode == ControllerMode.UtilW)    
                    prep.ShutOffWater();
                else if (mode == ControllerMode.UtilG)
                    prep.ShutOffGas();
                else if (mode == ControllerMode.UtilE)
                    prep.ShutOffElectricity();
                
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    //prep.ShutOffUtil();
                }
            }
        }
	}

    //HouseCode 
    void houseRay()
    {
        RaycastHit hit;
       
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30, mask_house))
        {
            //laser.gameObject.SetActive(true);
            //laser.SetPosition(0, gameObject.transform.position);
            //laser.SetPosition(1, telLocation);

            //housePositionerInitial = transform.position.y;
            //housePositionInitial = house.position.y;
            //Debug.DrawRay(transform.position, hit.point, Color.green);
            holdingHouse = true;
        }
    }

	//spawn currently selected furniture
	void SpawnObject() {
		//objMenuManager.SpawnCurrentObject();
		sceneManager.scene.placedObj = true;
	}

	//get touchpad press; 0 is up, goes clockwise to 3
	public void TouchpadPress(int direction) {
		switch (direction) {
			case 0:
				SpawnObject();
				break;
			case 1:
				//objMenuManager.MenuRight();
				sceneManager.scene.hasSwiped = true;
				break;
			case 2:

				break;
			case 3:
				objMenuManager.MenuLeft();
				sceneManager.scene.hasSwiped = true;
				break;
		}
	}

	//If the user presses the trigger while in range of a moveable object, grab, throw, or place the object.
	//trigger is steam controller
	void OnTriggerStay(Collider col) {
		if (mode == ControllerMode.Grab) {
            if (devType == DeviceType.Quest)
            {
                if (col.gameObject.CompareTag("Throwable"))
                {
                    if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger) ||  (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))) 
                    {
                        ThrowObject(col);
                    }
                    else if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                    {
                        held = 1;
                        GrabObject(col);
                    }
                }
                else if (col.gameObject.CompareTag("Furniture"))
                {
                    if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        PlaceObjectGravity(col);

                    }
                    else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        held = 1;
                        GrabObject(col);
                    }
                }
            }
            else if (devType == DeviceType.Steam) {
                if (col.gameObject.CompareTag("Throwable"))
                {

                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        ThrowObject(col);

                    }
                    else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && held < 1)
                    {
                        //origParent.transform.parent = col.transform.parent;
                        held = 1;
                        GrabObject(col);
                    }
                }
                else if (col.gameObject.CompareTag("Furniture"))
                {
                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        PlaceObjectGravity(col);
                    }
                    else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        //origParent.transform.parent = col.transform.parent;
                        held = 1;
                        GrabObject(col);
                    }

                }
            }
        }


        
	}

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject==insure) {
			nearInsure = true;
		}

    
        if (col.gameObject == prep.boardButton)
        {
            nearWood = true;
        }

        if (col.gameObject == prep.femaButton)
        {
            nearFema = true;
        }

        if (col.gameObject == prep.gasButton)
        {
            nearGas = true;
        }

        if (col.gameObject == prep.electButton)
        {
            nearElec = true;
        }

        if (col.gameObject == prep.waterButton)
        {
            nearWater = true;
        }

    }

    void OnTriggerExit(Collider col) {
		
		if (col.gameObject == insure) {
			nearInsure = false;
		}

        if (col.gameObject == prep.boardButton)
        {
            nearWood = false;
        }

        if (col.gameObject == prep.femaButton)
        {
            nearFema = false;
        }

        if (col.gameObject == prep.gasButton)
        {
            nearGas = false;
        }

        if (col.gameObject == prep.electButton)
        {
            nearElec = false;
        }

        if (col.gameObject == prep.waterButton)
        {
            nearWater = false;
        }
    }
	
	private void ThrowObject(Collider coli) {
		coli.transform.SetParent(sceneManager.scene.furnitureParent.transform);
		sceneManager.scene.furniture.Add(new ObjMenuManager.Furn(coli.gameObject, coli.gameObject.transform));
		Rigidbody rb = coli.GetComponent<Rigidbody>();
		rb.isKinematic = false;
        if (device != null) {
            rb.velocity = device.velocity * throwForce;
            rb.angularVelocity = device.angularVelocity;
        }
        else
        {
            rb.velocity = OVRInput.GetLocalControllerVelocity(ocHand);
            rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(ocHand);
        }

        held = 0;
	}

	private void PlaceObject(Collider coli) {
		coli.transform.SetParent(null);
		held = 0;
	}

	private void PlaceObjectGravity(Collider coli) {
		held = 0;
		Rigidbody rb = coli.GetComponent<Rigidbody>();		
		rb.isKinematic = false;
		rb.velocity = (device.velocity * throwForce) / 5;
		rb.angularVelocity = device.angularVelocity;
		coli.transform.SetParent(sceneManager.scene.furnitureParent.transform);
		sceneManager.scene.furniture.Add(new ObjMenuManager.Furn(coli.gameObject,coli.gameObject.transform));

	}

	private void GrabObject(Collider coli) {
		Debug.Log("Object Grab");
		held += 1;
		coli.transform.SetParent(gameObject.transform);
		coli.GetComponent<Rigidbody>().isKinematic = true;
		//device.TriggerHapticPulse(2000);
		sceneManager.scene.movedObj = true;
	}
    
	private void StartLaser() {

      

        //Create the laser going from the controller to the Raycast hit point at a distance of 15.
        //show line and target to tele location

        if (laser)
            laser.gameObject.SetActive(true);

		if(telAimObj == null)
		    telAimObj = Instantiate(teleportMarkerPrefab) as GameObject;

        if (laser)
		    laser.SetPosition(0, gameObject.transform.position);

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask)) {
			if(hit.transform.gameObject.layer == 8 && hit.transform.gameObject.tag != "Door"){
				telLocation = hit.point;
				laser.SetPosition(1, telLocation);
				telAimObj.transform.position = new Vector3(telLocation.x, telLocation.y + yNudge, telLocation.z);
				canMove = true;
			} else if (hit.transform.gameObject.layer == 0) {

			}

		} else {
			canMove = false;
		}
	}

	private void StartMove() {
		if (canMove) {
            if (laser) {
			    laser.gameObject.SetActive(false);
            }
			Destroy(telAimObj);
			//Move the player to the teleport destination.
			if (teleport) {
                
				diff.x = player.transform.position.x - cameraEye.transform.position.x;
				diff.z = player.transform.position.z - cameraEye.transform.position.z;
				telLocation = new Vector3(telLocation.x + diff.x, player.transform.position.y, telLocation.z + diff.z);
				player.transform.position = telLocation;
                
                if (sceneManager.scene != null) {
                    sceneManager.scene.teleported = true;
                }	
                 
			} 
		
		}
	}

    void SimulatePath() {
        StartLaser();
        Vector3[] segments = new Vector3[segmentCount];
        segments[0] = transform.position;
        // The initial velocity
        Vector3 segVelocity = model.transform.forward * segScale;
        
        _hitObject = null;

        if (laser)
            laser.gameObject.SetActive(true);

        if (telAimObj == null)
            telAimObj = Instantiate(teleportMarkerPrefab) as GameObject;

        if (laser)
            laser.SetPosition(0, gameObject.transform.position);

        for (int i = 1; i < segmentCount; i++){
            if (_hitObject != null) {
                segments[i] = _hitVector;
                continue;
            }

            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
            float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics.gravity * segTime;

            // Check to see if we're going to hit a physics object
            RaycastHit hit;
            if (Physics.Raycast(segments[i - 1], segVelocity, out hit, segmentScale, laserMask)) {
                // remember who we hit
                _hitObject = hit.collider;
                telLocation = hit.point;
                // set next position to the position where we hit the physics object
                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                // correct ending velocity, since we didn't actually travel an entire segment
                segVelocity = segVelocity - Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
                // flip the velocity to simulate a bounce
                segVelocity = Vector3.Reflect(segVelocity, hit.normal);

                telAimObj.transform.position = new Vector3(telLocation.x, telLocation.y + yNudge, telLocation.z);
                canMove = true;
				_hitVector = segments[i];

                if (laser) {
                    laser.startColor = Color.blue;
				    laser.endColor = new Color(0, 0, 1, .5f);
                }
			}
			else if (Physics.Raycast(segments[i - 1], segVelocity, out hit, segmentScale, teleStopMask)) { //Contact with walls
																										   // set next position to the position where we hit the physics object
				//segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
				// correct ending velocity, since we didn't actually travel an entire segment
				//segVelocity = segVelocity - Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;                                                                           // set next position to the position where we hit the physics object

                if (laser) {
                    laser.startColor = new Color(0,0,0,0);
				    laser.endColor = new Color(0, 0, 0, 0);
                }
				
				telAimObj.transform.position = new Vector3(100, 0, 0);
				canMove = false;
			}

            // If our raycast hit no objects, then set the next position to the last one plus v*t
            else {				
                segments[i] = segments[i - 1] + segVelocity * segTime;
                canMove = false;
            }
        }
		// At the end, apply our simulations to the LineRenderer
		//print(model.transform.forward);
        if (laser)
		    laser.positionCount = segmentCount;

		for (int j = 0; j < segmentCount; j++)
            if (laser)
			    laser.SetPosition(j, segments[j]);
	}
   

    private void DisableUtil()
    {
        scene.util = false;
    }


}


