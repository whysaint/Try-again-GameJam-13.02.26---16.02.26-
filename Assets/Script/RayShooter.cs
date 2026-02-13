using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1.9f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform coffeeMashinePoint;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private LayerMask coffeeMashiteLayer;

    private bool isInCoffeeMashine;

    private Camera _cam;
    private GameObject heldObject;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject == null)
            {
                TryPickUp();
            }
            else
            {
                if (!TrySetCoffee() || isInCoffeeMashine)
                {
                    DropObject();
                }
            }
        }
    }


    void TryPickUp()
    {
        Vector3 point = new Vector3(_cam.pixelWidth / 2, _cam.pixelHeight / 2);
        Ray ray = _cam.ScreenPointToRay(point);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, pickupLayer))
        {
            GameObject obj = hit.collider.gameObject;

            heldObject = obj;

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            obj.transform.SetParent(holdPoint);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }

    bool TrySetCoffee()
    {
        Vector3 point = new Vector3(_cam.pixelWidth / 2, _cam.pixelHeight / 2);
        Ray ray = _cam.ScreenPointToRay(point);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance / 2, coffeeMashiteLayer))
        {
            heldObject.transform.SetParent(coffeeMashinePoint);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            heldObject = null;
            return true;
        }

        return false;
    }

    void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        heldObject.transform.SetParent(null);
        heldObject = null;
    }
}