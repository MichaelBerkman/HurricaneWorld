using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("CarCrashingIntoTree")]
    public Animator car;
    public AudioSource carTree;
    public Animator tree;
    public float carCrashingIntoTreeTimeout;

    [Header("CarCrashingIntoCar")]
    public Animator grayCar;
    public Animator blackCar;
    public float carCrashingIntoCarTimeout;

    [Header("FallingTree")]
    public Animator palm;
    public float fallingTreeTimeout;

    [Header("FloatingDebris")]
    public FloatingDebris[] floatingDebrisObjects;
    public float floatingDebrisTimeout;
   public GameObject floatDebris;

    [Header("Windows")]
    public GameObject window1, window2, window3;
    public static bool WindowBroken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DamageStart()
    {
        StartCoroutine(carCrashingIntoTree());
        StartCoroutine(carCrashingIntoCar());
        StartCoroutine(fallingTree());
        BreakWindows();
        //StartCoroutine(floatingDebris());
        //floatDebris.SetActive(true);
    }

    IEnumerator carCrashingIntoTree()
    {
        car.SetBool("Damage", true);
        tree.SetBool("Damage", true);

        yield return new WaitForSeconds(3f);
        //carTree.Play();

        /**
        yield return new WaitForSeconds(carCrashingIntoTreeTimeout);
        car.enabled = true;
        tree.enabled = true;
        */
        yield return null;
    }

    IEnumerator carCrashingIntoCar()
    {
        grayCar.SetBool("Damage", true);
        blackCar.SetBool("Damage", true);
        yield return null;
        
    }

    IEnumerator fallingTree()
    {
        palm.SetBool("Damage", true);
        yield return null;
    }

    public void ResetAll()
    {
        gameObject.SetActive(false);
        grayCar.SetBool("Damage", false);
        blackCar.SetBool("Damage", false);
        palm.SetBool("Damage", false);

        car.SetBool("Damage", false);
        tree.SetBool("Damage", false);
        //floatDebris.SetActive(false);
        BreakWindows();
        WindowBroken = false;

    }

    public void Flood2()
    {
        //floatDebris.SetActive(true);

    }

    IEnumerator floatingDebris()
    {
        foreach (FloatingDebris floatingDebrisObject in floatingDebrisObjects)
        {
            //floatingDebrisObject.enabled = false;
        }

        yield return new WaitForSeconds(floatingDebrisTimeout);

        foreach (FloatingDebris floatingDebrisObject in floatingDebrisObjects)
        {
            //floatingDebrisObject.enabled = true;
        }        
    }

    public void BreakWindows()
    {
        window1.SetActive(!window1.activeSelf);
        window2.SetActive(!window2.activeSelf);
        window3.SetActive(!window3.activeSelf);
        
        
    }
}
