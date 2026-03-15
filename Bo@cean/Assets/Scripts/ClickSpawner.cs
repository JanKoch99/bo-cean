using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public float spawnHeight = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Linksklick
        {
            SpawnSphere();
        }
    }

    void SpawnSphere()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 spawnPos = hit.point + Vector3.up * spawnHeight;
            Instantiate(spherePrefab, spawnPos, Quaternion.identity);
        }
    }
}
