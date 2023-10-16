using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{



    public sceneManager scene;
    public Prep prep;
    public Transform house;
    public Vector3 housePos;

    public GameObject r_controller;

    public GameObject windowBoards;

    // Start is called before the first frame update
    void Start()
    {
        housePos = house.position;
    }

    public void ConfirmInput()
    {
        if (r_controller.GetComponent<Renderer>().material.color != Color.red)
        {
            r_controller.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            r_controller.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        housePos = house.position;

    }

    public void houseRaiser()
    {
        house.position = new Vector3(house.position.x, -7.75f, house.position.z);
        scene.houseRaised = true;
    }

    public void insureSigner()
    {
        scene.insuranceSigned = true;
    }

    public void boardWindows()
    {
        prep.BoardWindows();
    }

    public void FemaKit()
    {
        prep.FemaKit();
    }


    public void ShutOffWater()
    {
        prep.ShutOffWater();
    }

    public void ShutOffGas()
    {
        prep.ShutOffGas();

    }

    public void ShutOffElectricity()
    {
        prep.ShutOffElectricity();

    }

    public void ShutOffUtil()
    {
        prep.ShutOffUtil();

    }

}
