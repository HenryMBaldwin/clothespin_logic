using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float rotationSpeed = 2f; // Speed of camera rotation

    private Vector3 lastMousePosition;

    void Update()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get input for camera rotation using mouse drag
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;

            // Rotate the camera based on mouse movement
            float rotationX = -deltaMouse.y * rotationSpeed;
            float rotationY = deltaMouse.x * rotationSpeed;

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.Self);
        }

        // Check for camera vertical movement (up and down)
        float verticalMovement = 0f;
        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
        {
            verticalMovement = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))
        {
            verticalMovement = -1f;
        }

        // Calculate the movement direction relative to the ground
        Vector3 forwardMovement = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * verticalInput;
        Vector3 rightMovement = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized * horizontalInput;
        Vector3 movement = (forwardMovement + rightMovement).normalized * moveSpeed * Time.deltaTime;

        // Apply movement to the camera's position
        transform.Translate(movement, Space.World);

        // Apply vertical movement to the camera's position
        transform.Translate(Vector3.up * verticalMovement * moveSpeed * Time.deltaTime, Space.World);
    }
}
