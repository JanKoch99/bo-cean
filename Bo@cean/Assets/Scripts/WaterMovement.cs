using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    private Renderer renderer;
    private float textureOffsetY;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (WaterManager.Instance == null)
        {
            return;
        }

        float speed = WaterManager.Instance.waterAnimationSpeed;
        textureOffsetY += speed * Time.deltaTime;
        renderer.material.mainTextureOffset = new Vector2(0, -textureOffsetY);
    }
}
