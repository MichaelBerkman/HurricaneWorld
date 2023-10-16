using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPointerTeleport : MonoBehaviour
{
    public Transform playerTransform;
    public XRBaseInteractor pointer;
    public LayerMask teleportLayer;
    public float maxTeleportDistance = 30.0f;
    public GameObject teleportMarkerPrefab;
    private GameObject currentTeleportMarker;

    private void Awake()
    {
        if (teleportMarkerPrefab)
        {
            currentTeleportMarker = Instantiate(teleportMarkerPrefab);
            currentTeleportMarker.SetActive(false);
        }
    }

    private void Update()
    {
        HandlePointer();
        HandleTeleport();
    }

    private void HandlePointer()
    {
        if (!pointer)
            return;

        Ray ray = new Ray(pointer.transform.position, pointer.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxTeleportDistance, teleportLayer))
        {
            if (currentTeleportMarker)
            {
                currentTeleportMarker.SetActive(true);
                currentTeleportMarker.transform.position = hit.point;
            }
        }
        else
        {
            if (currentTeleportMarker)
                currentTeleportMarker.SetActive(false);
        }
    }

    private void HandleTeleport()
    {
        if (currentTeleportMarker && currentTeleportMarker.activeSelf && Input.GetKeyDown(KeyCode.T))
        {
            playerTransform.position = currentTeleportMarker.transform.position;
        }
    }
}
