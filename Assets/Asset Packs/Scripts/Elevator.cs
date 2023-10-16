using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool open;
    public Transform doorL, doorR;

    public Animator elevAnimL;
    public Animator elevAnimR;


    public List<GameObject> buttons;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (open)
            {
                CloseElev();
            }
            else OpenElev();
        }
    }

    public void OpenElev()
    {
        //elevAnimL.SetBool("Open", true);
        //elevAnimR.SetBool("Open", true);
        doorL.Translate(new Vector3(-.95f, 0, 0));
        doorR.Translate(new Vector3(.95f, 0, 0));
        Debug.Log("Open");
        open = true;
    }

    public void CloseElev()
    {

        //elevAnimL.SetBool("Open", false);
        //elevAnimR.SetBool("Open", false);
        doorL.Translate(new Vector3(.95f, 0, 0));
        doorR.Translate(new Vector3(-.95f, 0, 0));
        open = false;

        Debug.Log("Close");
    }

    IEnumerator GoingUp()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
