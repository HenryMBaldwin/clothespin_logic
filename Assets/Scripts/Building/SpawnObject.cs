using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject pinPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 spawnPosition = hit.point;
                Instantiate(pinPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
