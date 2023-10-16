using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTrigger : MonoBehaviour {

	public Animator anim;
	public GameObject room2floors;
    public int teleports;
    public bool ready;
	public bool quest;
	[TextArea]
	public string scene2Change;
	// Use this for initialization
	void Start () {
        //room2floors.layer = LayerMask.NameToLayer("Default");
		if (quest) {
			scene2Change = "TybeeQuest";
		}
		else if (!quest) {
			scene2Change = "THIS is the main scene";
		}
  
	}
	
	// Update is called once per frame
	void Update () {
  
	}

	void OnTriggerEnter(Collider c) {

		if (c.GetComponent<PlayerCollider>() != null) {
			anim.SetBool("Open", true);
			print("OPEN!");
			SceneManager.LoadScene(1);
		} else if (c.name == "TrackingSpace") {

			SceneManager.LoadScene(scene2Change);

		}

	}
}
