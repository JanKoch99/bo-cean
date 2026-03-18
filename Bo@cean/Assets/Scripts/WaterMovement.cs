using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float speed = 0.01f;
    private Renderer renderer;


    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * speed;
        renderer.material.mainTextureOffset = new Vector2(0, -offset);
    }
}
