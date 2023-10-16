using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaItems : MonoBehaviour
{
    public GameObject target;
    public bool move = false;
    public GameObject item = null;
    public Transform p1, p2, p3;
    //public GameObject item1, item2, item3, item4, item5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // if (move == true)
        {
           // item.transform.position = Vector3.MoveTowards(item.transform.position, target.transform.position, 5);
        }
       // if (item.transform.position == target.transform.position)
        {
           //move = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
           StartCoroutine(MoveItem(item.transform));
        }
    } 

    public void AddItem(GameObject item)
    {
        StartCoroutine(MoveItem(item.transform));
            

        
        //item.SetActive(!item.activeSelf);
    }

    public IEnumerator MoveItem(Transform item)
    {
        while (item.position != p1.position)
        {
            item.position = Vector3.MoveTowards(item.position , p1.position, .01f);
            yield return new WaitForSeconds(.01f);
        }

        while (item.position != p2.position)
        {
            item.position = Vector3.MoveTowards(item.position, p2.position, .01f);
            yield return new WaitForSeconds(.01f);

        }

        while (item.position != p3.position)
        {
            item.position = Vector3.MoveTowards(item.position, p3.position, .01f);
            yield return new WaitForSeconds(.01f);

        }

        while (item.position != target.transform.position)
        {
            item.position = Vector3.MoveTowards(item.position, target.transform.position, .01f);
            yield return new WaitForSeconds(.01f);

        }
    }

    
}
