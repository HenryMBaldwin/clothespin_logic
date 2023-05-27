using UnityEngine;

public class CameraRig : GMSubscribe
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float rotationSpeed = .8f; // Speed of camera rotation
    public float rotationSmoothing = 50f; // Smoothing factor for camera rotation

    private Vector3 lastMousePosition;
    private Vector3 previousMovement;
    private Quaternion targetRotation;

    public bool freeCam;

    private float minVerticalRotation = 0f; // Minimum vertical rotation angle (horizon)
    private float maxVerticalRotation = 90f; // Maximum vertical rotation angle (straight down)

    void Awake()
    {
        Subscribe();
        targetRotation = transform.rotation; // Initialize targetRotation with current rotation
    }

    void OnDestroy()
    {
        UnSubscribe();
    }

    void Update()
    {
        if (freeCam)
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

                float currentRotationX = targetRotation.eulerAngles.x;
                float clampedRotationX = Mathf.Clamp(currentRotationX + rotationX, minVerticalRotation, maxVerticalRotation);

                targetRotation = Quaternion.Euler(clampedRotationX, targetRotation.eulerAngles.y + rotationY, 0f);
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

            // Smoothly interpolate between previous and current movement
            Vector3 movement = Vector3.Lerp(previousMovement, forwardMovement + rightMovement, 0.5f).normalized * moveSpeed * Time.deltaTime;
            previousMovement = forwardMovement + rightMovement;

            // Apply movement to the camera's position
            transform.Translate(movement, Space.World);

            // Apply vertical movement to the camera's position
            transform.Translate(Vector3.up * verticalMovement * moveSpeed * Time.deltaTime, Space.World);

            // Smoothly rotate the camera towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
        }
    }

    public override void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        Debug.Log(state);
        freeCam = (state == GameManager.GameState.FreeCam);
    }
}
