using UnityEngine;

public class PuddleTrigger : MonoBehaviour
{
    public Transform waterTransform;
    private float initialWaterY;
    public GameObject puddleEffectPrefab;

    private void Start()
    {
        if (waterTransform)
        {
            initialWaterY = waterTransform.position.y;
        }
    }

    private void Update()
    {
        if (waterTransform.position.y > initialWaterY + 2)
        {
            ActivatePuddles();
        }
    }

    private void ActivatePuddles()
    {
        Collider[] objectsBelowWater = Physics.OverlapBox(waterTransform.position, new Vector3(50, 1, 50), Quaternion.identity);

        foreach (Collider obj in objectsBelowWater)
        {
            if (obj.transform.position.y < waterTransform.position.y)
            {
                GameObject puddle = Instantiate(puddleEffectPrefab, obj.transform.position, Quaternion.identity);
                puddle.transform.SetParent(obj.transform);
                Animator puddleAnimator = puddle.GetComponent<Animator>();
                if (puddleAnimator)
                {
                    puddleAnimator.SetTrigger("ActivatePuddle");
                }
            }
        }
    }
}
