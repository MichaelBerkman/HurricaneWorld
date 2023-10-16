using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject surveyMenu;
    public GameObject startMenu;
    public Survey survey;
   // public Text debugText;
    public Elevator elev;
    public List<GameObject> avatars;
    public int avatarIndex;
    public AudioSource elevatorMove;
    public AudioSource elevatorDing;

    public GameObject handL, handR, handLaser;

    public DataCollection data;

    public Transform pos1, pos2, pos3, spawn;

    // Start is called before the first frame update
    void Start()
    {

       


        avatarIndex = 0;
        //debugText = GameObject.Find("Test").GetComponent<Text>();
        //data.WriteData();
        //debugText.gameObject.SetActive(true);

        survey = GameObject.FindObjectOfType<Survey>();
        surveyMenu.SetActive(false);
        data.InitializeData();

        //StartSimulation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIDebug(string words)
    {
       // debugText.text = words;
    }
    
    public void StartSimulation()
    {
        
        handL.SetActive(false);
        handR.SetActive(false);
        handLaser.SetActive(false);
        startMenu.SetActive(false);
        StartCoroutine(WalkIn());
        
    }

    IEnumerator WalkIn()
    {
        survey.surveyFin = false;
        yield return new WaitForSeconds(10f);
        //Intro Sequence
        //AvatarWalk(0);

        elevatorDing.Play();

        elev.OpenElev();
        //Elevator Open

        avatars[avatarIndex].gameObject.SetActive(true);
        
        Animator ava = avatars[avatarIndex].GetComponent<Animator>();
        
        ava.SetBool("Walking", true);
        Debug.Log("TrueWalking");
        while(avatars[avatarIndex].transform.position != pos1.position)
        {
            avatars[avatarIndex].transform.position = Vector3.MoveTowards(avatars[avatarIndex].transform.position, pos1.position, 35.5f * Time.fixedDeltaTime);
            yield return new WaitForSeconds(.05f);
        }
        ava.SetBool("Walking", false);
        Debug.Log("WalkingFalse");

        yield return new WaitForSeconds(1f);

        //Person Enter
        //person enter code goes here

        elev.CloseElev();
       // elevatorDing.Play();

        yield return new WaitForSeconds(1f);
        elevatorMove.Play();
        yield return new WaitForSeconds(10f);
        StartCoroutine(Survey());
        //StartCoroutine(WalkOut());
    }

    IEnumerator Survey()
    {
        handR.SetActive(true);
        handL.SetActive(true);
        handLaser.SetActive(true);


        surveyMenu.SetActive(true);
        yield return new WaitForSeconds(1f);

        while (survey.surveyFin == false)
        {
            yield return null;
        }
        survey.surveyFin = true;
        StartCoroutine(WalkOut());

    }

    IEnumerator WalkOut()
    {

        handL.SetActive(false);
        handR.SetActive(false);
        handLaser.SetActive(false);

        elevatorDing.Play();

        elev.OpenElev();
        yield return new WaitForSeconds(1);

     

        Animator ava = avatars[avatarIndex].GetComponent<Animator>();

        if (ava == null)
        {
            EndScene();
            Debug.Log("AvaDone");
            
        }

        ava.SetBool("Turning", true);


        while(ava.transform.rotation.eulerAngles.y < 180)
        {
        avatars[avatarIndex].transform.Rotate(new Vector3(0, 105, 0) * Time.deltaTime);
        yield return new WaitForSeconds(0.0001f);
        }



        //yield return new WaitForSeconds(.75f);

        ava.SetBool("Turning", false);

        yield return new WaitForSeconds(1f);


        ava.SetBool("Walking", true);

        Debug.Log("TrueWalking");

        while (avatars[avatarIndex].transform.position != spawn.position)
        {
            avatars[avatarIndex].transform.position = Vector3.MoveTowards(avatars[avatarIndex].transform.position, spawn.position,  35.5f * Time.fixedDeltaTime);
            yield return new WaitForSeconds(.05f);
        }
        //elevatorDing.Play();

        elev.CloseElev();

        ava.SetBool("Walking", false);
        Debug.Log("WalkingFalse");
        avatars[avatarIndex].gameObject.SetActive(false);

        avatarIndex++;

        StartCoroutine(WalkIn());

    }

    public void EndScene()
    {
       // debugText.gameObject.SetActive(true);
       // debugText.text = "Exiting";

        StartCoroutine(Wait(5));
        Application.Quit();
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }


    public void AvatarWalk(int avaIndex)
    {
        

    }
}
