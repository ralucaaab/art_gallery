using UnityEngine;

public class PaintingInfoPanel : MonoBehaviour
{
    [Header("Referințe")]
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private Transform[] controllerTransforms;
    [SerializeField] private float proximityRadius = 0.5f;

    [Header("Gaze")]
    [SerializeField] private Camera vrCamera;
    [SerializeField] private float gazeMaxDistance = 10f;

    private bool isGazing = false;
    private bool isNearController = false;

    private void Awake()
    {
        if (infoCanvas != null)
            infoCanvas.SetActive(false);

        if (vrCamera == null)
            vrCamera = Camera.main;
    }

    private void Update()
    {
        CheckGaze();
        CheckProximity();
        UpdateVisibility();
    }

    private void CheckGaze()
    {
        if (vrCamera == null) return;

        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, gazeMaxDistance))
        {
            isGazing = hit.transform == transform;
        }
        else
        {
            isGazing = false;
        }
    }

    private void CheckProximity()
    {
        bool nearAny = false;
        foreach (var ctrl in controllerTransforms)
        {
            if (ctrl != null && Vector3.Distance(ctrl.position, transform.position) <= proximityRadius)
            {
                nearAny = true;
                break;
            }
        }
        isNearController = nearAny;
    }

    private void UpdateVisibility()
    {
        infoCanvas.SetActive(isGazing || isNearController);
    }
}