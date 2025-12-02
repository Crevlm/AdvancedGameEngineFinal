using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private InputSystem_Actions input;
    Vector2 moveInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = .5f;
    [SerializeField] private float gravity = -9.81f; //Earth's gravity rate

    private float yVelocity;
    private Transform playerCamera;
    private float xRotation = 0f;

    //Stamina 
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaDrain = 1.5f;
    [SerializeField] private float staminaRegen = 1f;
    [SerializeField] private Slider staminaBar;

    [SerializeField] private float sprintMultipler = 1.7f;
    private bool isSprinting;

    public bool canLook = true;


    private void OnEnable()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().transform;

        stamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }
    }

    void Update()
    {
        bool sprintPressed = input.Player.Sprint.ReadValue<float>() > 0f;

        if (sprintPressed && stamina > 0f)
        {
            stamina -= staminaDrain * Time.deltaTime;
            isSprinting = true;
        }
        else
        {
            stamina += staminaRegen * Time.deltaTime;
            isSprinting = false;
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        if (stamina <= 0.01f)
        {
            isSprinting = false;
        }

        float currentSpeed = isSprinting ? moveSpeed * sprintMultipler : moveSpeed;

        if (isSprinting)
        {
            currentSpeed *= sprintMultipler;
            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            stamina += staminaRegen * Time.deltaTime;
        }

        if (staminaBar != null)
        {
            staminaBar.value = stamina;
        }

        //Read movement input
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        //Camera relative movement
        Vector3 camForward = playerCamera.forward;
        Vector3 camRight = playerCamera.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * moveInput.y + camRight * moveInput.x;



        //Gravity
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        if (!controller.isGrounded)
        {
            yVelocity += -5f * Time.deltaTime;
        }

        Vector3 finalMove = (moveDir * currentSpeed) + (Vector3.up * yVelocity);

        controller.Move(finalMove * Time.deltaTime);

        if (canLook)
        {
            Vector2 lookInput = input.Player.Look.ReadValue<Vector2>() * lookSensitivity;
            transform.Rotate(Vector3.up * lookInput.x);
            xRotation -= lookInput.y;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
