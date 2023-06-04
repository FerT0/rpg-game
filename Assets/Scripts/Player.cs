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
    private float m_AnimHorizontal, m_AnimVertical;
    private Animator anim;
    private CharacterController characterController;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private float m_SmoothingSpeed = 2;



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
            Animate(horizontalInput, verticalInput);

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
            if ((staminaRegenTimer >= staminaTimeToRegen) && (HandleStamina.stamina < 100))
            {
                HandleStamina.stamina += 0.1f;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
            }
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

    private void Animate(float horizontalInput, float verticalInput)
    {
        float multiplier = anim.GetBool("isRunning") ? 1 : 0.5f;
        float targetHorizontal = horizontalInput * multiplier;
        float targetVertical = verticalInput * multiplier;
        if (Mathf.Abs(m_AnimHorizontal - targetHorizontal) < 0.1f)
        {


            m_AnimHorizontal = targetHorizontal;
        }
        if (Mathf.Abs(m_AnimVertical - targetVertical) < 0.1f)
        {

            
            m_AnimVertical = targetVertical;
        }
        if (m_AnimHorizontal < targetHorizontal)
        {
            m_AnimHorizontal += Time.deltaTime * m_SmoothingSpeed;
        } else if (m_AnimHorizontal > targetHorizontal)
        {
            m_AnimHorizontal -= Time.deltaTime * m_SmoothingSpeed;
       
        }
        if (m_AnimVertical < targetVertical)
        {
            m_AnimVertical += Time.deltaTime * m_SmoothingSpeed;
        } else if (m_AnimVertical > targetVertical)
        {
            m_AnimVertical -= Time.deltaTime * m_SmoothingSpeed;
            
        }
        anim.SetFloat("horizontal", m_AnimHorizontal);
        anim.SetFloat("vertical", m_AnimVertical);

    }



    


}
