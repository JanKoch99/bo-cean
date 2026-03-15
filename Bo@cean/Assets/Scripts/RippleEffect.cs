using UnityEngine;

public class RippleEffect : MonoBehaviour
{
    public float growSpeed = 4f;
    public float fadeSpeed = 1f;

    Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Ripple is growing
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        
        Destroy(gameObject, 1f);
    }
}
