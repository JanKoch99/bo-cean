using UnityEngine;

public class LightningClickSpawner : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float spawnHeight = 5f; //over 5 it doesnt work idk why
    public bool canStrike = false;

    void Update()
    {
        if (canStrike && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 spawnPos = hit.point + Vector3.up * spawnHeight;
                Instantiate(lightningPrefab, spawnPos, Quaternion.identity);
                canStrike = false;
            }
        }
    }

    public void EnableLightning()
    {
        canStrike = true;
    }
}