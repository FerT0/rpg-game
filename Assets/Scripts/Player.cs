using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float walkSpeed = 4.0f;
    private float runSpeed = 8.0f;
    public float rotationSpeed;
    private Animator anim;
    private CharacterController characterController;
    [SerializeField]
    private Transform cameraTransform;
    
    
    void Start ()
    {
        anim = this.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01 (movementDirection.magnitude) * speed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            anim.SetBool("isMoving", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                anim.SetBool("isRunning", true);
                HandleStamina.stamina -= 0.05f;
            } else
            {
                speed = walkSpeed;
                anim.SetBool("isRunning", false);
            }
        } else
        {
            anim.SetBool("isMoving", false);
        }
        
    }

    private void OnApplicationFocus(bool focus)
    {
        
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


}
