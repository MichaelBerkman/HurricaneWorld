using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRMenuPopup : MonoBehaviour
{
    public GameObject menuXR;
    private bool isMenuVisible = false;
    public XRBaseInteractor pointer;

    private void Awake()
    {
        menuXR.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }

        RaycastHit hit;
        if (Physics.Raycast(pointer.transform.position, pointer.transform.forward, out hit))
        {
            if (hit.collider.gameObject == menuXR)
            {
                SelectMenuItem(hit);
            }
        }
    }

    private void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuXR.SetActive(isMenuVisible);
    }

private void SelectMenuItem(RaycastHit hit)
{
    string itemName = hit.collider.gameObject.name;
    GameObject spawnedObject;
    switch (itemName)
    {
        case "Item1":
            spawnedObject = Instantiate(Resources.Load<GameObject>("Prefab1"), new Vector3(0, 1, 0), Quaternion.identity);
            spawnedObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            break;
        case "Item2":
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            hit.collider.gameObject.GetComponent<Renderer>().material.color = randomColor;
            break;
        case "Item3":
            float randomScale = Random.Range(0.5f, 1.5f);
            hit.collider.gameObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            break;
        case "Item4":
            Rigidbody rb = hit.collider.gameObject.AddComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 500f);
            break;
        case "MoveItem":
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            hit.collider.gameObject.transform.Translate(randomDirection);
            break;
        case "DespawnItem":
            Destroy(hit.collider.gameObject);
            break;
        case "RespawnItem":
            spawnedObject = Instantiate(hit.collider.gameObject, new Vector3(0, 2, 0), Quaternion.identity);
            break;
        case "ChangeColorItem":
            Color newColor = new Color(Random.value, Random.value, Random.value);
            hit.collider.gameObject.GetComponent<Renderer>().material.color = newColor;
            break;
        case "DeleteItem":
            Destroy(hit.collider.gameObject);
            break;
        default:
            Debug.Log("Unknown item selected.");
            break;
    }
}

