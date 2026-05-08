using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    private Renderer waterRenderer;
    private float textureOffsetY;

    void Start()
    {
        waterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (WaterManager.Instance == null)
        {
            return;
        }

        float speed = WaterManager.Instance.waterAnimationSpeed;
        textureOffsetY += speed * Time.deltaTime;
        waterRenderer.material.mainTextureOffset = new Vector2(0, -textureOffsetY);
    }
    
    public void ResetWater()
    {
        textureOffsetY = 0f;

        waterRenderer.material.mainTextureOffset =
            Vector2.zero;
    }
}