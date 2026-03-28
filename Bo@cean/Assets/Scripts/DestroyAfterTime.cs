using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 3.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}