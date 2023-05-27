using UnityEngine;

public class SpawnObject : GMSubscribe
{
    public GameObject currentPrefab;
    public GameObject currentBlueprint;
    private bool Building;
    private Vector3 hidden;
    private Quaternion blueprintRotation = Quaternion.identity;

    void Awake()
    {
        Subscribe();
        // Values to hide blueprint
        hidden.x = 0;
        hidden.y = -10;
        hidden.z = 0;
    }

    void OnDestroy()
    {
        UnSubscribe();
    }

    public void SetPrefab(GameObject newPrefab, GameObject newBlueprint)
    {
        // Destroy old blueprint
        if (currentBlueprint)
        {
            Destroy(currentBlueprint);
        }

        currentPrefab = newPrefab;
        currentBlueprint = newBlueprint;
        // Instantiate currentBlueprint hidden under the floor for use in building (if not null)
        if (currentBlueprint)
        {
            currentBlueprint = Instantiate(currentBlueprint, hidden, Quaternion.identity);
        }
    }

    void Update()
    {
        if (Building)
        {
            if (currentBlueprint != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 blueprintPosition = hit.point;
                    blueprintPosition.y = 1;
                    currentBlueprint.transform.position = blueprintPosition;
                    currentBlueprint.transform.rotation = blueprintRotation;
                }

                if (Input.GetKey(KeyCode.R)) // "r" key pressed
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        blueprintRotation *= Quaternion.Euler(0, -0.5f, 0);
                    }
                    else
                    {
                        blueprintRotation *= Quaternion.Euler(0, 0.5f, 0);
                    }
                    currentBlueprint.transform.rotation = blueprintRotation;
                }
                
            }

            if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 spawnPosition = hit.point;
                    spawnPosition.y = 1;
                    Instantiate(currentPrefab, spawnPosition, blueprintRotation);
                }
            }
        }
    }

    public override void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        Building = (state == GameManager.GameState.Building);
    }
}
