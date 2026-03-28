using UnityEngine;
using System.Collections;
public class WaterManager : MonoBehaviour
{
    public static WaterManager Instance;

    [Range(0f, 5f)] public float waterSpeed = 1f;
    public float waterAnimationSpeed = 0.2f;
    
    [Range(0f, 1f)] public float waterHeight = 0f;
    private float baseHeight = 0f;
    private Coroutine waterRoutine;

    public Vector3 flowDirection = Vector3.back;
    
    void Awake()
    {
        Instance = this;
        baseHeight = waterHeight;
        waterAnimationSpeed = waterSpeed * 0.02f;
    }

    public void IncreaseWaterHeightButton()
    {
        IncreaseWaterHeight(3f);
    }
    public void IncreaseWaterHeight(float amount)
    {
        if (waterRoutine != null)
        {
            return;
        }

        waterRoutine = StartCoroutine(WaterPulse(amount));
    }

    private IEnumerator WaterPulse(float amount)
    {
        float startHeight = waterHeight;
        float targetHeight = baseHeight + amount;
        
        // Water should rise in 0.5s
        float time = 0f;
        float duration = 0.5f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            waterHeight = Mathf.Lerp(startHeight, targetHeight, t);
            yield return null;
        }
        
        waterHeight = targetHeight;
        
        // Wait for 1s before resetting
        yield return new WaitForSeconds(1f);
        
        // Water should fall back to base height in 0.5s
        time = 0f;
        duration = 0.5f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            waterHeight = Mathf.Lerp(targetHeight, baseHeight, t);
            yield return null;
        }

        waterHeight = baseHeight;

        waterRoutine = null;
    }
}
