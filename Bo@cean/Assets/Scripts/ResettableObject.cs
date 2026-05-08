using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void ResetObject()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
