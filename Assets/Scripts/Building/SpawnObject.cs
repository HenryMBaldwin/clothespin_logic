using UnityEngine;

public class SpawnObject : GMSubscribe
{
    public GameObject pinPrefab;

    private bool Build;

    void Awake(){
        Subscribe();
    }

    void OnDestroy(){
        UnSubscribe();
    }
    void Update()
    {
        if (Build && Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 spawnPosition = hit.point;
                spawnPosition.y = 1;
                Instantiate(pinPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    public override void GameManagerOnGameStateChanged(GameManager.GameState state){
        Build = (state == GameManager.GameState.Build);
    }
}
