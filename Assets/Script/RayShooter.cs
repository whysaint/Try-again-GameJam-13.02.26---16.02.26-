using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private CoffeeMachine coffeeMachine;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private LayerMask machineLayer;

    private Camera _cam;
    private GameObject heldObject;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
            {
                if (TryTakeCoffee())
                    return;

                TryPickUp();
            }
            else
            {
                if (!TrySetCoffee())
                    DropObject();
            }
        }
    }

    bool TryPickUp()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, pickupLayer))
        {
            if (coffeeMachine.IsBrewing)
                return false;

            GameObject obj = hit.collider.gameObject;

            heldObject = obj;

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            obj.transform.SetParent(holdPoint);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            return true;
        }

        return false;
    }

    bool TrySetCoffee()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, machineLayer))
        {
            if (coffeeMachine.TryPlaceCup(heldObject))
            {
                heldObject = null;
                return true;
            }
        }

        return false;
    }

    bool TryTakeCoffee()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, machineLayer))
        {
            GameObject cup = coffeeMachine.TryTakeCoffee();

            if (cup != null)
            {
                heldObject = cup;

                cup.transform.SetParent(holdPoint);
                cup.transform.localPosition = Vector3.zero;
                cup.transform.localRotation = Quaternion.identity;

                Rigidbody rb = cup.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                return true;
            }
        }

        return false;
    }

    void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        heldObject.transform.SetParent(null);
        heldObject = null;
    }
}
