using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

	sceneManager scene;



	public enum FloodState { Raising, Paused, Receding, Complete, None }
    
	public FloodState state = FloodState.None;
  

	public Vector2 waterLevelRange;
	public float waterLevel;
	public Vector3 riseSpeed;
    public Vector3 lowerSpeed;

    bool check;

    void Start() {

		conditionData.flood = 3;
        
        if (conditionData.flood == 2) //Low Surge
        {
            waterLevelRange.y = -2.1f;
        }
        else if (conditionData.flood == 3)//Mid Surge
        {
            waterLevelRange.y = -1.1f;
        }
        else //High Surge
        {
            waterLevelRange.y = 1f;
        }
     
		
        
		waterLevel = transform.position.y;
		waterLevelRange.x = transform.position.y;
    }


	
    // Update is called once per frame
	void Update () {
		waterLevel = transform.position.y;
		//waterLevelRange.y = -1.6f;

		if (state == FloodState.Paused)
        {
			transform.Translate(-riseSpeed);
        }

	}

	public IEnumerator Flood(float completeTime) {
		state = FloodState.Raising;
		while (waterLevel <= waterLevelRange.y) {
			transform.Translate(riseSpeed);
			yield return null;
		}

		//state = FloodState.Paused;
        

		state = FloodState.Complete;
		yield return new WaitForSeconds(completeTime);
        if (conditionData.flood == 2)
        {
            yield return new WaitForSeconds(completeTime);
            yield return new WaitForSeconds(completeTime);
            yield return new WaitForSeconds(completeTime);
            yield return new WaitForSeconds(completeTime);
            
        }
		state = FloodState.Receding;

		while (waterLevel >= waterLevelRange.x) {
			transform.Translate(lowerSpeed);
			yield return null;
		}
		//If flood1 already isn't true, then now it is
		if (sceneManager.scene.floods != 1) {
			print("Water has reached maximum level");
			sceneManager.scene.floods = 1;
		}
		//If the water has returned back to it's original position, stop receding
		if (waterLevel <= waterLevelRange.x && state == FloodState.Receding) {
			print("Water has receded");
			sceneManager.scene.floods = 1;
			state = FloodState.None;

		}
		state = FloodState.None;
	}
}
