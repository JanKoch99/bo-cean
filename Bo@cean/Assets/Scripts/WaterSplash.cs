using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    public GameObject ripplePrefab;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 pos = collision.contacts[0].point;
        
        Instantiate(ripplePrefab, pos + Vector3.up * 0.02f, Quaternion.Euler(90, 0, 0));
    }
}
