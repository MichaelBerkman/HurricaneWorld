using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMenuManager : MonoBehaviour
{


    public List<GameObject> objList;
    public List<GameObject> objPrefabList;
    public int currentObj = 0;
    public int objCount = 0;
    public int objMax = 15;
    public GameObject hand;
	public GameObject parent;



    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            objList.Add(child.gameObject);
        }
    }

    // Move once to the left through the object menu or if there is no object to the left, move to the end of the list
    public void MenuLeft()
    {
        objList[currentObj].SetActive(false);
        currentObj--;
        if (currentObj < 0)
        {
            currentObj = objList.Count - 1;
        }
        objList[currentObj].SetActive(true);
    }

    // Move once to the right through the object menu or if there is no object to the right, move to the beginning of the list.
    public void MenuRight()
    {
        objList[currentObj].SetActive(false);
        currentObj++;
        if (currentObj > objList.Count - 1)
        {
            currentObj = 0;
        }
        objList[currentObj].SetActive(true);
    }

    // Spawn the currently selected object.
    public void SpawnCurrentObject()
    {
        if (objCount <= objMax)
        {
            GameObject furnitureSpawn =
            Instantiate(objPrefabList[currentObj], objList[currentObj].transform.position + hand.transform.forward,
               objList[currentObj].transform.rotation);
            furnitureSpawn.AddComponent<floatingObject>();
            furnitureSpawn.layer = 10;

            sceneManager.scene.furniture.Add(new Furn(furnitureSpawn, furnitureSpawn.transform));
            furnitureSpawn.transform.parent = sceneManager.scene.furnitureParent.transform;

            objList[currentObj].SetActive(false);
            objCount++;
        }
    }

	[System.Serializable]
	public class Furn {
		public Vector3 originalPosition;
		public Quaternion originalRotation;
		public GameObject gameobject;

		public Furn(GameObject g, Transform t) {
			originalPosition = t.position;
			originalRotation = t.rotation;
			gameobject = g;
		}
		
	}
}