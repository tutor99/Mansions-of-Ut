using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    bool isShiftPressed = false;
    bool speedIncreased = false;
    float lastShiftPressTime;
    private float originalMoveSpeed;
    int tired = 0;
    public float groundDrag;
    

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        originalMoveSpeed = moveSpeed;      
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        float timeSinceLastShiftPress = Time.time - lastShiftPressTime;

        if (Input.GetKey(KeyCode.LeftShift) && !isShiftPressed && !speedIncreased && tired == 0)
        {
            isShiftPressed = true; // Marcar que la tecla Shift está presionada
            moveSpeed *= 2; // Aumentar la velocidad en 5
            speedIncreased = true; // Marcar que la velocidad ha sido incrementada

        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || tired >= 8 )
        {
            moveSpeed = originalMoveSpeed; // Reducir la velocidad a la mitad
            isShiftPressed = false; // Restablecer el estado de la tecla Shift
            speedIncreased = false; // Restablecer el estado de la velocidad aumentada
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Si ha pasado 1 segundo desde la última vez que se presionó la tecla Shift
            if (timeSinceLastShiftPress >= 1f)
            {
                tired++; // Aumentar la variable tired
                lastShiftPressTime = Time.time; // Actualizar el tiempo de la última presión de tecla Shift
            }
        }
        if (Time.time - lastShiftPressTime >= 5f)
        {
            tired = 0;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}