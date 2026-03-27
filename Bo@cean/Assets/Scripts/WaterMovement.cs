using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float speed = WaterManager.Instance.waterAnimationSpeed;
        float offset = Time.time * speed;
        renderer.material.mainTextureOffset = new Vector2(0, -offset);
    }
}
