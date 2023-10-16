using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingObject : MonoBehaviour {
    Vector3 direction;

    public bool active;

	Material origMat;
	Renderer render;

    //Water Reference Variables
	public GameObject water;
	float thisHeight;
    float waterHeight;
    float bounceHeight;
    float bounce;
    float ground;
    
    //Float Physics Variables
    public float factor;
	public int layer;
    Vector3 point;
    Vector3 up;
	Rigidbody rb;
	public float mass;
	public Vector3 grav;
	Quaternion rotateOrig;

	public sceneManager scene;

    public bool dirty;
	
	public bool wet = false;

    public bool disappear;
    
    // Use this for initialization
    void Start () {

		scene = FindObjectOfType<sceneManager>();
		water = GameObject.Find("Water");
        waterHeight = water.transform.position.y;

		origMat = gameObject.GetComponent<Renderer>().material;
		render = gameObject.GetComponent<Renderer>();
        dirty = false;

        //Physics Info
        grav = Physics.gravity;
        //bounceHeight = waterHeight;
        bounce = .0001f;
        up = new Vector3(0,5,0);
		rb = gameObject.GetComponent<Rigidbody>();
		mass = rb.mass;
		//layer = gameObject.layer;
        rb.angularDrag = 20;
        grav = Physics.gravity;
        ground = -2.31f;
        disappear = false;

    }


    /*
     * Disables gravity and adds some random force to simulate floating when in the water.
     * */
    void shuffle()
    {
        
        //wet = true;
        if (wet)
        {
            rb.useGravity = false;
            rb.mass = mass * 10f;
            this.transform.Translate(new Vector3(0, 0.0000003f, 0));
            factor = .25f;        
			float thing = factor - GetComponent<Rigidbody>().velocity.y * bounce;
			up = new Vector3(0,thing);
            GetComponent<Rigidbody>().AddForceAtPosition(up,point);
            if (!dirty)
            {
                StartCoroutine(dirtyMat());
                dirty = true;
            }
		}

	}

    IEnumerator dirtyMat()
    {
        //Sets secondary detail normal map to 'dirty' texture.
        yield return new WaitForSeconds(1.5f);

            render.material = scene.dirtyMat;              
        
            Debug.Log("DirtyMats");
            
    }
	public void resetMat() {
		if (render != null) {
            render.material = origMat;
            render.material.SetTexture("_DetailNormalMap", null);
			render.material.DisableKeyword("_DETAIL_MULX2");
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (scene.waterController.state == WaterController.FloodState.Complete && disappear == false)
        {
            StartCoroutine(Vanish());
            disappear = true;
        }
        //Updates   
		waterHeight = water.transform.position.y;
		thisHeight = this.transform.position.y;
              
          if (thisHeight < ground) {
            wet = false;  
              thisHeight = ground;
          }
              
          if (waterHeight >= thisHeight+2.5f) {
              wet = true;	
              shuffle();  
          }
        else if (waterHeight < thisHeight) {
              gameObject.GetComponent<Rigidbody>().useGravity = true;
              wet = false;
          }      
        
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(25);
        gameObject.SetActive(false);

    }


}
