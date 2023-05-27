using UnityEngine;

public class SpawnObject : GMSubscribe
{
    public GameObject currentPrefab;
    public GameObject currentBlueprint;
    private bool Building;
    private Vector3 hidden;
    void Awake(){
        Subscribe();
        //values to hide blueprint
        hidden.x = 0;
        hidden.y = -10;
        hidden.z = 0;
    }

    void OnDestroy(){
        UnSubscribe();
    }

    
    public void SetPrefab(GameObject newPrefab, GameObject newBlueprint)
    {
        //destroy old blupring
        if (currentBlueprint)
        {
            Destroy(currentBlueprint);
        }

        currentPrefab = newPrefab;
        currentBlueprint = newBlueprint;
        //instatiate currentBlueprint hidden under the floor for use in building (if not null)

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
                }
            }

            if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 spawnPosition = hit.point;
                    spawnPosition.y = 1;
                    Instantiate(currentPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }


    public override void GameManagerOnGameStateChanged(GameManager.GameState state){
        Building = (state == GameManager.GameState.Building);
    }
}
