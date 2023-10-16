using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prep : MonoBehaviour
{

    public sceneManager scene;

    public GameObject femaButton;
    public GameObject boardButton;


    public GameObject femaKit;
    public GameObject windowBoards;
    public GameObject waterButton;
    public GameObject gasButton;
    public GameObject electButton;

    public bool finished, boarded, femaReady, waterOff, elecOff, gasOff;
    private int count;

    public AudioSource waterSound, electricSound, gasSound, femaKitSound;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= 5)
        {
            finished = true;

        }
    }

    public void BoardWindows()
    {
        windowBoards.SetActive(!windowBoards.activeSelf);
        scene.sun.intensity = .01f;
        RenderSettings.ambientIntensity = .2f;
        boarded = true;
        count++;
    }

    public void FemaKit()
    {
        femaKitSound.Play();
        femaReady = true;
        femaKit.SetActive(!femaKit.activeSelf);
        count++;
        scene.femaKit = true;
    }

    public void ShutOffElectricity()
    {
        electricSound.Play();
        ShutOffUtil();
        Debug.Log("ShutOffElectricity");

    }
    public void ShutOffGas()
    {
        gasOff = true;
        gasSound.Play();
        Debug.Log("ShutOffGas");

    }
    public void ShutOffWater()
    {
        waterSound.Play();
        waterOff = true;
        Debug.Log("ShutOffWater");

    }

    public void ShutOffUtil()
    {

        elecOff = true;

        scene.ShutOffUtility("electricity");
        Debug.Log("ShutOffUtil");
        count += 3;

    }


}
