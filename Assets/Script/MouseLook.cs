using System;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseYandX = 0,
        MouseY = 1,
        MouseX = 2
    }

    public RotationAxes axes = RotationAxes.MouseYandX;
    
    public float sensivityHor = 4.5f;
    public float sensivityVert = 4.5f;

    public float minimumVert = -45f;
    public float maximumVert = 45f;

    public float verticalRot = 0f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        } else if (axes == RotationAxes.MouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensivityHor;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;
            
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0f);
        }else if (axes == RotationAxes.MouseYandX)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensivityHor;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0f);
        }
    }

    public void EditSensivity(float sensivity)
    {
        sensivityHor = sensivity;
        sensivityVert = sensivity;
    }
}