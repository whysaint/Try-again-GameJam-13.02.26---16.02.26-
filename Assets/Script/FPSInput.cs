using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
    public float baseSpeed = 6f;
    public float sprintMultiplier = 1.5f;
    public float gravity = -9.8f;

    private CharacterController controller;
    private float currentSpeed;
    private float yVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = isSprinting 
            ? baseSpeed * sprintMultiplier 
            : baseSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded)
        {
            yVelocity = 0f;
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = move * currentSpeed;
        finalMove.y = yVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }
}