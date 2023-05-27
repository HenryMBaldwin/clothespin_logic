using UnityEngine;

public class SpawnObject : GMSubscribe
{
    public GameObject currentPrefab;
    public GameObject currentBlueprint;
    private bool Building;

    void Awake(){
        Subscribe();
    }

    void OnDestroy(){
        UnSubscribe();
    }

    
    public void SetPrefab(GameObject newPrefab, GameObject newBlueprint)
    {
        currentPrefab = newPrefab;
        currentBlueprint = newBlueprint;
    }

    void Update()
    {
        if (Building && Input.GetMouseButtonDown(0)) // Left mouse button clicked
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

    public override void GameManagerOnGameStateChanged(GameManager.GameState state){
        Building = (state == GameManager.GameState.Building);
    }
}
