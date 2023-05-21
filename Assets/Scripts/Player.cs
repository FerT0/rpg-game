using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float walkSpeed = 4.0f;
    private float runSpeed = 8.0f;
    public float rotationSpeed;
    private float staminaRegenTimer = 0.0f;
    private float staminaTimeToRegen = 3.0f;
    private Animator anim;
    private CharacterController characterController;
    [SerializeField]
    private Transform cameraTransform;
    
    
    void Start ()
    {
        anim = this.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
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
            
        } else
        {
            anim.SetBool("isMoving", false);
        }

        if (HandleStamina.stamina > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        } else
        {
            anim.SetBool("isRunning", false);
        }

        if (anim.GetBool("isRunning") == true)
        {
            speed = runSpeed;
            HandleStamina.stamina -= 0.3f;
            staminaRegenTimer = 0.0f;
        } else
        {
            speed = walkSpeed;
            if (staminaRegenTimer >= staminaTimeToRegen)
            {
                HandleStamina.stamina += 0.1f;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
            }
        }
        
        Debug.Log(HandleStamina.stamina);
        
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
