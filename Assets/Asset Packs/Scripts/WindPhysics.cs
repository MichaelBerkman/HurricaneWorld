using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<floatingObject>() != null)
        {
            Debug.Log(other.gameObject.name);
            StartCoroutine(Wind(other));
            //other.GetComponent<floatingObject>().active = true;

        }
    }

    private IEnumerator Wind(Collider other)
    {
        Debug.Log("windphysics");
        int x, y, z;
       // while (sceneManager.scene.phase <= 2) 
       while (true)
        {
            
                x = Random.Range(-15, 45);
                y = Random.Range(0, 65);
                z = Random.Range(-45, 45);

                // if (DamageManager.WindowBroken)
                {
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z));
                }
                yield return new WaitForSeconds(.075f);

            
            //gameObject.SetActive(false);
        }
    }
     
}
