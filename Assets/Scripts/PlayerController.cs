using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float PLAYER_SPEED = 12f;
    [SerializeField] private float MOUSE_SENSITIVITY = 200f;
    [SerializeField] private float GRAVITY = -9f;

    [SerializeField] private bool isGrounded;

    [SerializeField] private TriggerActor groundTrigger;
    [SerializeField] private CharacterController controller;

    Vector3 velocity;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        groundTrigger.Triggered += GroundedToggle;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MOUSE_SENSITIVITY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MOUSE_SENSITIVITY * Time.deltaTime;

        Turn(mouseX, mouseY);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Move(x, z);

        if (!isGrounded)
            Fall();

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void GroundedToggle(bool grounded)
    {
        isGrounded = grounded;

        if (isGrounded)
            velocity.y = 0;
    }

    private void Turn(float mouseX, float mouseY)
    {
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Move(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * PLAYER_SPEED * Time.deltaTime);
    }

    private void Fall()
    {
        velocity.y += GRAVITY * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
