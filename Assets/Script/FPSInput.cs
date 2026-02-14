using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
    public float baseSpeed = 6f;
    public float sprintMultiplier = 1.5f;
    public float gravity = -9.8f;
    
    [SerializeField] private GameObject screamerObject;
    [SerializeField] private AudioSource screamerSound;

    [SerializeField] private GameObject lastEnemy;

    private bool screamerTriggered = false;

    private CharacterController controller;
    private float currentSpeed;
    private float yVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isGameOver) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMovingInput = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f;

        // Если свет выключен и игрок пытается двигаться
        if (GameManager.Instance != null && !GameManager.Instance.lightOn && isMovingInput)
        {
            TriggerScreamer();
            StartCoroutine(GameOver());
            return;
        }

        HandleMovement(x, z);
    }


    
    void TriggerScreamer()
    {
        if (screamerTriggered) return;

        screamerTriggered = true;

        if (screamerObject != null)
            screamerObject.SetActive(true);

        if (screamerSound != null)
            screamerSound.Play();
    }
    
    
    void HandleMovement(float x, float z)
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        currentSpeed = isSprinting 
            ? baseSpeed * sprintMultiplier 
            : baseSpeed;

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

    private bool isGameOver = false;

    private IEnumerator GameOver()
    {
        isGameOver = true;
        lastEnemy.SetActive(true);
        screamerSound.Play();
        
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainManu");
    }

}