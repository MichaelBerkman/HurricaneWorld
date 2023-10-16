using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour
{
    [SerializeField]
    private RouteControlPoint[] controlPoints;

    [SerializeField]
    private float speed;
    private int current = 0;
    private bool isStopped = false;
    private Vector3 direction;
    private Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            direction = (controlPoints[current < controlPoints.Length ? current : 0].transformPoint.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * Time.deltaTime);

            if (current == controlPoints.Length) {
                Debug.Log("Raft has completed the route");
                isStopped = true;
            } else if (transform.position != controlPoints[current].transformPoint.position)
            {
                Vector3 position = Vector3.MoveTowards(transform.position, controlPoints[current].transformPoint.position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(position);
            } else if (controlPoints[current].isStop && !controlPoints[current].isVisited)
            {
                StartCoroutine(stopRaft());
            } else if (current <= controlPoints.Length) {
                current++;
            }
        }
    }

    public IEnumerator stopRaft()
    {
        isStopped = true;
        yield return new WaitForSeconds(controlPoints[current].timeout);
        isStopped = false;
        current++;
    }
}
